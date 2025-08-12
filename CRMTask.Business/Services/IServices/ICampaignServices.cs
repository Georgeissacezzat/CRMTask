using CRMTask.DataAccess.DTOs.CampaignDTOs;
using CRMTask.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMTask.Business.Services.IServices
{
	public interface ICampaignService
	{
		Task<List<CampaignDto>> GetAllCampaignsAsync();
		Task<CampaignDto> GetCampaignByIdAsync(int id);
		Task<CampaignDto> GetCampaignByMailchimpIdAsync(string mailchimpId);
		Task<List<CampaignDto>> GetCampaignsByStatusAsync(CampaignStatus status);
		Task<CampaignDto> CreateCampaignAsync(CreateCampaignDto dto);
		Task<CampaignDto> UpdateCampaignAsync(int id,UpdateCampaignDto dto);
		Task<bool> DeleteCampaignAsync(int id);
	}
}
