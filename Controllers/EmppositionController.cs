using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Newtonsoft.Json;
using T5PWebAPI.Models;

namespace T5PWebAPI.Controllers
{
    public class EmppositionController : ApiController
    {
        private T5PWebAPIContext db = new T5PWebAPIContext();
        // GET api/empposition
        public IQueryable<emppositionDTO> Getempposition()
        {
            List<emppositionDTO> theList = new List<emppositionDTO>();
            PropertyInfo[] properties1 = typeof(emppositionDTO).GetProperties();
            PropertyInfo[] properties2 = typeof(empposition).GetProperties();

            foreach (var pos in db.empposition)
            {
                emppositionDTO theempposition = new emppositionDTO();

                foreach (PropertyInfo property1 in properties1)
                {
                    PropertyInfo theProperty = Array.Find(properties2, p => p.Name.CompareTo(property1.Name) == 0);
                    var value = theProperty.GetValue(pos);
                    property1.SetValue(theempposition, value);
                }
                theList.Add(theempposition);
            }
            return theList.AsQueryable();
        }

        // GET api/empposition/5
        [ResponseType(typeof(empposition))]
        public async Task<IHttpActionResult> Getempposition(int id)
        {
            empposition theempposition = await db.empposition.FindAsync(id);
            if (theempposition == null)
            {
                return NotFound();
            }

            return Ok(theempposition);
        }

        // PUT api/empposition/5
        public async Task<IHttpActionResult> Putempposition(int id)
        {

            string jsonContent = Request.Content.ReadAsStringAsync().Result;
            empposition theempposition = JsonConvert.DeserializeObject<empposition>(jsonContent);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != theempposition.empid)
            {
                return BadRequest();
            }
            db.empposition.Attach(theempposition);

            DbEntityEntry<empposition> entry = db.Entry(theempposition);

            Type type = theempposition.GetType();
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
                if (!EmppositionExists(id))
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

        // POST api/empposition
        [ResponseType(typeof(empposition))]
        public async Task<IHttpActionResult> Postempposition()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string jsonContent = Request.Content.ReadAsStringAsync().Result;
            empposition theempposition = JsonConvert.DeserializeObject<empposition>(jsonContent);

            Type type = theempposition.GetType();
            PropertyInfo[] properties = type.GetProperties();
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    var sql = "INSERT INTO empposition (empid) values( @empid )";
                    db.Database.ExecuteSqlCommand(sql, new SqlParameter( "empid", theempposition.empid )
                        ); 
                    //db.Database.ExecuteSqlCommand("insert into empposition (empid) values(" + theempposition.empid +
                    //                              ")");
                    //db.empposition.Add(theempposition);
                    db.empposition.Attach(theempposition);
                    DbEntityEntry<empposition> entry = db.Entry(theempposition);
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
                    return StatusCode(HttpStatusCode.NotModified);
                }
                return CreatedAtRoute("DefaultApi", new { id = theempposition.empid }, theempposition);
            }
        }

        // DELETE api/empposition/5
        [ResponseType(typeof(empposition))]
        public async Task<IHttpActionResult> Deleteempposition(int id)
        {
            empposition theempposition = await db.empposition.FindAsync(id);
            if (theempposition == null)
            {
                return NotFound();
            }

            db.empposition.Remove(theempposition);
            await db.SaveChangesAsync();

            return Ok(theempposition);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmppositionExists(int id)
        {
            return db.empposition.Count(e => e.empid == id) > 0;
        }
    }
}