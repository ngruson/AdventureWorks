using AW.Services.Customer.Application.Common;

namespace AW.Services.Customer.Application.GetCustomer
{
    public class AddressDto : IMapFrom<Domain.Address>
    {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string StateProvince { get; set; }
    }
}