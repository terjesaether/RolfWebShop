using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RolfWebShop.Models
{
    public partial class Order
    {
        public int OrderId { get; set; }

        //[Display(Name = "Brukernavn"), Required(ErrorMessage = "Brukernavn er påkrevd!")]
        public string Username { get; set; }

        [Display(Name = "Fornavn"), Required(ErrorMessage = "Fornavn er påkrevd!")]
        public string FirstName { get; set; }
        [Display(Name = "Etternavn"), Required(ErrorMessage = "Etternavn er påkrevd!")]
        public string LastName { get; set; }

        [StringLength(140)]
        [Display(Name = "Adresse"), Required(ErrorMessage = "Adresse er påkrevd!")]
        public string Address { get; set; }

        [StringLength(40)]
        [Display(Name = "By"), Required(ErrorMessage = "By er påkrevd!")]
        public string City { get; set; }
        //public string State { get; set; }

        [Display(Name = "Postnummer"), Required(ErrorMessage = "Postnummer er påkrevd!")]
        public string PostalCode { get; set; }

        [Display(Name = "Land")]
        public string Country { get; set; }

        [Display(Name = "Telefon"), StringLength(12)]
        public string Phone { get; set; }

        [Display(Name = "E-post"), Required(ErrorMessage = "E-post er påkrevd!")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        public decimal Total { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }
}