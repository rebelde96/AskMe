using System;
using System.Collections.Generic;
using System.Text;

namespace AskMe.Services.DTOs
{
	public class ApplicationUserDTO
	{
		public string ApplicationUserId { get; set; }

		public string UserName { get; set; }

		public string PhoneNumber { get; set; }

		public DateTime CreatedAt { get; set; }

		public UserInfoDTO UserInfoDTO { get; set; }

		public int AdsCount { get; set; }
	}
}
