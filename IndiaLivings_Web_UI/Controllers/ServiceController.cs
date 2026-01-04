using IndiaLivings_Web_DAL.Models;
using IndiaLivings_Web_UI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Dynamic;
using System.Net;
using System.Reflection;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace IndiaLivings_Web_UI.Controllers
{
    public class ServiceController : Controller
    {
        public async Task<IActionResult> Services()
        {
            ServiceViewModel svm = new ServiceViewModel();
            List<ServiceViewModel> categories = await svm.getAllCategories();
            return View(categories);
        }
        public async Task<IActionResult> AdminServices()
        {
            return View();
        }
        public async Task<IActionResult> ServiceDetails(int categoryId)
        {
            ServiceViewModel svm = new ServiceViewModel();
            ServiceViewModel categories = await svm.ViewServiceCategory(categoryId);
            return View(categories);
        }
        public async Task<IActionResult> ServiceProviderDashboard()
        {
            string UserId = (HttpContext.Session.GetInt32("UserId") ?? 0).ToString();
            List<AssignedServicesToProviderViewModel> bookings = await new AssignedServicesToProviderViewModel().GetAssignedServices(UserId);
            return View(bookings);
        }
        public async Task<IActionResult> ServicesList()
        {
            TempData.Keep("BookingStatus");
            ServiceViewModel svm = new ServiceViewModel();
            List<ServiceViewModel> categories = await svm.getAllCategories();
            return View(categories);
        }
        public async Task<IActionResult> ServicesSubCategory(int categoryId)
        {
            ViewBag.State = HttpContext.Session.GetString("State");
            ViewBag.City = HttpContext.Session.GetString("City");
            ViewBag.Country = HttpContext.Session.GetString("Country");
            ServicesSubCategoriesViewModel sscvm = new ServicesSubCategoriesViewModel();
            List<ServicesSubCategoriesViewModel> subCategories = await sscvm.GetAllActiveSubCategoriesByCategoryId(categoryId);
            return View(subCategories);
        }
        public async Task<IActionResult> MyServices()
        {
            ViewBag.Service = "MyServices";
            int userId = HttpContext.Session.GetInt32("UserId") ?? 0;
            ServiceBookingViewModel booking = new ServiceBookingViewModel();
            List<ServiceBookingViewModel> myservices = await booking.GetBookingsByUser(userId);
            return View(myservices);
        }
        public async Task<IActionResult> UpcomingServices()
        {
            ViewBag.Service = "Upcoming";
            int userId = HttpContext.Session.GetInt32("UserId") ?? 0;
            ServiceBookingViewModel booking = new ServiceBookingViewModel();
            List<ServiceBookingViewModel> myservices = await booking.GetBookingsByUser(userId);
            List<ServiceBookingViewModel> upcomingServices = myservices.Where(s => s.Status == "Scheduled" || s.Status == "InProgress" || s.Status == "PENDING" || s.Status == "ASSIGNED").ToList();
            return View("MyServices", upcomingServices);
        }
        public async Task<IActionResult> CompletedServices()
        {
            ViewBag.Service = "Completed";
            int userId = HttpContext.Session.GetInt32("UserId") ?? 0;
            ServiceBookingViewModel booking = new ServiceBookingViewModel();
            List<ServiceBookingViewModel> myservices = await booking.GetBookingsByUser(userId);
            List<ServiceBookingViewModel> completedServices = myservices.Where(s => s.Status == "COMPLETED").ToList();
            return View("MyServices", completedServices);
        }
        public async Task<IActionResult> CancelledServices()
        {
            ViewBag.Cancelled = "Cancelled";
            int userId = HttpContext.Session.GetInt32("UserId") ?? 0;
            ServiceBookingViewModel booking = new ServiceBookingViewModel();
            List<ServiceBookingViewModel> myservices = await booking.GetBookingsByUser(userId);
            List<ServiceBookingViewModel> cancelledServices = myservices.Where(s => s.Status == "CANCELLED").ToList();
            return View("MyServices", cancelledServices);
        }
        public async Task<IActionResult> AdminBookings()
        {
            List<UserBookingResponseViewModel> allBookings = await new ServiceBookingViewModel().GetAllBookings() ?? new List<UserBookingResponseViewModel>();
            //List<UserBookingResponseViewModel> pendingBookings = allBookings.Where(b => b.Status == "PENDING").ToList();
            List<ServiceProviderViewModel> providerViewModels = await new ServiceProviderViewModel().GetActiveServiceProviders();
            dynamic data = new ExpandoObject();
            data.Bookings = allBookings;
            data.Providers = providerViewModels;
            return View(data);
        }
        public async Task<IActionResult> CompletedBookings()
        {
            List<UserBookingResponseViewModel> allBookings = await new UserBookingResponseViewModel().GetAllBookings();
            List<UserBookingResponseViewModel> completedBookings = allBookings.Where(b => b.Status == "COMPLETED").ToList();
            return View("AdminBookings", completedBookings);
        }
        public async Task<IActionResult> AllBookings()
        {
            List<UserBookingResponseViewModel> allBookings = await new UserBookingResponseViewModel().GetAllBookings();
            return View("AdminBookings", allBookings);
        }
        public async Task<IActionResult> AssignedBookings()
        {
            List<UserBookingResponseViewModel> allBookings = await new UserBookingResponseViewModel().GetAllBookings();
            List<UserBookingResponseViewModel> assignedBookings = allBookings.Where(b => b.Status == "ASSIGNED").ToList();
            return View("AdminBookings", assignedBookings);
        }
        public async Task<IActionResult> AdminProviders()
        {
            List<ServiceProviderViewModel> providers = await new ServiceProviderViewModel().GetAllServiceProviders();
            return View(providers);
        }
        public async Task<JsonResult> AddServiceCategory(string name, string slug, string description, string image)
        {
            var username = HttpContext.Session.GetString("userName") ?? "";
            ServiceViewModel svm = new ServiceViewModel();
            string result = await svm.CreateServiceCategory(name, slug, description, image, username);
            var response = JObject.Parse(result);
            return Json(response);
        }
        public async Task<JsonResult> UpdateServiceCategory(int categoryId, string name, string slug, string description, bool isActive)
        {
            var username = HttpContext.Session.GetString("userName") ?? "";
            ServiceViewModel svm = new ServiceViewModel();
            string result = await svm.UpdateServiceCategory(categoryId, name, slug, description, isActive, username);
            var response = JObject.Parse(result);
            return Json(response);
        }
        public async Task<JsonResult> DeleteServiceCategory(int categoryId)
        {
            var username = HttpContext.Session.GetString("userName") ?? "";
            ServiceViewModel svm = new ServiceViewModel();
            var result = await svm.DeleteServiceCategory(categoryId, username);
            var response = JObject.Parse(result);
            return Json(response);
        }
        public async Task<IActionResult> ServiceBookings()
        {
            return View();
        }
        public async Task<JsonResult> CreateBooking(int serviceId, decimal price, DateTime date, string country, string state, string city, string address, string notes)
        {
            ServiceBookingViewModel sbvm = new ServiceBookingViewModel();
            sbvm.CustomerUserId = Convert.ToString(HttpContext.Session.GetInt32("UserId")) ?? "";
            sbvm.CustomerName = HttpContext.Session.GetString("UserFullName") ?? "";
            sbvm.CustomerEmail = HttpContext.Session.GetString("Email") ?? "";
            sbvm.CustomerPhone = HttpContext.Session.GetString("Mobile") ?? "";
            sbvm.ServiceId = serviceId;
            sbvm.RequestedStartAt = date;
            sbvm.RequestedEndAt = date;
            sbvm.AddressLine1 = address;
            sbvm.AddressLine2 = "";
            sbvm.City = city;
            sbvm.State = state;
            sbvm.PostalCode = "";
            sbvm.Country = country;
            sbvm.Latitude = 0;
            sbvm.Longitude = 0;
            sbvm.PriceQuoted = price;
            sbvm.Notes = notes;

            string result = await ServiceBookingViewModel.BookService(sbvm);
            var response = JObject.Parse(result);
            return Json(response);
        }
        public async Task<JsonResult> CancelBooking(int bookingId, string reason)
        {
            string cancelledBy = HttpContext.Session.GetString("userName") ?? "";
            ServiceBookingViewModel bookingStatus = new ServiceBookingViewModel();
            var response = await bookingStatus.CancelBooking(bookingId, reason, cancelledBy);
            var result = JObject.Parse(response);
            return Json(result);
        }
        public byte[] GetByteInfo(IFormFile productImage)
        {
            byte[] bytes = null;
            using (var br = new MemoryStream())
            {
                productImage.OpenReadStream().CopyTo(br);
                bytes = br.ToArray();
            }
            return bytes;
        }
        public async Task<JsonResult> CreateServiceProvider(IFormFile ProviderImage, IFormCollection ServiceProvider)
        {
            ServiceProviderViewModel spvm = new ServiceProviderViewModel();
            spvm.UserId = Convert.ToString(HttpContext.Session.GetInt32("UserId")) ?? "";
            spvm.ProviderId = ServiceProvider["ProviderId"];
            spvm.DisplayName = ServiceProvider["DisplayName"].ToString();
            byte[] image = [];
            if (ProviderImage != null)
            {
                image = GetByteInfo(ProviderImage);
            }
            spvm.ProviderImage = image;
            //spvm.ContactName = contactName;
            spvm.Email = ServiceProvider["Email"];
            spvm.Phone = ServiceProvider["Phone"];
            spvm.AltPhone = ServiceProvider["altPhone"];
            //spvm.CompanyName = companyName;
            spvm.AddressLine1 = ServiceProvider["AddressLine1"];
            spvm.AddressLine2 = ServiceProvider["AddressLine2"];
            spvm.City = ServiceProvider["City"];
            spvm.State = ServiceProvider["State"];
            spvm.PostalCode = ServiceProvider["PostalCode"];
            spvm.Country = ServiceProvider["Country"];
            spvm.IsVerified = false;
            spvm.IsActive = true;
            string result = "";
            if (Convert.ToInt32(ServiceProvider["ProviderCode"]) > 0)
            {
                result = await spvm.UpdateServiceProvider(spvm);
            }
            else
            {
                result = await spvm.CreateServiceProvider(spvm);
            }
            var response = JObject.Parse(result);
            return Json(response);
        }
        public async Task<JsonResult> CreateServiceSubCategory(int categoryid, string name, decimal basePrice, string description, int durationMin)
        {
            ServiceSubCategoryViewModel sscvm = new ServiceSubCategoryViewModel();
            sscvm.CategoryId = categoryid;
            sscvm.Name = name;
            sscvm.Description = description;
            sscvm.BasePrice = basePrice;
            sscvm.DurationMin = durationMin;
            sscvm.CreatedBy = HttpContext.Session.GetString("userName") ?? "";
            string result = await sscvm.CreateSubCategory(sscvm);
            var response = JObject.Parse(result);
            return Json(response);
        }
        public async Task<JsonResult> UpdateServiceSubCategory(int ServiceId, int CategoryId, string Name, decimal BasePrice, string Description, bool Status, int DurationMin)
        {
            ServiceSubCategoryViewModel sscvm = new ServiceSubCategoryViewModel();
            sscvm.ServiceId = ServiceId;
            sscvm.CategoryId = CategoryId;
            sscvm.Name = Name;
            sscvm.Description = Description;
            sscvm.BasePrice = BasePrice;
            sscvm.DurationMin = DurationMin;
            sscvm.CreatedBy = HttpContext.Session.GetString("userName") ?? "";
            sscvm.UpdatedBy = HttpContext.Session.GetString("userName") ?? "";
            string result = await sscvm.UpdateSubCategory(sscvm);
            var response = JObject.Parse(result);
            return Json(response);
        }
        public async Task<JsonResult> DeleteServiceSubCategory(int serviceId)
        {
            var username = HttpContext.Session.GetString("userName") ?? "";
            ServiceSubCategoryViewModel sscvm = new ServiceSubCategoryViewModel();
            var result = await sscvm.DeleteSubCategory(serviceId, username);
            var response = JObject.Parse(result);
            return Json(response);
        }
        //public async Task<IActionResult> AdminSubServices()
        //{
        //    ServiceSubCategoryViewModel sscvm = new ServiceSubCategoryViewModel();
        //    List<ServiceSubCategoryViewModel> subCategories = sscvm.GetAllSubCategories();
        //    return View(subCategories);
        //}
        public async Task<IActionResult> GetServiceSubCategoryById(int serviceId)
        {
            ServiceSubCategoryViewModel sscvm = new ServiceSubCategoryViewModel();
            ServiceSubCategoryViewModel subCategory = await sscvm.GetSubCategoryById(serviceId);
            return View(subCategory);
        }
        public async Task<IActionResult> AdminSubServices(int categoryId)
        {
            ViewBag.CategoryId = categoryId;
            ServiceSubCategoryViewModel sscvm = new ServiceSubCategoryViewModel();
            List<ServiceSubCategoryViewModel> subCategories = await sscvm.GetSubServicesByCategory(categoryId);
            return View(subCategories);
        }
        public async Task<JsonResult> AssignProvider(int bookingId, int providerId)
        {
            var assignedBy = HttpContext.Session.GetString("userName") ?? "";
            AssignProviderRequestViewModel bookingStatus = new AssignProviderRequestViewModel();
            string notes = "";
            var response = await bookingStatus.AssignProvider(bookingId, providerId, assignedBy, notes);
            var result = JObject.Parse(response);
            return Json(result);
        }
        public void SaveBookingToSession(ServiceBookingViewModel booking)
        {
            booking.CustomerUserId = Convert.ToString(HttpContext.Session.GetInt32("UserId")) ?? "";
            booking.CustomerName = HttpContext.Session.GetString("UserFullName") ?? "";
            booking.CustomerEmail = HttpContext.Session.GetString("Email") ?? "";
            booking.CustomerPhone = HttpContext.Session.GetString("Mobile") ?? "";
            booking.Latitude = 0;
            booking.Longitude = 0;
            booking.AddressLine2 = "";
            HttpContext.Session.SetString("PendingBooking",JsonConvert.SerializeObject(booking));
        }
        public async Task<JsonResult> ApproveOrRejectBooking(int bookingId, string status, string remarks)
        {
            string approvedBy = HttpContext.Session.GetString("userName") ?? "";
            string response = await new ServiceBookingViewModel().ApproveOrRejectBooking(bookingId, status, remarks, approvedBy);
            var result = JObject.Parse(response);
            return Json(result);
        }
    }
}
