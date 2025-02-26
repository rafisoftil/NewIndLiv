using IndiaLivings_Web_DAL;
using System.Data;

namespace IndiaLivingsAPI.Model.Products
{
    public class clsProduct
    {
        public int productId { get; set; } = 0;
        public string productName { get; set; } = string.Empty;
        public string productDescription { get; set; } = string.Empty;
        public string productDescription2 { get; set; } = string.Empty;
        public decimal productPrice { get; set; } = 0;
        public int productQuantity { get; set; } = 0;
        public int productCondition { get; set; } = 0;
        public int productCategoryID { get; set; } = 0;
        public string productCategoryName { get; set; } = string.Empty;
        public int productsubCategoryID { get; set; } = 0;
        public string productSubCategoryName { get; set; } = string.Empty;
        public bool productSold { get; set; } = false;
        public int productOwner { get; set; } = 0;
        public string productOwnerName { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public DateTime createdDate { get; set; } = DateTime.MinValue;
        public string createdBy { get; set; } = string.Empty;
        public DateTime updatedDate { get; set; } = DateTime.MinValue;
        public string updatedBy { get; set; } = string.Empty;
    }
    public class AdsDetails
    {
        public int adsCount { get; set; } = 0;
        public int adsCategoryID { get; set; } = 0;
        public string adsCategoryName { get; set; } = string.Empty;
        public int adssubCategoryID { get; set; } = 0;
        public string adsSubCategoryName { get; set; } = string.Empty;
    }
    public class clsProductDetails
    {
        public bool insertProduct(clsProduct _clsProduct)
        {
            const string SP_Name = "usp_insertProduct";
            int result = 0;
            try
            {
                DataAccess _objDM = new DataAccess("IndiaLivings");
                _objDM.InitializeParameterList();
                _objDM.AddParameter("@productName", _clsProduct.productName, ParameterDirection.Input);
                _objDM.AddParameter("@productDescription", _clsProduct.productDescription, ParameterDirection.Input);
                _objDM.AddParameter("@productDescription2", _clsProduct.productDescription2, ParameterDirection.Input);
                _objDM.AddParameter("@productPrice", Convert.ToDecimal(_clsProduct.productPrice), ParameterDirection.Input);
                _objDM.AddParameter("@productQuantity", (int)_clsProduct.productQuantity, ParameterDirection.Input);
                _objDM.AddParameter("@productCondition", Convert.ToInt32(_clsProduct.productCondition), ParameterDirection.Input);
                _objDM.AddParameter("@productCategoryID", Convert.ToInt32(_clsProduct.productCategoryID), ParameterDirection.Input);
                _objDM.AddParameter("@productsubCategoryID", Convert.ToInt32(_clsProduct.productsubCategoryID), ParameterDirection.Input);
                _objDM.AddParameter("@productSold", Convert.ToBoolean(_clsProduct.productSold), ParameterDirection.Input);
                _objDM.AddParameter("@productOwner", Convert.ToInt32(_clsProduct.productOwner), ParameterDirection.Input);
                _objDM.AddParameter("@createdBy", _clsProduct.createdBy, ParameterDirection.Input);

                 result = Convert.ToInt32(_objDM.GetScalar(SP_Name));
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result > 0 ? true : false;
        }
        
        public List<clsProduct> viewAllProduct(int intProductOwner)
        {
            const string SP_Name = "usp_getAllProductDetails";
            DataSet ds = null;
            List<clsProduct> lsProduct = new List<clsProduct>();
            clsProduct _product = null;

            try
            {
                DataAccess _objDM = new DataAccess("IndiaLivings");
                _objDM.InitializeParameterList();
                _objDM.AddParameter("@productOwner", intProductOwner, ParameterDirection.Input);
                ds = _objDM.GetDataSet(SP_Name);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        _product = new clsProduct();
                        _product.productId = (int)ds.Tables[0].Rows[i]["productID"];
                        _product.productName = ds.Tables[0].Rows[i]["productName"].ToString();
                        _product.productDescription = ds.Tables[0].Rows[i]["productDescription"].ToString();
                        _product.productDescription2 = ds.Tables[0].Rows[i]["productDescription2"].ToString();
                        _product.productPrice = Convert.ToDecimal(ds.Tables[0].Rows[i]["productPrice"].ToString());
                        _product.productQuantity = Convert.ToInt32(ds.Tables[0].Rows[i]["productQuantity"].ToString());
                        _product.productCondition = Convert.ToInt32(ds.Tables[0].Rows[i]["productCondition"].ToString());
                        _product.productCategoryID = Convert.ToInt32(ds.Tables[0].Rows[i]["productCategoryID"].ToString());
                        _product.productCategoryName = ds.Tables[0].Rows[i]["categoryName"].ToString();
                        _product.productsubCategoryID = Convert.ToInt32(ds.Tables[0].Rows[i]["productsubCategoryID"].ToString());
                        _product.productSubCategoryName = ds.Tables[0].Rows[i]["subCatergoryName"].ToString();
                        _product.productSold = Convert.ToBoolean(ds.Tables[0].Rows[i]["productSold"].ToString());
                        _product.productOwner = Convert.ToInt32(ds.Tables[0].Rows[i]["productOwner"].ToString());
                        _product.productOwnerName = ds.Tables[0].Rows[i]["OwnerName"].ToString();
                        _product.IsActive = Convert.ToBoolean(ds.Tables[0].Rows[i]["IsActive"].ToString());
                        _product.createdDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["createdDate"].ToString());
                        _product.createdBy = ds.Tables[0].Rows[i]["createdBy"].ToString();
                        _product.updatedDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["updatedDate"].ToString());
                        _product.updatedBy = ds.Tables[0].Rows[i]["updatedBy"].ToString();

                        lsProduct.Add(_product);
                    }

                }

            }
            catch (Exception)
            {

                throw;
            }
            return lsProduct;
        }

        public List<AdsDetails> getAdsCounts(int subCategoryFlag)
        {
            const string SP_Name = "usp_GetAdsCount";
            DataSet ds = null;
            List<AdsDetails> lsAdsdetails = new List<AdsDetails>();
            AdsDetails _adDetails = null;

            try
            {
                DataAccess _objDM = new DataAccess("IndiaLivings");
                _objDM.InitializeParameterList();
                _objDM.AddParameter("@subCategoryFlag", subCategoryFlag, ParameterDirection.Input);
                ds = _objDM.GetDataSet(SP_Name);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        _adDetails = new AdsDetails();
                        _adDetails.adsCount = Convert.ToInt32(ds.Tables[0].Rows[i]["Posted_Ads"]);
                        _adDetails.adsCategoryID = Convert.ToInt32(ds.Tables[0].Rows[i]["categoryID"].ToString());
                        _adDetails.adsCategoryName = ds.Tables[0].Rows[i]["categoryName"].ToString();
                        if (subCategoryFlag > 0)
                        {
                            _adDetails.adssubCategoryID = Convert.ToInt32(ds.Tables[0].Rows[i]["subCategoryID"].ToString());
                            _adDetails.adsSubCategoryName = ds.Tables[0].Rows[i]["subCatergoryName"].ToString();
                        }

                        lsAdsdetails.Add(_adDetails);
                    }

                }

            }
            catch (Exception)
            {

                throw;
            }
            return lsAdsdetails;
        }
    }
}
