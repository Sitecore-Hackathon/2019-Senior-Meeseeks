using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Foundation.DependencyInjection;

namespace Sitecore.Foundation.Gyrus.Services
{
    using System.Linq;
    using Newtonsoft.Json;
    using Sitecore.Foundation.Gyrus.Models;

    [Service(typeof(IPredictionService))]
    public class PredictionService : IPredictionService
    {
        public async Task<Dictionary<ProfileType, float>> PredictProfile(UserInteraction userInteraction)
        {
            return await this.InvokeRequestResponseService(userInteraction).ConfigureAwait(false);
        }
        private async Task<Dictionary<ProfileType, float>> InvokeRequestResponseService(UserInteraction userInteraction)
        {
            using (var client = new HttpClient())
            {
                var scoreRequest = new
                {

                    Inputs = new Dictionary<string, StringTable>() {
                        {
                            "UserInteraction",
                            new StringTable()
                            {
                                ColumnNames = new string[]
                                {
                                    "Tier",
                                    "Picture",
                                    "Summary",
                                    "Specs",
                                    "Reviews",
                                    "Comments",
                                    "Share",
                                    "Buy",
                                    "Related",
                                    "Profile"
                                },
                                Values = new string[,]
                                {
                                    {
                                        userInteraction.Tier.ToString(),
                                        userInteraction.Picture.ToString(),
                                        userInteraction.Summary.ToString(),
                                        userInteraction.Specs.ToString(),
                                        userInteraction.Reviews.ToString(),
                                        userInteraction.Comments.ToString(),
                                        userInteraction.Share.ToString(),
                                        userInteraction.Buy.ToString(),
                                        userInteraction.Related.ToString(),
                                        string.Empty
                                    }
                                }
                            }
                        },
                    },
                    GlobalParameters = new Dictionary<string, string>()
                    {
                    }
                };
                const string apiKey = "LX+nJ3AokIWzfZdYk1DZWDJvwf933torz1HeJVQ5yQunDVnPRmSUb3aj4YUFYLkO57bE/bjEbBg81KFexEjglQ=="; // Replace this with the API key for the web service
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
                client.BaseAddress = new Uri("https://ussouthcentral.services.azureml.net/workspaces/045ca17afe7e4720b9252182eceec6f9/services/7d241d2da2d247fa8ec83e14cd15ac5c/execute?api-version=2.0&details=true");

                HttpResponseMessage response = await client.PostAsJsonAsync("", scoreRequest);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var profilePredictionResponse = JsonConvert.DeserializeObject<ProfilePredictionResponse>(result);

                    return new Dictionary<ProfileType, float>()
                    {
                        { ProfileType.Archiever,
                            float.TryParse(profilePredictionResponse.Results.ProfilePrediction.value
                                .Values[0][scoreRequest.Inputs.First().Value.ColumnNames.Length], out var archieverIndex)
                            ? archieverIndex
                            : 0f
                            },
                        { ProfileType.Explorer,float.TryParse(profilePredictionResponse.Results.ProfilePrediction.value
                            .Values[0][scoreRequest.Inputs.First().Value.ColumnNames.Length + 1], out var explorerIndex)
                            ? explorerIndex
                            : 0f
                        },
                        { ProfileType.Killer,float.TryParse(profilePredictionResponse.Results.ProfilePrediction.value
                            .Values[0][scoreRequest.Inputs.First().Value.ColumnNames.Length + 2], out var killerIndex)
                            ? killerIndex
                            : 0f
                        },
                        { ProfileType.Socializer,float.TryParse(profilePredictionResponse.Results.ProfilePrediction.value
                            .Values[0][scoreRequest.Inputs.First().Value.ColumnNames.Length + 3], out var socializerIndex)
                            ? socializerIndex
                            : 0f
                        }
                    };
                }
                else
                {
                    Console.WriteLine(string.Format("The request failed with status code: {0}", response.StatusCode));
                    string responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseContent);
                }

                return new Dictionary<ProfileType, float>() { { ProfileType.None, 0 } };
            }
        }
    }
}
