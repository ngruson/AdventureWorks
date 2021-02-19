using System;

namespace AW.Core.Domain.Person
{    
    public partial class Password
    {
        public int BusinessEntityID { get; set; }
        public string PasswordHash { get; set; }

        public string PasswordSalt { get; set; }

        public Guid rowguid { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}