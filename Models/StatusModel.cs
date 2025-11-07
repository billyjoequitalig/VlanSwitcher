using System.ComponentModel.DataAnnotations;

namespace VLAN_Switching.Models
{
    public class StatusModel
    {
        [Key]
        public int StatusId { get; set; }
        public string Status { get; set; }
    }
}
