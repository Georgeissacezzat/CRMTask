using CRMTask.Business.Services.IServices;
using CRMTask.DataAccess.DTOs.CampaignDTOs;
using CRMTask.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRMTask.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CampaignController : ControllerBase
	{
		private readonly ICampaignService _campaignService;

		public CampaignController(ICampaignService campaignService)
		{
			_campaignService = campaignService;
		}

		// GET: api/campaign
		[HttpGet]
		public async Task<ActionResult<List<CampaignDto>>> GetAllCampaigns()
		{
			var campaigns = await _campaignService.GetAllCampaignsAsync();
			return Ok(campaigns);
		}

		// GET: api/campaign/{id}
		[HttpGet("{id:int}")]
		public async Task<ActionResult<CampaignDto>> GetCampaignById(int id)
		{
			var campaign = await _campaignService.GetCampaignByIdAsync(id);
			if (campaign == null) return NotFound();
			return Ok(campaign);
		}

		// GET: api/campaign/mailchimp/{mailchimpId}
		[HttpGet("mailchimp/{mailchimpId}")]
		public async Task<ActionResult<CampaignDto>> GetByMailchimpId(string mailchimpId)
		{
			var campaign = await _campaignService.GetCampaignByMailchimpIdAsync(mailchimpId);
			if (campaign == null) return NotFound();
			return Ok(campaign);
		}

		// GET: api/campaign/status/{status}
		[HttpGet("status/{status}")]
		public async Task<ActionResult<List<CampaignDto>>> GetByStatus(CampaignStatus status)
		{
			var campaigns = await _campaignService.GetCampaignsByStatusAsync(status);
			return Ok(campaigns);
		}

		// POST: api/campaign
		[HttpPost]
		public async Task<ActionResult<CampaignDto>> CreateCampaign([FromBody] CreateCampaignDto dto)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState);

			var created = await _campaignService.CreateCampaignAsync(dto);
			return CreatedAtAction(nameof(GetCampaignById), new { id = created.Id }, created);
		}

		// PUT: api/campaign/{id}
		[HttpPut("{id:int}")]
		public async Task<ActionResult<CampaignDto>> UpdateCampaign(int id, [FromBody] UpdateCampaignDto dto)
		{
			var updated = await _campaignService.UpdateCampaignAsync(id, dto);
			if (updated == null) return NotFound();
			return Ok(updated);
		}

		// DELETE: api/campaign/{id}
		[HttpDelete("{id:int}")]
		public async Task<IActionResult> DeleteCampaign(int id)
		{
			var success = await _campaignService.DeleteCampaignAsync(id);
			if (!success) return NotFound();
			return NoContent();
		}
	}
}
