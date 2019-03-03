namespace Sitecore.Feature.Product.Infrastructure.Pipelines
{
    using System.Web.Mvc;
    using System.Web.Routing;
    using Sitecore.Pipelines;

    public class RegisterWebApiRoutes
  {
    public void Process(PipelineArgs args)
    {
        RouteTable.Routes.MapRoute("Feature.Product.Api", "api/{controller}/{action}", new { controller = "Product", action = "TrackClickAccordionSummary" });
    }
  }
}