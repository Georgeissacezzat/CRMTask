using CRMTask.DataAccess.DTOs.MailSTOs;
using CRMTask.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CRMTask.DataAccess.Repositories.Interfaces
{
    public interface IMailRepository : IRepository<Mail>
    {
        Task<List<Mail>> GetByUserIdAsync(int userId);
        Task<List<Mail>> GetByCampaignIdAsync(int campaignId);
        Task<EmailStatsDto> GetCampaignStatsAsync(int campaignId);
        Task<Mail> GetByMailchimpEmailIdAsync(string mailchimpEmailId);
    }
}
