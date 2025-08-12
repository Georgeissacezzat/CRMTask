using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMTask.DataAccess.Models
{
    public enum MailStatus
    {
        Sent,
        Delivered,
        Opened,
        Clicked,
        Bounced,
        Unsubscribed,
        Complained
    }
}
