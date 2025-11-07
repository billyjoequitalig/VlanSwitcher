using Microsoft.EntityFrameworkCore;
using System;
using VLAN_Switching.Data;
namespace VLAN_Switching.Services;
public class VlanSwitchingService
{
    private readonly SshService _ssh;
    private readonly AppDbContext _dbContext;

    public VlanSwitchingService(SshService ssh,AppDbContext dbContext)
    {
        _ssh = ssh;
        _dbContext = dbContext;
    }

    public string SwitchUserVlan(string host,int userId)
    {
        // 1. Fetch user from DB
        var user = _dbContext.tbl_Users
            .Include(u => u.Campaign)
            .Include(u => u.SwitchPort)
            .FirstOrDefault(u => u.IdNumber == userId);

        if (user == null)
            throw new Exception("User not found.");

        // 2. Get VLAN ID from campaign
        var vlanId = user.Campaign?.VlanId ?? throw new Exception("VLAN ID not found for campaign.");


        // 3. Get switch port assigned to this user (custom logic)
        var interfaceName = user.SwitchPort?.PortName ?? throw new Exception("User has no assigned switch port.");

        // 4. Execute SSH VLAN change
        return _ssh.ExecuteVlanChange(host,interfaceName, vlanId);
    }
}
