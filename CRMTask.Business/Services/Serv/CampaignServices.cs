using AutoMapper;
using CRMTask.Business.Services.IServices;
using CRMTask.DataAccess.DTOs.CampaignDTOs;
using CRMTask.DataAccess.Models;
using CRMTask.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRMTask.Business.Services.Serv
{
	public class CampaignService : ICampaignService
	{
		private readonly IMailchimpService _mailchimpService;
		private readonly ICampaignRepository _campaignRepository;
		private readonly IMapper _mapper;

		public CampaignService(IMailchimpService mailchimpService, ICampaignRepository campaignRepository, IMapper mapper)
		{
			_mailchimpService = mailchimpService;
			_campaignRepository = campaignRepository;
			_mapper = mapper;
		}

		public async Task<CampaignDto> CreateCampaignAsync(CreateCampaignDto dto)
		{
			// Create campaign in Mailchimp first
			var mailchimpCampaignId = await _mailchimpService.CreateCampaignInMailchimpAsync(dto);

			// Save to database
			var campaign = new Campaign
			{
				MailchimpCampaignId = mailchimpCampaignId,
				Name = dto.Name,
				Subject = dto.Subject,
				TemplateId = dto.TemplateId,
				Status = CampaignStatus.Draft,
				CreatedAt = DateTime.UtcNow,
				SentAt = default, // no value yet
				TotalRecipients = 0
			};

			var createdCampaign = await _campaignRepository.AddAsync(campaign);
			return _mapper.Map<CampaignDto>(createdCampaign);
		}

		public async Task<List<CampaignDto>> GetAllCampaignsAsync()
		{
			var campaigns = await _campaignRepository.GetAllAsync();
			return _mapper.Map<List<CampaignDto>>(campaigns);
		}

		public async Task<CampaignDto> GetCampaignByIdAsync(int id)
		{
			var campaign = await _campaignRepository.GetByIdAsync(id);
			return _mapper.Map<CampaignDto>(campaign);
		}

		public async Task<CampaignDto> GetCampaignByMailchimpIdAsync(string mailchimpId)
		{
			var campaign = await _campaignRepository.GetByMailchimpIdAsync(mailchimpId);
			return _mapper.Map<CampaignDto>(campaign);
		}

		public async Task<List<CampaignDto>> GetCampaignsByStatusAsync(CampaignStatus status)
		{
			var campaigns = await _campaignRepository.GetByStatusAsync(status);
			return _mapper.Map<List<CampaignDto>>(campaigns);
		}

		public async Task<bool> UpdateCampaignStatusAsync(int id, CampaignStatus newStatus)
		{
			var campaign = await _campaignRepository.GetByIdAsync(id);
			if (campaign == null) return false;

			campaign.Status = newStatus;

			// Update SentAt timestamp if status is Sent
			if (newStatus == CampaignStatus.Sent && campaign.SentAt == default)
			{
				campaign.SentAt = DateTime.UtcNow;
			}

			await _campaignRepository.UpdateAsync(campaign);
			return true;
		}

		public async Task<CampaignDto> UpdateCampaignAsync(int id, UpdateCampaignDto dto)
		{
			var campaign = await _campaignRepository.GetByIdAsync(id);
			if (campaign == null) return null;

			// Update in Mailchimp if applicable
			if (!string.IsNullOrEmpty(campaign.MailchimpCampaignId))
			{
				await _mailchimpService.UpdateCampaignInMailchimpAsync(campaign.MailchimpCampaignId, dto);
			}

			if (!string.IsNullOrWhiteSpace(dto.Name))
				campaign.Name = dto.Name;
			if (!string.IsNullOrWhiteSpace(dto.Subject))
				campaign.Subject = dto.Subject;
			if (dto.TemplateId > 0)   
				campaign.TemplateId = dto.TemplateId;

			await _campaignRepository.UpdateAsync(campaign);
			return _mapper.Map<CampaignDto>(campaign);
		}

		public async Task<bool> DeleteCampaignAsync(int id)
		{
			var campaign = await _campaignRepository.GetByIdAsync(id);
			if (campaign == null) return false;

			// Try deleting from Mailchimp
			if (!string.IsNullOrEmpty(campaign.MailchimpCampaignId))
			{
				try
				{
					await _mailchimpService.DeleteCampaignInMailchimpAsync(campaign.MailchimpCampaignId);
				}
				catch
				{
					// Ignore Mailchimp deletion failure
				}
			}

			await _campaignRepository.DeleteAsync(id);
			return true;
		}
	}
}
