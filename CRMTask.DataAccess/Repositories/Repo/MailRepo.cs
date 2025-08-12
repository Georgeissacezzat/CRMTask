using CRMTask.DataAccess.Data;
using CRMTask.DataAccess.DTOs.MailSTOs;
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
    public class MailRepo : Repo<Mail>, IMailRepository
    {
        public MailRepo(AppDbContext context) : base(context) { }

        public async Task<List<Mail>> GetByUserIdAsync(int userId)
        {
            return await _context.Mail
                .Include(m => m.User)
                .Include(m => m.Campaign)
                .Include(m => m.Template)
                .Where(m => m.UserId == userId)
                .ToListAsync();
        }

        public async Task<List<Mail>> GetByCampaignIdAsync(int campaignId)
        {
            return await _context.Mail
                .Include(m => m.User)
                .Include(m => m.Campaign)
                .Include(m => m.Template)
                .Where(m => m.CampaignId == campaignId)
                .ToListAsync();
        }

        public async Task<EmailStatsDto> GetCampaignStatsAsync(int campaignId)
        {
            var mails = await GetByCampaignIdAsync(campaignId);

            return new EmailStatsDto
            {
                TotalSent = mails.Count,
                TotalDelivered = mails.Count(m => m.Status >= MailStatus.Delivered),
                TotalOpened = mails.Count(m => m.Status >= MailStatus.Opened),
                TotalClicked = mails.Count(m => m.Status >= MailStatus.Clicked),
                TotalBounced = mails.Count(m => m.Status == MailStatus.Bounced),
                TotalUnsubscribed = mails.Count(m => m.Status == MailStatus.Unsubscribed),
                TotalComplaints = mails.Count(m => m.Status == MailStatus.Complained),
                OpenRate = mails.Count > 0 ? (double)mails.Count(m => m.Status >= MailStatus.Opened) / mails.Count * 100 : 0,
                ClickRate = mails.Count > 0 ? (double)mails.Count(m => m.Status >= MailStatus.Clicked) / mails.Count * 100 : 0,
                BounceRate = mails.Count > 0 ? (double)mails.Count(m => m.Status == MailStatus.Bounced) / mails.Count * 100 : 0,
                Mails = mails.Select(m => new MailDto
                {
                    Id = m.Id,
                    UserEmail = m.User.Email,
                    UserName = $"{m.User.FirstName} {m.User.LastName}",
                    CampaignName = m.Campaign.Name,
                    TemplateName = m.Template.Name,
                    Status = m.Status,
                    SentAt = m.SentAt,
                    OpenedAt = m.OpenedAt,
                    ClickedAt = m.ClickedAt,
                    OpenCount = m.OpenCount,
                    ClickCount = m.ClickCount
                }).ToList()
            };
        }

        public async Task<Mail> GetByMailchimpEmailIdAsync(string mailchimpEmailId)
        {
            return await _context.Mail
                .Include(m => m.User)
                .Include(m => m.Campaign)
                .FirstOrDefaultAsync(m => m.MailchimpEmailId == mailchimpEmailId);
        }
    }
}
