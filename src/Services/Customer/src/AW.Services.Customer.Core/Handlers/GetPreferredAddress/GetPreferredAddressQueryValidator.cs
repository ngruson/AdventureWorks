using FluentValidation;

namespace AW.Services.Customer.Core.Handlers.GetPreferredAddress
{
    public class GetPreferredAddressQueryValidator : AbstractValidator<GetPreferredAddressQuery>
    {
        public GetPreferredAddressQueryValidator()
        {
            RuleFor(_ => _.CustomerId)
                .NotEmpty();

            RuleFor(_ => _.AddressType)
                .NotEmpty();
        }
    }
}
