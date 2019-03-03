using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Foundation.AnalyticsConnect.Models
{
    public enum InteractionType
    {
        None = 0,
        Summary = 1,
        Specs = 2,
        Review = 3,
        Buy = 4,
        Comment = 5,
        Share = 6
    }
}