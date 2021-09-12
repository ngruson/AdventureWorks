using System.Security.Principal;

namespace AW.UI.Web.Store.Services
{
    public interface IIdentityParser<T>
    {
        T Parse(IPrincipal principal);
    }
}