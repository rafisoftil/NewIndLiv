using IndiaLivings_Web_DAL.Helpers;
using IndiaLivings_Web_DAL.Models;

namespace IndiaLivings_Web_UI.Models
{
    public class RoleViewModel
    {
        public int RoleID { get; set; } 
        public string RoleName { get; set; }
        public bool RoleStatus { get; set; }

        public List<RoleViewModel> GetAllRoles()
        {
            List<RoleViewModel> roles = new List<RoleViewModel>();  
            AuthenticationHelper AH = new AuthenticationHelper();
            try
            {
                var roleList = AH.RolesList();
                if (roleList != null)
                {
                    foreach (var roleDetails in roleList)
                    {
                        RoleViewModel role = new RoleViewModel();
                        role.RoleID = roleDetails.RoleID;
                        role.RoleName = roleDetails.RoleName;
                        role.RoleStatus = roleDetails.IsActive;
                        roles.Add(role);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return roles;
        }
    }
    
}
