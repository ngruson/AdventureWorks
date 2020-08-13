using System;

namespace AW.Domain.Person
{    
    public partial class Password : BaseEntity
    {
        public string PasswordHash { get; set; }

        public string PasswordSalt { get; set; }

        public Guid rowguid { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}