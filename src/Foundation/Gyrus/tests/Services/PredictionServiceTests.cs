
namespace Sitecore.Foundation.Gyrus.Tests
{
    using System;
    using System.Threading.Tasks;
    using Sitecore.Foundation.Gyrus.Models;
    using Sitecore.Foundation.Gyrus.Services;
    using Xunit;

    public class PredictionServiceTests
    {
        [Fact]
        public async Task GetServiceAsync()
        {
            var trainingService = new PredictionService();
            var response = await trainingService.PredictProfile(new UserInteraction()
            {
                Tier = 2,
                Picture = 1,
                Summary = 1,
                Specs = 1,
                Reviews = 1,
                Comments = 1,
                Share = 1,
                Buy = 0,
                Related = 0
            });
        }
    }
}
