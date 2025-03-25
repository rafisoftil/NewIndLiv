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
            subCategories= JsonConvert.DeserializeObject<List<SubCategoryModel>>(lst);
            return subCategories;
        }
        public List<AdConitionTypeModel> GetAdConditions()
        {
            List<AdConitionTypeModel> adConitions = new List<AdConitionTypeModel>();
            string url = "https://api.indialivings.com/api/AdConditions/GetAllAdConditionsTypeName?strAdConditionTypeName=''";
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
    }
}

