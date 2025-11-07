using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Utilities;
using System.Data;
using VLAN_Switching.Data;
using VLAN_Switching.Models;

namespace VLAN_Switching.Pages
{
    public class AddPortModel : PageModel
    {
        private readonly AppDbContext _appDbContext;
        public AddPortModel(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public List<SwitchPortModel> Ports { get; set; } = new();
        [BindProperty]
        public SwitchPortModel newPort { get; set; }
        public void OnGet()
        {
            // Load existing ports when the page is first loaded
            Ports = _appDbContext.tbl_SwitchPorts.ToList();
        }
        public void OnPost()
        {
            if (!ModelState.IsValid)
            {
                // Reload dropdown data

            }
            _appDbContext.tbl_SwitchPorts.Add(newPort);
            _appDbContext.SaveChanges();

        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var usersUsingPort = await _appDbContext.tbl_Users
        .Where(u => u.SwitchPortId == id)
        .ToListAsync();

            foreach (var user in usersUsingPort)
            {
                user.SwitchPortId = null;
            }
            await _appDbContext.SaveChangesAsync();
            var port = await _appDbContext.tbl_SwitchPorts.FindAsync(id);
            if (port != null)
            {
                _appDbContext.tbl_SwitchPorts.Remove(port);
                await _appDbContext.SaveChangesAsync();
            }

            // Redirect to the same page after delete
            return RedirectToPage();
        }
    }
}
