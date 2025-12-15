using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndiaLivings_Web_DAL.Models
{
    public class ServiceBookingModel
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
        public string? Notes { get; set; }
        public string? ProviderName { get; set; }
    }
    public class AssignProviderRequestModel
    {
        public long BookingId { get; set; }
        public int ProviderId { get; set; }
        public string? AssignedByUserId { get; set; }
        public string? Notes { get; set; }
    }
}
