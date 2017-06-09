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
    public class FeedbacksController : ApiController
    {
        private ITSupportContext db = new ITSupportContext();

        // GET: api/Feedbacks
        /// <summary>
        /// Gets All Feedbacks
        /// </summary>
        /// <returns>Collection of Feedback</returns>
        public IQueryable<Feedback> GetFeedbacks()
        {
            return db.Feedbacks;
        }

        // GET: api/Feedbacks/5
        /// <summary>
        /// Get specific feedback by given Id
        /// </summary>
        /// <param name="id">Feedback ıd</param>
        /// <returns>A Feedback</returns>
        [ResponseType(typeof(Feedback))]
        public IHttpActionResult GetFeedback(Guid id)
        {
            Feedback feedback = db.Feedbacks.Find(id);
            if (feedback == null)
            {
                return NotFound();
            }

            return Ok(feedback);
        }


        /// <summary>
        /// Gets Feedbacks of given employee id
        /// </summary>
        /// <param name="employeeId">Employee ID</param>
        /// <returns>List of feedbacks</returns>
        [HttpGet]
        public IQueryable<Feedback> GetEmployeeFeedBacks(Guid employeeId)
        {
            return db.Feedbacks.Where(feedback => feedback.RelatedTicket.AssignedTo.UserId == employeeId);
        }


        /// <summary>
        /// Gets Feedback average rate of the given employee id
        /// </summary>
        /// <param name="employeeId">Employee ID</param>
        /// <returns>Double</returns>
        [HttpGet]
        public double GetEmployeeRate(Guid employeeId)
        {
            return db.Feedbacks.Where(feedback => feedback.RelatedTicket.AssignedTo.UserId == employeeId).Select(feedback => feedback.Rate).Average();
        }

        // PUT: api/Feedbacks/5
        /// <summary>
        /// Updates a feedback
        /// </summary>
        /// <param name="id">FeedbackId</param>
        /// <param name="feedback">Feedback Object</param>
        /// <returns>Status Code</returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFeedback(Guid id, Feedback feedback)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != feedback.FeedbackId)
            {
                return BadRequest();
            }

            db.Entry(feedback).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FeedbackExists(id))
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

        // POST: api/Feedbacks
        /// <summary>
        /// Creates a feedback
        /// </summary>
        /// <param name="feedback">Feedback Object</param>
        /// <returns></returns>
        [ResponseType(typeof(Feedback))]
        public IHttpActionResult PostFeedback(Feedback feedback)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var relatedTicket = db.Tickets.Find(feedback.RelatedTicketId);

            if (relatedTicket == null)
            {
                return NotFound();
            }

            feedback.RelatedTicket = relatedTicket;

            db.Feedbacks.Add(feedback);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = feedback.FeedbackId }, feedback);
        }

        // DELETE: api/Feedbacks/5
        /// <summary>
        /// Deletes a feedback
        /// </summary>
        /// <param name="id">Feedback Id</param>
        /// <returns></returns>
        [ResponseType(typeof(Feedback))]
        public IHttpActionResult DeleteFeedback(Guid id)
        {
            Feedback feedback = db.Feedbacks.Find(id);
            if (feedback == null)
            {
                return NotFound();
            }

            db.Feedbacks.Remove(feedback);
            db.SaveChanges();

            return Ok(feedback);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FeedbackExists(Guid id)
        {
            return db.Feedbacks.Count(e => e.FeedbackId == id) > 0;
        }
    }
}