namespace AW.Services.Customer.Core.Interfaces
{
    public interface ICustomer
    {
        public abstract CustomerType CustomerType { get; set; }
    }
}