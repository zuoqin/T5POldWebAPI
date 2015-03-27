using System;
using System.Collections.Generic;
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
    public class Empleavedetail_ongoingController : ApiController
    {
        private T5PWebAPIContext db = new T5PWebAPIContext();
        // GET api/empleavedetail_ongoing
        /// <summary>
        /// Retrieves all empleavedetail_ongoing table records
        /// </summary>
        /// <param name="page">Page number of the data, default is 0</param>
        /// <param name="pageSize">Pagesize of the data, default is int.MaxValue</param>
        public IQueryable<empleavedetail_ongoingDTO> Getempleavedetail_ongoing(int page = 0, int pageSize = int.MaxValue)
        {
            List<empleavedetail_ongoingDTO> theList = new List<empleavedetail_ongoingDTO>();
            PropertyInfo[] properties1 = typeof(empleavedetail_ongoingDTO).GetProperties();
            PropertyInfo[] properties2 = typeof(empleavedetail_ongoing).GetProperties();

            foreach (var pos in db.empleavedetail_ongoing.OrderBy(c=>c.empid).ThenBy(c=>c.rowindex).ThenBy(c=>c.forminstanceid).ThenBy(c=>c.leavefromdate).ThenBy(c=>c.leavefromtime).Skip(page*pageSize).Take(pageSize))
            {
                empleavedetail_ongoingDTO theempleavedetail_ongoing = new empleavedetail_ongoingDTO();

                foreach (PropertyInfo property1 in properties1)
                {
                    PropertyInfo theProperty = Array.Find(properties2, p => p.Name.CompareTo(property1.Name) == 0);
                    var value = theProperty.GetValue(pos);
                    property1.SetValue(theempleavedetail_ongoing, value);
                }
                theList.Add(theempleavedetail_ongoing);
            }

            var urlHelper = new UrlHelper(Request);
            //var thestr = urlHelper.Link("DefaultApi", new { page = page + 1, pageSize = pageSize, controller = "Empleavedetail_ongoing" });
            var totalCount = db.empleavedetail_ongoing.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / (pageSize > 0 ? pageSize : 1));

            var prevLink = page > 0 ? urlHelper.Link("DefaultApi", new { page = page - 1, pageSize = pageSize, controller = "Empleavedetail_ongoing" }) : "";
            var nextLink = page < db.empleavedetail_ongoing.Count() - 1 ? urlHelper.Link("DefaultApi", new { page = page + 1, pageSize = pageSize, controller = "Empleavedetail_ongoing" }) : "";

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

        // GET api/empleavedetail_ongoing/5
        /// <summary>
        /// Retrieves specific empleavedetail_ongoing table record
        /// </summary>
        /// <param name="forminstanceid">long forminstanceid</param>
        /// <param name="rowindex">int rowindex</param>
        /// <param name="empid">int empid</param>
        /// <param name="leavecode">string leavecode</param>
        /// <param name="leavefromdate">DateTime leavefromdate</param>
        /// <param name="leavefromtime">DateTime leavefromtime</param>
        [ResponseType(typeof(empleavedetail_ongoing))]
        public async Task<IHttpActionResult> Getempleavedetail_ongoing(long forminstanceid, int rowindex, int empid,
            string leavecode, DateTime leavefromdate, DateTime leavefromtime)
        {
            /*var query = from b in db.empleavedetail_ongoing
                        select new
                        {
                            forminstanceid = b.forminstanceid,
                            rowindex = b.rowindex,
                            empid = b.empid,
                            leavecode = b.leavecode,
                            leavefromdate = b.leavefromdate,
                            leavefromtime = b.leavefromtime
                        };
            var parameters = query.ToList().Select(r => new empleavedetail_ongoing
            {
                forminstanceid = r.forminstanceid,
                rowindex = r.rowindex,
                empid = r.empid,
                leavecode = r.leavecode,
                leavefromdate = r.leavefromdate,
                leavefromtime = r.leavefromtime
            }).AsQueryable();


            var filteredItems = parameters.Where(  p => ( p.forminstanceid == forminstanceid ) & (p.rowindex == rowindex)
                & (p.empid == empid) & (p.leavecode.CompareTo(leavecode) == 0) & (p.leavefromdate == leavefromdate) &
                (p.leavefromtime == leavefromtime));
            return Ok(filteredItems.FirstOrDefault());*/

            List<empleavedetail_ongoing> theList = new List<empleavedetail_ongoing>();
            PropertyInfo[] properties = typeof(empleavedetail_ongoing).GetProperties();
            return Ok(db.empleavedetail_ongoing.OrderBy(e=>e.leavefromdate).ThenBy(e=>e.leavefromtime).Where(
                e => (e.forminstanceid == forminstanceid &
                      e.rowindex == rowindex &
                      e.empid == empid &
                      e.leavecode.CompareTo(leavecode) == 0 &
                      e.leavefromdate >= leavefromdate &
                      e.leavefromtime >= leavefromtime)

                    ).FirstOrDefault());

            
        }

        // PUT api/empleavedetail_ongoing/5
        /// <summary>
        /// Updates specific empleavedetail_ongoing table record, an input should be a valid empleavedetail_ongoing JSON
        /// </summary>
        public async Task<IHttpActionResult> Putempleavedetail_ongoing()
        {

            string jsonContent = Request.Content.ReadAsStringAsync().Result;
            empleavedetail_ongoing theempleavedetail_ongoing = JsonConvert.DeserializeObject<empleavedetail_ongoing>(jsonContent);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.empleavedetail_ongoing.Attach(theempleavedetail_ongoing);

            DbEntityEntry<empleavedetail_ongoing> entry = db.Entry(theempleavedetail_ongoing);

            Type type = theempleavedetail_ongoing.GetType();
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
                if (!empleavedetail_ongoingExists(theempleavedetail_ongoing))
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

        // POST api/empleavedetail_ongoing
        /// <summary>
        /// Insert a new empleavedetail_ongoing table record, an input should be a valid empleavedetail_ongoing JSON
        /// </summary>
        [ResponseType(typeof(empleavedetail_ongoing))]
        public async Task<IHttpActionResult> Postempleavedetail_ongoing()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string jsonContent = Request.Content.ReadAsStringAsync().Result;
            empleavedetail_ongoing theempleavedetail_ongoing = JsonConvert.DeserializeObject<empleavedetail_ongoing>(jsonContent);

            Type type = theempleavedetail_ongoing.GetType();
            PropertyInfo[] properties = type.GetProperties();
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    var sql = "INSERT INTO empleavedetail_ongoing (forminstanceid, rowindex, empid, leavecode, leavefromdate, leavefromtime) VALUES ( @forminstanceid, @rowindex, @empid, @leavecode, @leavefromdate, @leavefromtime )";
                    db.Database.ExecuteSqlCommand(sql, new SqlParameter("forminstanceid", theempleavedetail_ongoing.forminstanceid),
                        new SqlParameter("rowindex", theempleavedetail_ongoing.rowindex), new SqlParameter("empid", theempleavedetail_ongoing.empid),
                        new SqlParameter("leavecode", theempleavedetail_ongoing.leavecode), new SqlParameter("leavefromdate", theempleavedetail_ongoing.leavefromdate),
                        new SqlParameter("leavefromtime", theempleavedetail_ongoing.leavefromtime)
                        );
                    //db.Database.ExecuteSqlCommand("insert into empleavedetail_ongoing (forminstanceid, rowindex, empid, leavecode, leavefromdate, leavefromtime) " +
                    //                              "values( " + theempleavedetail_ongoing.forminstanceid + ", " + theempleavedetail_ongoing.rowindex +
                    //                              ", " + theempleavedetail_ongoing.empid + ", '" + theempleavedetail_ongoing.leavecode +
                    //                              "', '"  + theempleavedetail_ongoing.leavefromdate + "', '" + theempleavedetail_ongoing.leavefromtime +
                    //                              "')");
                    //db.empleavedetail_ongoing.Add(theempleavedetail_ongoing);
                    db.empleavedetail_ongoing.Attach(theempleavedetail_ongoing);
                    DbEntityEntry<empleavedetail_ongoing> entry = db.Entry(theempleavedetail_ongoing);
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
                return CreatedAtRoute("DefaultApi", new { theempleavedetail_ongoing }, theempleavedetail_ongoing);
            }
        }

        // DELETE api/empleavedetail_ongoing/5
        /// <summary>
        /// Delete specific empleavedetail_ongoing table record
        /// </summary>
        /// <param name="forminstanceid">long forminstanceid</param>
        /// <param name="rowindex">int rowindex</param>
        /// <param name="empid">int empid</param>
        /// <param name="leavecode">string leavecode</param>
        /// <param name="leavefromdate">DateTime leavefromdate</param>
        /// <param name="leavefromtime">DateTime leavefromtime</param>
        [ResponseType(typeof(empleavedetail_ongoing))]
        public async Task<IHttpActionResult> Deleteempleavedetail_ongoing(long forminstanceid, int rowindex, int empid,
            string leavecode, DateTime leavefromdate, DateTime leavefromtime)
        {
            List<empleavedetail_ongoing> theEmpleavedatas = await db.empleavedetail_ongoing.Where(
                e => (e.empid == empid &
                      e.forminstanceid == forminstanceid &
                      e.rowindex == rowindex &
                      e.leavecode.CompareTo(leavecode) == 0 &
                      e.leavefromdate == leavefromdate &
                      e.leavefromtime == leavefromtime)
                    ).ToListAsync();
            empleavedetail_ongoing theEmpleavedata = theEmpleavedatas.FirstOrDefault();
            if (theEmpleavedata == null)
            {
                return NotFound();
            }

            db.empleavedetail_ongoing.Remove(theEmpleavedata);
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

        private bool empleavedetail_ongoingExists(empleavedetail_ongoing theempleavedetail_ongoing)
        {
            return db.empleavedetail_ongoing.Count(e => (e.forminstanceid == theempleavedetail_ongoing.forminstanceid &
                e.rowindex == theempleavedetail_ongoing.rowindex &
                e.empid == theempleavedetail_ongoing.empid &
                e.leavecode.CompareTo(theempleavedetail_ongoing.leavecode) == 0 &
                e.leavefromdate == theempleavedetail_ongoing.leavefromdate &
                e.leavefromtime == theempleavedetail_ongoing.leavefromtime
                )    ) > 0;
        }
    }
}