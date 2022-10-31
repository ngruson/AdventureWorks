﻿using AW.UI.Web.SharedKernel.ReferenceData.Handlers.GetTerritories;
using System.Collections.Generic;

namespace AW.UI.Web.Admin.Mvc.ViewModels.SalesPerson
{
    public class SalesPersonIndexViewModel
    {
        public IEnumerable<SharedKernel.SalesPerson.Handlers.GetSalesPersons.SalesPerson> SalesPersons { get; set; }
        public IEnumerable<Territory> Territories { get; set; }
    }
}