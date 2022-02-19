namespace AW.SharedKernel.Interfaces
{
    public interface ICustomer
    {
        public abstract CustomerType CustomerType { get; set; }
    }
}