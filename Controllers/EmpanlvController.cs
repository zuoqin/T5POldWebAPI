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
    public class EmpanlvController : ApiController
    {
        private T5PWebAPIContext db = new T5PWebAPIContext();
        // GET api/empanlv
        /// <summary>
        /// Retrieves all empanlv table records
        /// </summary>
        /// <param name="page">Page number of the data, default is 0</param>
        /// <param name="pageSize">Pagesize of the data, default is int.MaxValue</param>
        public IQueryable<empanlvDTO> Getempanlv(int page = 0, int pageSize = int.MaxValue)
        {
            List<empanlvDTO> theList = new List<empanlvDTO>();
            PropertyInfo[] properties1 = typeof(empanlvDTO).GetProperties();
            PropertyInfo[] properties2 = typeof(empanlv).GetProperties();

            foreach (var pos in db.empanlv.OrderBy(c => c.empid).ThenBy(c => c.year_balance).Skip(page * pageSize).Take(pageSize))
            {
                empanlvDTO theempanlv = new empanlvDTO();

                foreach (PropertyInfo property1 in properties1)
                {
                    PropertyInfo theProperty = Array.Find(properties2, p => p.Name.CompareTo(property1.Name) == 0);
                    var value = theProperty.GetValue(pos);
                    property1.SetValue(theempanlv, value);
                }
                theList.Add(theempanlv);
            }

            var urlHelper = new UrlHelper(Request);
            //var thestr = urlHelper.Link("DefaultApi", new { page = page + 1, pageSize = pageSize, controller = "empanlv" });
            var totalCount = db.empanlv.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / (pageSize > 0 ? pageSize : 1));

            var prevLink = page > 0 ? urlHelper.Link("DefaultApi", new { page = page - 1, pageSize = pageSize, controller = "empanlv" }) : "";
            var nextLink = page < db.empanlv.Count() - 1 ? urlHelper.Link("DefaultApi", new { page = page + 1, pageSize = pageSize, controller = "empanlv" }) : "";

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

        // GET api/empanlv/5
        /// <summary>
        /// Retrieves specific empanlv table record
        /// </summary>
        /// <param name="forminstanceid">long forminstanceid</param>
        /// <param name="rowindex">int rowindex</param>
        /// <param name="empid">int empid</param>
        /// <param name="leavecode">string leavecode</param>
        /// <param name="leavefromdate">DateTime leavefromdate</param>
        /// <param name="leavefromtime">DateTime leavefromtime</param>
        [ResponseType(typeof(empanlv))]
        public async Task<IHttpActionResult> Getempanlv(int empid)
        {
            /*var query = from b in db.empanlv
                        select new
                        {
                            forminstanceid = b.forminstanceid,
                            rowindex = b.rowindex,
                            empid = b.empid,
                            leavecode = b.leavecode,
                            leavefromdate = b.leavefromdate,
                            leavefromtime = b.leavefromtime
                        };
            var parameters = query.ToList().Select(r => new empanlv
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

            return Ok(db.empanlv.OrderBy(e => e.year_balance).Where(
            e => (e.empid == empid )    ).FirstOrDefault());


        }

       
    }
}