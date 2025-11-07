using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VLAN_Switching.Data;
using VLAN_Switching.Models;

namespace VLAN_Switching.Pages
{
    public class CampaignConfigModel : PageModel
    {
        private readonly AppDbContext _appDbContext;
        public CampaignConfigModel (AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [BindProperty]
        public UserModel newUser { get; set; }
        public List<UserModel> Users { get; set; }
        public List<CampaignModel> Campaigns { get; set; }
        public List<SwitchPortModel> Ports { get; set; }
        public List<RoleModel> role { get; set; }
        public void OnGet()
        {
            Campaigns = _appDbContext.tbl_Campaigns.AsNoTracking().ToList();
            role = _appDbContext.tbl_Roles.AsNoTracking().ToList();
            Ports = _appDbContext.tbl_SwitchPorts.AsNoTracking().ToList();
            Users = _appDbContext.tbl_Users.AsNoTracking().ToList();
        }
    }
}
