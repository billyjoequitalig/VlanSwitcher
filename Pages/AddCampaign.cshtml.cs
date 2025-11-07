using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VLAN_Switching.Data;
using VLAN_Switching.Models;

namespace VLAN_Switching.Pages
{
    public class AddCampaignModel : PageModel
    {
        private readonly AppDbContext _appDbContext;
        public AddCampaignModel(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        [BindProperty]
        public CampaignModel newCampagin { get; set; }
        public void OnGet()
        {
        }
        public void OnPost()
        {
            _appDbContext.tbl_Campaigns.Add(newCampagin);
            _appDbContext.SaveChanges();
        }
    }
}
