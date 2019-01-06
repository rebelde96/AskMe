using AskMe.Data.Models;
using AskMe.Services.DTOs;
using AutoMapper;
using System.Linq;

namespace AskMe.Services.Config.AutoMapper
{
    public class ServicesMappingProfile : Profile
    {
        public ServicesMappingProfile()
        {
            CreateMap<Conversation, ConversationDTO>();
            CreateMap<ApplicationUserConversation, ApplicationUserConversationDTO>();
            CreateMap<Message, MessageDTO>()
                .ForMember(dto => dto.UserName, opt => opt.MapFrom(m => m.ApplicationUser.UserName));
            CreateMap<ApplicationUser, ApplicationUserDTO>();
            CreateMap<UserInfo, UserInfoDTO>();
            CreateMap<Ad, AdDTO>()
                .ForMember(dto => dto.RatingPoints, 
                    opt => opt.MapFrom(a => a.AdRatings.Select(ar => ar.RatingPoints).DefaultIfEmpty(0).Average()));
            CreateMap<Category, CategoryDTO>();
        }
    }
}
