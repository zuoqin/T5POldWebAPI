using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
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
    public class EmpleavedataController : ApiController
    {
        private T5PWebAPIContext db = new T5PWebAPIContext();
        // GET api/empleavedata
        /// <summary>
        /// Retrieves all empleavedata table records
        /// </summary>
        /// <param name="page">Page number of the data, default is 0</param>
        /// <param name="pageSize">Pagesize of the data, default is int.MaxValue</param>
        public IQueryable<empleavedataDTO> Getempleavedata(int page = 0, int pageSize = int.MaxValue)
        {
            List<empleavedataDTO> theList = new List<empleavedataDTO>();
            PropertyInfo[] properties1 = typeof(empleavedataDTO).GetProperties();
            PropertyInfo[] properties2 = typeof(empleavedata).GetProperties();

            foreach (var pos in db.empleavedata.OrderBy(c => c.empid).ThenBy(c => c.leavefromdate).ThenBy(c => c.leavefromtime).Skip(page * pageSize).Take(pageSize))
            {
                empleavedataDTO theempleavedata = new empleavedataDTO();

                foreach (PropertyInfo property1 in properties1)
                {
                    PropertyInfo theProperty = Array.Find(properties2, p => p.Name.CompareTo(property1.Name) == 0);
                    var value = theProperty.GetValue(pos);
                    property1.SetValue(theempleavedata, value);
                }
                theList.Add(theempleavedata);
            }

            var urlHelper = new UrlHelper(Request);
            //var thestr = urlHelper.Link("DefaultApi", new { page = page + 1, pageSize = pageSize, controller = "empleavedata" });
            var totalCount = db.empleavedata.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / (pageSize > 0 ? pageSize : 1));

            var prevLink = page > 0 ? urlHelper.Link("DefaultApi", new { page = page - 1, pageSize = pageSize, controller = "empleavedata" }) : "";
            var nextLink = page < db.empleavedata.Count() - 1 ? urlHelper.Link("DefaultApi", new { page = page + 1, pageSize = pageSize, controller = "empleavedata" }) : "";

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

        // GET api/empleavedata/5/LV01/2015-03-01/2015-03-01
        /// <summary>
        /// Retrieves specific empleavedata table record
        /// </summary>
        /// <param name="empid">long empid</param>
        /// <param name="leavecode">string leavecode</param>
        /// <param name="leavefromdate">DateTime leavefromdate</param>
        /// <param name="leavefromtime">DateTime leavefromtime</param>
        [ResponseType(typeof(empleavedata))]
        public async Task<IHttpActionResult> Getempleavedata(long empid,
            string leavecode, DateTime leavefromdate, DateTime leavefromtime
            )
        {
            return Ok(db.empleavedata.Where(
                e => (e.empid == empid &
                      e.leavecode.Contains(leavecode) &
                      e.leavefromdate >= leavefromdate &
                      e.leavefromtime >= leavefromtime)
                    ).FirstOrDefault());
        }

        
        
        
        // GET api/empleavedata/5
        /// <summary>
        /// Retrieves specific empleavedata table record
        /// </summary>
        /// <param name="autoid">long autoid</param>
        /// <returns>Returns empleavedata table record JSON object if found</returns>
        [ResponseType(typeof(empleavedata))]
        public async Task<IHttpActionResult> Getempleavedata(long autoid
            //long empid, string leavecode, DateTime leavefromdate, DateTime leavefromtime
            )
        {
            //List<empleavedata> theList = new List<empleavedata>();
            //PropertyInfo[] properties = typeof(empleavedata).GetProperties();
            //return Ok(db.empleavedata.Where(
            //    e => (e.empid == empid &
            //          e.leavecode.CompareTo(leavecode) == 0 &
            //          e.leavefromdate >= leavefromdate &
            //          e.leavefromtime >= leavefromtime)
            //        ).FirstOrDefault());
            empleavedata theEmpleavedata = await db.empleavedata.FindAsync(autoid);
            if (theEmpleavedata == null)
            {
                return NotFound();
            }
            return Ok(theEmpleavedata);
        }


        // POST api/empleavedata/5/cancel
        /// <summary>
        /// Cancels specific empleavedata table record
        /// </summary>
        /// <param name="autoid">long autoid</param>
        /// <returns>Returns new empleavedata_cancel record JSON object if successfull</returns>
        [ResponseType(typeof(empleavedata_cancel))]
        [Route("api/empleavedata/{autoid:long}/cancel")]
        [HttpPost]
        public async Task<IHttpActionResult> PostCancelLeave(long autoid)
        {
            PropertyInfo[] properties1 = typeof(empleavedata).GetProperties();
            PropertyInfo[] properties2 = typeof(empleavedata_cancel).GetProperties();

            //empleavedata theEmpleavedata = db.empleavedata.Find(autoid);
            empleavedata theEmpleavedata = db.empleavedata.Where(
                      e => (e.autoid == autoid)
                    ).FirstOrDefault();
            if (theEmpleavedata == null)
            {
                return NotFound();
            }

            empleavedata_cancel theEmpleavedataCancel = new empleavedata_cancel();

            foreach (PropertyInfo property2 in properties2)
            {

                PropertyInfo theProperty = Array.Find(properties1, p => p.Name.CompareTo(property2.Name) == 0);
                if (property2.Name.CompareTo("leaveid")==0)
                {
                    theProperty = Array.Find(properties1, p => p.Name.CompareTo("autoid") == 0);
                }
                if (theProperty != null)
                {
                    var value = theProperty.GetValue(theEmpleavedata);
                    property2.SetValue(theEmpleavedataCancel, value);
                }
                    
            }
                

            db.empleavedata_cancel.Add(theEmpleavedataCancel);
            await db.SaveChangesAsync();
            return Ok(theEmpleavedataCancel);
        }

        // PUT api/empleavedata/5
        /// <summary>
        /// Updates specific empleavedata table record, an input should be a valid empleavedata JSON
        /// </summary>
        public async Task<IHttpActionResult> Putempleavedata(int id)
        {

            string jsonContent = Request.Content.ReadAsStringAsync().Result;
            empleavedata theempleavedata = JsonConvert.DeserializeObject<empleavedata>(jsonContent);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != theempleavedata.autoid)
            {
                return BadRequest();
            }
            db.empleavedata.Attach(theempleavedata);

            DbEntityEntry<empleavedata> entry = db.Entry(theempleavedata);

            Type type = theempleavedata.GetType();
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
                if (empleavedataExists(theempleavedata) == 0)
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

        // POST api/empleavedata
        /// <summary>
        /// Insert a new empleavedata table record, an input should be a valid empleavedata JSON
        /// </summary>
        [ResponseType(typeof(empleavedata))]
        public async Task<IHttpActionResult> Postempleavedata()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string jsonContent = Request.Content.ReadAsStringAsync().Result;
            empleavedata theempleavedata = JsonConvert.DeserializeObject<empleavedata>(jsonContent);

            Type type = theempleavedata.GetType();
            PropertyInfo[] properties = type.GetProperties();
            //db.empleavedata.Add(theempleavedata);
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    var sql =
                        @"INSERT INTO empleavedata ( empid, leavecode, leavefromdate, leavefromtime, leavetodate) VALUES( @empid, @leavecode, @leavefromdate, @leavefromtime, @leavetodate)";
                    db.Database.ExecuteSqlCommand( sql, new SqlParameter("empid", theempleavedata.empid),
                        new SqlParameter("leavecode", theempleavedata.leavecode),
                        new SqlParameter("leavefromdate", theempleavedata.leavefromdate),
                        new SqlParameter("leavefromtime", theempleavedata.leavefromtime),
                        new SqlParameter("leavetodate", "1900-01-01")
                       );
                    
                    //db.Database.ExecuteSqlCommand("insert into empleavedata ( empid, leavecode, leavefromdate, leavefromtime, leavetodate) " +
                    //                              "values( "  + theempleavedata.empid + ", '" + theempleavedata.leavecode +
                    //                              "', '" + theempleavedata.leavefromdate + "', '" + theempleavedata.leavefromtime +
                    //                              "', '1900-01-01')");
                    empleavedata theempleavedata2 = db.empleavedata.Where(
                      e => (e.empid == theempleavedata.empid &
                      e.leavecode.CompareTo(theempleavedata.leavecode) == 0 &
                      e.leavefromdate == theempleavedata.leavefromdate &
                      e.leavefromtime == theempleavedata.leavefromtime)
                    ).FirstOrDefault();
                    theempleavedata.autoid = theempleavedata2.autoid;
                    ((IObjectContextAdapter)db).ObjectContext.Detach(theempleavedata2);
                    db.empleavedata.Attach(theempleavedata);
                    DbEntityEntry<empleavedata> entry = db.Entry(theempleavedata);
                    foreach (PropertyInfo prop in properties)
                    {
                        if (jsonContent.Contains("\"" + prop.Name + "\":"))
                        {
                            entry.Property(prop.Name).IsModified = true;
                        }
                        else
                        {
                            entry.Property(prop.Name).IsModified = false;
                        }
                    }
                    await db.SaveChangesAsync();

                    dbContextTransaction.Commit();
                    
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                }
                
            }
            await db.SaveChangesAsync();
            return CreatedAtRoute("DefaultApi", new { theempleavedata.autoid }, theempleavedata);
        }

        // DELETE api/empleavedata/5
        /// <summary>
        /// Delete specific empleavedata table record
        /// </summary>
        /// <param name="empid">int empid</param>
        /// <param name="leavecode">string leavecode</param>
        /// <param name="leavefromdate">DateTime leavefromdate</param>
        /// <param name="leavefromtime">DateTime leavefromtime</param>
        [ResponseType(typeof(empleavedata))]
        public async Task<IHttpActionResult> Deleteempleavedata(int empid,
            string leavecode, DateTime leavefromdate, DateTime leavefromtime)
        {
            List<empleavedata> theEmpleavedatas = await db.empleavedata.Where(
                e => (e.empid == empid &
                      e.leavecode.CompareTo(leavecode) == 0 &
                      e.leavefromdate == leavefromdate &
                      e.leavefromtime == leavefromtime)
                    ).ToListAsync();
            empleavedata theEmpleavedata = theEmpleavedatas.FirstOrDefault();
            if (theEmpleavedata == null)
            {
                return NotFound();
            }

            db.empleavedata.Remove(theEmpleavedata);
            await db.SaveChangesAsync();
            return Ok(theEmpleavedata);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private int empleavedataExists(empleavedata theempleavedata)
        {
            return db.empleavedata.Count(e => (e.empid == theempleavedata.empid &
                e.leavecode.CompareTo(theempleavedata.leavecode) == 0 &
                e.leavefromdate == theempleavedata.leavefromdate &
                e.leavefromtime == theempleavedata.leavefromtime
                ));
        }
    }
}