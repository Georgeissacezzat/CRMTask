using CRMTask.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMTask.DataAccess.Repositories.Interfaces
{
    public interface ICampaignRepository : IRepository<Campaign>
    {
        Task<List<Campaign>> GetByStatusAsync(CampaignStatus status);
        Task<Campaign> GetByMailchimpIdAsync(string mailchimpId);
    }
}
