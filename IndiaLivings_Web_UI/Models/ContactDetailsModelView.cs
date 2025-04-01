namespace IndiaLivings_Web_UI.Models
{
    public class ContactDetailsModelView
    {
        public int ContactDetailsID { get; set; }

        public string ContactDetailsName { get; set; }

        public string ContactDetailsEmail { get; set; }

        public string ContactDetailsSubject { get; set; }

        public string ContactDetailsMessage { get; set; }

        public DateTime? ContactDetailsCreatedDate { get; set; }

        public bool ContactDetailsRead { get; set; }

        public bool ContactDetailDeleteMessage { get; set; }

        public string ContactDetailsAddedBy { get; set; }

        public DateTime? ContactDetailsAddedDate { get; set; }

        public DateTime? ContactDetailsUpdatedDate { get; set; }

        public string ContactDetailsUpdatedBy { get; set; }

    }
}
