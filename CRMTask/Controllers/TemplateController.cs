using CRMTask.Business.Services.IServices;
using CRMTask.DataAccess.DTOs.TemplateDTOs;
using Microsoft.AspNetCore.Mvc;

namespace CRMTask.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TemplateController : ControllerBase
	{
		private readonly ITemplateService _templateService;

		public TemplateController(ITemplateService templateService)
		{
			_templateService = templateService;
		}

		// GET: api/template
		[HttpGet]
		public async Task<ActionResult<List<TemplateDto>>> GetAll()
		{
			var templates = await _templateService.GetAllTemplatesAsync();
			return Ok(templates);
		}

		// GET: api/template/{id}
		[HttpGet("{id:int}")]
		public async Task<ActionResult<TemplateDto>> GetById(int id)
		{
			var template = await _templateService.GetTemplateAsync(id);
			if (template == null)
				return NotFound();

			return Ok(template);
		}

		// POST: api/template
		[HttpPost]
		public async Task<ActionResult<TemplateDto>> Create(CreateTemplateDto dto)
		{
			var createdTemplate = await _templateService.CreateTemplateAsync(dto);
			return CreatedAtAction(nameof(GetById), new { id = createdTemplate.Id }, createdTemplate);
		}

		// PUT: api/template/{id}
		[HttpPut("{id:int}")]
		public async Task<ActionResult<TemplateDto>> Update(int id, UpdateTemplateDto dto)
		{
			var updatedTemplate = await _templateService.UpdateTemplateAsync(id, dto);
			if (updatedTemplate == null)
				return NotFound();

			return Ok(updatedTemplate);
		}

		// DELETE: api/template/{id}
		[HttpDelete("{id:int}")]
		public async Task<IActionResult> Delete(int id)
		{
			var deleted = await _templateService.DeleteTemplateAsync(id);
			if (!deleted)
				return NotFound();

			return NoContent();
		}
	}
}
