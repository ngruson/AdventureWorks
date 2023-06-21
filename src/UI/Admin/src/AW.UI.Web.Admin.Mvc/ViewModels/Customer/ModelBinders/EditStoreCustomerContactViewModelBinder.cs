using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AW.UI.Web.Admin.Mvc.ViewModels.Customer.ModelBinders;

public class EditStoreCustomerContactViewModelBinder : ViewModelModelBinder<EditStoreCustomerContactViewModel>
{
    protected override EditStoreCustomerContactViewModel? BuildViewModel(ModelBindingContext bindingContext)
    {
        var form = bindingContext.HttpContext.Request.Form;
        var viewModel = base.BuildViewModel(bindingContext);

        foreach (var item in form.Where(_ => _.Key.StartsWith("email_")))
        {
            viewModel!.CustomerContact!.ContactPerson.EmailAddresses.Add(
                new EditStoreCustomerContactPersonEmailAddressViewModel
                {
                    EmailAddress = item.Value
                }
            );
        }

        foreach (var item in form.Where(_ => _.Key.StartsWith("phone_")))
        {
            viewModel!.CustomerContact!.ContactPerson.PhoneNumbers.Add(
                new EditStoreCustomerContactPersonPhoneViewModel
                {
                    PhoneNumber = item.Value,
                    PhoneNumberType = form.Single(_ => _.Key == "phoneSelect").Value[0]
                }
            );
        }

        return viewModel;
    }
}
