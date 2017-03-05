using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RolfWebShop.Models
{
    public class Customer
    {

        public int CustomerId { get; set; }

        [Required, Display(Name = "Fornavn"), MaxLength(100)]
        public string FirstName { get; set; }

        [Required, Display(Name = "Etternavn"), MaxLength(100)]
        public string LastName { get; set; }

        [Required, Display(Name = "Epost")]
        [EmailAddress]
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}