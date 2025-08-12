namespace CRMTask.DataAccess.DTOs.TemplateDTOs
{
    public class TemplateDto
    {
        public int Id { get; set; }
        public string MailchimpTemplateId { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public string HtmlContent { get; set; }
        public string TextContent { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
