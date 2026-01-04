using IndiaLivings_Web_DAL.Helpers;
using IndiaLivings_Web_DAL.Models;
using System.Text.Json.Serialization;
using static IndiaLivings_Web_UI.Models.AssignedServicesToProviderViewModel;

namespace IndiaLivings_Web_UI.Models
{
    public class ServiceBookingViewModel
    {
        public int BookingId { get; set; }
        public string? Status { get; set; }
        public string? StatusName { get; set; }
        public string? CustomerUserId { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerPhone { get; set; }
        public string? CustomerEmail { get; set; }
        public int ServiceId { get; set; }
        public string? ServiceName { get; set; }
        public DateTime RequestedStartAt { get; set; }
        public DateTime RequestedEndAt { get; set; }
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public decimal? PriceQuoted { get; set; }
        public string? Currency { get; set; } = "INR";
        public string? Notes { get; set; } = string.Empty;
        public string? ProviderName { get; set; }
        public static async Task<string> BookService(ServiceBookingViewModel booking)
        {
            string response = "An error occured";
            ServiceHelper SH = new ServiceHelper();
            try
            {
                ServiceBookingModel serviceBooking = new ServiceBookingModel();
                serviceBooking.CustomerUserId = booking.CustomerUserId;
                serviceBooking.CustomerName = booking.CustomerName;
                serviceBooking.CustomerPhone = booking.CustomerPhone;
                serviceBooking.CustomerEmail = booking.CustomerEmail;
                serviceBooking.ServiceId = booking.ServiceId;
                serviceBooking.RequestedStartAt = booking.RequestedStartAt;
                serviceBooking.RequestedEndAt = booking.RequestedEndAt;
                serviceBooking.AddressLine1 = booking.AddressLine1;
                serviceBooking.AddressLine2 = booking.AddressLine2;
                serviceBooking.City = booking.City;
                serviceBooking.State = booking.State;
                serviceBooking.PostalCode = booking.PostalCode;
                serviceBooking.Country = booking.Country;
                serviceBooking.Latitude = booking.Latitude;
                serviceBooking.Longitude = booking.Longitude;
                serviceBooking.PriceQuoted = booking.PriceQuoted;
                serviceBooking.Currency = booking.Currency;
                serviceBooking.Notes = booking.Notes;
                response = await ServiceHelper.BookService(serviceBooking);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }

        public async Task<List<UserBookingResponseViewModel>> GetAllBookings()
        {
            List<UserBookingResponseViewModel> myservices = new List<UserBookingResponseViewModel>();
            List<UserBookingResponseModel> services = await ServiceHelper.GetAllBookings();
            try
            {
                foreach (var ser in services)
                {
                    UserBookingResponseViewModel booking = new UserBookingResponseViewModel();
                    booking.BookingId = ser.BookingId;
                    booking.Status = ser.Status;
                    booking.StatusName = ser.StatusName;
                    booking.ServiceId = ser.ServiceId;
                    booking.ServiceName = ser.ServiceName;
                    booking.RequestedStartAt = ser.RequestedStartAt;
                    booking.RequestedEndAt = ser.RequestedEndAt;
                    booking.PriceQuoted = ser.PriceQuoted;
                    booking.Currency = ser.Currency;
                    booking.City = ser.City;
                    booking.State = ser.State;
                    booking.CreatedAt = ser.CreatedAt;
                    booking.UpdatedAt = ser.UpdatedAt;
                    booking.ProviderName = ser.ProviderName;
                    booking.AssignmentStatus = ser.AssignmentStatus;
                    booking.AssignedAt = ser.AssignedAt;
                    booking.AcceptedAt = ser.AcceptedAt;
                    myservices.Add(booking);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return myservices;
        }
        public async Task<List<ServiceBookingViewModel>> GetBookingsByUser(int userId)
        {
            List<ServiceBookingViewModel> myservices = new List<ServiceBookingViewModel>();
            List<ServiceBookingModel> services = await ServiceHelper.GetUserBookings(userId);
            try
            {
                foreach (var ser in services)
                {
                    ServiceBookingViewModel booking = new ServiceBookingViewModel();
                    booking.BookingId = ser.BookingId;
                    booking.Status = ser.Status;
                    booking.StatusName = ser.StatusName;
                    booking.ServiceId = ser.ServiceId;
                    booking.ServiceName = ser.ServiceName;
                    booking.ProviderName = ser.ProviderName;
                    booking.AddressLine1 = ser.AddressLine1;
                    booking.PriceQuoted = ser.PriceQuoted;
                    booking.Currency = ser.Currency;
                    booking.City = ser.City;
                    booking.State = ser.State;
                    booking.RequestedStartAt = ser.RequestedStartAt;
                    booking.RequestedEndAt = ser.RequestedEndAt;
                    myservices.Add(booking);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return myservices;
        }
        public async Task<string> CancelBooking(int bookingId, string reason, string cancelledBy)
        {
            string response = "An error occured";
            try
            {
                response = await ServiceHelper.CancelBooking(bookingId, reason, cancelledBy);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }
        public async Task<string> ApproveOrRejectBooking(int bookingId, string status, string remarks, string approvedBy)
        {
            ApproveRejectRequestModel req = new ApproveRejectRequestModel
            {
                BookingId = bookingId,
                Action = status == "APPROVED" ? ApproveRejectAction.APPROVED : ApproveRejectAction.REJECTED,
                Reason = remarks,
                ActionByUserId = approvedBy
            };
            string response = "An error occured";
            try
            {
                response = await ServiceHelper.ApproveOrRejectBooking(req);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }
    }
    public class AssignProviderRequestViewModel
    {
        public long BookingId { get; set; }
        public int ProviderId { get; set; }
        public string? AssignedByUserId { get; set; }
        public string? Notes { get; set; }

        public async Task<string> AssignProvider(int bookingId, int providerId, string assignedBy, string notes)
        {
            string response = "An error occured";
            try
            {
                AssignProviderRequestModel assignProviderRequest = new AssignProviderRequestModel()
                {
                    BookingId = bookingId,
                    ProviderId = providerId,
                    AssignedByUserId = assignedBy,
                    Notes = notes
                };
                response = await ServiceHelper.AssignProvider(assignProviderRequest);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }
    }
    public class UserBookingResponseViewModel
    {
        public long BookingId { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? StatusName { get; set; }
        public int ServiceId { get; set; }
        public string? ServiceName { get; set; }
        public DateTime? RequestedStartAt { get; set; }
        public DateTime? RequestedEndAt { get; set; }
        public decimal? PriceQuoted { get; set; }
        public string? Currency { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string? ProviderName { get; set; }
        public string? AssignmentStatus { get; set; }
        public DateTime? AssignedAt { get; set; }
        public DateTime? AcceptedAt { get; set; }

        public async Task<List<UserBookingResponseViewModel>> GetAllBookings()
        {
            List<UserBookingResponseViewModel> myservices = new List<UserBookingResponseViewModel>();
            List<UserBookingResponseModel> services = await ServiceHelper.GetAllBookings();
            try
            {
                foreach (var ser in services)
                {
                    UserBookingResponseViewModel booking = new UserBookingResponseViewModel();
                    booking.BookingId = ser.BookingId;
                    booking.Status = ser.Status;
                    booking.StatusName = ser.StatusName;
                    booking.ServiceId = ser.ServiceId;
                    booking.ServiceName = ser.ServiceName;
                    booking.RequestedStartAt = ser.RequestedStartAt;
                    booking.RequestedEndAt = ser.RequestedEndAt;
                    booking.PriceQuoted = ser.PriceQuoted;
                    booking.Currency = ser.Currency;
                    booking.City = ser.City;
                    booking.State = ser.State;
                    booking.CreatedAt = ser.CreatedAt;
                    booking.UpdatedAt = ser.UpdatedAt;
                    booking.ProviderName = ser.ProviderName;
                    booking.AssignmentStatus = ser.AssignmentStatus;
                    booking.AssignedAt = ser.AssignedAt;
                    booking.AcceptedAt = ser.AcceptedAt; 
                    myservices.Add(booking);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return myservices;
        }
    }
    public class AssignedServicesToProviderViewModel
    {
        public int AssignmentId { get; set; }
        public long BookingId { get; set; }
        public string? ProviderId { get; set; }
        public string? AssignedByUserId { get; set; }
        public DateTime? AssignedAt { get; set; }
        public DateTime? AcceptedAt { get; set; }
        public DateTime? DeclinedAt { get; set; }
        public string? Status { get; set; }
        public string? Notes { get; set; }
        public string? CustomerUserId { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerPhone { get; set; }
        public string? CustomerEmail { get; set; }
        public int ServiceId { get; set; }
        public DateTime? RequestedStartAt { get; set; }
        public DateTime? RequestedEndAt { get; set; }
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? ServiceName { get; set; }
        public string? ServiceDescription { get; set; }
        public decimal? BasePrice { get; set; }
        public int CategoryId { get; set; }
        public string? ServiceCategoryName { get; set; }
        public string? ServiceCategoryDescription { get; set; }

        public async Task<List<AssignedServicesToProviderViewModel>> GetAssignedServices(string UserId)
        {
            List<AssignedServicesToProviderViewModel> myservices = new List<AssignedServicesToProviderViewModel>();
            List<AssignedServicesToProviderModel> services = await ServiceHelper.GetBookingsAssignedByProvider(UserId);
            try
            {
                foreach (var ser in services)
                {
                    AssignedServicesToProviderViewModel booking = new AssignedServicesToProviderViewModel();
                    booking.AssignmentId = ser.AssignmentId;
                    booking.BookingId = ser.BookingId;
                    booking.ProviderId = ser.ProviderId;
                    booking.AssignedByUserId = ser.AssignedByUserId;
                    booking.AssignedAt = ser.AssignedAt;
                    booking.AcceptedAt = ser.AcceptedAt;
                    booking.DeclinedAt = ser.DeclinedAt;
                    booking.Status = ser.Status;
                    booking.Notes = ser.Notes;
                    booking.CustomerUserId = ser.CustomerUserId;
                    booking.CustomerName = ser.CustomerName;
                    booking.CustomerPhone = ser.CustomerPhone;
                    booking.CustomerEmail = ser.CustomerEmail;
                    booking.ServiceId = ser.ServiceId;
                    booking.RequestedStartAt = ser.RequestedStartAt;
                    booking.RequestedEndAt = ser.RequestedEndAt;
                    booking.AddressLine1 = ser.AddressLine1;
                    booking.AddressLine2 = ser.AddressLine2;
                    booking.City = ser.City;
                    booking.State = ser.State;
                    booking.PostalCode = ser.PostalCode;
                    booking.Country = ser.Country;
                    booking.CreatedAt = ser.CreatedAt;
                    booking.UpdatedAt = ser.UpdatedAt;
                    booking.ServiceName = ser.ServiceName;
                    booking.ServiceDescription = ser.ServiceDescription;
                    booking.BasePrice = ser.BasePrice;
                    booking.CategoryId = ser.CategoryId;
                    booking.ServiceCategoryName = ser.ServiceCategoryName;
                    myservices.Add(booking);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return myservices;
        }
        public class ApproveRejectRequestViewModel
        {
            public long BookingId { get; set; }
            [JsonConverter(typeof(JsonStringEnumConverter))]
            public ApproveRejectAction Action { get; set; }
            public string? Reason { get; set; }
            public string? ActionByUserId { get; set; }
        }
    } 
}
