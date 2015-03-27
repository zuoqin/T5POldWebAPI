using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Routing;
using Newtonsoft.Json;
using T5PWebAPI.Models;

namespace T5PWebAPI.Controllers
{
    public class EmpselfController : ApiController
    {
        private T5PWebAPIContext db = new T5PWebAPIContext();
        // GET api/Empself
        /// <summary>
        /// Retrieves all Empself table records
        /// </summary>
        /// <param name="page">Page number of the data, default is 0</param>
        /// <param name="pageSize">Pagesize of the data, default is int.MaxValue</param>
        public IQueryable<empselfDTO> Getempself(int page = 0, int pageSize = int.MaxValue)
        {
            List<empselfDTO> theList = new List<empselfDTO>();
            PropertyInfo[] properties1 = typeof(empselfDTO).GetProperties();
            PropertyInfo[] properties2 = typeof(empself).GetProperties();

            foreach (var pos in db.empself.OrderBy(c => c.empid).Skip(page * pageSize).Take(pageSize))
            {
                empselfDTO theEmpselfDto = new empselfDTO();

                foreach (PropertyInfo property1 in properties1)
                {
                    PropertyInfo theProperty = Array.Find(properties2, p => p.Name.CompareTo(property1.Name) == 0);
                    var value = theProperty.GetValue(pos);
                    property1.SetValue(theEmpselfDto, value);
                }
                theList.Add(theEmpselfDto);
            }

            var urlHelper = new UrlHelper(Request);
            //var thestr = urlHelper.Link("DefaultApi", new { page = page + 1, pageSize = pageSize, controller = "empself" });
            var totalCount = db.empself.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / (pageSize > 0 ? pageSize : 1));

            var prevLink = page > 0 ? urlHelper.Link("DefaultApi", new { page = page - 1, pageSize = pageSize, controller = "Empself" }) : "";
            var nextLink = page < db.empself.Count() - 1 ? urlHelper.Link("DefaultApi", new { page = page + 1, pageSize = pageSize, controller = "Empself" }) : "";

            var paginationHeader = new
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                PrevPageLink = prevLink,
                NextPageLink = nextLink
            };

            System.Web.HttpContext.Current.Response.Headers.Add("X-Pagination",
            Newtonsoft.Json.JsonConvert.SerializeObject(paginationHeader));

            return theList.AsQueryable();
        }

        // GET api/Empself/5
        [ResponseType(typeof(empself))]
        public async Task<IHttpActionResult> Getempself(int id)
        {
            //empself theEmpself = await db.empself.FindAsync(id);
            //if (theEmpself == null)
            //{
            //    return NotFound();
            //}

            //return Ok(theEmpself);
            var theEmpself = await db.empself
                .Include(b => b.EmpSickCur)
                .Include(b => b.Empanlv)
                .Select(b =>
                    new empselfDTO()
                    {
                        empid = b.empid,
                        passport1 = b.passport1,
                        EmpSickCur = b.EmpSickCur,
                        Empanlv = b.Empanlv
                    })
                .SingleOrDefaultAsync(b => b.empid == id);
            if (theEmpself == null)
            {
                return NotFound();
            }
            return Ok(theEmpself);
        }

        // PUT api/Empself/5
        public async Task<IHttpActionResult> Putempself(int id)
        {
            
            string jsonContent = Request.Content.ReadAsStringAsync().Result;
            empself theEmpself = JsonConvert.DeserializeObject<empself>(jsonContent);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != theEmpself.empid)
            {
                return BadRequest();
            }
            db.empself.Attach(theEmpself);

            DbEntityEntry<empself> entry = db.Entry(theEmpself);

            Type type = theEmpself.GetType();
            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo prop in properties)
            {
                if (jsonContent.Contains( "\"" + prop.Name + "\":"))
                {
                    entry.Property( prop.Name ).IsModified = true;
                }
            }
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!empselfExists(id))
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

        // POST api/Empself
        [ResponseType(typeof(empself))]
        public async Task<IHttpActionResult> Postempself(empself theEmpself)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            //db.empself.Add(theEmpself);
            //await db.SaveChangesAsync();

            //return CreatedAtRoute("DefaultApi", new { id = theEmpself.empid }, theEmpself);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string jsonContent = Request.Content.ReadAsStringAsync().Result;
            empself theempself = JsonConvert.DeserializeObject<empself>(jsonContent);

            Type type = theempself.GetType();
            PropertyInfo[] properties = type.GetProperties();
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    var sql = "INSERT INTO empself (empid) values( @empid )";
                    db.Database.ExecuteSqlCommand(sql, new System.Data.SqlClient.SqlParameter("empid", theempself.empid)
                        ); 
                    //db.Database.ExecuteSqlCommand("insert into empself ( empid ) " +
                    //                              "values( " + theempself.empid + ")");

                    db.empself.Attach(theempself);
                    DbEntityEntry<empself> entry = db.Entry(theempself);
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
                }
                return CreatedAtRoute("DefaultApi", new { theempself }, theempself);
            }
        }

        // DELETE api/Empself/5
        [ResponseType(typeof(empself))]
        public async Task<IHttpActionResult> Deleteempself(int id)
        {
            empself theEmpself = await db.empself.FindAsync(id);
            if (theEmpself == null)
            {
                return NotFound();
            }

            db.empself.Remove(theEmpself);
            await db.SaveChangesAsync();

            return Ok(theEmpself);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool empselfExists(int id)
        {
            return db.empself.Count(e => e.empid == id) > 0;
        }
    }
}