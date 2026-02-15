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

        public static async Task<string> CreateServiceCategory(ServiceModel service)
        {
            string response = string.Empty;
            try
            {
                response = await ServiceAPI.PostApiAsync($"{BaseApiUrl}/Service/category/createServiceCategory", service);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }

        public static async Task<List<ServicesSubCategoriesModel>> GetServiceSubCategories(int categoryId)
        {
            List<ServicesSubCategoriesModel> subCategories = new List<ServicesSubCategoriesModel>();
            try
            {
                var response = await ServiceAPI.GetAsyncApi($"{BaseApiUrl}/Service/category/getActiveServicesByCategory/{categoryId}");
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

        public static async Task<List<ServiceModel>> GetActiveServiceCategories()
        {
            List<ServiceModel> services = new List<ServiceModel>();
            try
            {
                var response = await ServiceAPI.GetAsyncApi($"{BaseApiUrl}/Service/categories/getActiveServiceCategories");
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

        public static async Task<List<ServiceModel>> GetAllServices()
        {
            List<ServiceModel> services = new List<ServiceModel>();
            try
            {
                var response = await ServiceAPI.GetAsyncApi($"{BaseApiUrl}/Service/category/getAllServiceCategories");
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

        public static async Task<string> UpdateServiceCategory(ServiceCategoryUpdateRequest service)
        {
            string response = string.Empty;
            try
            {
                response = await ServiceAPI.PostApiAsync($"{BaseApiUrl}/Service/category/updateServiceCategory", service);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }

        public static async Task<string> DeleteServiceCategory(int categoryId, string username)
        {
            string response = string.Empty;
            try
            {
                response = await ServiceAPI.PostApiAsync($"{BaseApiUrl}/Service/category/deleteServiceCategory/{categoryId}?categoryId={categoryId}&deletedBy={username}");
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }

        public static async Task<ServiceModel> ViewServiceSubCategory(int categoryId)
        {
            ServiceModel category = new ServiceModel();
            try
            {
                var response = await ServiceAPI.GetAsyncApi($"{BaseApiUrl}/Service/category/getServiceCategoryById/{categoryId}");
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
        public static async Task<string> BookService(ServiceBookingModel booking)
        {
            string bookedService = string.Empty;
            try
            {
                bookedService = await ServiceAPI.PostApiAsync($"{BaseApiUrl}/ServiceBooking/CreateBooking", booking);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return bookedService;
        }
        public static async Task<List<UserBookingResponseModel>> GetAllBookings()
        {
            List<UserBookingResponseModel> myservices = new List<UserBookingResponseModel>();
            try
            {
                var response = await ServiceAPI.GetAsyncApi($"{BaseApiUrl}/ServiceBooking/AllBookings");
                var json = JObject.Parse(response);
                var data = json["data"]?.ToString();
                if (!string.IsNullOrEmpty(data))
                {
                    myservices = JsonConvert.DeserializeObject<List<UserBookingResponseModel>>(data) ?? new List<UserBookingResponseModel>();
                }
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return myservices;
        }
        public static async Task<List<ServiceBookingModel>> GetUserBookings(int userId)
        {
            List<ServiceBookingModel> myservices = new List<ServiceBookingModel>();
            try
            {
                var response = await ServiceAPI.GetAsyncApi($"{BaseApiUrl}/ServiceBooking/bookingsByUser/{userId}");
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
        public static async Task<string> CancelBooking(int bookingId, string reason, string cancelledBy)
        {
            string response = string.Empty;
            try
            {
                response = await ServiceAPI.PostApiAsync($"{BaseApiUrl}/ServiceBooking/cancelBooking/{bookingId}?bookingId={bookingId}&reason={reason}&cancelledBy={cancelledBy}");
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }
        public static async Task<string> CreateServiceProvider(ServiceProviderModel provider)
        {
            string response = string.Empty;
            try
            {
                response = await ServiceAPI.PostApiAsync($"{BaseApiUrl}/ServiceProvider/createServiceProvider", provider);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }
        public static async Task<string> UpdateServiceProvider(ServiceProviderModel provider)
        {
            string response = string.Empty;
            try
            {
                response = await ServiceAPI.PostApiAsync($"{BaseApiUrl}/ServiceProvider/updateServiceProvider", provider);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }
        public static async Task<List<ServiceProviderModel>> ServiceProviders()
        {
            List<ServiceProviderModel> providers = new List<ServiceProviderModel>();
            try
            {
                var response = await ServiceAPI.GetAsyncApi($"{BaseApiUrl}/ServiceProvider/ServiceProviders");
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
        public static async Task<List<ServiceProviderModel>> ActiveServiceProviders()
        {
            List<ServiceProviderModel> providers = new List<ServiceProviderModel>();
            try
            {
                var response = await ServiceAPI.GetAsyncApi($"{BaseApiUrl}/ServiceProvider/activeServiceProviders");
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
        public static async Task<string> CreateServiceSubCategory(ServiceSubCategoryModel subCategory)
        {
            string response = string.Empty;
            try
            {
                response = await ServiceAPI.PostApiAsync($"{BaseApiUrl}/Service/category/createCategoryService", subCategory);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }
        public static async Task<string> UpdateServiceSubCategory(ServiceUpdateRequest subCategory)
        {
            string response = string.Empty;
            try
            {
                response = await ServiceAPI.PostApiAsync($"{BaseApiUrl}/Service/category/updateService", subCategory);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }
        public static async Task<string> DeleteServiceSubCategory(int serviceId, string username)
        {
            string response = string.Empty;
            try
            {
                response = await ServiceAPI.PostApiAsync($"{BaseApiUrl}/Service/category/deleteService/{serviceId}?serviceId={serviceId}&serviceId={username}");
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
          }
        public static async Task<List<ServiceSubCategoryModel>> GetAllServiceSubCategories()
        {
            List<ServiceSubCategoryModel> subCategory = new List<ServiceSubCategoryModel>();
            try
            {
                var response = await ServiceAPI.GetAsyncApi($"{BaseApiUrl}/Service/category/getAllServices");
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
        public static async Task<ServiceSubCategoryModel> GetServiceSubCategoryById(int serviceId)
        {
            ServiceSubCategoryModel subCategory = new ServiceSubCategoryModel();
            try
            {
                var response = await ServiceAPI.GetAsyncApi($"{BaseApiUrl}/Service/category/getServiceById/{serviceId}");
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
        public static async Task<List<ServiceSubCategoryModel>> GetSubServiceByCategory(int categoryId)
        {
            List<ServiceSubCategoryModel> subCategory = new List<ServiceSubCategoryModel>();
            try
            {
                var response = await ServiceAPI.GetAsyncApi($"{BaseApiUrl}/Service/category/getServicesByCategory/{categoryId}");
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
        public static async Task<string> ApproveOrRejectBooking(ApproveRejectRequestModel req)
        {
            string response = string.Empty;
            try
            {
                response = await ServiceAPI.PostApiAsync($"{BaseApiUrl}/ServiceBooking/approveOrReject", req);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }
        public static async Task<string> AssignProvider(AssignProviderRequestModel assignProvider)
        {
            string response = string.Empty;
            try
            {
                response = await ServiceAPI.PostApiAsync($"{BaseApiUrl}/ServiceBooking/assignProvider", assignProvider);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }
        public static async Task<List<AssignedServicesToProviderModel>> GetBookingsAssignedByProvider(string UserId)
        {
            List<AssignedServicesToProviderModel> bookings = new List<AssignedServicesToProviderModel>();
            try
            {
                var response = await ServiceAPI.GetAsyncApi($"{BaseApiUrl}/ServiceBooking/GetAssignedBookings?UserId={UserId}");
                var json = JObject.Parse(response);
                var data = json["data"]?.ToString();
                if (!string.IsNullOrEmpty(data))
                {
                    bookings = JsonConvert.DeserializeObject<List<AssignedServicesToProviderModel>>(data) ?? new List<AssignedServicesToProviderModel>();
                }
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return bookings;
        }
    }
}
