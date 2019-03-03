using System;
using System.Collections.Generic;
using System.Text;

namespace Sitecore.Foundation.Gyrus.Services
{
    using System.Threading.Tasks;
    using Sitecore.Foundation.Gyrus.Models;

    public interface IPredictionService
    {
        Task<Dictionary<ProfileType, float>> PredictProfile(UserInteraction userInteraction);
    }
}
