using IndiaLivings_Web_UI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Dynamic;

namespace IndiaLivings_Web_UI.Controllers
{
    public class ServiceController : Controller
    {
        public IActionResult Services()
        {
            ServiceViewModel svm = new ServiceViewModel();
            List<ServiceViewModel> categories = svm.getAllCategories();
            return View(categories);
        }
        public IActionResult AdminServices()
        {
            return View();
        }
        public IActionResult ServiceDetails(int categoryId)
        {
            ServiceViewModel svm = new ServiceViewModel();
            ServiceViewModel categories = svm.ViewServiceCategory(categoryId);
            return View(categories);
        }
        public IActionResult ServiceInfo()
        {
            return View();
        }
        public IActionResult ServicesList()
        {
            ServiceViewModel svm = new ServiceViewModel();
            List<ServiceViewModel> categories = svm.getAllCategories();
            return View(categories);
        }
        public IActionResult ServicesSubCategory(int categoryId)
        {
            ViewBag.State = HttpContext.Session.GetString("State");
            ViewBag.City = HttpContext.Session.GetString("City");
            ViewBag.Country = HttpContext.Session.GetString("Country");
            ServicesSubCategoriesViewModel sscvm = new ServicesSubCategoriesViewModel();
            List<ServicesSubCategoriesViewModel> subCategories = sscvm.GetAllActiveSubCategoriesByCategoryId(categoryId);
            return View(subCategories);
        }
        public IActionResult MyServices()
        {
            int userId = HttpContext.Session.GetInt32("UserId") ?? 0;
            ServiceBookingViewModel booking = new ServiceBookingViewModel();
            List<ServiceBookingViewModel> myservices = booking.GetBookingsByUser(userId);
            return View(myservices);
        }
        public IActionResult UpcomingServices()
        {
            int userId = HttpContext.Session.GetInt32("UserId") ?? 0;
            ServiceBookingViewModel booking = new ServiceBookingViewModel();
            List<ServiceBookingViewModel> myservices = booking.GetBookingsByUser(userId);
            List<ServiceBookingViewModel> upcomingServices = myservices.Where(s => s.Status == "Scheduled" || s.Status == "InProgress").ToList();
            return View("MyServices", upcomingServices);
        }
        public IActionResult CompletedServices()
        {
            int userId = HttpContext.Session.GetInt32("UserId") ?? 0;
            ServiceBookingViewModel booking = new ServiceBookingViewModel();
            List<ServiceBookingViewModel> myservices = booking.GetBookingsByUser(userId);
            List<ServiceBookingViewModel> completedServices = myservices.Where(s => s.Status == "Completed").ToList();
            return View("MyServices", completedServices);
        }
        public IActionResult CancelledServices()
        {
            int userId = HttpContext.Session.GetInt32("UserId") ?? 0;
            ServiceBookingViewModel booking = new ServiceBookingViewModel();
            List<ServiceBookingViewModel> myservices = booking.GetBookingsByUser(userId);
            List<ServiceBookingViewModel> cancelledServices = myservices.Where(s => s.Status == "Cancelled").ToList();
            return View("MyServices", cancelledServices);
        }
        public IActionResult AdminBookings()
        {
            List<ServiceBookingViewModel> allBookings = new ServiceBookingViewModel().GetAllBookings();
            List<ServiceBookingViewModel> pendingBookings = allBookings.Where(b => b.Status == "PENDING").ToList();
            List<ServiceProviderViewModel> providerViewModels = new ServiceProviderViewModel().GetActiveServiceProviders();
            dynamic data = new ExpandoObject();
            data.Bookings = pendingBookings;
            data.Providers = providerViewModels;
            return View(data);
        }
        public IActionResult CompletedBookings()
        {
            List<ServiceBookingViewModel> allBookings = new ServiceBookingViewModel().GetAllBookings();
            List<ServiceBookingViewModel> completedBookings = allBookings.Where(b => b.Status == "COMPLETED").ToList();
            return View("AdminBookings", completedBookings);
        }
        public IActionResult AllBookings()
        {
            List<ServiceBookingViewModel> allBookings = new ServiceBookingViewModel().GetAllBookings();
            return View("AdminBookings", allBookings);
        }
        public IActionResult AssignedBookings()
        {
            List<ServiceBookingViewModel> allBookings = new ServiceBookingViewModel().GetAllBookings();
            List<ServiceBookingViewModel> assignedBookings = allBookings.Where(b => b.Status == "ASSIGNED").ToList();
            return View("AdminBookings", assignedBookings);
        }
        public IActionResult AdminProviders()
        {
            List<ServiceProviderViewModel> providers = new ServiceProviderViewModel().GetAllServiceProviders();
            return View(providers);
        }
        public JsonResult AddServiceCategory(string name, string slug, string description, string image)
        {
            var username = HttpContext.Session.GetString("userName") ?? "";
            ServiceViewModel svm = new ServiceViewModel();
            string result = svm.CreateServiceCategory(name, slug, description, image, username);
            var response = JObject.Parse(result);
            return Json(response);
        }
        public JsonResult UpdateServiceCategory(int categoryId, string name, string slug, string description, bool isActive)
        {
            var username = HttpContext.Session.GetString("userName") ?? "";
            ServiceViewModel svm = new ServiceViewModel();
            string result = svm.UpdateServiceCategory(categoryId, name, slug, description, isActive, username);
            var response = JObject.Parse(result);
            return Json(response);
        }
        public JsonResult DeleteServiceCategory(int categoryId)
        {
            var username = HttpContext.Session.GetString("userName") ?? "";
            ServiceViewModel svm = new ServiceViewModel();
            var result = svm.DeleteServiceCategory(categoryId, username);
            var response = JObject.Parse(result);
            return Json(response);
        }
        public IActionResult ServiceBookings()
        {
            return View();
        }
        public JsonResult CreateBooking(int serviceId, decimal price, DateTime date, string country, string state, string city, string address, string notes)
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

            string result = sbvm.BookService(sbvm);
            var response = JObject.Parse(result);
            return Json(response);
        }
        public JsonResult CancelBooking(int bookingId, string reason)
        {
            string cancelledBy = HttpContext.Session.GetString("userName") ?? "";
            ServiceBookingViewModel bookingStatus = new ServiceBookingViewModel();
            var response = bookingStatus.CancelBooking(bookingId, reason, cancelledBy);
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
        public JsonResult CreateServiceProvider(IFormFile ProviderImage, IFormCollection ServiceProvider)
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
                result = spvm.UpdateServiceProvider(spvm);
            }
            else
            {
                result = spvm.CreateServiceProvider(spvm);
            }
            var response = JObject.Parse(result);
            return Json(response);
        }
        public JsonResult CreateServiceSubCategory(int categoryid, string name, decimal basePrice, string description, int durationMin)
        {
            ServiceSubCategoryViewModel sscvm = new ServiceSubCategoryViewModel();
            sscvm.CategoryId = categoryid;
            sscvm.Name = name;
            sscvm.Description = description;
            sscvm.BasePrice = basePrice;
            sscvm.DurationMin = durationMin;
            sscvm.CreatedBy = HttpContext.Session.GetString("userName") ?? "";
            string result = sscvm.CreateSubCategory(sscvm);
            var response = JObject.Parse(result);
            return Json(response);
        }
        public JsonResult UpdateServiceSubCategory([FromBody] ServiceSubCategoryViewModel subService)
        {
            ServiceSubCategoryViewModel sscvm = new ServiceSubCategoryViewModel();
            //sscvm.ServiceId = Convert.ToInt32(subService["serviceid"]);
            //sscvm.CategoryId = Convert.ToInt32(subService["categoryid"]);
            //sscvm.Name = subService["name"];
            //sscvm.Description = subService["description"];
            //sscvm.BasePrice = Convert.ToDecimal(subService["basePrice"]);
            //sscvm.DurationMin = Convert.ToInt32(subService["durationMin"]);
            //sscvm.CreatedBy = HttpContext.Session.GetString("userName") ?? "";
            subService.UpdatedBy = HttpContext.Session.GetString("userName") ?? "";
            string result = sscvm.UpdateSubCategory(subService);
            var response = JObject.Parse(result);
            return Json(response);
        }
        public JsonResult DeleteServiceSubCategory(int serviceId)
        {
            var username = HttpContext.Session.GetString("userName") ?? "";
            ServiceSubCategoryViewModel sscvm = new ServiceSubCategoryViewModel();
            var result = sscvm.DeleteSubCategory(serviceId, username);
            var response = JObject.Parse(result);
            return Json(response);
        }
        //public IActionResult AdminSubServices()
        //{
        //    ServiceSubCategoryViewModel sscvm = new ServiceSubCategoryViewModel();
        //    List<ServiceSubCategoryViewModel> subCategories = sscvm.GetAllSubCategories();
        //    return View(subCategories);
        //}
        public IActionResult GetServiceSubCategoryById(int serviceId)
        {
            ServiceSubCategoryViewModel sscvm = new ServiceSubCategoryViewModel();
            ServiceSubCategoryViewModel subCategory = sscvm.GetSubCategoryById(serviceId);
            return View(subCategory);
        }
        public IActionResult AdminSubServices(int categoryId)
        {
            ViewBag.CategoryId = categoryId;
            ServiceSubCategoryViewModel sscvm = new ServiceSubCategoryViewModel();
            List<ServiceSubCategoryViewModel> subCategories = sscvm.GetSubServicesByCategory(categoryId);
            return View(subCategories);
        }
        public JsonResult AssignProvider(int bookingId, int providerId)
        {
            var assignedBy = HttpContext.Session.GetString("userName") ?? "";
            AssignProviderRequestViewModel bookingStatus = new AssignProviderRequestViewModel();
            string notes = "";
            var response = bookingStatus.AssignProvider(bookingId, providerId, assignedBy, notes);
            var result = JObject.Parse(response);
            return Json(result);
        }
    }
}
