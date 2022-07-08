using MediatR;

namespace AW.UI.Web.SharedKernel.Customer.Handlers.GetIndividualCustomer
{
    public class GetIndividualCustomerQuery : IRequest<IndividualCustomer>
    {
        public GetIndividualCustomerQuery(string? accountNumber)
        {
            AccountNumber = accountNumber;
        }

        public string? AccountNumber { get; set; }
    }
}