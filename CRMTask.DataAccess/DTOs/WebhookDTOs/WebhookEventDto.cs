namespace CRMTask.DataAccess.DTOs.WebhookDTOs
{
    public class WebhookEventDto
    {
        public string Type { get; set; }
        public DateTime FiredAt { get; set; }
        public WebhookDataDto Data { get; set; }
    }
}
