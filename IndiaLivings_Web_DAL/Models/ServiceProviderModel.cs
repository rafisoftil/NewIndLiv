using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndiaLivings_Web_DAL.Models
{
    public class ServiceProviderModel
    {
        public string? UserId { get; set; }
        public string? ProviderId { get; set; }
        public string DisplayName { get; set; } = string.Empty;
        public byte[] ProviderImage { get; set; } = null;
        public string? ContactName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? AltPhone { get; set; }
        public string? CompanyName { get; set; }
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public string KYCStatus { get; set; } = "PENDING";
        public bool IsVerified { get; set; } = false;
        public bool IsActive { get; set; } = true;
    }
}
