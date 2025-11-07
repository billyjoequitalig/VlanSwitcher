//using VLAN_Switching.Data;
//using VLAN_Switching.Models;
//using Microsoft.EntityFrameworkCore;
//namespace VLAN_Switching.Services
//{
//    public class NetworkService
//    {
//        private readonly AppDbContext _dbContext;

//        public NetworkService(AppDbContext dbContext)
//        {
//            _dbContext = dbContext;
//        }

//        public User GetUserFromDb(int userId)
//        {
//            return _dbContext.tbl_Users
//                .Include(u => u.Campaign) // Optional if you need campaign info
//                .FirstOrDefault(u => u.IdNumber == userId);
//        }

//        public int GetVlanFromCampaign(int campaignId)
//        {
//            var campaign = _dbContext.tbl_Campaigns.FirstOrDefault(c => c.Id == campaignId);
//            return campaign?.VlanId ?? -1;
//        }

//        public string GetUserPort(int userId)
//        {
//            var port = _dbContext.tbl_SwitchPorts.FirstOrDefault(p => p.Id == userId);
//            return port?.PortName ?? "Unknown";
//        }
//    }
//}
