namespace Sitecore.Foundation.AnalyticsConnect.Services
{
    using System;
    using Sitecore.Foundation.DependencyInjection;
    using Sitecore.XConnect;
    using Sitecore.XConnect.Client.Configuration;
    using System.Linq;
    using Sitecore.Analytics;
    using Sitecore.XConnect.Client;
    using System.Collections.Generic;
    using Sitecore.Foundation.AnalyticsConnect.Models;
    using Sitecore.Foundation.Gyrus.Models;

    [Service(typeof(IXConnectFactory))]
    public class XConnectFactory : IXConnectFactory
    {
        #region Constructor
        public IXdbContext CreateContext()
        {
            return SitecoreXConnectClientConfiguration.GetClient();
        }
        #endregion

        #region Operations

        /// <summary>
        /// Save goal linked to the contat user
        /// </summary>
        /// <param name="interactionType">Goal type</param>
        /// <param name="productId">Product sitecore id</param>
        public void SaveGoal(InteractionType interactionType, Guid productId)
        {
            using (var client = SitecoreXConnectClientConfiguration.GetClient())
            {
                var id = this.GetContactId();
                if (id == null)
                {
                    return;
                }

                var contactReference = new IdentifiedContactReference(id.Source, id.Identifier);
                var contact = client.Get(contactReference, new ContactExpandOptions(){
                    Interactions = new RelatedInteractionsExpandOptions()
                    {
                        StartDateTime = DateTime.MinValue,
                        EndDateTime = DateTime.MaxValue,
                        Limit = int.MaxValue
                    }
                });

                if (contact == null)
                {
                    return;
                }

                // Create new interaction for this contact
                Guid channelId = Guid.NewGuid(); // Replace with channel ID from Sitecore
                string userAgent = "Mozilla/5.0 (Nintendo Switch; ShareApplet) AppleWebKit/601.6 (KHTML, like Gecko) NF/4.0.0.5.9 NintendoBrowser/5.1.0.13341";
                var interaction = new Sitecore.XConnect.Interaction(contact, InteractionInitiator.Brand, channelId, userAgent);

                var goalId = this.GetGoalId(interactionType);

                // Create new instance of goal
                Goal goal = new Goal(goalId, DateTime.UtcNow);

                //Add aditional information like the page id
                goal.CustomValues.Add("page",productId.ToString());
                
                // Add goal to interaction
                interaction.Events.Add(goal);

                // Add interaction operation to client
                client.AddInteraction(interaction);

                // Submit interaction
                client.Submit();
            }
        }

        /// <summary>
        /// Verify the interations counter is greater than a specific number
        /// </summary>
        /// <param name="maxRecords">Quantity of records to evaluate</param>
        /// <returns></returns>
        public bool VerifyInteractionsUser(int maxRecords)
        {
            bool returnValue = false;
            List<GoalPage> goalPages= new List<GoalPage>();
            using (var client = SitecoreXConnectClientConfiguration.GetClient())
            {
                var id = this.GetContactId();
                if (id == null)
                {
                    return false;
                }

                var contactReference = new IdentifiedContactReference(id.Source, id.Identifier);
                //Get the contacts with their interactions
                var contact = client.Get(contactReference, new ContactExpandOptions()
                {
                    Interactions = new RelatedInteractionsExpandOptions()
                    {
                        StartDateTime = DateTime.MinValue,
                        EndDateTime = DateTime.MaxValue,
                        Limit = int.MaxValue
                    }
                });

                if (contact == null)
                {
                    return false;
                }

                var results = contact.Interactions.Where(x => x.Events.OfType<Goal>().Any()).ToList();


                //results.
                foreach (var itemInteraction in results)
                {
                    var events = itemInteraction.Events.Where(x => x.GetType().Name == "Goal");

                    foreach (var itemEvent in events)
                    {
                        goalPages.Add(new GoalPage {TimeStamp = itemEvent.Timestamp, GoalId = itemEvent.DefinitionId, PageId = itemEvent.CustomValues.FirstOrDefault().Value});
                    }
                }


                goalPages = goalPages.OrderBy(x => x.TimeStamp).ToList();
                returnValue = goalPages.Count >= maxRecords;

            }

            return returnValue;
        }

        /// <summary>
        /// Get the interactions (goals) of the user logged in the system to send to the service exposed in Machine learning Azure plataform
        /// </summary>
        /// <returns></returns>
        public UserInteraction GetInteractionsUser()
		{
            UserInteraction userInteraction= new UserInteraction();

            List<GoalPage> goalPages= new List<GoalPage>();
			using (var client = SitecoreXConnectClientConfiguration.GetClient())
			{
                var id = this.GetContactId();
                if (id == null)
                {
                    return null;
                }

                var contactReference = new IdentifiedContactReference(id.Source, id.Identifier);
                var contact = client.Get(contactReference, new ContactExpandOptions(){
                    Interactions = new RelatedInteractionsExpandOptions()
                    {
                        StartDateTime = DateTime.MinValue,
                        EndDateTime = DateTime.MaxValue,
                        Limit = int.MaxValue
                    }
                });

                if (contact == null)
                {
                    return null;
                }

                var results =contact.Interactions.Where(x=>  x.Events.OfType<Goal>().Any()).ToList();

                
                //results 
                foreach (var itemInteraction in results)
                {
                   var events= itemInteraction.Events.Where(x => x.GetType().Name == "Goal");

                   foreach (var itemEvent in events)
                   {
                       goalPages.Add(new GoalPage{TimeStamp = itemEvent.Timestamp, GoalId=itemEvent.DefinitionId, PageId = itemEvent.CustomValues.FirstOrDefault().Value});
                   }
                }
                
                goalPages = goalPages.OrderBy(x => x.TimeStamp). ToList();
                int countPages = goalPages.Count;

                var distinctGoals= goalPages.Select(item => item.GoalId).Distinct();

                foreach (var goalItem in distinctGoals)
                {
                    //Calculate average Goal id per page numbers
                    float avg = (float) goalPages.Select(x => x.GoalId).Where(x => x == goalItem).Count()/countPages;
                    
                    if (goalItem == Sitecore.Foundation.AnalyticsConnect.Configuration.BuyGoalId)
                    {
                        userInteraction.Buy = avg;
                    }
                    else if (goalItem == Sitecore.Foundation.AnalyticsConnect.Configuration.CommentGoalId)
                    {
                        userInteraction.Comments = avg;
                    }
                    else if (goalItem == Sitecore.Foundation.AnalyticsConnect.Configuration.PictureGoalId)
                    {
                        userInteraction.Picture = avg;
                    }
                    else if (goalItem == Sitecore.Foundation.AnalyticsConnect.Configuration.ReviewGoalId)
                    {
                        userInteraction.Reviews = avg;
                    }
                    else if (goalItem == Sitecore.Foundation.AnalyticsConnect.Configuration.ShareGoalId)
                    {
                        userInteraction.Share = avg;
                    }
                    else if (goalItem == Sitecore.Foundation.AnalyticsConnect.Configuration.SpecsGoalId)
                    {
                        userInteraction.Specs = avg;
                    }
                    else if (goalItem == Sitecore.Foundation.AnalyticsConnect.Configuration.SummaryGoalId)
                    {
                        userInteraction.Summary = avg;
                    }
                }
            }
            return userInteraction;
        }
        #endregion
        #region Private methods
        private Analytics.Model.Entities.ContactIdentifier GetContactId()
        {
            if (Tracker.Current?.Contact == null)
            {
                return null;
            }
            //this.contactManager.SaveContact();
            return Tracker.Current.Contact.Identifiers.FirstOrDefault();
        }

        private Guid GetGoalId(InteractionType interactionType)
        {
            switch (interactionType)
            {
                case InteractionType.Summary:
                    return Sitecore.Foundation.AnalyticsConnect.Configuration.SummaryGoalId;
                case InteractionType.Specs:
                    return Sitecore.Foundation.AnalyticsConnect.Configuration.SpecsGoalId;
                case InteractionType.Review:
                    return Sitecore.Foundation.AnalyticsConnect.Configuration.ReviewGoalId;
                case InteractionType.Buy:
                    return Sitecore.Foundation.AnalyticsConnect.Configuration.BuyGoalId;
                case InteractionType.Comment:
                    return Sitecore.Foundation.AnalyticsConnect.Configuration.CommentGoalId;
                case InteractionType.Share:
                    return Sitecore.Foundation.AnalyticsConnect.Configuration.ShareGoalId;
                default:
                    return Guid.Empty;
            }
        }

        
        #endregion
    }
}