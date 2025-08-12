using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMTask.DataAccess.Models
{
    public class Template
    {
        public int Id { get; set; }
        public string MailchimpTemplateId { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public string HtmlContent { get; set; }
        public string TextContent { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public virtual ICollection<Campaign> Campaigns { get; set; } = new List<Campaign>();
        public virtual ICollection<Mail> Mails { get; set; } = new List<Mail>();
    }
}
