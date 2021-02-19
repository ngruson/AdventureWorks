using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AW.Services.Customer.Domain
{
    public class PersonCustomer : Person
    {
        public List<CustomerAddress> MyProperty { get; set; }
    }
}
