using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMTask.DataAccess.Models
{
    public class Mail
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CampaignId { get; set; }
        public int TemplateId { get; set; }
        public string MailchimpEmailId { get; set; }
        public MailStatus Status { get; set; }
        public DateTime SentAt { get; set; }
        public DateTime? OpenedAt { get; set; }
        public DateTime? ClickedAt { get; set; }
        public int OpenCount { get; set; }
        public int ClickCount { get; set; }
        public virtual User User { get; set; }
        public virtual Campaign Campaign { get; set; }
        public virtual Template Template { get; set; }
    }
}
