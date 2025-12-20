using IndiaLivings_Web_DAL.Helpers;
using IndiaLivings_Web_DAL.Models;
using System.Text.Json.Serialization;

namespace IndiaLivings_Web_UI.Models
{
    public class CompanySetupViewModel
    {
        public int companyId { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string companyName { get; set; } = string.Empty;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string aboutUs { get; set; } = string.Empty;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string contactUs { get; set; } = string.Empty;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string email { get; set; } = string.Empty;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string phone { get; set; } = string.Empty;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string address { get; set; } = string.Empty;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string city { get; set; } = string.Empty;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string state { get; set; } = string.Empty;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string country { get; set; } = string.Empty;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string postalCode { get; set; } = string.Empty;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string location { get; set; } = string.Empty;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string region { get; set; } = string.Empty;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string currency { get; set; } = string.Empty;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string timeZone { get; set; } = string.Empty;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string website { get; set; } = string.Empty;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string facebookUrl { get; set; } = string.Empty;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string twitterUrl { get; set; } = string.Empty;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string linkedInUrl { get; set; } = string.Empty;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string instagramUrl { get; set; } = string.Empty;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public byte[] companyLogo { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string logoType { get; set; } = string.Empty;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool isActive { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public DateTime createdDate { get; set; } = DateTime.MinValue;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string createdBy { get; set; } = string.Empty;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public DateTime updatedDate { get; set; } = DateTime.MinValue;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string updatedBy { get; set; } = string.Empty;

        public async Task<CompanySetupViewModel> GetCompanySetupById(int companyId)
        {
            CompanySetupViewModel companySetup = new CompanySetupViewModel();
            try
            {
                CompanySetupModel response = await AuthenticationHelper.GetCompanySetupById(companyId);
                companySetup = new CompanySetupViewModel
                {
                    companyId = response.companyId,
                    companyName = response.companyName,
                    aboutUs = response.aboutUs,
                    contactUs = response.contactUs,
                    email = response.email,
                    phone = response.phone,
                    address = response.address,
                    city = response.city,
                    state = response.state,
                    country = response.country,
                    postalCode = response.postalCode,
                    location = response.location,
                    region = response.region,
                    currency = response.currency,
                    timeZone = response.timeZone,
                    website = response.website,
                    facebookUrl = response.facebookUrl,
                    twitterUrl = response.twitterUrl,
                    linkedInUrl = response.linkedInUrl,
                    instagramUrl = response.instagramUrl,
                    companyLogo = response.companyLogo,
                    logoType = response.logoType,
                    isActive = response.isActive,
                    createdDate = response.createdDate,
                    createdBy = response.createdBy,
                    updatedDate = response.updatedDate,
                    updatedBy = response.updatedBy
                };
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return companySetup;
        }
    }
}
