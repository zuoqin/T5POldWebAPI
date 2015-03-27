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
using System.Text.RegularExpressions;

namespace T5PWebAPI.Controllers
{
    public class ParameterController : ApiController
    {
        private T5PWebAPIContext db = new T5PWebAPIContext();
        private string GetParatype(string URL)
        {
            Match res =
                Regex.Match(URL, @"api/([A-Za-z0-9\-]+)\/{id}$", RegexOptions.IgnoreCase);

            string container = res.Groups[1].Value;


            string paratype = "";
            switch (container)
            {
                case "infrom":
                    paratype = "IN_FROM";
                    break;
                case "ethnic":
                    paratype = "ETHNIC";
                    break;
                case "city":
                    paratype = "CITY";
                    break;
                case "province":
                    paratype = "PROVINCE";
                    break;
                case "gender":
                    paratype = "GENDER";
                    break;
                case "salutation":
                    paratype = "SALUTATION";
                    break;
                default:
                    paratype = "VALUE";
                    break;

            }
            return paratype;
        }
        // GET api/cities
        public IQueryable<parameter> GetParameters()
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
            

            var filteredItems = parameters.Where( p => p.paratype == GetParatype(
                Request.GetRouteData().Route.RouteTemplate.ToString()));
            return filteredItems;
        }


        /// <summary>
        /// Looks up some data by param.
        /// </summary>
        /// <param name="theParam">The ID of the data.</param>
        public async Task<IHttpActionResult> PutParameter(parameter theParam)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

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

        // POST api/Book
        [ResponseType(typeof(Book))]
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

        // DELETE api/Book/5
        [ResponseType(typeof(Book))]
        public async Task<IHttpActionResult> DeleteParameter(string id)
        {
            string str = GetParatype(Request.GetRouteData().Route.RouteTemplate.ToString());
            var theParam =
                await db.parameters.Where(x => (x.paracode == id && x.paratype == 
                    str)).ToListAsync();
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