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
    [Serializable]
    public class EmployeesController : ApiController
    {
        private ITSupportContext db = new ITSupportContext();

        // GET: api/Employees
        /// <summary>
        /// Gets all employees
        /// </summary>
        /// <returns></returns>
        public IQueryable<Employee> GetEmployees()
        {
            var Emplyees = db.Employees.ToList();
            return db.Employees;
        }

        // GET: api/Employees/5
        /// <summary>
        /// Gets specific employee for given id
        /// </summary>
        /// <param name="id">Empoyee Id</param>
        /// <returns></returns>
        [ResponseType(typeof(Employee))]
        public IHttpActionResult GetEmployee(Guid id)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        // PUT: api/Employees/5
        /// <summary>
        /// Updates an employee for a given employee id
        /// </summary>
        /// <param name="id">Empoyee Id</param>
        /// <param name="employee">Employee Object</param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEmployee(Guid id, Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employee.UserId)
            {
                return BadRequest();
            }

            db.Entry(employee).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
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

        // POST: api/Employees
        /// <summary>
        /// Creates employee for given object
        /// </summary>
        /// <param name="employee">Employee object</param>
        /// <returns></returns>
        [ResponseType(typeof(Employee))]
        public IHttpActionResult PostEmployee(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Employees.Add(employee);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = employee.UserId }, employee);
        }

        // DELETE: api/Employees/5
        /// <summary>
        /// Deletes an employee with given employee id
        /// </summary>
        /// <param name="id">Employee id</param>
        /// <returns></returns>
        [ResponseType(typeof(Employee))]
        public IHttpActionResult DeleteEmployee(Guid id)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }

            db.Employees.Remove(employee);
            db.SaveChanges();

            return Ok(employee);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployeeExists(Guid id)
        {
            return db.Employees.Count(e => e.UserId == id) > 0;
        }
    }
}