namespace IndiaLivings_Web_UI.Models
{
    public class DashboardViewModel
    {
        public List<ProductViewModel> Products { get; set; }

        public NewsletterViewModel Newsletter { get; set; }

        public MembershipViewModel Membership { get; set; }

        public int ActiveAds { get; set; }

        public int BookingAds { get; set; }

        public int SalesAds { get; set; }

        public int RentalAds { get; set; }
    }
}
