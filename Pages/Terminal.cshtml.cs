using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Telerik.Web.UI.com.hisoftware.api2;
using VLAN_Switching.Models;
using VLAN_Switching.Services;
namespace VLAN_Switching.Pages
{
    public class TerminalModel : PageModel
    {
        [BindProperty]
        public string Host { get; set; }

        private readonly VlanSwitchingService _vlanSwitchingService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TerminalModel(VlanSwitchingService vlanSwitchingService, IHttpContextAccessor httpContextAccessor)
        {
            _vlanSwitchingService = vlanSwitchingService;
            _httpContextAccessor = httpContextAccessor;
        }
        public void OnGet()
        {
        }

        
        public async Task<IActionResult> OnPostSwitchVlanAsync()
        {
            try
            {
                var user = _httpContextAccessor.HttpContext?.User;
                var idNumber = user?.FindFirst("IDNumber")?.Value;
                if (int.TryParse(idNumber, out int toIntidNumber))
                {
                    var result = _vlanSwitchingService.SwitchUserVlan(Host,toIntidNumber);
                    ViewData["Result"] = result;
                }
                    //int userId = SshConfig.userID;
            }
            catch(Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
                
            }
            return Page();
        }
    }

}
