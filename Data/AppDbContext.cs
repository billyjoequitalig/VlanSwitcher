using Microsoft.EntityFrameworkCore;
using System.Data;
using VLAN_Switching.Models;

namespace VLAN_Switching.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<UserModel> tbl_Users { get; set; }
        public DbSet<CampaignModel> tbl_Campaigns { get; set; }
        public DbSet<SwitchPortModel> tbl_SwitchPorts { get; set; }
        public DbSet<RoleModel> tbl_Roles { get; set; }
        public DbSet<StatusModel> tbl_Status { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CampaignModel>().HasData(
                new CampaignModel
                {
                    Id = 1,
                    VlanId = 76,
                    Name = "Shopee"  // if Name is required
                }
            );

            modelBuilder.Entity<UserModel>().HasData(
                new UserModel
                {
                    Id = 1,
                    IdNumber = 2025005704,
                    NTlogin = "Joe.Quitalig",
                    FullName = "Joe Quitalig",
                    CampaignId = 1,  // must match above
                    SwitchPortId = 1,
                    RoleID = 1,
                    StatusId = 1
                }
            );
            modelBuilder.Entity<SwitchPortModel>().HasData(
                new SwitchPortModel
                {
                    Id = 1,
                    SwitchIP = "10.5.2.34",
                    PortName = "Gi1/0/38"
                }
            );
            modelBuilder.Entity<RoleModel>().HasData(
                new RoleModel
                {
                    RoleID = 1,
                    Role = "Admin"
                },
                new RoleModel
                {
                    RoleID = 2,
                    Role = "Agent"
                }
            );

            modelBuilder.Entity<StatusModel>().HasData(
                new StatusModel
                {
                    StatusId = 1,
                    Status = "Active"
                },
                new StatusModel
                {
                    StatusId = 2,
                    Status = "Inactive"
                }
            );
        }

}
}
