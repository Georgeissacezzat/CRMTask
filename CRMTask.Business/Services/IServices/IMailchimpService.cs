using CRMTask.DataAccess.DTOs.CampaignDTOs;
using CRMTask.DataAccess.DTOs.MailSTOs;
using CRMTask.DataAccess.DTOs.TemplateDTOs;
using CRMTask.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRMTask.Business.Services.IServices
{
	public interface IMailchimpService
	{
		// Template Operations
		Task<string> CreateTemplateAsync(CreateTemplateDto templateDto);
		Task UpdateTemplateAsync(string templateId, UpdateTemplateDto templateDto);
		Task DeleteTemplateAsync(string templateId);
		Task<TemplateDto> GetTemplateAsync(string templateId);
		Task<List<TemplateDto>> GetAllTemplatesAsync();

		// Mailchimp Campaign Operations (for CampaignService integration)
		Task<string> CreateCampaignInMailchimpAsync(CreateCampaignDto request);
		Task UpdateCampaignInMailchimpAsync(string campaignId, UpdateCampaignDto updateDto);
		Task DeleteCampaignInMailchimpAsync(string campaignId);

		// Campaign Actions
		Task<string> SendCampaignAsync(string campaignId);
		Task<bool> ScheduleCampaignAsync(string campaignId, DateTime scheduleTime);
		Task<bool> CancelCampaignAsync(string campaignId);

		// Stats and Reporting
		Task<EmailStatsDto> GetCampaignStatsAsync(string campaignId);
		Task<List<MailDto>> GetCampaignReportsAsync(string campaignId);
		Task<bool> AddUserToListAsync(string userId, string userEmail, string firstName, string lastName, string listId);
		Task<bool> SendTemplateToUserAsync(string templateId, string userId, string userEmail, string subject, Dictionary<string, string> mergeFields);
	}
}