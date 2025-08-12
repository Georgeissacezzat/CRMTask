using CRMTask.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMTask.DataAccess.Repositories.Interfaces
{
    public interface ITemplateRepository : IRepository<Template>
    {
        Task<List<Template>> GetActiveTemplatesAsync();
        Task<Template> GetByMailchimpIdAsync(string mailchimpId);
    }
}
