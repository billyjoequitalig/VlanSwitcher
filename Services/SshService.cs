using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Renci.SshNet;
using System.Text;
using VLAN_Switching.Models;

public class SshService
{

    private readonly SshConfigModel _config;

    public SshService(IOptions<SshConfigModel> config)
    {
        _config = config.Value;
    }


    public string ExecuteVlanChange(string host, string interfaceName, int vlanId)
    {
        var output = new StringBuilder();

        using var client = new SshClient(host,_config.Username, _config.Password);

        client.Connect();

        if (!client.IsConnected)
            throw new Exception("SSH connection failed.");
        var commandText = $@"
                enable
                configure terminal
                interface {interfaceName}
                switchport mode access
                switchport access vlan {vlanId}
                end
                copy running-config startup-config
            ";
        var command = client.CreateCommand(commandText);
        var result = command.Execute();
        output.AppendLine(result);

        client.Disconnect();

        return output.ToString();
    }
}
