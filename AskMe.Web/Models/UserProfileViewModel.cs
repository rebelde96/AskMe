using AskMe.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskMe.Web.Models
{
	public class UserProfileViewModel
	{
        public UserProfileViewModel()
        {
            this.UserInfo = new UserInfoDTO();
        }

        public string ApplicationUserId { get; set; }

        public string UserName { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime CreatedAt { get; set; }

        public UserInfoDTO UserInfo { get; set; }

        public int AdsCount { get; set; }
    }
}
