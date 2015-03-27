using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;
using Newtonsoft.Json;
using T5PWebAPI.Models;

namespace T5PWebAPI.Controllers
{
    public class PositionController : ApiController
    {
        private T5PWebAPIContext db = new T5PWebAPIContext();
        // GET api/position
        [RequireHttps]
        public IQueryable<positionDTO> Getposition()
        {
            List<positionDTO> theList = new List<positionDTO>();
            PropertyInfo[] properties1 = typeof(positionDTO).GetProperties();
            PropertyInfo[] properties2 = typeof(position).GetProperties();
            
            foreach (var pos in db.positions)
            {
                positionDTO thePosition = new positionDTO();

                foreach (PropertyInfo property1 in properties1)
                {
                    PropertyInfo theProperty = Array.Find(properties2, p => p.Name.CompareTo(property1.Name) == 0);
                    var value = theProperty.GetValue(pos);
                    property1.SetValue(thePosition, value);
                }
                theList.Add(thePosition);
            }            
            return theList.AsQueryable();
        }

        // GET api/position/5
        [RequireHttps]
        [ResponseType(typeof(position))]
        public async Task<IHttpActionResult> Getposition(int id)
        {
            position theposition = await db.positions.FindAsync(id);
            if (theposition == null)
            {
                return NotFound();
            }

            return Ok(theposition);
        }

        // PUT api/position/5
        public async Task<IHttpActionResult> Putposition(int id)
        {

            string jsonContent = Request.Content.ReadAsStringAsync().Result;
            position theposition = JsonConvert.DeserializeObject<position>(jsonContent);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != theposition.positionid)
            {
                return BadRequest();
            }
            db.positions.Attach(theposition);

            DbEntityEntry<position> entry = db.Entry(theposition);

            Type type = theposition.GetType();
            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo prop in properties)
            {
                if (jsonContent.Contains("\"" + prop.Name + "\":"))
                {
                    entry.Property(prop.Name).IsModified = true;
                }
            }
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!positionExists(id))
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

        // POST api/position
        [ResponseType(typeof(position))]
        public async Task<IHttpActionResult> Postposition()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string jsonContent = Request.Content.ReadAsStringAsync().Result;
            position theposition = JsonConvert.DeserializeObject<position>(jsonContent);

            Type type = theposition.GetType();
            PropertyInfo[] properties = type.GetProperties();
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    var sql = "INSERT INTO position (positionid) values( @positionid )";
                    db.Database.ExecuteSqlCommand(sql, new System.Data.SqlClient.SqlParameter("positionid", theposition.positionid)
                        ); 
                    //db.Database.ExecuteSqlCommand("insert into position (positionid) values(" + theposition.positionid +
                    //                              ")");
                    //db.positions.Add(theposition);
                    db.positions.Attach(theposition);
                    DbEntityEntry<position> entry = db.Entry(theposition);
                    foreach (PropertyInfo prop in properties)
                    {
                        if (jsonContent.Contains("\"" + prop.Name + "\":"))
                        {
                            entry.Property(prop.Name).IsModified = true;
                        }
                    }
                    await db.SaveChangesAsync();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                    return StatusCode(HttpStatusCode.Ambiguous);
                }
                return CreatedAtRoute("DefaultApi", new {id = theposition.positionid}, theposition);
            }
        }

        // DELETE api/position/5
        [ResponseType(typeof(position))]
        public async Task<IHttpActionResult> Deleteposition(int id)
        {
            position theposition = await db.positions.FindAsync(id);
            if (theposition == null)
            {
                return NotFound();
            }

            db.positions.Remove(theposition);
            await db.SaveChangesAsync();

            return Ok(theposition);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool positionExists(int id)
        {
            return db.positions.Count(e => e.positionid == id) > 0;
        }
    }
}