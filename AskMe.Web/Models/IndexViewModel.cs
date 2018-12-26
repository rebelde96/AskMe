using AskMe.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskMe.Web.Models
{
	public class IndexViewModel
	{
		public ICollection<AdDTO> Ads { get; set; }
	}
}
