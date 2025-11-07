using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace VLAN_Switching.Pages
{
    public class CampaignDetailsModel : PageModel
    {
        public string LocalMachineIP { get; private set; }

        [BindProperty]
        public string IpAddress { get; set; } 
        public IPAddress IP { get; set; }
        public string Status { get; set; } = "N/A";
        public long AverageResponseTime { get; set; }
        public int Ttl { get; set; }
        public int PacketLossPercent { get; set; }
        public string ErrorMessage { get; set; }
        public void OnGet()
        {
            LocalMachineIP = GetLocalIPv4();

        }

        public void OnPost(string Action)
        {
            if (string.IsNullOrWhiteSpace(IpAddress))
            {
                ErrorMessage = "Please enter a hostname or IP address.";
                return;
            }

            try
            {

                // Try to resolve hostname to IP
                IP = Dns.GetHostAddresses(IpAddress).FirstOrDefault();

                if (IP == null)
                {
                    ErrorMessage = "Could not resolve hostname.";
                    return;
                }   

                const int pingCount = 4;
                int packetsSent = pingCount;
                int packetsReceived = 0;
                long totalTime = 0;
                int ttl = 0;

                using Ping pingSender = new();
                for (int i = 0; i < pingCount; i++)
                {
                    PingReply reply = pingSender.Send(IP, 1000);

                    if (reply.Status == IPStatus.Success)
                    {
                        packetsReceived++;
                        totalTime += reply.RoundtripTime;
                        ttl = reply.Options?.Ttl ?? ttl; // take TTL from any successful reply
                    }
                }

                Status = packetsReceived > 0 ? "Success" : "Failed";

                AverageResponseTime = packetsReceived > 0 ? totalTime / packetsReceived : 0;

                Ttl = ttl;

                PacketLossPercent = (int)(((packetsSent - packetsReceived) / (double)packetsSent) * 100);
            }
            catch (Exception ex)
            {
                ErrorMessage = "Error: " + ex.Message;
            }
        }
        private string GetLocalIPv4()
        {
            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (ni.OperationalStatus != OperationalStatus.Up ||
                    ni.NetworkInterfaceType == NetworkInterfaceType.Loopback ||
                    ni.Description.ToLower().Contains("virtual"))
                    continue;

                var ipProps = ni.GetIPProperties();

                foreach (UnicastIPAddressInformation addr in ipProps.UnicastAddresses)
                {
                    if (addr.Address.AddressFamily == AddressFamily.InterNetwork)
                    {
                        return addr.Address.ToString(); // Your LAN IP (like 192.168.1.19)
                    }
                }
            }

            return "IP not found";
        }
    }
}
