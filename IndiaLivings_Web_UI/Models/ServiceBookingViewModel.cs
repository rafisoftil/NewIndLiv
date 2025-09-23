using IndiaLivings_Web_DAL.Helpers;
using IndiaLivings_Web_DAL.Models;

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
        public DateTime? RequestedStartAt { get; set; }
        public DateTime? RequestedEndAt { get; set; }
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
        public string BookService(ServiceBookingViewModel booking)
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
                response =  SH.BookService(serviceBooking);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }
        public List<ServiceBookingViewModel> GetAllBookings()
        {
            List<ServiceBookingViewModel> myservices = new List<ServiceBookingViewModel>();
            ServiceHelper SH = new ServiceHelper();
            List<ServiceBookingModel> services = SH.GetAllBookings();
            try
            {
                foreach (var ser in services)
                {
                    ServiceBookingViewModel booking = new ServiceBookingViewModel();
                    booking.BookingId = ser.BookingId;
                    booking.StatusName = ser.StatusName;
                    booking.CustomerName = ser.CustomerName; 
                    booking.ServiceName = ser.ServiceName;
                    booking.ProviderName = ser.ProviderName;
                    booking.AddressLine1 = ser.AddressLine1;
                    booking.AddressLine2 = ser.AddressLine2;
                    booking.City = ser.City;
                    booking.State = ser.State;
                    booking.PostalCode = ser.PostalCode;
                    booking.Country = ser.Country;
                    booking.RequestedStartAt = ser.RequestedStartAt;
                    booking.PriceQuoted = ser.PriceQuoted;
                    booking.Status = ser.Status;
                    myservices.Add(booking);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return myservices;
        }
        public List<ServiceBookingViewModel> GetBookingsByUser(int userId)
        {
            List<ServiceBookingViewModel> myservices = new List<ServiceBookingViewModel>();
            ServiceHelper SH = new ServiceHelper();
            List<ServiceBookingModel> services = SH.GetUserBookings(userId);
            try
            {
                foreach (var ser in services)
                {
                    ServiceBookingViewModel booking = new ServiceBookingViewModel();
                    booking.BookingId = ser.BookingId;
                    booking.ServiceName = ser.ServiceName;
                    booking.ProviderName = ser.ProviderName;
                    booking.AddressLine1 = ser.AddressLine1;
                    booking.RequestedStartAt = ser.RequestedStartAt;
                    myservices.Add(booking);
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
            string response = "An error occured";
            try
            {
                ServiceHelper SH = new ServiceHelper();
                response = SH.CancelBooking(bookingId, reason, cancelledBy);
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

        public string AssignProvider(int bookingId, int providerId, string assignedBy, string notes)
        {
            string response = "An error occured";
            try
            {
                ServiceHelper SH = new ServiceHelper();
                AssignProviderRequestModel assignProviderRequest = new AssignProviderRequestModel()
                {
                    BookingId = bookingId,
                    ProviderId = providerId,
                    AssignedByUserId = assignedBy,
                    Notes = notes
                };
                response = SH.AssignProvider(assignProviderRequest);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }
    }
}
