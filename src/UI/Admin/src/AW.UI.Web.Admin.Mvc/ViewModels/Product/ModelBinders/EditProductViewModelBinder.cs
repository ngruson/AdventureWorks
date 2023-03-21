﻿using AW.UI.Web.Admin.Mvc.ViewModels.ModelBinders;
using Microsoft.Extensions.Primitives;

namespace AW.UI.Web.Admin.Mvc.ViewModels.Product.ModelBinders
{
    public class EditProductViewModelBinder : ViewModelModelBinder<EditProductViewModel>
    {
        protected override object GetValueForKey(string key, StringValues value)
        {
            if (key == nameof(EditProductViewModel.Product.MakeFlag) ||
                key == nameof(EditProductViewModel.Product.FinishedGoodsFlag))
            {
                var stringValue = value.ToString();
                if (stringValue.Contains(','))
                    stringValue = stringValue[..stringValue.IndexOf(',')];

                if (bool.TryParse(stringValue, out var flag))
                    return flag;

                throw new ArgumentException($"{key} value {stringValue} could not be parsed");
            }
            else if (key == nameof(EditProductViewModel.Product.DaysToManufacture) ||
                key == nameof(EditProductViewModel.Product.SafetyStockLevel) ||
                key == nameof(EditProductViewModel.Product.ReorderPoint))
            {
                if (int.TryParse(value.ToString(), out var intValue))
                    return intValue;

                throw new ArgumentException($"{key} value {value.ToString()} could not be parsed");
            }

            return base.GetValueForKey(key, value);
        }
    }
}