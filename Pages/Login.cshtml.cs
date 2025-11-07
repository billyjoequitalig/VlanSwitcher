using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;
using System.Security.Claims;
using System.Text.Json;

namespace VLAN_Switching.Pages
{
    [Authorize]
    public class LoginModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public VentusLibrary.DataLibrary rs;

        [BindProperty]
        public string txtNTLogin { get; set; }
        [BindProperty]
        public string txtPassword { get; set; }
        public LoginModel(IConfiguration configuration)
        {
            _configuration = configuration;
            string connString = _configuration.GetConnectionString("AU_ConnString");
            rs = new VentusLibrary.DataLibrary(connString);
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost(string Action)
        {
            if (Action == "Login")
            {
                //ConnectionTestResult = TestDatabaseConnection();
                string ntLogin = VentusLibrary.CommonFunctions.FStr(txtNTLogin);

                string iqry = "Select * from tbl_Users where NTLogin = " + VentusLibrary.CommonFunctions.FStr(txtNTLogin) + " and StatusId = '2'";
                DataTable dt = rs.CreateDataTableFromQry(iqry);
                //jd.dt = jd.rs.CreateDataTableFromQry(jd.iqry);
                if (dt.Rows.Count != 0)
                {
                    TempData["InactiveUserAlert"] = "Ask Administrator to Activate your Account";
                    return RedirectToPage();
                }

                // LDAP auth
                var oLDAPLib = new VentusLibrary.LDAPLibrary();
                oLDAPLib.LDAPPath = _configuration["domainName_inspiro"];

                oLDAPLib.AuthenticateUser(oLDAPLib.LDAPPath, txtNTLogin, txtPassword);

                if (oLDAPLib.Authenticated)// == false)
                {
                    iqry = "Select * from tbl_Users where NTLogin = " + VentusLibrary.CommonFunctions.FStr(txtNTLogin);
                    dt = rs.CreateDataTableFromQry(iqry);
                    if (dt.Rows.Count == 0)
                    {
                        TempData["PopupMessage"] = "You have no authority to open this application.";
                        return Page();
                    }

                    var claims = new List<Claim>
{
    new Claim(ClaimTypes.Name, dt.Rows[0]["NTLogin"].ToString()),       // NTLogin as user identity
    new Claim("IDNumber", dt.Rows[0]["IDNumber"].ToString()),
    new Claim("FullName", dt.Rows[0]["FullName"].ToString().ToUpper()),
    new Claim("SwitchPortId", dt.Rows[0]["SwitchPortId"].ToString()),
    new Claim("CampaignId", dt.Rows[0]["CampaignId"].ToString()),
    new Claim("RoleID", dt.Rows[0]["RoleID"].ToString()),
    new Claim("StatusId", dt.Rows[0]["StatusId"].ToString())
};
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    // Sign in
                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        principal,
                        new AuthenticationProperties
                        {
                            IsPersistent = true,
                            ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1)
                        });

                    return RedirectToPage("/Index");
                }
                else
                {



                    TempData["PopupMessage"] = "Logon failure: unknown user name or bad password.!";
                    return Page();

                }
            }
            return Page();

        }
    }
}
