using Microsoft.Extensions.Primitives;

namespace AW.UI.Web.Admin.Mvc.ViewModels.SalesOrder.ModelBinders;

public class UpdateOrderInfoViewModelBinder : ViewModelModelBinder<UpdateOrderInfoViewModel>
{
    protected override object GetValueForKey(string key, StringValues value)
    {
        if (key == nameof(UpdateOrderInfoViewModel.SalesOrder.OnlineOrderFlag))
        {
            var stringValue = value.ToString();
            if (stringValue.Contains(','))
            {
                stringValue = stringValue[..stringValue.IndexOf(',')];
            }

            return bool.Parse(stringValue);
        }

        return base.GetValueForKey(key, value);
    }
}
