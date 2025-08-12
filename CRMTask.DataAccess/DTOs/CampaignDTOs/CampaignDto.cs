using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMTask.DataAccess.DTOs.CampaignDTOs
{
	public class CampaignDto : CampaignBaseDto
	{
		public int Id { get; set; }
		public string MailchimpCampaignId { get; set; }
		public string Status { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
	}
}
