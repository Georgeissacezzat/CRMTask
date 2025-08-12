using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMTask.DataAccess.DTOs.CampaignDTOs
{
	public class CampaignBaseDto
	{
		public string Name { get; set; }
		public string Subject { get; set; }
		public int TemplateId { get; set; }
	}
}
