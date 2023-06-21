using Microsoft.Extensions.Primitives;

namespace AW.UI.Web.Admin.Mvc.ViewModels.Product.ModelBinders;

public class AddProductViewModelBinder : ViewModelModelBinder<AddProductViewModel>
{
    protected override object GetValueForKey(string key, StringValues value)
    {
        if (key == nameof(AddProductViewModel.Product.MakeFlag) ||
            key == nameof(AddProductViewModel.Product.FinishedGoodsFlag))
        {
            var stringValue = value.ToString();
            if (stringValue.Contains(','))
                stringValue = stringValue[..stringValue.IndexOf(',')];

            if (bool.TryParse(stringValue, out var flag))
                return flag;

            throw new ArgumentException($"{key} value {value} could not be parsed");
        }
        else if (key == nameof(AddProductViewModel.Product.DaysToManufacture) ||
            key == nameof(AddProductViewModel.Product.SafetyStockLevel) ||
            key == nameof(AddProductViewModel.Product.ReorderPoint))
        {
            if (int.TryParse(value.ToString(), out var intValue))
                return intValue;

            throw new ArgumentException($"{key} value {value} could not be parsed");
        }
        else if (key == nameof(AddProductViewModel.Product.Weight))
        {
            if (!string.IsNullOrEmpty(value.ToString()))
            {
                if (decimal.TryParse(value.ToString(), out var decimalValue))
                    return decimalValue;

                throw new ArgumentException($"{key} value {value} could not be parsed");
            }

            return 0;
        }
        else if (key == nameof(AddProductViewModel.Product.StandardCost))
        {
            if (!string.IsNullOrEmpty(value.ToString()))
            {
                var strValue = value.ToString();
                if (strValue.StartsWith('$'))
                    strValue = strValue[1..].Trim();

                if (decimal.TryParse(strValue, out var standardCost))
                    return standardCost;

                throw new ArgumentException($"Standard cost value {value} could not be parsed");
            }

            return 0;
        }
        else if (key == nameof(AddProductViewModel.Product.ListPrice))
        {
            if (!string.IsNullOrEmpty(value.ToString()))
            {
                var strValue = value.ToString();
                if (strValue.StartsWith('$'))
                    strValue = strValue[1..].Trim();

                if (decimal.TryParse(strValue, out var listPrice))
                    return listPrice;

                throw new ArgumentException($"List price value {value} could not be parsed");
            }

            return 0;
        }

        return base.GetValueForKey(key, value);
    }
}
