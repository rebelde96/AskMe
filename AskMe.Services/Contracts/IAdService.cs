using AskMe.Services.Common;
using AskMe.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AskMe.Services.Contracts
{
	public interface IAdService
	{
		Task<OperationResult> CreateAd(CreateAdDTO createAdDTO, string userId);

		Task<ICollection<AdDTO>> GetAll();

		Task<AdDTO> GetAd(int id);
	}
}
