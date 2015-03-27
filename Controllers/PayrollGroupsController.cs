using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using T5PWebAPI.Models;

namespace T5PWebAPI.Controllers
{
    public class PayrollGroupsController : ApiController
    {
        private T5PWebAPIContext db = new T5PWebAPIContext();

        // GET api/PayrollGroups
        public IQueryable<PayrollGroupsDTO> Getpayrollgroups()
        {
            var payrollgroups = from b in db.payrollgroups
                        select new PayrollGroupsDTO()
                        {
                            payrollgroupid = b.payrollgroupid,
                            english = b.english,
                            chinese = b.chinese
                        };

            return payrollgroups;
            //return db.payrollgroups;
        }

        // GET api/PayrollGroups/5
        [ResponseType(typeof(payrollgroups))]
        public async Task<IHttpActionResult> Getpayrollgroups(int id)
        {
            payrollgroups payrollgroups = await db.payrollgroups.FindAsync(id);
            if (payrollgroups == null)
            {
                return NotFound();
            }

            return Ok(payrollgroups);
        }

        // PUT api/PayrollGroups/5
        public async Task<IHttpActionResult> Putpayrollgroups(int id, payrollgroups payrollgroups)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != payrollgroups.payrollgroupid)
            {
                return BadRequest();
            }

            db.Entry(payrollgroups).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!payrollgroupsExists(id))
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

        // POST api/PayrollGroups
        [ResponseType(typeof(payrollgroups))]
        public async Task<IHttpActionResult> Postpayrollgroups(payrollgroups payrollgroups)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            db.payrollgroups.Add(payrollgroups);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = payrollgroups.payrollgroupid }, payrollgroups);
        }

        // DELETE api/PayrollGroups/5
        [ResponseType(typeof(payrollgroups))]
        public async Task<IHttpActionResult> Deletepayrollgroups(int id)
        {
            payrollgroups payrollgroups = await db.payrollgroups.FindAsync(id);
            if (payrollgroups == null)
            {
                return NotFound();
            }

            db.payrollgroups.Remove(payrollgroups);
            await db.SaveChangesAsync();

            return Ok(payrollgroups);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool payrollgroupsExists(int id)
        {
            return db.payrollgroups.Count(e => e.payrollgroupid == id) > 0;
        }
    }
}