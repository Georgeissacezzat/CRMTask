using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMTask.DataAccess.Models
{
    public enum CampaignStatus
    {
        Draft,
        Scheduled,
        Sending,
        Sent,
        Paused,
        Canceled,
		Ready,
		Failed
	}
}
