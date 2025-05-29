using IndiaLivings_Web_DAL.Models;
using IndiaLivings_Web_DAL.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.SqlServer.Server;
using Newtonsoft.Json;
using System.Net.Http.Headers;

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
        public List<ProductModel> GetWishlistItems(int userid)
        {
            List<ProductModel> products = new List<ProductModel>();
            try
            {
                var productsList = ServiceAPI.Get_async_Api("https://api.indialivings.com/api/Product/GetWishlistProductsByOwner?intProductOwner=" + userid);
                products = JsonConvert.DeserializeObject<List<ProductModel>>(productsList);

            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return products;
        }
        public int GetCount(int productOwnerID)
        {
            int count = 0;
            try
            {
                var wishlistCount = ServiceAPI.Get_async_Api($"https://api.indialivings.com/api/Product/GetWishListCounts?intProductOwner={productOwnerID}");
                count = JsonConvert.DeserializeObject<int>(wishlistCount);

            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return count;
        }

        public string UpdateWishlist(int productID, int userID, string createdBy, int status)
        {
            string response = String.Empty;
            try
            {
                response = ServiceAPI.Post_Api($"https://api.indialivings.com/api/Product/AddWishList?productID={productID}&UserID={userID}&createdBy={createdBy}&IsActive={status}");
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
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

        public int InsertProduct(ProductModel product)
        {
            int insertedId = 0;
            try
            {
                var response = ServiceAPI.Post_Api("https://api.indialivings.com/api/Product/addProduct", product);
                response = response.Trim('\"');
                insertedId = Convert.ToInt32(response);
               
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }

            return insertedId;
        }

        public bool InserProductImage(int productId,string imageName,string imageType,string createdBy,IFormFile productImage)
        {
            var form = new MultipartFormDataContent();
            if (productImage != null && productImage.Length > 0)
            {
                using var ms = new MemoryStream();
                productImage.CopyToAsync(ms);
                var byteArrayContent = new ByteArrayContent(ms.ToArray());
                byteArrayContent.Headers.ContentType = new MediaTypeHeaderValue(productImage.ContentType);

                form.Add(byteArrayContent, "ProductImg", productImage.FileName);
            }
            var Url = $"https://api.indialivings.com/api/Product/AddProductimages?intProductID={productId}&strProductImageName={imageName}&strProductImageType={imageType}&createdBy={createdBy}";
            var task = ServiceAPI.PostMultipartApi(Url, form);
            task.Wait(); // If you're not using async all the way
            var response = task.Result?.Trim('\"');
            if (response == "1")
            {
                return true;
            }
            return false;
        }
        public List<ProductModel> GetProduct(int productCategoryID)
        {
            List<ProductModel> products = new List<ProductModel>();
            try
            {
                var productsList = ServiceAPI.Get_async_Api($"https://api.indialivings.com/api/Product/GetAllProductsByCategory?intCategoryID={productCategoryID}");
                products = JsonConvert.DeserializeObject<List<ProductModel>>(productsList);
                return products;
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return products;
        }
        public List<ProductModel> GetProductList(string strProductName, string strCity, string strState, decimal decMinPrice, decimal decMaxPrice, string strSearchType, string strSearchText)
        {
            List<ProductModel> products = new List<ProductModel>();
            try
            {
                var queryParams = new List<string>();

                if (!string.IsNullOrEmpty(strProductName))
                    queryParams.Add($"strProductName={Uri.EscapeDataString(strProductName)}");

                if (!string.IsNullOrEmpty(strCity))
                    queryParams.Add($"strCity={Uri.EscapeDataString(strCity)}");

                if (!string.IsNullOrEmpty(strState))
                    queryParams.Add($"strState={Uri.EscapeDataString(strState)}");

                if (decMinPrice > 0)
                    queryParams.Add($"decMinPrice={decMinPrice}");

                if (decMaxPrice > 0)
                    queryParams.Add($"decMaxPrice={decMaxPrice}");

                if (!string.IsNullOrEmpty(strSearchType))
                    queryParams.Add($"strSearchType={Uri.EscapeDataString(strSearchType)}");

                if (!string.IsNullOrEmpty(strSearchText))
                    queryParams.Add($"strSearchText={Uri.EscapeDataString(strSearchText)}");

                string queryString = string.Join("&", queryParams);
                string mainURL = $"https://api.indialivings.com/api/Product/SearchProductByTopPanel?{queryString}";
                var productsList = ServiceAPI.Get_async_Api(mainURL);
                products = JsonConvert.DeserializeObject<List<ProductModel>>(productsList);
               
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return products;
        }



    }
}