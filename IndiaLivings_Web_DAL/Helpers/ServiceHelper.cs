using IndiaLivings_Web_DAL.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndiaLivings_Web_DAL.Helpers
{
    public class ServiceHelper
    {
        public string CreateServiceCategory(ServiceModel service)
        {
            string response = string.Empty;
            try
            {
                response = ServiceAPI.Post_Api("https://api.indialivings.com/api/Service/category/createServiceCategory", service).Trim('\"');
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }
        public List<ServicesSubCategoriesModel> GetServiceSubCategories(int categoryId)
        {
            List<ServicesSubCategoriesModel> subCategories = new List<ServicesSubCategoriesModel>();
            try
            {
                var response = ServiceAPI.Get_async_Api($"https://api.indialivings.com/api/Service/category/getActiveServicesByCategory/{categoryId}");
                var json = JObject.Parse(response);
                var data = json["data"]?.ToString();
                if (!string.IsNullOrEmpty(data))
                {
                    subCategories = JsonConvert.DeserializeObject<List<ServicesSubCategoriesModel>>(data) ?? new List<ServicesSubCategoriesModel>();
                }
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return subCategories;
        }
        public List<ServiceModel> GetActiveServiceCategories()
        {
            List<ServiceModel> services = new List<ServiceModel>();
            try
            {
                var response = ServiceAPI.Get_async_Api("https://api.indialivings.com/api/Service/categories/getActiveServiceCategories");
                var json = JObject.Parse(response);
                var data = json["data"]?.ToString();
                if (!string.IsNullOrEmpty(data))
                {
                    services = JsonConvert.DeserializeObject<List<ServiceModel>>(data) ?? new List<ServiceModel>();
                }
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return services;
        }
        public List<ServiceModel> GetAllServices()
        {
            List<ServiceModel> services = new List<ServiceModel>();
            try
            {
                var response = ServiceAPI.Get_async_Api("https://api.indialivings.com/api/Service/category/getAllServiceCategories");
                var json = JObject.Parse(response);
                var data = json["data"]?.ToString();
                if (!string.IsNullOrEmpty(data))
                {
                    services = JsonConvert.DeserializeObject<List<ServiceModel>>(data) ?? new List<ServiceModel>();
                }
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return services; 
        }
        public string UpdateServiceCategory(ServiceModel service)
        {
            string response = string.Empty;
            try
            {
                response = ServiceAPI.Put_Api("https://api.indialivings.com/api/Service/category/updateServiceCategory", service).Trim('\"');
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }
        public string DeleteServiceCategory(int categoryId, string username) 
        {
            string response = string.Empty;
            try
            {
                response = ServiceAPI.Delete_Api($"https://api.indialivings.com/api/Service/category/deleteServiceCategory/{categoryId}?categoryId={categoryId}&deletedBy={username}").Trim('\"');
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }
        public ServiceModel ViewServiceSubCategory(int categoryId)
        {
            ServiceModel category = new ServiceModel();
            try
            {
                var response = ServiceAPI.Post_Api($"https://api.indialivings.com/api/Service/category/getServiceCategoryById{categoryId}?categoryId={categoryId}").Trim('\"');
                var json = JObject.Parse(response);
                var data = json["data"]?.ToString();
                if (!string.IsNullOrEmpty(data))
                {
                    category = JsonConvert.DeserializeObject<ServiceModel>(data) ?? new ServiceModel();
                }
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return category;
        }
    }
}
