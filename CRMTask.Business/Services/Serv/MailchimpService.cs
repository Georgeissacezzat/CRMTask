using CRMTask.Business.Services.IServices;
using CRMTask.DataAccess.DTOs.CampaignDTOs;
using CRMTask.DataAccess.DTOs.MailSTOs;
using CRMTask.DataAccess.DTOs.TemplateDTOs;
using CRMTask.DataAccess.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CRMTask.Business.Services.Serv
{
	public class MailchimpService : IMailchimpService
	{
		private readonly HttpClient _httpClient;
		private readonly string _apiKey;
		private readonly string _serverPrefix;
		private readonly string _baseUrl;
		private readonly string _defaultListId;

		public MailchimpService(HttpClient httpClient, IConfiguration configuration)
		{
			_httpClient = httpClient;

			_apiKey = configuration["Mailchimp:ApiKey"];
			_serverPrefix = _apiKey.Split('-')[1];
			_baseUrl = $"https://{_serverPrefix}.api.mailchimp.com/3.0";
			_defaultListId = configuration["Mailchimp:DefaultListId"];

			_httpClient.DefaultRequestHeaders.Authorization =
				new AuthenticationHeaderValue(
					"Basic",
					Convert.ToBase64String(Encoding.ASCII.GetBytes($"anystring:{_apiKey}"))
				);
		}


		#region Template Operations
		public async Task<string> CreateTemplateAsync(CreateTemplateDto templateDto)
		{
			var template = new
			{
				name = templateDto.Name,
				html = templateDto.HtmlContent
			};

			var json = JsonConvert.SerializeObject(template);
			var content = new StringContent(json, Encoding.UTF8, "application/json");

			var response = await _httpClient.PostAsync($"{_baseUrl}/templates", content);
			response.EnsureSuccessStatusCode();

			var responseContent = await response.Content.ReadAsStringAsync();
			dynamic result = JsonConvert.DeserializeObject(responseContent);

			return result.id;
		}

		public async Task UpdateTemplateAsync(string templateId, UpdateTemplateDto templateDto)
		{
			var template = new
			{
				name = templateDto.Name,
				html = templateDto.HtmlContent
			};

			var json = JsonConvert.SerializeObject(template);
			var content = new StringContent(json, Encoding.UTF8, "application/json");

			var response = await _httpClient.PatchAsync($"{_baseUrl}/templates/{templateId}", content);
			response.EnsureSuccessStatusCode();
		}

		public async Task DeleteTemplateAsync(string templateId)
		{
			var response = await _httpClient.DeleteAsync($"{_baseUrl}/templates/{templateId}");
			response.EnsureSuccessStatusCode();
		}

		public async Task<TemplateDto> GetTemplateAsync(string templateId)
		{
			var response = await _httpClient.GetAsync($"{_baseUrl}/templates/{templateId}");
			response.EnsureSuccessStatusCode();

			var content = await response.Content.ReadAsStringAsync();
			dynamic result = JsonConvert.DeserializeObject(content);

			return new TemplateDto
			{
				MailchimpTemplateId = result.id,
				Name = result.name,
				HtmlContent = result.source?.html,
				CreatedAt = result.created_at
			};
		}

		public async Task<List<TemplateDto>> GetAllTemplatesAsync()
		{
			var response = await _httpClient.GetAsync($"{_baseUrl}/templates?count=100");
			response.EnsureSuccessStatusCode();

			var content = await response.Content.ReadAsStringAsync();
			dynamic result = JsonConvert.DeserializeObject(content);

			var templates = new List<TemplateDto>();
			foreach (var template in result.templates)
			{
				templates.Add(new TemplateDto
				{
					MailchimpTemplateId = template.id,
					Name = template.name,
					CreatedAt = template.created_at
				});
			}

			return templates;
		}
		#endregion

		#region Mailchimp Campaign Operations (for CampaignService)
		public async Task<string> CreateCampaignInMailchimpAsync(CreateCampaignDto request)
		{
			var listId = _defaultListId // use your existing config

			var campaign = new
			{
				type = "regular",
				recipients = new { list_id = listId },
				settings = new
				{
					subject_line = request.Subject,
					title = request.Name,
					from_name = request.FromName ?? "Your Company",
					reply_to = request.ReplyTo ?? "verified-email@yourcompany.com" // must be verified in Mailchimp
				}
			};

			var json = JsonConvert.SerializeObject(campaign);
			var content = new StringContent(json, Encoding.UTF8, "application/json");

			var response = await _httpClient.PostAsync($"{_baseUrl}/campaigns", content);

			if (!response.IsSuccessStatusCode)
			{
				var errorContent = await response.Content.ReadAsStringAsync();
				throw new Exception($"Mailchimp campaign creation failed: {errorContent}");
			}

			var responseContent = await response.Content.ReadAsStringAsync();
			dynamic result = JsonConvert.DeserializeObject(responseContent);

			return result.id;
		}



		public async Task UpdateCampaignInMailchimpAsync(string campaignId, UpdateCampaignDto updateDto)
		{
			// Update campaign settings
			var updateData = new
			{
				settings = new
				{
					subject_line = updateDto.Subject,
					title = updateDto.Name
				}
			};

			var json = JsonConvert.SerializeObject(updateData);
			var content = new StringContent(json, Encoding.UTF8, "application/json");

			var response = await _httpClient.PatchAsync($"{_baseUrl}/campaigns/{campaignId}", content);
			response.EnsureSuccessStatusCode();
		}

		public async Task DeleteCampaignInMailchimpAsync(string campaignId)
		{
			var response = await _httpClient.DeleteAsync($"{_baseUrl}/campaigns/{campaignId}");
			response.EnsureSuccessStatusCode();
		}
		#endregion

		#region Stats and Reporting
		public async Task<EmailStatsDto> GetCampaignStatsAsync(string campaignId)
		{
			var response = await _httpClient.GetAsync($"{_baseUrl}/reports/{campaignId}");
			response.EnsureSuccessStatusCode();

			var content = await response.Content.ReadAsStringAsync();
			dynamic result = JsonConvert.DeserializeObject(content);

			return new EmailStatsDto
			{
				TotalSent = result.emails_sent,
				TotalDelivered = result.delivered,
				TotalOpened = result.opens.unique_opens,
				TotalClicked = result.clicks.unique_clicks,
				TotalBounced = result.bounces.hard_bounces + result.bounces.soft_bounces,
				OpenRate = (double)result.opens.open_rate * 100,
				ClickRate = (double)result.clicks.click_rate * 100,
				BounceRate = (double)result.bounces.bounce_rate * 100
			};
		}

		public async Task<List<MailDto>> GetCampaignReportsAsync(string campaignId)
		{
			var response = await _httpClient.GetAsync($"{_baseUrl}/reports/{campaignId}/sent-to");
			response.EnsureSuccessStatusCode();

			var content = await response.Content.ReadAsStringAsync();
			dynamic result = JsonConvert.DeserializeObject(content);

			var mails = new List<MailDto>();
			foreach (var member in result.sent_to)
			{
				mails.Add(new MailDto
				{
					UserEmail = member.email_address,
					Status = GetMailStatusFromString(member.status),
					OpenCount = member.opens_count,
					ClickCount = member.clicks_count
				});
			}

			return mails;
		}
		#endregion

		#region Additional Mailchimp Operations (kept for backwards compatibility)
		public async Task<string> SendCampaignAsync(string campaignId)
		{
			var response = await _httpClient.PostAsync($"{_baseUrl}/campaigns/{campaignId}/actions/send", null);
			response.EnsureSuccessStatusCode();
			return campaignId;
		}

		public async Task<bool> ScheduleCampaignAsync(string campaignId, DateTime scheduleTime)
		{
			var scheduleData = new { schedule_time = scheduleTime.ToString("yyyy-MM-ddTHH:mm:ssZ") };
			var json = JsonConvert.SerializeObject(scheduleData);
			var content = new StringContent(json, Encoding.UTF8, "application/json");

			var response = await _httpClient.PostAsync($"{_baseUrl}/campaigns/{campaignId}/actions/schedule", content);
			return response.IsSuccessStatusCode;
		}

		public async Task<bool> CancelCampaignAsync(string campaignId)
		{
			var response = await _httpClient.PostAsync($"{_baseUrl}/campaigns/{campaignId}/actions/cancel-send", null);
			return response.IsSuccessStatusCode;
		}
		#endregion

		#region Private Helper Methods
		private async Task<string> GetOrCreateListAsync()
		{
			// Implementation to get or create a Mailchimp list
			// This is a simplified version - you might want to create dynamic lists
			return "your_list_id_here";
		}

		private async Task SetCampaignContentAsync(string campaignId, string templateId, string htmlContent)
		{
			object contentData;

			if (!string.IsNullOrEmpty(templateId))
			{
				contentData = new
				{
					template = new
					{
						id = int.Parse(templateId)
					}
				};
			}
			else
			{
				contentData = new
				{
					html = htmlContent
				};
			}

			var json = JsonConvert.SerializeObject(contentData);
			var content = new StringContent(json, Encoding.UTF8, "application/json");

			var response = await _httpClient.PutAsync($"{_baseUrl}/campaigns/{campaignId}/content", content);
			response.EnsureSuccessStatusCode();
		}

		private MailStatus GetMailStatusFromString(string status)
		{
			return status.ToLower() switch
			{
				"sent" => MailStatus.Sent,
				"opened" => MailStatus.Opened,
				"clicked" => MailStatus.Clicked,
				"bounced" => MailStatus.Bounced,
				"unsubscribed" => MailStatus.Unsubscribed,
				_ => MailStatus.Delivered
			};
		}
		#endregion
	}
}