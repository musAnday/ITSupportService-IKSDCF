using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ITSupportService.Models
{
    public class Feedback
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid FeedbackId { get; set; }

        [Required]
        public Guid RelatedTicketId { get; set; }
        
        public Ticket RelatedTicket { get; set; }

        public string Description { get; set; }

        public int Rate { get; set; }

    }
}