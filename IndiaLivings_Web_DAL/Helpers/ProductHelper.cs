using IndiaLivings_Web_DAL.Models;
using IndiaLivings_Web_DAL.Repositories;
using Newtonsoft.Json;
using static System.Net.WebRequestMethods;

namespace IndiaLivings_Web_DAL.Helpers
{
    public class ProductHelper : IProductRepository
    {
        public List<CategoryModel> GetCategoriesCount()
        {
            List<CategoryModel> categories = new List<CategoryModel>();
            var lst = ServiceAPI.Get_async_Api("https://api.indialivings.com/api/Category/GetCategoryCounts");
            categories = JsonConvert.DeserializeObject<List<CategoryModel>>(lst);
            return categories;
        }
        public List<SubCategoryModel> GetSubCategories(int CategoryId)
        {
            List<SubCategoryModel> subCategories = new List<SubCategoryModel>();
            var lst = ServiceAPI.Get_async_Api("https://api.indialivings.com/api/Category/GetActiveListofSubCategory?intCategoryID=" + CategoryId);
            subCategories = JsonConvert.DeserializeObject<List<SubCategoryModel>>(lst);
            return subCategories;
        }
        public List<AdConitionTypeModel> GetAdConditions()
        {
            List<AdConitionTypeModel> adConitions = new List<AdConitionTypeModel>();
            string url = "https://api.indialivings.com/api/AdConditions/GetAllAdConditionsTypeName";
            var lst = ServiceAPI.Get_async_Api(url);
            adConitions = JsonConvert.DeserializeObject<List<AdConitionTypeModel>>(lst);
            return adConitions;
        }
        public List<ProductModel> GetProductsbyOwner(int userid)
        {
            try

            {   List<ProductModel> products=new List<ProductModel>();

                var productsList = ServiceAPI.Get_async_Api($"https://api.indialivings.com/api/Product/GetAllProductsByOwner?intProductOwner={userid}");
                products = JsonConvert.DeserializeObject<List<ProductModel>>(productsList);
                return products;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<ProductModel> GetAdsList(int status)
        {
            List<ProductModel> products = new List<ProductModel>();
            try
            {
                var productsList = ServiceAPI.Get_async_Api($"https://api.indialivings.com/api/Product/GetAdAdminReview?intStatus={status}");
                products = JsonConvert.DeserializeObject<List<ProductModel>>(productsList);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return products;
        }
        public string UpdateAdAdminReview(int productid, bool status, string username)
        {
            string reviewStatus = string.Empty;
            try
            {
                reviewStatus = ServiceAPI.Post_Api($"https://api.indialivings.com/api/Product/UpdateAdAdminReview?intProductID={productid}&boolProductAdminReview={status}&strUpdatedBy={username}");
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return reviewStatus;
        }

        public List<ProductImageModel> GetProductImage(int productId)
        {
            List<ProductImageModel> productImage = new List<ProductImageModel>();
            try
            {
                var response = ServiceAPI.Get_async_Api($"https://api.indialivings.com/api/Product/GetProductImagesbyPID?intProductID={productId}");
                productImage = JsonConvert.DeserializeObject<List<ProductImageModel>>(response);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return productImage;
        }
        public bool InsertProduct(ProductModel product)
        {
            bool isInsert = false;
            var response = ServiceAPI.Post_Api("https://api.indialivings.com/api/Product/addProduct", product);
            if(!response.Contains("Error"))
                isInsert = true;
            return isInsert;
        }
    
        public List<ProductModel> GetAdsByOwner(int userid)
        {
            List<ProductModel> products = new List<ProductModel>();
            try
            {
                var productsList = ServiceAPI.Get_async_Api($"https://api.indialivings.com/api/Product/GetAllProductsByOwner?intProductOwner={userid}");
                products = JsonConvert.DeserializeObject<List<ProductModel>>(productsList);
                return products;
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return products;
        }
    }
}