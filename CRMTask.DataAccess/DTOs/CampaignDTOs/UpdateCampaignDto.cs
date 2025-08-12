using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMTask.DataAccess.DTOs.CampaignDTOs
{
	public class UpdateCampaignDto : CampaignBaseDto
	{
		public int Id { get; set; }
		public string MailchimpCampaignId { get; set; }
		public string Status { get; set; }
	}
}
