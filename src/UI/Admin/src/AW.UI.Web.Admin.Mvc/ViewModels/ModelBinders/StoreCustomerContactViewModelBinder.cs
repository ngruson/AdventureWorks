using AW.UI.Web.Admin.Mvc.ViewModels.Customer;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AW.UI.Web.Admin.Mvc.ViewModels.ModelBinders
{
    public class StoreCustomerContactViewModelBinder : ViewModelModelBinder<StoreCustomerContactViewModel>
    {
        protected override StoreCustomerContactViewModel? BuildViewModel(ModelBindingContext bindingContext)
        {
            var form = bindingContext.HttpContext.Request.Form;
            var viewModel = base.BuildViewModel(bindingContext);

            foreach (var item in form.Where(_ => _.Key.StartsWith("email_")))
            {
                viewModel?.CustomerContact?.ContactPerson.EmailAddresses.Add(
                    new PersonEmailAddressViewModel
                    {
                        EmailAddress = item.Value
                    }
                );
            }

            foreach (var item in form.Where(_ => _.Key.StartsWith("phone_")))
            {
                viewModel?.CustomerContact?.ContactPerson.PhoneNumbers.Add(
                    new PersonPhoneViewModel
                    {
                        PhoneNumber = item.Value,
                        PhoneNumberType = form.Single(_ => _.Key == "phoneSelect").Value[0]
                    }
                );
            }

            return viewModel;
        }
    }
}