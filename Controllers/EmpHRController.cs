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
    public class EmpHRController : ApiController
    {
        private T5PWebAPIContext db = new T5PWebAPIContext();

        // GET api/EmpHR
        /// <summary>
        /// Retrieves all EmpHR table records
        /// </summary>
        /// <param name="page">Page number of the data, default is 0</param>
        /// <param name="pageSize">Pagesize of the data, default is int.MaxValue</param>
        public IQueryable<emphrDTO> Getemphr(int page = 0, int pageSize = int.MaxValue)
        {
            //var emphrs = from b in db.emphrs
            //            select new emphrDTO()
            //            {
            //                empid = b.empid,
            //                empcode = b.empcode,
            //                payrollgroupid = b.payrollgroupid,
            //                pinyin = b.pinyin,
            //                english = b.english
            //            };

            //return emphrs;
            List<emphrDTO> theList = new List<emphrDTO>();
            PropertyInfo[] properties1 = typeof(emphrDTO).GetProperties();
            PropertyInfo[] properties2 = typeof(emphr).GetProperties();

            foreach (var pos in db.emphrs.OrderBy(c => c.empid).Skip(page * pageSize).Take(pageSize))
            {
                emphrDTO theEmphrDto = new emphrDTO();

                foreach (PropertyInfo property1 in properties1)
                {
                    PropertyInfo theProperty = Array.Find(properties2, p => p.Name.CompareTo(property1.Name) == 0);
                    var value = theProperty.GetValue(pos);
                    property1.SetValue(theEmphrDto, value);
                }
                theList.Add(theEmphrDto);
            }

            var urlHelper = new UrlHelper(Request);
            //var thestr = urlHelper.Link("DefaultApi", new { page = page + 1, pageSize = pageSize, controller = "Empleavedetail_ongoing" });
            var totalCount = db.empleavedetail_ongoing.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / (pageSize > 0 ? pageSize : 1));

            var prevLink = page > 0 ? urlHelper.Link("DefaultApi", new { page = page - 1, pageSize = pageSize, controller = "EmpHR" }) : "";
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

        // GET api/EmpHR/5
        [ResponseType(typeof(emphr))]
        public async Task<IHttpActionResult> Getemphr(int id)
        {
            emphr emphr = await db.emphrs.FindAsync(id);
            if (emphr == null)
            {
                return NotFound();
            }

            return Ok(emphr);
        }

        // PUT api/EmpHR/5
        public async Task<IHttpActionResult> Putemphr(int id)
        {
            
            string jsonContent = Request.Content.ReadAsStringAsync().Result;
            emphr theEmphr = JsonConvert.DeserializeObject<emphr>(jsonContent);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != theEmphr.empid)
            {
                return BadRequest();
            }
            db.emphrs.Attach(theEmphr);

            DbEntityEntry<emphr> entry = db.Entry(theEmphr);

            Type type = theEmphr.GetType();
            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo prop in properties)
            {
                if (jsonContent.Contains( "\"" + prop.Name + "\":"))
                {
                    entry.Property( prop.Name ).IsModified = true;
                }
            }

            /*
            //using (var context = new T5PWebAPIContext())
            //{
                
                if (jsonContent.Contains("\"empcode\":"))
                {
                    entry.Property(e => e.empcode).IsModified = true;
                }
                if (jsonContent.Contains("\"payrollgroupid\":"))
                {
                    entry.Property(e => e.payrollgroupid).IsModified = true;
                }
                if (jsonContent.Contains("\"pinyin\":"))
                {
                    entry.Property(e => e.pinyin).IsModified = true;
                }

                if (jsonContent.Contains("\"english\":"))
                {
                    entry.Property(e => e.english).IsModified = true;
                }
                if (jsonContent.Contains("\"english\":"))
                {
                    entry.Property(e => e.english).IsModified = true;
                }

                if (jsonContent.Contains("\"chinese\":"))
                {
                    entry.Property(e => e.chinese).IsModified = true;
                }
                if (jsonContent.Contains("\"big5\":"))
                {
                    entry.Property(e => e.big5).IsModified = true;
                }
                if (jsonContent.Contains("\"servicestatus\":"))
                {
                    entry.Property(e => e.servicestatus).IsModified = true;
                }
                if (jsonContent.Contains("\"hrstatus\":"))
                {
                    entry.Property(e => e.hrstatus).IsModified = true;
                }
                if (jsonContent.Contains("\"address\":"))
                {
                    entry.Property(e => e.address).IsModified = true;
                }

                if (jsonContent.Contains("\"note\":"))
                {
                    entry.Property(e => e.note).IsModified = true;
                }

                if (jsonContent.Contains("\"province\":"))
                {
                    entry.Property(e => e.province).IsModified = true;
                }

                if (jsonContent.Contains("\"city\":"))
                {
                    entry.Property(e => e.city).IsModified = true;
                }

                if (jsonContent.Contains("\"zipcode\":"))
                {
                    entry.Property(e => e.zipcode).IsModified = true;
                }

                if (jsonContent.Contains("\"raddress\":"))
                {
                    entry.Property(e => e.raddress).IsModified = true;
                }

                if (jsonContent.Contains("\"rzipcode\":"))
                {
                    entry.Property(e => e.rzipcode).IsModified = true;
                }

                if (jsonContent.Contains("\"ethnic\":"))
                {
                    entry.Property(e => e.ethnic).IsModified = true;
                }

                if (jsonContent.Contains("\"nationality\":"))
                {
                    entry.Property(e => e.nationality).IsModified = true;
                }

                if (jsonContent.Contains("\"pid\":"))
                {
                    entry.Property(e => e.pid).IsModified = true;
                }

                if (jsonContent.Contains("\"birthday\":"))
                {
                    entry.Property(e => e.birthday).IsModified = true;
                }

                if (jsonContent.Contains("\"marital\":"))
                {
                    entry.Property(e => e.marital).IsModified = true;
                }

                if (jsonContent.Contains("\"gender\":"))
                {
                    entry.Property(e => e.gender).IsModified = true;
                }

                if (jsonContent.Contains("\"homephone\":"))
                {
                    entry.Property(e => e.homephone).IsModified = true;
                }

                if (jsonContent.Contains("\"mobile\":"))
                {
                    entry.Property(e => e.mobile).IsModified = true;
                }

                if (jsonContent.Contains("\"businessphone\":"))
                {
                    entry.Property(e => e.businessphone).IsModified = true;
                }

                if (jsonContent.Contains("\"cemail\":"))
                {
                    entry.Property(e => e.cemail).IsModified = true;
                }

                if (jsonContent.Contains("\"pemail\":"))
                {
                    entry.Property(e => e.pemail).IsModified = true;
                }

                if (jsonContent.Contains("\"zippassword\":"))
                {
                    entry.Property(e => e.zippassword).IsModified = true;
                }

                if (jsonContent.Contains("\"portrait\":"))
                {
                    entry.Property(e => e.portrait).IsModified = true;
                }

                if (jsonContent.Contains("\"party\":"))
                {
                    entry.Property(e => e.party).IsModified = true;
                }

                if (jsonContent.Contains("\"hiredate\":"))
                {
                    entry.Property(e => e.hiredate).IsModified = true;
                }

                if (jsonContent.Contains("\"hirevalid\":"))
                {
                    entry.Property(e => e.hirevalid).IsModified = true;
                }

                if (jsonContent.Contains("\"hiresociety\":"))
                {
                    entry.Property(e => e.hiresociety).IsModified = true;
                }

                if (jsonContent.Contains("\"inprobation\":"))
                {
                    entry.Property(e => e.inprobation).IsModified = true;
                }

                if (jsonContent.Contains("\"probationenddate\":"))
                {
                    entry.Property(e => e.agencycode).IsModified = true;
                }

                if (jsonContent.Contains("\"probationenddate\""))
                {
                    entry.Property(e => e.agencycode).IsModified = true;
                }

                if (jsonContent.Contains("\"hiresource\":"))
                {
                    entry.Property(e => e.hiresource).IsModified = true;
                }

                if (jsonContent.Contains("\"in_from\":"))
                {
                    entry.Property(e => e.in_from).IsModified = true;
                }

                if (jsonContent.Contains("\"laborbook\":"))
                {
                    entry.Property(e => e.laborbook).IsModified = true;
                }

                if (jsonContent.Contains("\"dangan\":"))
                {
                    entry.Property(e => e.dangan).IsModified = true;
                }

                if (jsonContent.Contains("\"huko\":"))
                {
                    entry.Property(e => e.huko).IsModified = true;
                }

                if (jsonContent.Contains("\"ppnumber\":"))
                {
                    entry.Property(e => e.ppnumber).IsModified = true;
                }

                if (jsonContent.Contains("\"ppissuecountry\":"))
                {
                    entry.Property(e => e.ppissuecountry).IsModified = true;
                }

                if (jsonContent.Contains("\"ppexpiredate\":"))
                {
                    entry.Property(e => e.ppexpiredate).IsModified = true;
                }

                if (jsonContent.Contains("\"adminemp\":"))
                {
                    entry.Property(e => e.adminemp).IsModified = true;
                }

                if (jsonContent.Contains("\"saemp\":"))
                {
                    entry.Property(e => e.saemp).IsModified = true;
                }

                if (jsonContent.Contains("\"tempemp\":"))
                {
                    entry.Property(e => e.tempemp).IsModified = true;
                }

                if (jsonContent.Contains("\"expatriate\":"))
                {
                    entry.Property(e => e.expatriate).IsModified = true;
                }

                if (jsonContent.Contains("\"laborworker\":"))
                {
                    entry.Property(e => e.laborworker).IsModified = true;
                }

                if (jsonContent.Contains("\"agencycode\":"))
                {
                    entry.Property(e => e.agencycode).IsModified = true;
                }

                if (jsonContent.Contains("\"agencyname\":"))
                {
                    entry.Property(e => e.agencyname).IsModified = true;
                }
                if (jsonContent.Contains("\"contracttype\":"))
                {
                    entry.Property(e => e.contracttype).IsModified = true;
                }
                if (jsonContent.Contains("\"contractstartdate\":"))
                {
                    entry.Property(e => e.contractstartdate).IsModified = true;
                }
                if (jsonContent.Contains("\"contractenddate\":"))
                {
                    entry.Property(e => e.contractenddate).IsModified = true;
                }
                if (jsonContent.Contains("\"contractnumber\":"))
                {
                    entry.Property(e => e.contractnumber).IsModified = true;
                }
                if (jsonContent.Contains("\"contractlocation\":"))
                {
                    entry.Property(e => e.contractlocation).IsModified = true;
                }
                if (jsonContent.Contains("\"monthmanagefee\":"))
                {
                    entry.Property(e => e.monthmanagefee).IsModified = true;
                }
                if (jsonContent.Contains("\"pensioncontribution\":"))
                {
                    entry.Property(e => e.pensioncontribution).IsModified = true;
                }
                if (jsonContent.Contains("\"hfcontribution\":"))
                {
                    entry.Property(e => e.hfcontribution).IsModified = true;
                }
                if (jsonContent.Contains("\"medicalpercentage\":"))
                {
                    entry.Property(e => e.medicalpercentage).IsModified = true;
                }
                if (jsonContent.Contains("\"orgstart\":"))
                {
                    entry.Property(e => e.orgstart).IsModified = true;
                }
                if (jsonContent.Contains("\"orgcode1\":"))
                {
                    entry.Property(e => e.orgcode1).IsModified = true;
                }
                if (jsonContent.Contains("\"orgcode2\":"))
                {
                    entry.Property(e => e.orgcode2).IsModified = true;
                }
                if (jsonContent.Contains("\"orgcode3\":"))
                {
                    entry.Property(e => e.orgcode3).IsModified = true;
                }
                if (jsonContent.Contains("\"orgcode4\":"))
                {
                    entry.Property(e => e.orgcode4).IsModified = true;
                }
                if (jsonContent.Contains("\"orgcode5\":"))
                {
                    entry.Property(e => e.orgcode5).IsModified = true;
                }
                if (jsonContent.Contains("\"orgcode6\":"))
                {
                    entry.Property(e => e.orgcode6).IsModified = true;
                }
                if (jsonContent.Contains("\"orgcode7\":"))
                {
                    entry.Property(e => e.orgcode7).IsModified = true;
                }
                if (jsonContent.Contains("\"orgcode8\":"))
                {
                    entry.Property(e => e.orgcode8).IsModified = true;
                }
                if (jsonContent.Contains("\"orgcode9\":"))
                {
                    entry.Property(e => e.orgcode9).IsModified = true;
                }
                if (jsonContent.Contains("\"orgcode10\":"))
                {
                    entry.Property(e => e.orgcode10).IsModified = true;
                }
                if (jsonContent.Contains("\"anlvcalcclass\":"))
                {
                    entry.Property(e => e.anlvcalcclass).IsModified = true;
                }
                if (jsonContent.Contains("\"sickleaveclass\":"))
                {
                    entry.Property(e => e.sickleaveclass).IsModified = true;
                }
                if (jsonContent.Contains("\"surname\":"))
                {
                    entry.Property(e => e.surname).IsModified = true;
                }
                if (jsonContent.Contains("\"christianname\":"))
                {
                    entry.Property(e => e.christianname).IsModified = true;
                }
                if (jsonContent.Contains("\"areacode\":"))
                {
                    entry.Property(e => e.areacode).IsModified = true;
                }
                if (jsonContent.Contains("\"spousename\":"))
                {
                    entry.Property(e => e.spousename).IsModified = true;
                }
                if (jsonContent.Contains("\"spousehkid\":"))
                {
                    entry.Property(e => e.spousehkid).IsModified = true;
                }

                if (jsonContent.Contains("\"spouseppnumber\":"))
                {
                    entry.Property(e => e.spouseppnumber).IsModified = true;
                }
                if (jsonContent.Contains("\"taxfileid\":"))
                {
                    entry.Property(e => e.taxfileid).IsModified = true;
                }
                if (jsonContent.Contains("\"spouseppissuecountry\":"))
                {
                    entry.Property(e => e.spouseppissuecountry).IsModified = true;
                }
                if (jsonContent.Contains("\"cleaveclass\":"))
                {
                    entry.Property(e => e.cleaveclass).IsModified = true;
                }
                if (jsonContent.Contains("\"quitlieutype\":"))
                {
                    entry.Property(e => e.quitlieutype).IsModified = true;
                }
                if (jsonContent.Contains("\"calendarcode\":"))
                {
                    entry.Property(e => e.calendarcode).IsModified = true;
                }
                if (jsonContent.Contains("\"atsbasicpolicy\":"))
                {
                    entry.Property(e => e.atsbasicpolicy).IsModified = true;
                }
                if (jsonContent.Contains("\"atsotpolicy\":"))
                {
                    entry.Property(e => e.atsotpolicy).IsModified = true;
                }
                if (jsonContent.Contains("\"cpfno\":"))
                {
                    entry.Property(e => e.cpfno).IsModified = true;
                }
                if (jsonContent.Contains("\"sg_ethnic\":"))
                {
                    entry.Property(e => e.sg_ethnic).IsModified = true;
                }
                if (jsonContent.Contains("\"nricetype\":"))
                {
                    entry.Property(e => e.nricetype).IsModified = true;
                }
                if (jsonContent.Contains("\"prbegindate\":"))
                {
                    entry.Property(e => e.prbegindate).IsModified = true;
                }
                if (jsonContent.Contains("\"japanese\":"))
                {
                    entry.Property(e => e.japanese).IsModified = true;
                }
                if (jsonContent.Contains("\"rosterflag\":"))
                {
                    entry.Property(e => e.rosterflag).IsModified = true;
                }
                if (jsonContent.Contains("\"carlinecode\":"))
                {
                    entry.Property(e => e.carlinecode).IsModified = true;
                }
                if (jsonContent.Contains("\"mealtype\":"))
                {
                    entry.Property(e => e.mealtype).IsModified = true;
                }
                if (jsonContent.Contains("\"gradecode\":"))
                {
                    entry.Property(e => e.gradecode).IsModified = true;
                }
                if (jsonContent.Contains("\"levelcode\":"))
                {
                    entry.Property(e => e.levelcode).IsModified = true;
                }
                if (jsonContent.Contains("\"titlecode\":"))
                {
                    entry.Property(e => e.titlecode).IsModified = true;
                }
                if (jsonContent.Contains("\"skilllevelcode\":"))
                {
                    entry.Property(e => e.skilllevelcode).IsModified = true;
                }
                if (jsonContent.Contains("\"atslocation\":"))
                {
                    entry.Property(e => e.atslocation).IsModified = true;
                }
                if (jsonContent.Contains("\"orgid\":"))
                {
                    entry.Property(e => e.orgid).IsModified = true;
                }
                if (jsonContent.Contains("\"companysickleaveclass\":"))
                {
                    entry.Property(e => e.companysickleaveclass).IsModified = true;
                }
                if (jsonContent.Contains("\"salutation\":"))
                {
                    entry.Property(e => e.salutation).IsModified = true;
                }
                if (jsonContent.Contains("\"othername\":"))
                {
                    entry.Property(e => e.othername).IsModified = true;
                }
                //context.SaveChanges();
            //}
            */
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!emphrExists(id))
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

        // POST api/EmpHR
        [ResponseType(typeof(emphr))]
        public async Task<IHttpActionResult> Postemphr()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string jsonContent = Request.Content.ReadAsStringAsync().Result;
            emphr theEmphr = JsonConvert.DeserializeObject<emphr>(jsonContent);

            Type type = theEmphr.GetType();
            PropertyInfo[] properties = type.GetProperties();
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    var sql = @"INSERT INTO [emphr] (empid) VALUES( @empid )";

                    db.Database.ExecuteSqlCommand(
                        sql,
                        new System.Data.SqlClient.SqlParameter("empid", theEmphr.empid));
                    //db.Database.ExecuteSqlCommand("insert into emphr (empid) " +
                    //                              "values( " + theEmphr.empid + 
                    //                              ")");

                    db.emphrs.Attach(theEmphr);
                    DbEntityEntry<emphr> entry = db.Entry(theEmphr);
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
                return CreatedAtRoute("DefaultApi", new { theEmphr }, theEmphr);
            }
        }

        // DELETE api/EmpHR/5
        [ResponseType(typeof(emphr))]
        public async Task<IHttpActionResult> Deleteemphr(int id)
        {
            emphr emphr = await db.emphrs.FindAsync(id);
            if (emphr == null)
            {
                return NotFound();
            }

            db.emphrs.Remove(emphr);
            await db.SaveChangesAsync();

            return Ok(emphr);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool emphrExists(int id)
        {
            return db.emphrs.Count(e => e.empid == id) > 0;
        }
    }
}