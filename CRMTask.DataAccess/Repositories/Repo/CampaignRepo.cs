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
    public class CampaignRepo : Repo<Campaign>, ICampaignRepository
    {
        public CampaignRepo(AppDbContext context) : base(context) { }

        public async Task<List<Campaign>> GetByStatusAsync(CampaignStatus status)
        {
            return await _context.Campaign.Where(c => c.Status == status).ToListAsync();
        }

        public async Task<Campaign> GetByMailchimpIdAsync(string mailchimpId)
        {
            return await _context.Campaign
                .Include(c => c.Template)
                .FirstOrDefaultAsync(c => c.MailchimpCampaignId == mailchimpId);
        }
    }
}
