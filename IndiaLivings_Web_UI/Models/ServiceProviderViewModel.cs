using IndiaLivings_Web_DAL.Models;
using IndiaLivings_Web_DAL.Helpers;

namespace IndiaLivings_Web_UI.Models
{
    public class ServiceProviderViewModel
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

        public string CreateServiceProvider(ServiceProviderViewModel provider)
        {
            string response = string.Empty;
            try
            {
                var serviceProvider = new ServiceProviderModel
                {
                    UserId = this.UserId,
                    ProviderId = this.ProviderId,
                    DisplayName = this.DisplayName,
                    ProviderImage = this.ProviderImage,
                    ContactName = this.ContactName,
                    Email = this.Email,
                    Phone = this.Phone,
                    AltPhone = this.AltPhone,
                    CompanyName = this.CompanyName,
                    AddressLine1 = this.AddressLine1,
                    AddressLine2 = this.AddressLine2,
                    City = this.City,
                    State = this.State,
                    PostalCode = this.PostalCode,
                    Country = this.Country,
                    Latitude = this.Latitude,
                    Longitude = this.Longitude,
                    KYCStatus = this.KYCStatus,
                    IsVerified = this.IsVerified,
                    IsActive = this.IsActive
                };
                response = new ServiceHelper().CreateServiceProvider(serviceProvider);
            }
            catch (Exception ex)
            {
                // Handle exception (log it, rethrow it, etc.)
            }
            return response;
        }
        public string UpdateServiceProvider(ServiceProviderViewModel provider)
        {
            string response = string.Empty;
            try
            {
                var serviceProvider = new ServiceProviderModel
                {
                    UserId = this.UserId,
                    ProviderId = this.ProviderId,
                    DisplayName = this.DisplayName,
                    ContactName = this.ContactName,
                    Email = this.Email,
                    Phone = this.Phone,
                    AltPhone = this.AltPhone,
                    CompanyName = this.CompanyName,
                    AddressLine1 = this.AddressLine1,
                    AddressLine2 = this.AddressLine2,
                    City = this.City,
                    State = this.State,
                    PostalCode = this.PostalCode,
                    Country = this.Country,
                    Latitude = this.Latitude,
                    Longitude = this.Longitude,
                    KYCStatus = this.KYCStatus,
                    IsVerified = this.IsVerified,
                    IsActive = this.IsActive
                };
                response = new ServiceHelper().UpdateServiceProvider(serviceProvider);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }
        public List<ServiceProviderViewModel> GetAllServiceProviders()
        {
            List<ServiceProviderViewModel> providers = new List<ServiceProviderViewModel>();
            try
            {
                var serviceHelper = new ServiceHelper();
                var serviceProviders = serviceHelper.ServiceProviders();
                providers = serviceProviders.Select(sp => new ServiceProviderViewModel
                {
                    UserId = sp.UserId,
                    ProviderId = sp.ProviderId,
                    DisplayName = sp.DisplayName,
                    ProviderImage = sp.ProviderImage,
                    ContactName = sp.ContactName,
                    Email = sp.Email,
                    Phone = sp.Phone,
                    AltPhone = sp.AltPhone,
                    CompanyName = sp.CompanyName,
                    AddressLine1 = sp.AddressLine1,
                    AddressLine2 = sp.AddressLine2,
                    City = sp.City,
                    State = sp.State,
                    PostalCode = sp.PostalCode,
                    Country = sp.Country,
                    Latitude = sp.Latitude,
                    Longitude = sp.Longitude,
                    KYCStatus = sp.KYCStatus,
                    IsVerified = sp.IsVerified,
                    IsActive = sp.IsActive
                }).ToList();
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return providers;
        }
        public List<ServiceProviderViewModel> GetActiveServiceProviders()
        {
            List<ServiceProviderViewModel> providers = new List<ServiceProviderViewModel>();
            try
            {
                var serviceHelper = new ServiceHelper();
                var serviceProviders = serviceHelper.ActiveServiceProviders();
                providers = serviceProviders.Select(sp => new ServiceProviderViewModel
                {
                    UserId = sp.UserId,
                    ProviderId = sp.ProviderId,
                    DisplayName = sp.DisplayName,
                    ProviderImage = sp.ProviderImage,
                    ContactName = sp.ContactName,
                    Email = sp.Email,
                    Phone = sp.Phone,
                    AltPhone = sp.AltPhone,
                    CompanyName = sp.CompanyName,
                    AddressLine1 = sp.AddressLine1,
                    AddressLine2 = sp.AddressLine2,
                    City = sp.City,
                    State = sp.State,
                    PostalCode = sp.PostalCode,
                    Country = sp.Country,
                    Latitude = sp.Latitude,
                    Longitude = sp.Longitude,
                    KYCStatus = sp.KYCStatus,
                    IsVerified = sp.IsVerified,
                    IsActive = sp.IsActive
                }).ToList();
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return providers;
        }
    }
}
