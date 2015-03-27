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
    public class InFromController : ApiController
    {
        private T5PWebAPIContext db = new T5PWebAPIContext();

        // GET api/ethnic
        public IQueryable<parameter> GetParameters()
        {
            System.Web.Http.Routing.IHttpRouteData data = Request.GetRouteData();
            var query = from b in db.parameters
                        select new
                        {
                            paratype = b.paratype,
                            paracode = b.paracode,
                            english = b.english,
                            chinese = b.chinese,
                            big5 = b.big5,
                            issystem = b.issystem,
                            japanese = b.japanese
                        };
            var parameters = query.ToList().Select(r => new parameter
            {
                paratype = r.paratype,
                paracode = r.paracode,
                english = r.english,
                chinese = r.chinese,
                big5 = r.big5,
                issystem = r.issystem,
                japanese = r.japanese
            }).AsQueryable();

            var filteredItems = parameters.Where(p => p.paratype == "IN_FROM");
            return filteredItems;
        }



        // PUT api/ethnic/5
        public async Task<IHttpActionResult> PutParameter(parameter theParam)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //if (id != book.Id)
            //{
            //    return BadRequest();
            //}

            db.Entry(theParam).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParameterExists(theParam))
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

        // POST api/Ethnic
        [ResponseType(typeof(parameter))]
        public async Task<IHttpActionResult> PostParameter(parameter theParam)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.parameters.Add(theParam);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi",
                new { paracode = theParam.paracode, paratype = theParam.paratype }, theParam);
        }

        // DELETE api/Province/5
        [ResponseType(typeof(parameter))]
        public async Task<IHttpActionResult> DeleteParameter(string paracode)
        {
            var theParam =
                await db.parameters.Where(x => (x.paracode == paracode && x.paratype == "IN_FROM")).ToListAsync();
            if (theParam == null)
            {
                return NotFound();
            }

            db.parameters.Remove(theParam[0]);
            await db.SaveChangesAsync();

            return Ok(theParam);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ParameterExists(parameter id)
        {
            return db.parameters.Count(e => (e.paracode == id.paracode && e.paratype == id.paratype)) > 0;
        }
    }
}