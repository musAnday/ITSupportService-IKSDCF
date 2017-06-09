using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ITSupportService.Migrations;
using ITSupportService.Models;

namespace ITSupportService.Controllers
{
    public class TicketsController : ApiController
    {
        private ITSupportContext db = new ITSupportContext();

        // GET: api/Tickets
        /// <summary>
        /// Gets all Tickets
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IQueryable<Ticket> GetTickets()
        {
            return db.Tickets;
        }

        /// <summary>
        /// Gets tickets which are received on given date.
        /// </summary>
        /// <param name="receivedOn"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("TicketsByReceivedOn")]
        public IQueryable<Ticket> TicketsByReceivedOn( DateTime receivedOn )
        {
            return db.Tickets.Where(x => x.ReceivedOn.Value.Day == receivedOn.Day &&
            x.ReceivedOn.Value.Month == receivedOn.Month &&
            x.ReceivedOn.Value.Year == receivedOn.Year);
        }


        /// <summary>
        /// Gets tickets which are completed but haven't received payment.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("TicketsAwaitingPayment")]
        public IQueryable<Ticket> TicketsAwaitingPayment()
        {
            return db.Tickets.Where(x => x.CompletedOn != null && !x.isPaymentDone);
        }

        // GET: api/Tickets/5
        /// <summary>
        /// Gets Specific Ticket for given id
        /// </summary>
        /// <param name="id">Ticket id</param>
        /// <returns></returns>
        [ResponseType(typeof(Ticket))]
        public IHttpActionResult GetTicket(Guid id)
        {
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return NotFound();
            }

            return Ok(ticket);
        }

        // PUT: api/Tickets/5
        /// <summary>
        /// Updates the ticket for ticket with given id
        /// </summary>
        /// <param name="id">Ticket id</param>
        /// <param name="ticket"> Ticket object </param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTicket(Guid id, Ticket ticket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ticket.TicketId)
            {
                return BadRequest();
            }

            db.Entry(ticket).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketExists(id) || !EmployeeExists(ticket.AssignedToId) || !CustomerExists(ticket.CustomerId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Tickets
        /// <summary>
        /// Creates a ticket
        /// </summary>
        /// <param name="ticket">Ticket object</param>
        /// <returns></returns>
        [ResponseType(typeof(Ticket))]
        public IHttpActionResult PostTicket(Ticket ticket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!EmployeeExists(ticket.AssignedToId) || !CustomerExists(ticket.CustomerId))
            {
                return NotFound();
            }

            db.Tickets.Add(ticket);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = ticket.TicketId }, ticket);
        }

        // DELETE: api/Tickets/5
        /// <summary>
        /// Removes the ticket with given id
        /// </summary>
        /// <param name="id">Ticket id </param>
        /// <returns></returns>
        [ResponseType(typeof(Ticket))]
        public IHttpActionResult DeleteTicket(Guid id)
        {
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return NotFound();
            }

            db.Tickets.Remove(ticket);
            db.SaveChanges();

            return Ok(ticket);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TicketExists(Guid id)
        {
            return db.Tickets.Count(e => e.TicketId == id) > 0;
        }

        private bool EmployeeExists(Guid id)
        {
            return db.Employees.Count(e => e.UserId == id) > 0;
        }

        private bool CustomerExists(Guid id)
        {
            return db.Customers.Count(e => e.CustomerId == id) > 0;
        }

    }
}