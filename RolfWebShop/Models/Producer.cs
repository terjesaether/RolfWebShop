using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RolfWebShop.Models
{
    public class Producer
    {
        public int ProducerId { get; set; }

        [Required, Display(Name = "Navn"), MaxLength(128)]
        public string Name { get; set; }

        [Display(Name = "Adresse")]
        public string Address { get; set; }

        [Display(Name = "Telefon")]
        public string Phone { get; set; }

        [Display(Name = "Epost")]
        [EmailAddress(ErrorMessage = "Feil Email-Adresse")]
        public string Mail { get; set; }

        [Display(Name = "Kontaktperson")]
        public string ContactPerson { get; set; }

        [Display(Name = "Kommentar")]
        public string Comment { get; set; }
        [Display(Name = "Om")]
        public string About { get; set; }
        public virtual List<Product> Products { get; set; }
    }
}