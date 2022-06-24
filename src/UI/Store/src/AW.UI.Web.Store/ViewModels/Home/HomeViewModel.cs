using System.Collections.Generic;

namespace AW.UI.Web.Store.ViewModels.Home
{
    public class HomeViewModel
    {
        public List<Infrastructure.ApiClients.ProductApi.Models.ProductCategory> ProductCategories { get; set; }
    }
}