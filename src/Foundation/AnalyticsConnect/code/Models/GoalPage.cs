using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Foundation.AnalyticsConnect.Models
{
    public class GoalPage
    {
        public string PageId { get; set; }
        public Guid GoalId { get; set; }

        public DateTime TimeStamp { get; set; }

    }
}