namespace IndiaLivings_Web_UI.Models
{
    public class RoleViewModel
    {
        public int RoleID { get; set; } 
        public string RoleName { get; set; }
        public bool RoleStatus { get; set; }

        public List<RoleViewModel> GetRoles()
        {
            List<RoleViewModel>roles = new List<RoleViewModel>();
            RoleViewModel R1=new RoleViewModel { RoleID = 1, RoleName = "Administrator", RoleStatus = true };
            RoleViewModel R2 = new RoleViewModel { RoleID = 2, RoleName = "User", RoleStatus = true };
            RoleViewModel R3 = new RoleViewModel { RoleID = 3, RoleName = "Guest", RoleStatus = true };
            roles.Add(R1);
            roles.Add(R2);
            roles.Add(R3);
            return roles;

        }
    }
    
}
