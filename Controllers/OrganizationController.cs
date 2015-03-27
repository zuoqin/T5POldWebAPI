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
    public class OrganizationController : ApiController
    {
        private T5PWebAPIContext db = new T5PWebAPIContext();

        // GET api/organization
        public IQueryable<organizationDTO> GetOrganizations()
        {
            var organizations = from b in db.organizations
                         select new organizationDTO()
                         {
                             orglevel = b.orglevel,
                             orgcode = b.orgcode,
                             chinese = b.chinese,
                             english = b.english,
                             big5 = b.big5,
                             parentorg = b.parentorg,
                             japanese = b.japanese,
                             orgid = b.orgid,
                             parentorgid = b.parentorgid
                         };

            return organizations;
        }

        // GET api/organization/5
        [ResponseType(typeof(organization))]
        public async Task<IHttpActionResult> GetOrganization(int id)
        {
            //var theOrganization = await db.organizations.Include(b => b.Author).Select(b =>
            organization theOrganization = await db.organizations.FindAsync(id);
            if (theOrganization == null)
            {
                return NotFound();
            }

            return Ok(theOrganization);
        }


        // PUT api/Book/5
        public async Task<IHttpActionResult> PutOrganization(int id, organization theOrganization)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != theOrganization.orgid )
            {
                return BadRequest();
            }

            db.Entry(theOrganization).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrganizationExists(id))
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

        // POST api/Organization
        [ResponseType(typeof(organization))]
        public async Task<IHttpActionResult> PostOrganization(organization theOrganization)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.organizations.Add(theOrganization);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = theOrganization.orgid }, theOrganization);
        }

        // DELETE api/Province/5
        [ResponseType(typeof(organization))]
        public async Task<IHttpActionResult> DeleteOrganization(int id)
        {
            organization theOrganization = await db.organizations.FindAsync(id);
            if (theOrganization == null)
            {
                return NotFound();
            }

            db.organizations.Remove(theOrganization);
            await db.SaveChangesAsync();

            return Ok(theOrganization);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrganizationExists(int id)
        {
            return db.organizations.Count(e => e.orgid == id) > 0;
        }
    }
}