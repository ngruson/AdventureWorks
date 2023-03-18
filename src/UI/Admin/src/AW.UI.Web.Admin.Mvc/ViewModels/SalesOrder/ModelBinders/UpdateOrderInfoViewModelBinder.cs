using AW.UI.Web.Admin.Mvc.ViewModels.ModelBinders;
using Microsoft.Extensions.Primitives;

namespace AW.UI.Web.Admin.Mvc.ViewModels.SalesOrder.ModelBinders
{
    public class UpdateOrderInfoViewModelBinder : ViewModelModelBinder<UpdateOrderInfoViewModel>
    {
        protected override string GetValueForKey(string key, StringValues value)
        {
            if (key == nameof(UpdateOrderInfoViewModel.SalesOrder.OnlineOrderFlag))
            {
                var stringValue = value.ToString();
                if (stringValue.Contains(','))
                {
                    return stringValue[..stringValue.IndexOf(',')];
                }
            }

            return base.GetValueForKey(key, value);
        }
    }
}
