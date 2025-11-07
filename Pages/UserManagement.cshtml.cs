using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VLAN_Switching.Data;
using VLAN_Switching.Models;

namespace VLAN_Switching.Pages
{
    public class UserManagementModel : PageModel
    {
        private readonly AppDbContext _appDbContext;

        public UserManagementModel(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public List<UserModel> Users { get; set; }

        public async Task OnGetAsync()
        {
            //Users = await _appDbContext.tbl_Users.ToListAsync();
            Users = await _appDbContext.tbl_Users
                .Include(d => d.SwitchPort)
                .Include(d => d.Campaign)
                .Include(d => d.Role)
                .Include(d => d.Status)
                .ToListAsync();
        }
        public IActionResult OnPostDelete(int userId)
        {
            var user = _appDbContext.tbl_Users.Find(userId);
            if (user != null)
            {
                _appDbContext.tbl_Users.Remove(user);
                _appDbContext.SaveChanges();
            }

            return RedirectToPage();  // Refresh page
        }
    }
}
