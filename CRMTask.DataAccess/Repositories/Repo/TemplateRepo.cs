using CRMTask.DataAccess.Data;
using CRMTask.DataAccess.Models;
using CRMTask.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMTask.DataAccess.Repositories.Repo
{
    public class TemplateRepo : Repo<Template>, ITemplateRepository
    {
        public TemplateRepo(AppDbContext context) : base(context) { }

        public async Task<List<Template>> GetActiveTemplatesAsync()
        {
            return await _context.Template.Where(t => t.IsActive).ToListAsync();
        }

        public async Task<Template> GetByMailchimpIdAsync(string mailchimpId)
        {
            return await _context.Template.FirstOrDefaultAsync(t => t.MailchimpTemplateId == mailchimpId);
        }
    }
}
