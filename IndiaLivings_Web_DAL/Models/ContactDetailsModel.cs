using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndiaLivings_Web_DAL.Models
{
    public class ContactDetailsModel
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
