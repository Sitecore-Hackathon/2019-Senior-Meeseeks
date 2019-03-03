using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Foundation.AnalyticsConnect
{
    using Sitecore.Configuration;

    public static class Configuration
    {
        public static Guid SummaryGoalId => Guid.Parse(Settings.GetSetting("Sitecore.Foundation.AnalyticsConnect.SummaryGoalId", "{8F74EF87-929E-4602-9CCC-00E91748CA8A}"));
        public static Guid SpecsGoalId => Guid.Parse(Settings.GetSetting("Sitecore.Foundation.AnalyticsConnect.SpecsGoalId", "{8F74EF87-929E-4602-9CCC-00E91748CA8A}"));
        public static Guid ReviewGoalId => Guid.Parse(Settings.GetSetting("Sitecore.Foundation.AnalyticsConnect.ReviewGoalId", "{8F74EF87-929E-4602-9CCC-00E91748CA8A}"));
        public static Guid BuyGoalId => Guid.Parse(Settings.GetSetting("Sitecore.Foundation.AnalyticsConnect.BuyGoalId", "{8F74EF87-929E-4602-9CCC-00E91748CA8A}"));
        public static Guid CommentGoalId => Guid.Parse(Settings.GetSetting("Sitecore.Foundation.AnalyticsConnect.CommentGoalId", "{8F74EF87-929E-4602-9CCC-00E91748CA8A}"));
        public static Guid ShareGoalId => Guid.Parse(Settings.GetSetting("Sitecore.Foundation.AnalyticsConnect.ShareGoalId", "{8F74EF87-929E-4602-9CCC-00E91748CA8A}"));
        public static Guid PictureGoalId => Guid.Parse(Settings.GetSetting("Sitecore.Foundation.AnalyticsConnect.PictureGoalId", "{8F74EF87-929E-4602-9CCC-00E91748CA8A}"));
    }
}