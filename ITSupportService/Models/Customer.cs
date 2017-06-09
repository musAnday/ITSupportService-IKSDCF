using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ITSupportService.Models
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid CustomerId { get; set; }

        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Lastname { get; set; }

        [Required]
        public string EmailAddress { get; set; }

        [Required]
        public string Phonenumber { get; set; }

        public string StreetAddress { get; set; }

        public Customer()
        {
            Tickets = new List<Ticket>();
        }

        public virtual ICollection<Ticket> Tickets { get; set; }

    }
}