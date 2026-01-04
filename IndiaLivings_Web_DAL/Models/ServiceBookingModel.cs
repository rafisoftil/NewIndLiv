using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
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
    public class UserBookingResponseModel
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
    }
    public class AssignedServicesToProviderModel
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
    }
    public class ApproveRejectRequestModel
    {
        public long BookingId { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ApproveRejectAction Action { get; set; }
        public string? Reason { get; set; }
        public string? ActionByUserId { get; set; }
    }
    public enum ApproveRejectAction
    {
        APPROVED,
        REJECTED
    }
}
