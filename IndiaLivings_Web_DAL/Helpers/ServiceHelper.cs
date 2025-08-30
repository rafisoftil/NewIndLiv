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

        public string UpdateServiceCategory(ServiceModel service)
        {
            string response = string.Empty;
            try
            {
                response = ServiceAPI.Put_Api($"{BaseApiUrl}/Service/category/updateServiceCategory", service).Trim('\"');
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
                response = ServiceAPI.Delete_Api($"{BaseApiUrl}/Service/category/deleteServiceCategory/{categoryId}?categoryId={categoryId}&deletedBy={username}").Trim('\"');
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
    }
}
