namespace Sitecore.Foundation.AnalyticsConnect.Services
{
    using System;
    using Sitecore.Foundation.AnalyticsConnect.Models;
    using Sitecore.Foundation.Gyrus.Models;
    using Sitecore.XConnect;

    public interface IXConnectFactory
    {
        IXdbContext CreateContext();
        UserInteraction GetInteractionsUser();

        void SaveGoal(InteractionType interactionType, Guid productId);

        bool VerifyInteractionsUser(int maxRecords);
    }
}
