using AW.Domain.Purchasing;
using AW.Domain.Sales;
using System;
using System.Collections.Generic;

namespace AW.Domain.HumanResources
{
    public partial class Employee : BaseEntity
    {
        public new int Id { get; set; }

        public string NationalIDNumber { get; set; }

        public string LoginID { get; set; }

        public short? OrganizationLevel { get; set; }

        public string JobTitle { get; set; }

        public DateTime BirthDate { get; set; }

        public string MaritalStatus { get; set; }

        public string Gender { get; set; }

        public DateTime HireDate { get; set; }

        public bool SalariedFlag { get; set; }

        public short VacationHours { get; set; }

        public short SickLeaveHours { get; set; }

        public bool CurrentFlag { get; set; }

        public Guid rowguid { get; set; }

        public DateTime ModifiedDate { get; set; }

        public virtual Person.Person Person { get; set; }

        public virtual ICollection<EmployeeDepartmentHistory> EmployeeDepartmentHistory { get; set; } = new List<EmployeeDepartmentHistory>();

        public virtual ICollection<EmployeePayHistory> EmployeePayHistory { get; set; } = new List<EmployeePayHistory>();

        public virtual ICollection<JobCandidate> JobCandidates { get; set; } = new List<JobCandidate>();

        public virtual ICollection<PurchaseOrderHeader> PurchaseOrderHeaders { get; set; } = new List<PurchaseOrderHeader>();

        public virtual SalesPerson SalesPerson { get; set; }
    }
}