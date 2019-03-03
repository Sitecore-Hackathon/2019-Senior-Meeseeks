using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data.Items;
using Sitecore.Mvc.Presentation;

namespace Sitecore.Feature.Product.Models
{
    using Sitecore.Foundation.Gyrus.Models;

    public class ProductDto
    {
        public Rendering Rendering { get; set; }
        public Item Item { get; set; }
        public Item PageItem { get; set; }
        public string Title { get; set; }
        public string Price { get; set; }
        public string Img { get; set; }
        public string Url { get; set; }
        public Dictionary<ProfileType, int> ProfileTypes { get; set; }
        public void Initialize(Rendering rendering)
        {
            Rendering = rendering;
            Item = rendering.Item;
            PageItem = PageContext.Current.Item;

        }
    }
}