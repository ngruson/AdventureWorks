using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AW.UI.Web.Store.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
    }
}