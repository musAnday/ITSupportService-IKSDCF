using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ITSupportService.Models
{
    public class Ticket
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid TicketId { get; set; }

        [Required]
        public string Subject { get; set; }

        public string Description { get; set; }

        public int IssueType { get; set; }

        public int DeliveryMethod { get; set; }

        public DateTime? ReceivedOn { get; set; }
        public DateTime? ApprovedOn { get; set; }
        public DateTime? CompletedOn { get; set; }

        public int EstimatedWorkHour { get; set; }

        public double EstimatedCost { get; set; }

        public bool isPaymentDone { get; set; }

        [Required]
        public Guid AssignedToId { get; set; }

        public Employee AssignedTo { get; set; }

        [Required]
        public Guid CustomerId{ get; set; }

        public Customer Customer { get; set; }



    }
}