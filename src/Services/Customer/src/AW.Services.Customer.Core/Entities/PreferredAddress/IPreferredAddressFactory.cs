namespace AW.Services.Customer.Core.Entities.PreferredAddress
{
    public interface IPreferredAddressFactory
    {
        Address GetPreferredAddress(string addressType);
    }
}