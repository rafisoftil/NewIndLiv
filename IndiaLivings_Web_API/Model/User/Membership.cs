using IndiaLivingsAPI.Model.Categories;
using IndiaLivings_Web_DAL;
using System.Data;

namespace IndiaLivingsAPI.Model.Users
{
    public class Membership
    {
        public int intMembershipID { get; set; } = 0;
        public string strMembershipName { get; set; } = string.Empty;
        public string strMembershipDescription { get; set; } = string.Empty;
        public int intMembershipAdListing { get; set; } = 0;
        public decimal decMembershipPrice { get; set; } = 0;
        public bool IsActive { get; set; } = true;
        public DateTime createdDate { get; set; } = DateTime.MinValue;
        public string createdBy { get; set; } = string.Empty;
        public DateTime updatedDate { get; set; } = DateTime.MinValue;
        public string updatedBy { get; set; } = string.Empty;
    }
    public class clsMembershipDetails
    {
        public bool insertMembership(string strMembershipName, int intMembershipAdsLimit, decimal decMembershipPrice, string strMembershipDescription, string strCreatedBy)
        {
            const string SP_Name = "usp_insertmembership";
            int result = 0;
            try
            {
                DataAccess _objDM = new DataAccess("IndiaLivings");
                _objDM.InitializeParameterList();
                _objDM.AddParameter("@membershipName", strMembershipName, ParameterDirection.Input);
                _objDM.AddParameter("@membershipAdListing", intMembershipAdsLimit, ParameterDirection.Input);
                _objDM.AddParameter("@membershipPrice", decMembershipPrice, ParameterDirection.Input);
                _objDM.AddParameter("@membershipDescription", strMembershipDescription, ParameterDirection.Input);
                _objDM.AddParameter("@IsActive", true, ParameterDirection.Input);
                _objDM.AddParameter("@createdBy", strCreatedBy, ParameterDirection.Input);


                result = Convert.ToInt32(_objDM.GetScalar(SP_Name));

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result > 0 ? true : false;
        }
        public bool updateMembership(int intMembershipID, string strMembershipName, int intMembershipAdsLimit, decimal decMembershipPrice, string strMembershipDescription, string strUpdatedBy)
        {
            const string SP_Name = "usp_updateMembership";
            int result = 0;
            try
            {
                DataAccess _objDM = new DataAccess("IndiaLivings");
                _objDM.InitializeParameterList();
                _objDM.AddParameter("@membershipID", intMembershipID, ParameterDirection.Input);
                _objDM.AddParameter("@membershipName", strMembershipName, ParameterDirection.Input);
                _objDM.AddParameter("@membershipAdListing", intMembershipAdsLimit, ParameterDirection.Input);
                _objDM.AddParameter("@membershipPrice", decMembershipPrice, ParameterDirection.Input);
                _objDM.AddParameter("@membershipDescription", strMembershipDescription, ParameterDirection.Input);
                _objDM.AddParameter("@IsActive", true, ParameterDirection.Input);
                _objDM.AddParameter("@updatedBy", strUpdatedBy, ParameterDirection.Input);
                _objDM.AddParameter("@flagDelete", 1, ParameterDirection.Input);

                result = Convert.ToInt32(_objDM.GetScalar(SP_Name));

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result > 0 ? true : false;
        }
        public bool deleteMembership(int intMembershipID, string strUpdatedBy)
        {
            const string SP_Name = "usp_updateMembership";
            int result = 0;
            try
            {
                DataAccess _objDM = new DataAccess("IndiaLivings");
                _objDM.InitializeParameterList();
                _objDM.AddParameter("@membershipID", intMembershipID, ParameterDirection.Input);
                _objDM.AddParameter("@membershipName", "", ParameterDirection.Input);
                _objDM.AddParameter("@membershipAdListing", 0, ParameterDirection.Input);
                _objDM.AddParameter("@membershipPrice", 0, ParameterDirection.Input);
                _objDM.AddParameter("@membershipDescription", "", ParameterDirection.Input);
                _objDM.AddParameter("@IsActive", false, ParameterDirection.Input);
                _objDM.AddParameter("@updatedBy", strUpdatedBy, ParameterDirection.Input);
                _objDM.AddParameter("@flagDelete", 0, ParameterDirection.Input);

                result = Convert.ToInt32(_objDM.GetScalar(SP_Name));

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result > 0 ? true : false;
        }
        public List<Membership> viewAllMembership(int intMembershipID)
        {
            const string SP_Name = "usp_getALLmembership";
            DataSet ds = null;
            List<Membership> lsMembership = new List<Membership>();
            Membership _membership = null;

            try
            {
                DataAccess _objDM = new DataAccess("IndiaLivings");
                _objDM.InitializeParameterList();
                _objDM.AddParameter("@intMembershipID", intMembershipID, ParameterDirection.Input);
                ds = _objDM.GetDataSet(SP_Name);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        _membership = new Membership();
                        _membership.intMembershipID = (int)ds.Tables[0].Rows[i]["membershipID"];
                        _membership.strMembershipName = ds.Tables[0].Rows[i]["membershipName"].ToString();
                        _membership.intMembershipAdListing = Convert.ToInt32(ds.Tables[0].Rows[i]["MembershipAdListing"].ToString());
                        _membership.decMembershipPrice = Convert.ToDecimal(ds.Tables[0].Rows[i]["MembershipPrice"].ToString());
                        _membership.strMembershipDescription = ds.Tables[0].Rows[i]["membershipDescription"].ToString();
                        _membership.IsActive = Convert.ToBoolean(ds.Tables[0].Rows[i]["IsActive"].ToString());
                        _membership.createdDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["createdDate"].ToString());
                        _membership.createdBy = ds.Tables[0].Rows[i]["createdBy"].ToString();
                        _membership.updatedDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["updatedDate"].ToString());
                        _membership.updatedBy = ds.Tables[0].Rows[i]["updatedBy"].ToString();

                        lsMembership.Add(_membership);
                    }

                }

            }
            catch (Exception)
            {

                throw;
            }
            return lsMembership;
        }
    }
}
