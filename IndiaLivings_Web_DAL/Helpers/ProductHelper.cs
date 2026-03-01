using IndiaLivings_Web_DAL.Models;
using IndiaLivings_Web_DAL.Repositories;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Data;
using System.Net.Http.Headers;

namespace IndiaLivings_Web_DAL.Helpers
{
    public class ProductHelper : IProductRepository
    {
        public List<CategoryModel> GetCategoriesCount()
        {
            List<CategoryModel> categories = new List<CategoryModel>();
            var lst = ServiceAPI.Get_async_Api("Category/GetCategoryCounts");
            lst = JsonConvert.DeserializeObject<string>(lst);

            categories = JsonConvert.DeserializeObject<List<CategoryModel>>(lst) ?? new List<CategoryModel>();
            return categories;
        }
        public static async Task<List<CategoryModel>> GetCategories()
        {
            List<CategoryModel> categories = new List<CategoryModel>();
            var lst = await ServiceAPI.GetAsyncApi("Category/GetCategoryCounts");
            lst = JsonConvert.DeserializeObject<string>(lst);
            categories = JsonConvert.DeserializeObject<List<CategoryModel>>(lst) ?? new List<CategoryModel>();
            return categories;
        }
        public List<SubCategoryModel> GetSubCategories(int CategoryId)
        {
            List<SubCategoryModel> subCategories = new List<SubCategoryModel>();
            var lst = ServiceAPI.Get_async_Api($"Category/GetActiveListofSubCategory?intCategoryID={CategoryId}");
            subCategories = JsonConvert.DeserializeObject<List<SubCategoryModel>>(lst) ?? new List<SubCategoryModel>();
            return subCategories;
        }
        public List<AdConitionTypeModel> GetAdConditions()
        {
            List<AdConitionTypeModel> adConitions = new List<AdConitionTypeModel>();
            string url = "AdConditions/GetAllAdConditionsTypeName";
            var lst = ServiceAPI.Get_async_Api(url);
            adConitions = JsonConvert.DeserializeObject<List<AdConitionTypeModel>>(lst) ?? new List<AdConitionTypeModel>();
            return adConitions;
        }
        public List<ProductModel> GetWishlistItems(int userid)
        {
            List<ProductModel> products = new List<ProductModel>();
            try
            {
                var productsList = ServiceAPI.Get_async_Api($"Product/GetWishlistProductsByOwner?intProductOwner={userid}");
                products = JsonConvert.DeserializeObject<List<ProductModel>>(productsList) ?? new List<ProductModel>();

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
                var wishlistCount = ServiceAPI.Get_async_Api($"Product/GetWishListCounts?intProductOwner={productOwnerID}");
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
                response = ServiceAPI.Post_Api($"Product/AddWishList?productID={productID}&UserID={userID}&createdBy={Uri.EscapeDataString(createdBy)}&IsActive={status}");
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
                var productsList = ServiceAPI.Get_async_Api($"Product/GetAdAdminReview?intStatus={status}");
                products = JsonConvert.DeserializeObject<List<ProductModel>>(productsList) ?? new List<ProductModel>();
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
                reviewStatus = ServiceAPI.Post_Api($"Product/UpdateAdAdminReview?intProductID={productid}&boolProductAdminReview={status}&strUpdatedBy={Uri.EscapeDataString(username)}").Trim('"');
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return reviewStatus;
        }

        public static async Task<List<ProductImageModel>> GetProductImage(int productId)
        {
            List<ProductImageModel> productImage = new List<ProductImageModel>();
            try
            {
                var response = await ServiceAPI.GetAsyncApi($"Product/GetProductImagesbyPID?intProductID={productId}");
                productImage = JsonConvert.DeserializeObject<List<ProductImageModel>>(response) ?? new List<ProductImageModel>();
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return productImage;
        }

        public static async Task<List<ProductModel>> GetAdsByOwner(int userid)
        {
            List<ProductModel> products = new List<ProductModel>();
            try
            {
                var productsList = await ServiceAPI.GetAsyncApi($"Product/GetAllProductsByOwner?intProductOwner={userid}");
                products = JsonConvert.DeserializeObject<List<ProductModel>>(productsList) ?? new List<ProductModel>();
                return products;
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return products;
        }

        public List<ProductModel> GetAllAdsByUser(int userid)
        {
            List<ProductModel> products = new List<ProductModel>();
            try
            {
                var productsList = ServiceAPI.Get_async_Api($"Product/GetAllAdsByUser?userId={userid}");
                products = JsonConvert.DeserializeObject<List<ProductModel>>(productsList) ?? new List<ProductModel>();
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
                var response = ServiceAPI.Post_Api("Product/addProduct", product);
                response = response.Trim('"');
                insertedId = Convert.ToInt32(response);

            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }

            return insertedId;
        }

        public bool InserProductImage(int productId, int productImageId, string imageName, string imageType, string createdBy, IFormFile productImage)
        {
            var form = new MultipartFormDataContent();
            if (productImage != null && productImage.Length > 0)
            {
                using var ms = new MemoryStream();
                // copy synchronously to avoid unobserved task in sync method
                productImage.OpenReadStream().CopyTo(ms);
                var byteArrayContent = new ByteArrayContent(ms.ToArray());
                byteArrayContent.Headers.ContentType = new MediaTypeHeaderValue(productImage.ContentType);

                form.Add(byteArrayContent, "ProductImg", productImage.FileName);
            }
            var relativeUrl = $"Product/AddProductimages?intProductID={productId}&intProductImageID={productImageId}&strProductImageName={Uri.EscapeDataString(imageName)}&strProductImageType={Uri.EscapeDataString(imageType)}&createdBy={Uri.EscapeDataString(createdBy)}";
            var task = ServiceAPI.PostMultipartApi(relativeUrl, form);
            task.Wait(); // If you're not using async all the way
            var response = task.Result?.Trim('"');
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
                var productsList = ServiceAPI.Get_async_Api($"Product/GetAllProductsByCategory?intCategoryID={productCategoryID}");
                products = JsonConvert.DeserializeObject<List<ProductModel>>(productsList) ?? new List<ProductModel>();
                return products;
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return products;
        }
        public string GetProductList(string strProductName, string strCity, string strState, decimal decMinPrice, decimal decMaxPrice, string strSearchType, string strSearchText)
        {
            string productsList = string.Empty;
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
                string relativeUrl = $"Product/SearchProductByTopPanel?{queryString}";
                productsList = ServiceAPI.Get_async_Api(relativeUrl);

            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return productsList;
        }
        public static async Task<List<SearchFilterDetailsModel>> GetProductFilter()
        {
            var filterDetails = new List<SearchFilterDetailsModel>();
            try
            {
                var response = await ServiceAPI.GetAsyncApi("Product/GetSearchFilterDetails");
                filterDetails = JsonConvert.DeserializeObject<List<SearchFilterDetailsModel>>(response) ?? new List<SearchFilterDetailsModel>();
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return filterDetails;
        }

        public static async Task<List<SearchFilterDetailsModel>> GetProductFilterDetails()
        {
            var filterDetails = new List<SearchFilterDetailsModel>();
            try
            {
                var response = await ServiceAPI.GetAsyncApi("Product/GetSearchFilterDetails");
                response = JsonConvert.DeserializeObject<string>(response);
                filterDetails = JsonConvert.DeserializeObject<List<SearchFilterDetailsModel>>(response) ?? new List<SearchFilterDetailsModel>();
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return filterDetails;
        }

        public string AddRating(int productId, int userId, int rating, string comments, string createdBy)
        {
            var response = "added";
            try
            {
                response = ServiceAPI.Post_Api($"Product/AddProductRating?productId={productId}&userId={userId}&rating={rating}&strComments={Uri.EscapeDataString(comments)}&createdBy={Uri.EscapeDataString(createdBy)}").Trim('"');
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }
        public ProductWithImagesModel GetProductById(int productId)
        {
            ProductWithImagesModel productWithImages = new ProductWithImagesModel();
            try
            {
                var product = ServiceAPI.Get_async_Api($"Product/GetProductsById?intProductId={productId}");
                productWithImages = JsonConvert.DeserializeObject<ProductWithImagesModel>(product) ?? new ProductWithImagesModel();
                return productWithImages;
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return productWithImages;
        }
        public string AddNotification(int productId, int userId, string notificationType, string message)
        {
            string response = string.Empty;
            try
            {
                response = ServiceAPI.Post_Api($"Product/AddProductNotification?productId={productId}&userId={userId}&notificationType={Uri.EscapeDataString(notificationType)}&message={Uri.EscapeDataString(message)}").Trim('"');
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }
        public List<ProductModel> GetTopRatedProducts(int count)
        {
            List<ProductModel> products = new List<ProductModel>();
            try
            {
                var productsList = ServiceAPI.Get_async_Api($"Product/GetTopRatedProducts?topCount={count}");
                products = JsonConvert.DeserializeObject<List<ProductModel>>(productsList) ?? new List<ProductModel>();
                return products;
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return products;
        }
        public List<ProductRatingModel> GetProductRatings(int productId)
        {
            List<ProductRatingModel> ratings = new List<ProductRatingModel>();
            try
            {
                var ratingsList = ServiceAPI.Get_async_Api($"Product/GetProductRatings?productId={productId}");
                ratings = JsonConvert.DeserializeObject<List<ProductRatingModel>>(ratingsList) ?? new List<ProductRatingModel>();
                return ratings;
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return ratings;
        }
        public string MarkProductNotificationAsRead(int notificationId, int userId)
        {
            string response = string.Empty;
            try
            {
                response = ServiceAPI.Post_Api($"Product/MarkProductNotificationAsRead?notificationId={notificationId}&userId={userId}");
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }
        public static async Task<List<ProductModel>> GetRecommendedAds(int topCount, int minRating, bool recommended)
        {
            try
            {
                var response = await ServiceAPI.GetAsyncApi($"Product/GetTopRatedAndRecommendedAds?topCount={topCount}&minRating={minRating}&includeRecommended={recommended}");
                return JsonConvert.DeserializeObject<List<ProductModel>>(response) ?? new List<ProductModel>();
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
                return new List<ProductModel>();
            }
        }
        public static async Task<string> GetAllFilterDetails(int userId, string mode, string productName, string city, string adtypes, int categoryid, int subcategoryid, string rating, decimal? minprice, decimal? maxprice, int page, int pagesize, string sortByPrice)
        {
            string response = string.Empty;
            try
            {
                response = await ServiceAPI.GetAsyncApi($"Product/GetAllFilterDetails?userId={userId}&mode={Uri.EscapeDataString(mode)}&productName={Uri.EscapeDataString(productName)}&city={Uri.EscapeDataString(city)}&adtypes={Uri.EscapeDataString(adtypes)}&categoryid={categoryid}&subcategoryid={subcategoryid}&rating={Uri.EscapeDataString(rating)}&minprice={minprice}&maxprice={maxprice}&page={page}&pagesize={pagesize}&sortByPrice={Uri.EscapeDataString(sortByPrice)}");
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }
        public static async Task<List<FilteredAdModel>> GetFeaturedAds(int userId)
        {
            List<FilteredAdModel> FeaturedAds = new List<FilteredAdModel>();
            try
            {
                var response = await ServiceAPI.GetAsyncApi($"Product/GetFeaturedAds?userId={userId}");
                FeaturedAds = JsonConvert.DeserializeObject<List<FilteredAdModel>>(response) ?? new List<FilteredAdModel>();
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return FeaturedAds;
        }
    }
}