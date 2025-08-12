using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMTask.DataAccess.Models
{
    public class Campaign
    {
        public int Id { get; set; }
        public string MailchimpCampaignId { get; set; }
        public string Name { get; set; }
		public string Subject { get; set; }
        public int TemplateId { get; set; }
        public CampaignStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime SentAt { get; set; }
        public int TotalRecipients { get; set; }
        public virtual Template Template { get; set; }
        public virtual ICollection<Mail> Mails { get; set; } = new List<Mail>();
    }
}
