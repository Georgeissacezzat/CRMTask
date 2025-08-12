using AutoMapper;
using CRMTask.Business.Services.IServices;
using CRMTask.DataAccess.DTOs.TemplateDTOs;
using CRMTask.DataAccess.Models;
using CRMTask.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMTask.Business.Services.Serv
{
    public class TemplateService : ITemplateService
    {
        private readonly IMailchimpService _mailchimpService;
        private readonly ITemplateRepository _templateRepository;
        private readonly IMapper _mapper;

        public TemplateService(IMailchimpService mailchimpService, ITemplateRepository templateRepository, IMapper mapper)
        {
            _mailchimpService = mailchimpService;
            _templateRepository = templateRepository;
            _mapper = mapper;
        }

        public async Task<TemplateDto> CreateTemplateAsync(CreateTemplateDto templateDto)
        {
            // Create template in Mailchimp
            var mailchimpTemplateId = await _mailchimpService.CreateTemplateAsync(templateDto);

            // Save to database
            var template = new Template
            {
                MailchimpTemplateId = mailchimpTemplateId,
                Name = templateDto.Name,
                Subject = templateDto.Subject,
                HtmlContent = templateDto.HtmlContent,
                TextContent = templateDto.TextContent,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _templateRepository.AddAsync(template);
            return _mapper.Map<TemplateDto>(template);
        }

        public async Task<TemplateDto> UpdateTemplateAsync(int id, UpdateTemplateDto templateDto)
        {
            var template = await _templateRepository.GetByIdAsync(id);
            if (template == null) return null;

            // Update in Mailchimp
            await _mailchimpService.UpdateTemplateAsync(template.MailchimpTemplateId, templateDto);

            // Update in database
            template.Name = templateDto.Name;
            template.Subject = templateDto.Subject;
            template.HtmlContent = templateDto.HtmlContent;
            template.TextContent = templateDto.TextContent;
            template.UpdatedAt = DateTime.UtcNow;

            await _templateRepository.UpdateAsync(template);
            return _mapper.Map<TemplateDto>(template);
        }

        public async Task<bool> DeleteTemplateAsync(int id)
        {
            var template = await _templateRepository.GetByIdAsync(id);
            if (template == null) return false;

            // Delete from Mailchimp
            await _mailchimpService.DeleteTemplateAsync(template.MailchimpTemplateId);

            // Soft delete in database
            template.IsActive = false;
            await _templateRepository.UpdateAsync(template);
            return true;
        }

        public async Task<TemplateDto> GetTemplateAsync(int id)
        {
            var template = await _templateRepository.GetByIdAsync(id);
            return _mapper.Map<TemplateDto>(template);
        }

        public async Task<List<TemplateDto>> GetAllTemplatesAsync()
        {
            var templates = await _templateRepository.GetActiveTemplatesAsync();
            return _mapper.Map<List<TemplateDto>>(templates);
        }
    }
}
