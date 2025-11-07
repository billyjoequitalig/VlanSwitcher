using Microsoft.AspNetCore.Mvc;

namespace VLAN_Switching.Controllers
{
    public class TerminalController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
