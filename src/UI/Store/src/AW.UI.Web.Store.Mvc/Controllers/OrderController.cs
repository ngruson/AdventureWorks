using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AW.UI.Web.Store.Mvc.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
    }
}