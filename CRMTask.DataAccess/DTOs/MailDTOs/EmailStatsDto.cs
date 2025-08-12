namespace CRMTask.DataAccess.DTOs.MailSTOs
{
    public class EmailStatsDto
    {
        public int TotalSent { get; set; }
        public int TotalDelivered { get; set; }
        public int TotalOpened { get; set; }
        public int TotalClicked { get; set; }
        public int TotalBounced { get; set; }
        public int TotalUnsubscribed { get; set; }
        public int TotalComplaints { get; set; }
        public double OpenRate { get; set; }
        public double ClickRate { get; set; }
        public double BounceRate { get; set; }
        public List<MailDto> Mails { get; set; }
    }
}
