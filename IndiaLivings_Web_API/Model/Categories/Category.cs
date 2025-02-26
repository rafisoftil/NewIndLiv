using IndiaLivings_Web_DAL;
using System.Data;
using System.Text.Json.Serialization;

namespace IndiaLivingsAPI.Model.Categories
{
    public class Category
    {
        public int intCategoryID { get; set; } = 0;
        public string strCategoryName { get; set; } = string.Empty;
        public string strCategoryImage { get; set; } = string.Empty;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool IsActive { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public DateTime? createdDate { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string createdBy { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public DateTime? updatedDate { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string updatedBy { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int intCategoryCount { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public List<subCategory> strSubCategoryDetails { get; set; }
    }
    public class subCategory
    {
        public int subCategoryID { get; set; } = 0;
        public string subCatergoryName { get; set; } = string.Empty;
        public string strCategoryName { get; set; } = string.Empty;
        public int intCategoryID { get; set; } = 0;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool IsActive { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public DateTime? createdDate { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string createdBy { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public DateTime? updatedDate { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string updatedBy { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int intSubCategoryCount { get; set; }
    }
    public class clsCategoryDetails()
    {
        public bool insertCategory(string strCategoryName, string strCategoryImage, string strCreatedBy)
        {
            const string SP_Name = "usp_insertCatgory";
            int result = 0;
            try
            {
                DataAccess _objDM = new DataAccess("IndiaLivings");
                _objDM.InitializeParameterList();
                _objDM.AddParameter("@categoryName", strCategoryName, ParameterDirection.Input);
                _objDM.AddParameter("@categoryImage", strCategoryImage, ParameterDirection.Input);
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
        public bool updateCategory(int intCategoryID, string strCategoryName, string strCategoryImage, string strUpdatedBy)
        {
            const string SP_Name = "usp_updateCategory";
            int result = 0;
            try
            {
                DataAccess _objDM = new DataAccess("IndiaLivings");
                _objDM.InitializeParameterList();
                _objDM.AddParameter("@categoryID", intCategoryID, ParameterDirection.Input);
                _objDM.AddParameter("@categoryName", strCategoryName, ParameterDirection.Input);
                _objDM.AddParameter("@categoryImage", strCategoryImage, ParameterDirection.Input);
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
        public bool deleteCategory(int intCategoryID, string strUpdatedBy)
        {
            const string SP_Name = "usp_updateCategory";
            int result = 0;
            try
            {
                DataAccess _objDM = new DataAccess("IndiaLivings");
                _objDM.InitializeParameterList();
                _objDM.AddParameter("@categoryID", intCategoryID, ParameterDirection.Input);
                _objDM.AddParameter("@categoryName", "", ParameterDirection.Input);
                _objDM.AddParameter("@categoryImage", "", ParameterDirection.Input);
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

        public List<Category> viewActiveCategory(int intCategoryID)
        {
            const string SP_Name = "usp_getALLCategory";
            DataSet ds = null;
            List<Category> lsCategory = new List<Category>();
            Category _category = null;

            try
            {
                DataAccess _objDM = new DataAccess("IndiaLivings");
                _objDM.InitializeParameterList();
                _objDM.AddParameter("@categoryID", intCategoryID, ParameterDirection.Input);
                ds = _objDM.GetDataSet(SP_Name);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        _category = new Category();
                        _category.intCategoryID = (int)ds.Tables[0].Rows[i]["categoryID"];
                        _category.strCategoryName = ds.Tables[0].Rows[i]["categoryName"].ToString();
                        _category.strCategoryImage = ds.Tables[0].Rows[i]["categoryImage"].ToString();
                        _category.IsActive = Convert.ToBoolean(ds.Tables[0].Rows[i]["IsActive"].ToString());
                        _category.createdDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["createdDate"].ToString());
                        _category.createdBy = ds.Tables[0].Rows[i]["createdBy"].ToString();
                        _category.updatedDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["updatedDate"].ToString());
                        _category.updatedBy = ds.Tables[0].Rows[i]["updatedBy"].ToString();
                        lsCategory.Add(_category);
                    }

                }

            }
            catch (Exception)
            {

                throw;
            }
            return lsCategory;
        }
        public List<Category> viewCategoryListCount()
        {
            const string SP_Name = "usp_getCategoryCounts";
            DataSet ds = null;
            List<Category> lsCategory = new List<Category>();
            List<subCategory> lssubCategory = new List<subCategory>();
            Category _category = null;
            clsSubCategoryDetails _subCategory = null;


            try
            {
                DataAccess _objDM = new DataAccess("IndiaLivings");
                _objDM.InitializeParameterList();
                _objDM.AddParameter("@categoryID", 0, ParameterDirection.Input);
                ds = _objDM.GetDataSet(SP_Name);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        _category = new Category();
                        _subCategory = new clsSubCategoryDetails();
                        _category.intCategoryID = (int)ds.Tables[0].Rows[i]["categoryID"];
                        _category.strCategoryName = ds.Tables[0].Rows[i]["categoryName"].ToString();
                        _category.strCategoryImage = ds.Tables[0].Rows[i]["categoryImage"].ToString();
                        _category.intCategoryCount = Convert.ToInt32(ds.Tables[0].Rows[i]["categoryCount"].ToString());
                        lssubCategory = _subCategory.viewSubCategoryListCount((int)ds.Tables[0].Rows[i]["categoryID"]);
                        _category.strSubCategoryDetails = lssubCategory;
                        lsCategory.Add(_category);
                    }

                }

            }
            catch (Exception)
            {

                throw;
            }
            return lsCategory;
        }

    }
    public class clsSubCategoryDetails
    {
        public bool insertSubCategory(string subCatergoryName, int intCategoryID, string strCreatedBy)
        {
            const string SP_Name = "usp_insertSubCategory";
            int result = 0;
            try
            {
                DataAccess _objDM = new DataAccess("IndiaLivings");
                _objDM.InitializeParameterList();
                _objDM.AddParameter("@subCatergoryName", subCatergoryName, ParameterDirection.Input);
                _objDM.AddParameter("@categoryID", intCategoryID, ParameterDirection.Input);
                _objDM.AddParameter("@createdBy", strCreatedBy, ParameterDirection.Input);


                result = Convert.ToInt32(_objDM.GetScalar(SP_Name));

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result > 0 ? true : false;
        }
        public bool updateSubCategory(int subCategoryID, string subCatergoryName, int intCategoryID, string strUpdatedBy)
        {
            const string SP_Name = "usp_updateSubCategory";
            int result = 0;
            try
            {
                DataAccess _objDM = new DataAccess("IndiaLivings");
                _objDM.InitializeParameterList();
                _objDM.AddParameter("@subCategoryID", subCategoryID, ParameterDirection.Input);
                _objDM.AddParameter("@subCatergoryName", subCatergoryName, ParameterDirection.Input);
                _objDM.AddParameter("@categoryID", intCategoryID, ParameterDirection.Input);
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
        public bool deleteSubCategory(int subCategoryID, string strUpdatedBy)
        {
            const string SP_Name = "usp_updateSubCategory";
            int result = 0;
            try
            {
                DataAccess _objDM = new DataAccess("IndiaLivings");
                _objDM.InitializeParameterList();
                _objDM.AddParameter("@subCategoryID", subCategoryID, ParameterDirection.Input);
                _objDM.AddParameter("@subCatergoryName", "", ParameterDirection.Input);
                _objDM.AddParameter("@categoryID", 0, ParameterDirection.Input);
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
        public List<subCategory> viewActiveSubCategory(int intCategoryID)
        {
            const string SP_Name = "usp_GetAllSubCategory";
            DataSet ds = null;
            List<subCategory> lssubCategory = new List<subCategory>();
            subCategory _subCategory = null;

            try
            {
                DataAccess _objDM = new DataAccess("IndiaLivings");
                _objDM.InitializeParameterList();
                _objDM.AddParameter("@CategoryID", intCategoryID, ParameterDirection.Input);
                ds = _objDM.GetDataSet(SP_Name);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        _subCategory = new subCategory();
                        _subCategory.subCategoryID = (int)ds.Tables[0].Rows[i]["subCategoryID"];
                        _subCategory.subCatergoryName = ds.Tables[0].Rows[i]["subCatergoryName"].ToString();
                        _subCategory.intCategoryID = Convert.ToInt32(ds.Tables[0].Rows[i]["categoryID"].ToString());
                        _subCategory.strCategoryName = ds.Tables[0].Rows[i]["categoryName"].ToString();
                        _subCategory.intCategoryID = Convert.ToInt32(ds.Tables[0].Rows[i]["categoryID"].ToString());
                        _subCategory.IsActive = Convert.ToBoolean(ds.Tables[0].Rows[i]["IsActive"].ToString());
                        _subCategory.createdDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["createdDate"].ToString());
                        _subCategory.createdBy = ds.Tables[0].Rows[i]["createdBy"].ToString();
                        _subCategory.updatedDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["updatedDate"].ToString());
                        _subCategory.updatedBy = ds.Tables[0].Rows[i]["updatedBy"].ToString();

                        lssubCategory.Add(_subCategory);
                    }

                }

            }
            catch (Exception)
            {

                throw;
            }
            return lssubCategory;
        }
        public List<subCategory> viewSubCategoryListCount(int intCategoryID)
        {
            const string SP_Name = "usp_getCategoryCounts";
            DataSet ds = null;
            List<subCategory> lssubCategory = new List<subCategory>();
            subCategory _subCategory = null;

            try
            {
                DataAccess _objDM = new DataAccess("IndiaLivings");
                _objDM.InitializeParameterList();
                _objDM.AddParameter("@CategoryID", intCategoryID, ParameterDirection.Input);
                ds = _objDM.GetDataSet(SP_Name);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        _subCategory = new subCategory();
                        _subCategory.subCategoryID = (int)ds.Tables[0].Rows[i]["subCategoryID"];
                        _subCategory.subCatergoryName = ds.Tables[0].Rows[i]["subCatergoryName"].ToString();
                        _subCategory.intCategoryID = Convert.ToInt32(ds.Tables[0].Rows[i]["categoryID"].ToString());
                        _subCategory.intSubCategoryCount = Convert.ToInt32(ds.Tables[0].Rows[i]["subCategoryCount"].ToString());
                        lssubCategory.Add(_subCategory);
                    }

                }

            }
            catch (Exception)
            {

                throw;
            }
            return lssubCategory;
        }

    }
}
