using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using AW.Domain.HumanResources;
using AW.Domain.Person;
using AW.Domain.Production;
using AW.Domain.Purchasing;
using AW.Domain.Sales;

namespace AW.Persistence.EntityFramework
{
    public partial class AWContext : DbContext
    {
        public AWContext()
            : base("AWContext")
        {
        }

        //public virtual DbSet<Department> Department { get; set; }
        //public virtual DbSet<Employee> Employee { get; set; }
        //public virtual DbSet<EmployeeDepartmentHistory> EmployeeDepartmentHistory { get; set; }
        //public virtual DbSet<EmployeePayHistory> EmployeePayHistory { get; set; }
        //public virtual DbSet<JobCandidate> JobCandidate { get; set; }
        //public virtual DbSet<Shift> Shift { get; set; }
        //public virtual DbSet<Address> Address { get; set; }
        //public virtual DbSet<AddressType> AddressType { get; set; }
        //public virtual DbSet<BusinessEntity> BusinessEntity { get; set; }
        //public virtual DbSet<BusinessEntityAddress> BusinessEntityAddress { get; set; }
        //public virtual DbSet<BusinessEntityContact> BusinessEntityContact { get; set; }
        //public virtual DbSet<ContactType> ContactType { get; set; }
        //public virtual DbSet<CountryRegion> CountryRegion { get; set; }
        //public virtual DbSet<EmailAddress> EmailAddress { get; set; }
        //public virtual DbSet<Password> Password { get; set; }
        //public virtual DbSet<Person> Person { get; set; }
        //public virtual DbSet<PersonPhone> PersonPhone { get; set; }
        //public virtual DbSet<PhoneNumberType> PhoneNumberType { get; set; }
        //public virtual DbSet<StateProvince> StateProvince { get; set; }
        //public virtual DbSet<BillOfMaterials> BillOfMaterials { get; set; }
        //public virtual DbSet<Culture> Culture { get; set; }
        //public virtual DbSet<Illustration> Illustration { get; set; }
        //public virtual DbSet<Location> Location { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        //public virtual DbSet<ProductCategory> ProductCategory { get; set; }
        //public virtual DbSet<ProductCostHistory> ProductCostHistory { get; set; }
        //public virtual DbSet<ProductDescription> ProductDescription { get; set; }
        //public virtual DbSet<ProductInventory> ProductInventory { get; set; }
        //public virtual DbSet<ProductListPriceHistory> ProductListPriceHistory { get; set; }
        //public virtual DbSet<ProductModel> ProductModel { get; set; }
        //public virtual DbSet<ProductModelIllustration> ProductModelIllustration { get; set; }
        //public virtual DbSet<ProductModelProductDescriptionCulture> ProductModelProductDescriptionCulture { get; set; }
        //public virtual DbSet<ProductPhoto> ProductPhoto { get; set; }
        //public virtual DbSet<ProductProductPhoto> ProductProductPhoto { get; set; }
        //public virtual DbSet<ProductReview> ProductReview { get; set; }
        //public virtual DbSet<ProductSubcategory> ProductSubcategory { get; set; }
        //public virtual DbSet<ScrapReason> ScrapReason { get; set; }
        //public virtual DbSet<TransactionHistory> TransactionHistory { get; set; }
        //public virtual DbSet<TransactionHistoryArchive> TransactionHistoryArchive { get; set; }
        //public virtual DbSet<UnitMeasure> UnitMeasure { get; set; }
        //public virtual DbSet<WorkOrder> WorkOrder { get; set; }
        //public virtual DbSet<WorkOrderRouting> WorkOrderRouting { get; set; }
        //public virtual DbSet<ProductVendor> ProductVendor { get; set; }
        //public virtual DbSet<PurchaseOrderDetail> PurchaseOrderDetail { get; set; }
        //public virtual DbSet<PurchaseOrderHeader> PurchaseOrderHeader { get; set; }
        //public virtual DbSet<ShipMethod> ShipMethod { get; set; }
        //public virtual DbSet<Vendor> Vendor { get; set; }
        //public virtual DbSet<CountryRegionCurrency> CountryRegionCurrency { get; set; }
        //public virtual DbSet<CreditCard> CreditCard { get; set; }
        //public virtual DbSet<Currency> Currency { get; set; }
        //public virtual DbSet<CurrencyRate> CurrencyRate { get; set; }
        //public virtual DbSet<Customer> Customer { get; set; }
        //public virtual DbSet<PersonCreditCard> PersonCreditCard { get; set; }
        //public virtual DbSet<SalesOrderDetail> SalesOrderDetail { get; set; }
        //public virtual DbSet<SalesOrderHeader> SalesOrderHeader { get; set; }
        //public virtual DbSet<SalesOrderHeaderSalesReason> SalesOrderHeaderSalesReason { get; set; }
        //public virtual DbSet<SalesPerson> SalesPerson { get; set; }
        //public virtual DbSet<SalesPersonQuotaHistory> SalesPersonQuotaHistory { get; set; }
        //public virtual DbSet<SalesReason> SalesReason { get; set; }
        //public virtual DbSet<SalesTaxRate> SalesTaxRate { get; set; }
        //public virtual DbSet<SalesTerritory> SalesTerritory { get; set; }
        //public virtual DbSet<SalesTerritoryHistory> SalesTerritoryHistory { get; set; }
        //public virtual DbSet<ShoppingCartItem> ShoppingCartItem { get; set; }
        //public virtual DbSet<SpecialOffer> SpecialOffer { get; set; }
        //public virtual DbSet<SpecialOfferProduct> SpecialOfferProduct { get; set; }
        //public virtual DbSet<Store> Store { get; set; }
        //public virtual DbSet<ProductDocument> ProductDocument { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
              .Where(type => !string.IsNullOrEmpty(type.Namespace))
              .Where(type => type.BaseType != null && type.BaseType.IsGenericType
                   && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }

            base.OnModelCreating(modelBuilder);


            //modelBuilder.Configurations.Add(new BillOfMaterialsConfiguration());
            
        }
    }
}