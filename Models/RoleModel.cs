using System.ComponentModel.DataAnnotations;

namespace VLAN_Switching.Models
{
    public class RoleModel
    {
        [Key]
        public int RoleID { get; set; }
        public string Role { get; set; }
    }
}
