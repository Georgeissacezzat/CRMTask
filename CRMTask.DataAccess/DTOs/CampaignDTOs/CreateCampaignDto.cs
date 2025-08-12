using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMTask.DataAccess.DTOs.CampaignDTOs
{
	public class CreateCampaignDto : CampaignBaseDto
	{
		public string FromName { get; set; }
		public string ReplyTo { get; set; }
	}
}
