using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AskMe.Data.Models;
using AskMe.Services;
using AskMe.Web.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AskMe.Web.Controllers
{
    public class ConversationController : Controller
    {
        private readonly IConversationService conversationService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMapper mapper;

        public ConversationController(IConversationService conversationService,
            UserManager<ApplicationUser> userManager,
             IHttpContextAccessor httpContextAccessor,
              IMapper mapper)
        {
            this.conversationService = conversationService;
            this.userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var user = await this.userManager.FindByNameAsync(this.User.Identity.Name);
            var conversations = await this.conversationService.GetUserConversations(user.Id);
            var model = new ConversationIndexViewModel();

            foreach (var conversation in conversations)
            {
                var conversationWith = string.Empty;

                if (conversation.Ad.ApplicationUser.UserName == user.UserName)
                {
                    conversationWith = conversation.Messages.FirstOrDefault(m => m.UserName != user.UserName).UserName;
                }
                else
                {
                    conversationWith = conversation.Ad.ApplicationUser.UserName;
                }

                model.Conversations.Add(new ConversationViewModel
                {

                    Id = conversation.Id,
                    ConversationName = $"{conversationWith} | {conversation.Ad.Title}"
                });
            }

            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> ShowConversation(int adId)
        {
            var user = await userManager.GetUserAsync(httpContextAccessor.HttpContext.User);
            var userWroteAdId = conversationService.GetUserIdByAdId(adId);
            var conversationDTO = await conversationService.GetConversationByAdId(adId, user.Id, userWroteAdId);
            if (conversationDTO != null)
            {
                var model = this.mapper.Map<ShowConversationViewModel>(conversationDTO);
                return View(model);
            }
            return View(new ShowConversationViewModel { Id = 0, Ad = new ViewDetailAdViewModel { Id = adId } });
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(int adId, int conversationId, ShowConversationViewModel model)
        {
            var user = await userManager.GetUserAsync(httpContextAccessor.HttpContext.User);
            await conversationService.SendMessage(model.SendMessage.MessageText, conversationId, user.Id, adId);
            return RedirectToAction("ShowConversation", "Conversation", new { adId = adId, conversationId = conversationId });
        }
    }
}
