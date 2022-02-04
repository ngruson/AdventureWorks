namespace AW.Services.Customer.Core.Entities.PreferredAddress
{
    public interface IPreferredAddressStrategy
    {
        Address GetPreferredAddress(string addressType);
    }
}