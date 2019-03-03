namespace Sitecore.Feature.Product.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Sitecore.Configuration;
     using Sitecore.Foundation.AnalyticsConnect.Models;
    using Sitecore.Foundation.AnalyticsConnect.Services;
    using Sitecore.Foundation.Gyrus.Services;
    using Sitecore.Data;
    using Sitecore.Data.Fields;
    using Sitecore.Feature.Product.Models;
    using Sitecore.Foundation.Gyrus.Models;
    using Sitecore.Foundation.SitecoreExtensions.Extensions;
    using Sitecore.Foundation.SitecoreExtensions.Repositories;
    using Sitecore.Foundation.SitecoreExtensions.Services;
    using Sitecore.Links;
    using Sitecore.Mvc.Presentation;

    public class ProductController : Controller
    {
         private readonly IXConnectFactory xconnectService;
         private readonly IPredictionService predictionService;

        
        public ProductController(ITrackerService trackerService, IXConnectFactory xconnectService,IPredictionService predictionService)
        {
            this.xconnectService = xconnectService;
            this.predictionService = predictionService;
        }        

        public ActionResult ProductDetail()
        {
            var renderingPropertiesRepository = new RenderingPropertiesRepository();
            var productDetailModel = renderingPropertiesRepository.Get<ProductDto>(RenderingContext.Current.Rendering);
            productDetailModel.Initialize(RenderingContext.Current.Rendering);
            productDetailModel.ProfileTypes = new Dictionary<ProfileType, int>();

            //Verify if the data is going to sent to machine learning webservice
            var minRecords = Settings.GetSetting("Sitecore.Feature.Product.MinRecordsMachineLearning", "15");
            if (xconnectService.VerifyInteractionsUser(System.Convert.ToInt32(minRecords)))
            {
                var interactions = xconnectService.GetInteractionsUser();
                predictionService.PredictProfile(interactions);
            }

            return this.View(productDetailModel);
        }

        /// <summary>
        /// View of the product list
        /// </summary>
        /// <returns></returns>
        public ActionResult Products()
        {
            var productsNode = Context.Database.GetItem(new ID("{64FFE16A-DE2F-438C-A536-3BF7CC541112}"));
            var items = productsNode.Axes.GetDescendants();
            var model = new ProductsDto()
            {
                Products = items.Select(x => new ProductDto()
                {
                    Title = x.Fields[Templates.Products.Fields.Tittle]?.Value,
                    Price = x.Fields[Templates.Products.Fields.Price]?.Value,
                    Img = ((ImageField)x.Fields[Templates.Products.Fields.Image1])?.ImageUrl(),
                    Url = LinkManager.GetItemUrl(x)
                }).ToList()
            };

            return this.View(model);
        }

        /// <summary>
        /// Api to set the interaction when the user has been click on the accordion
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="interactionType"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult TrackClickAccordionSummary(string productId, string interactionType)
        {
            try
            {
                InteractionType _interactionType = InteractionType.None;
                Enum.TryParse(interactionType, out _interactionType);

                var _productId = new Guid();
                Guid.TryParse(productId, out _productId); 

                xconnectService.SaveGoal(_interactionType, _productId);

                return Json(new { Response = "ok" }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { Response = "error" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}