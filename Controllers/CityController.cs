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
    public class CityController : ApiController
    {
        private T5PWebAPIContext db = new T5PWebAPIContext();

        // GET api/cities
        public IQueryable<parameter> GetCities()
        {
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

            var filteredItems = parameters.Where(p => p.paratype == "CITY");
            return filteredItems;
        }

 

        // PUT api/Book/5
        public async Task<IHttpActionResult> PutCity(parameter city)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //if (id != book.Id)
            //{
            //    return BadRequest();
            //}

            db.Entry(city).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CityExists(city))
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

        // POST api/Book
        [ResponseType(typeof(Book))]
        public async Task<IHttpActionResult> PostBook(Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Books.Add(book);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = book.Id }, book);
        }

        // DELETE api/Book/5
        [ResponseType(typeof(Book))]
        public async Task<IHttpActionResult> DeleteBook(int id)
        {
            Book book = await db.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            db.Books.Remove(book);
            await db.SaveChangesAsync();

            return Ok(book);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CityExists(parameter id)
        {
            return db.parameters.Count(e => (e.paracode == id.paracode && e.paratype ==id.paratype ) ) > 0;
        }
    }
}