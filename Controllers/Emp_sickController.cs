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
    public class emp_sickController : ApiController
    {
        private T5PWebAPIContext db = new T5PWebAPIContext();
        // GET api/emp_sick
        /// <summary>
        /// Retrieves all emp_sick table records
        /// </summary>
        /// <param name="page">Page number of the data, default is 0</param>
        /// <param name="pageSize">Pagesize of the data, default is int.MaxValue</param>
        public IQueryable<emp_sick_curDTO> Getemp_sick(int page = 0, int pageSize = int.MaxValue)
        {
            List<emp_sick_curDTO> theList = new List<emp_sick_curDTO>();
            PropertyInfo[] properties1 = typeof(emp_sick_curDTO).GetProperties();
            PropertyInfo[] properties2 = typeof(emp_sick_cur).GetProperties();

            foreach (var pos in db.emp_sick.OrderBy(c => c.empid).ThenBy(c => c.begindate).Skip(page * pageSize).Take(pageSize))
            {
                emp_sick_curDTO theemp_sick = new emp_sick_curDTO();

                foreach (PropertyInfo property1 in properties1)
                {
                    PropertyInfo theProperty = Array.Find(properties2, p => p.Name.CompareTo(property1.Name) == 0);
                    var value = theProperty.GetValue(pos);
                    property1.SetValue(theemp_sick, value);
                }
                theList.Add(theemp_sick);
            }

            var urlHelper = new UrlHelper(Request);
            //var thestr = urlHelper.Link("DefaultApi", new { page = page + 1, pageSize = pageSize, controller = "emp_sick" });
            var totalCount = db.emp_sick.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / (pageSize > 0 ? pageSize : 1));

            var prevLink = page > 0 ? urlHelper.Link("DefaultApi", new { page = page - 1, pageSize = pageSize, controller = "emp_sick" }) : "";
            var nextLink = page < db.emp_sick.Count() - 1 ? urlHelper.Link("DefaultApi", new { page = page + 1, pageSize = pageSize, controller = "emp_sick" }) : "";

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

        // GET api/emp_sick/5
        /// <summary>
        /// Retrieves specific emp_sick table record
        /// </summary>
        /// <param name="forminstanceid">long forminstanceid</param>
        /// <param name="rowindex">int rowindex</param>
        /// <param name="empid">int empid</param>
        /// <param name="leavecode">string leavecode</param>
        /// <param name="leavefromdate">DateTime leavefromdate</param>
        /// <param name="leavefromtime">DateTime leavefromtime</param>
        [ResponseType(typeof(emp_sick_cur))]
        public async Task<IHttpActionResult> Getemp_sick(int empid)
        {
            /*var query = from b in db.emp_sick
                        select new
                        {
                            forminstanceid = b.forminstanceid,
                            rowindex = b.rowindex,
                            empid = b.empid,
                            leavecode = b.leavecode,
                            leavefromdate = b.leavefromdate,
                            leavefromtime = b.leavefromtime
                        };
            var parameters = query.ToList().Select(r => new emp_sick
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

            return Ok(db.emp_sick.OrderBy(e => e.begindate).Where(
            e => (e.empid == empid)).FirstOrDefault());


        }


    }
}