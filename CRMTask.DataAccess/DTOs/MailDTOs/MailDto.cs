using CRMTask.DataAccess.Models;

namespace CRMTask.DataAccess.DTOs.MailSTOs
{
    public class MailDto
    {
        public int Id { get; set; }
        public string UserEmail { get; set; }
        public string UserName { get; set; }
        public string CampaignName { get; set; }
        public string TemplateName { get; set; }
        public MailStatus Status { get; set; }
        public DateTime SentAt { get; set; }
        public DateTime? OpenedAt { get; set; }
        public DateTime? ClickedAt { get; set; }
        public int OpenCount { get; set; }
        public int ClickCount { get; set; }
    }
}
