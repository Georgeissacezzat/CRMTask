using CRMTask.DataAccess.DTOs.TemplateDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMTask.Business.Services.IServices
{
    public interface ITemplateService
    {
        Task<TemplateDto> CreateTemplateAsync(CreateTemplateDto templateDto);
        Task<TemplateDto> UpdateTemplateAsync(int id, UpdateTemplateDto templateDto);
        Task<bool> DeleteTemplateAsync(int id);
        Task<TemplateDto> GetTemplateAsync(int id);
        Task<List<TemplateDto>> GetAllTemplatesAsync();
    }
}
