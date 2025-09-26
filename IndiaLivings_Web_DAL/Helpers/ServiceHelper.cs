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
        private const string BaseApiUrl = "https://api.indialivings.com/api";

        public string CreateServiceCategory(ServiceModel service)
        {
            string response = string.Empty;
            try
            {
                response = ServiceAPI.Post_Api($"{BaseApiUrl}/Service/category/createServiceCategory", service).Trim('\"');
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
                var response = ServiceAPI.Get_async_Api($"{BaseApiUrl}/Service/category/getActiveServicesByCategory/{categoryId}");
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
                var response = ServiceAPI.Get_async_Api($"{BaseApiUrl}/Service/categories/getActiveServiceCategories");
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
                var response = ServiceAPI.Get_async_Api($"{BaseApiUrl}/Service/category/getAllServiceCategories");
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

        public string UpdateServiceCategory(ServiceCategoryUpdateRequest service)
        {
            string response = string.Empty;
            try
            {
                response = ServiceAPI.Post_Api($"{BaseApiUrl}/Service/category/updateServiceCategory", service).Trim('\"');
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
                response = ServiceAPI.Post_Api($"{BaseApiUrl}/Service/category/deleteServiceCategory/{categoryId}?categoryId={categoryId}&deletedBy={username}").Trim('\"');
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
                var response = ServiceAPI.Get_async_Api($"{BaseApiUrl}/Service/category/getServiceCategoryById/{categoryId}").Trim('\"');
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
        public string BookService(ServiceBookingModel booking)
        {
            string bookedService = string.Empty;
            try
            {
                bookedService = ServiceAPI.Post_Api($"{BaseApiUrl}/ServiceBooking/CreateBooking", booking);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return bookedService;
        }
        public List<ServiceBookingModel> GetAllBookings()
        {
            List<ServiceBookingModel> myservices = new List<ServiceBookingModel>();
            try
            {
                var response = ServiceAPI.Get_async_Api($"https://localhost:7158/api/ServiceBooking/AllBookings");
                var json = JObject.Parse(response);
                var data = json["data"]?.ToString();
                if (!string.IsNullOrEmpty(data))
                {
                    myservices = JsonConvert.DeserializeObject<List<ServiceBookingModel>>(data) ?? new List<ServiceBookingModel>();
                }
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return myservices;
        }
        public List<ServiceBookingModel> GetUserBookings(int userId)
        {
            List<ServiceBookingModel> myservices = new List<ServiceBookingModel>();
            try
            {
                var response = ServiceAPI.Get_async_Api($"{BaseApiUrl}/ServiceBooking/bookingsByUser/{userId}");
                var json = JObject.Parse(response);
                var data = json["data"]?.ToString();
                if (!string.IsNullOrEmpty(data))
                {
                    myservices = JsonConvert.DeserializeObject<List<ServiceBookingModel>>(data) ?? new List<ServiceBookingModel>();
                }
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return myservices;
        }
        public string CancelBooking(int bookingId, string reason, string cancelledBy)
        {
            string response = string.Empty;
            try
            {
                response = ServiceAPI.Post_Api($"{BaseApiUrl}/ServiceBooking/cancelBooking/{bookingId}?bookingId={bookingId}&reason={reason}&cancelledBy={cancelledBy}");
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }
        public string CreateServiceProvider(ServiceProviderModel provider)
        {
            string response = string.Empty;
            try
            {
                response = ServiceAPI.Post_Api($"{BaseApiUrl}/ServiceProvider/createServiceProvider", provider).Trim('\"');
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }
        public string UpdateServiceProvider(ServiceProviderModel provider)
        {
            string response = string.Empty;
            try
            {
                response = ServiceAPI.Post_Api($"{BaseApiUrl}/ServiceProvider/updateServiceProvider", provider).Trim('\"');
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }
        public List<ServiceProviderModel> ServiceProviders()
        {
            List<ServiceProviderModel> providers = new List<ServiceProviderModel>();
            try
            {
                var response = ServiceAPI.Get_async_Api($"{BaseApiUrl}/ServiceProvider/ServiceProviders");
                var json = JObject.Parse(response);
                var data = json["data"]?.ToString();
                if (!string.IsNullOrEmpty(data))
                {
                    providers = JsonConvert.DeserializeObject<List<ServiceProviderModel>>(data) ?? new List<ServiceProviderModel>();
                }
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return providers;
        }
        public List<ServiceProviderModel> ActiveServiceProviders()
        {
            List<ServiceProviderModel> providers = new List<ServiceProviderModel>();
            try
            {
                var response = ServiceAPI.Get_async_Api($"{BaseApiUrl}/ServiceProvider/activeServiceProviders");
                var json = JObject.Parse(response);
                var data = json["data"]?.ToString();
                if (!string.IsNullOrEmpty(data))
                {
                    providers = JsonConvert.DeserializeObject<List<ServiceProviderModel>>(data) ?? new List<ServiceProviderModel>();
                }
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return providers;
        }
        public string CreateServiceSubCategory(ServiceSubCategoryModel subCategory)
        {
            string response = string.Empty;
            try
            {
                response = ServiceAPI.Post_Api($"{BaseApiUrl}/Service/category/createCategoryService", subCategory).Trim('\"');
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }
        public string UpdateServiceSubCategory(ServiceUpdateRequest subCategory)
        {
            string response = string.Empty;
            try
            {
                response = ServiceAPI.Post_Api($"{BaseApiUrl}/Service/category/updateService", subCategory).Trim('\"');
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }
        public string DeleteServiceSubCategory(int serviceId, string username)
        {
            string response = string.Empty;
            try
            {
                response = ServiceAPI.Post_Api($"{BaseApiUrl}/Service/category/deleteService/{serviceId}?serviceId={serviceId}&deletedBy={username}").Trim('\"');
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }
        public List<ServiceSubCategoryModel> GetAllServiceSubCategories()
        {
            List<ServiceSubCategoryModel> subCategory = new List<ServiceSubCategoryModel>();
            try
            {
                var response = ServiceAPI.Get_async_Api($"{BaseApiUrl}/Service/category/getAllServices").Trim('\"');
                var json = JObject.Parse(response);
                var data = json["data"]?.ToString();
                if (!string.IsNullOrEmpty(data))
                {
                    subCategory = JsonConvert.DeserializeObject<List<ServiceSubCategoryModel>>(data) ?? new List<ServiceSubCategoryModel>();
                }
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return subCategory;
        }
        public ServiceSubCategoryModel GetServiceSubCategoryById(int serviceId)
        {
            ServiceSubCategoryModel subCategory = new ServiceSubCategoryModel();
            try
            {
                var response = ServiceAPI.Get_async_Api($"{BaseApiUrl}/Service/category/getServiceById/{serviceId}").Trim('\"');
                var json = JObject.Parse(response);
                var data = json["data"]?.ToString();
                if (!string.IsNullOrEmpty(data))
                {
                    subCategory = JsonConvert.DeserializeObject<ServiceSubCategoryModel>(data) ?? new ServiceSubCategoryModel();
                }
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return subCategory;
        }
        public List<ServiceSubCategoryModel> GetSubServiceByCategory(int categoryId)
        {
            List<ServiceSubCategoryModel> subCategory = new List<ServiceSubCategoryModel>();
            try
            {
                var response = ServiceAPI.Get_async_Api($"{BaseApiUrl}/Service/category/getServicesByCategory/{categoryId}").Trim('\"');
                var json = JObject.Parse(response);
                var data = json["data"]?.ToString();
                if (!string.IsNullOrEmpty(data))
                {
                    subCategory = JsonConvert.DeserializeObject<List<ServiceSubCategoryModel>>(data) ?? new List<ServiceSubCategoryModel>();
                }
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return subCategory;
        }
        public string ApproveOrRejectBooking(int bookingId, string status, string remarks, string approvedBy)
        {
            string response = string.Empty;
            try
            {
                response = ServiceAPI.Post_Api($"{BaseApiUrl}/ServiceBooking/approveOrRejectBooking/{bookingId}?bookingId={bookingId}&status={status}&remarks={remarks}&approvedBy={approvedBy}");
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }
        public string AssignProvider(AssignProviderRequestModel assignProvider)
        {
            string response = string.Empty;
            try
            {
                response = ServiceAPI.Post_Api($"{BaseApiUrl}/ServiceBooking/assignProvider", assignProvider);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }
    }
}
