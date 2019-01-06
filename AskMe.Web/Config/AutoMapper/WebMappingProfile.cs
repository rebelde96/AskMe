using AskMe.Services.DTOs;
using AskMe.Web.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskMe.Web.Config.AutoMapper
{
    public class WebMappingProfile : Profile
    {
        public WebMappingProfile()
        {
            CreateMap<IndexViewModel, AdDTO>();
            CreateMap<ViewDetailAdViewModel, AdDTO>();
            CreateMap<UserProfileViewModel, ApplicationUserDTO>();
            CreateMap<CreateAdViewModel, CreateAdDTO>();
            CreateMap<CategoryViewModel, CategoryDTO>();
            CreateMap<ShowConversationViewModel, ConversationDTO>();
            CreateMap<SendMessageViewModel, SendMessageDTO>();
            CreateMap<MessageViewModel, MessageDTO>();
        }
    }
}
