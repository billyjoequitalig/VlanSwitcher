using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VLAN_Switching.Data;
using VLAN_Switching.Models;

namespace VLAN_Switching.Pages
{
    public class AddUserModel : PageModel
    {
        private readonly AppDbContext _appDbContext;
        public AddUserModel(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        [BindProperty]
        public UserModel newUser { get; set; }
        public List<CampaignModel> Campaigns { get; set; }
        public List<SwitchPortModel> Ports { get; set; }
        public List<RoleModel> role { get; set; }

        public void OnGet()
        {
            Campaigns = _appDbContext.tbl_Campaigns.AsNoTracking().ToList();
            role = _appDbContext.tbl_Roles.AsNoTracking().ToList();
            Ports = _appDbContext.tbl_SwitchPorts.AsNoTracking().ToList();
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                // Reload dropdown data
                Campaigns = _appDbContext.tbl_Campaigns.ToList();
                role = _appDbContext.tbl_Roles.ToList();
                Ports = _appDbContext.tbl_SwitchPorts.ToList();
                return Page();
            }
            newUser.StatusId = 1;
            _appDbContext.tbl_Users.Add(newUser);
            _appDbContext.SaveChanges();
            return RedirectToPage("/UserManagement");
        }
    }
}
