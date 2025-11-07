using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VLAN_Switching.Models
{
    public class UserModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int IdNumber { get; set; }
        [Required]
        public string NTlogin { get; set; }
        public string? FullName { get; set; }
        public int? SwitchPortId { get; set; }
        public int?  CampaignId { get; set; }
        public int? RoleID { get; set; }
        public int? StatusId { get; set; }
        public CampaignModel? Campaign { get; set; }
        public SwitchPortModel? SwitchPort { get; set; }
        public RoleModel? Role { get; set; }
        public StatusModel? Status { get; set; }
    }
}
