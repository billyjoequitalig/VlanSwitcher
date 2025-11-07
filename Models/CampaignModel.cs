namespace VLAN_Switching.Models
{
    public class CampaignModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int VlanId { get; set; }
        public ICollection<UserModel> tbl_Users { get; set; }
    }
}
