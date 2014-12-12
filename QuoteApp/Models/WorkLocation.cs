﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuoteApp.Models
{
    public class WorkLocation
    {
        [Key]
        [Required]
        public int WorkLocationId { get; set; }

        [Required]
        [DisplayName("Club name")]
        public string WorkLocationName { get; set; }

        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Town { get; set; }
        public string Country { get; set; }
        public string PostCode { get; set; }
        public string PhoneNumber { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public virtual Company Company { get; set; }

        public virtual ICollection<Contact> Contacts { get; set; }
    }
}