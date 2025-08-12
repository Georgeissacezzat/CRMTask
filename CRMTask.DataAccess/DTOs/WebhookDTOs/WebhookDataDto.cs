namespace CRMTask.DataAccess.DTOs.WebhookDTOs
{
    public class WebhookDataDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string CampaignId { get; set; }
        public string Reason { get; set; }
        public string Ip { get; set; }
        public string Url { get; set; }
    }
}
