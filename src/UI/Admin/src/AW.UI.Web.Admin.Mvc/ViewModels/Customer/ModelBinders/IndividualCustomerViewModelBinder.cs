using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AW.UI.Web.Admin.Mvc.ViewModels.Customer.ModelBinders
{
    public class IndividualCustomerViewModelBinder : ViewModelModelBinder<IndividualCustomerViewModel>
    {
        protected override IndividualCustomerViewModel? BuildViewModel(ModelBindingContext bindingContext)
        {
            var form = bindingContext.HttpContext.Request.Form;
            var viewModel = base.BuildViewModel(bindingContext);

            foreach (var item in form.Where(_ => _.Key.StartsWith("email_")))
            {
                viewModel?.Person?.EmailAddresses.Add(
                    new PersonEmailAddressViewModel
                    {
                        EmailAddress = item.Value
                    }
                );
            }

            foreach (var item in form.Where(_ => _.Key.StartsWith("phone_")))
            {
                viewModel?.Person?.PhoneNumbers.Add(
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
