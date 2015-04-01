using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Routing;
using Newtonsoft.Json;
using T5PWebAPI.Models;


namespace T5PWebAPI.Controllers
{
    public class EmpleaveController : ApiController
    {
        private T5PWebAPIContext db = new T5PWebAPIContext();


        // GET api/Book/5
        [ResponseType(typeof(WFData))]
        public async Task<IHttpActionResult> GetDetails(int empid, int forminstanceid)
        {
            empleavedetail_ongoing theEmpleavedetailOngoing = db.empleavedetail_ongoing.Where(
                e=>(e.empid == empid &
                    e.forminstanceid == forminstanceid)
                ).FirstOrDefault();

            if (theEmpleavedetailOngoing == null)
            {
                HttpResponseMessage responseMsg = new HttpResponseMessage(HttpStatusCode.NotFound);
                responseMsg.Content = new StringContent("EmpleavedetailOngoing record was not found");
                IHttpActionResult response = ResponseMessage(responseMsg);
                return response;
            }

            forminstance theForminstance = db.forminstance.Find(forminstanceid);
            if (theForminstance == null)
            {
                HttpResponseMessage responseMsg = new HttpResponseMessage(HttpStatusCode.NotFound);
                responseMsg.Content = new StringContent("forminstance record was not found");
                IHttpActionResult response = ResponseMessage(responseMsg);
                return response;
            }
            workflowinstance theWorkflowinstance = db.workflowinstance.Find(theForminstance.workflowinstanceid);
            if (theWorkflowinstance == null)
            {
                HttpResponseMessage responseMsg = new HttpResponseMessage(HttpStatusCode.NotFound);
                responseMsg.Content = new StringContent("workflowinstance record was not found");
                IHttpActionResult response = ResponseMessage(responseMsg);
                return response;
            }

            forminstancedetail theForminstancedetail = db.forminstancedetail.Where(
                e => (e.tablecode == "empleavedata" &
                    e.forminstanceid == forminstanceid &
                    e.tablecode == "empleavedata" &
                    e.fieldcode == "notes")
                ).FirstOrDefault();


            workflowinstancedetail theWorkflowinstancedetail = db.workflowinstancedetail.Where(
                e => (e.workflowinstanceid == theWorkflowinstance.workflowinstanceid )
                ).FirstOrDefault();
            if (theWorkflowinstancedetail == null)
            {
                HttpResponseMessage responseMsg = new HttpResponseMessage(HttpStatusCode.NotFound);
                responseMsg.Content = new StringContent("workflowinstancedetail record was not found");
                IHttpActionResult response = ResponseMessage(responseMsg);
                return response;
            }

            WFData theData = new WFData();
            theData.empid = empid;
            theData.forminstanceid = forminstanceid;
            theData.leavedays = theEmpleavedetailOngoing.leavedays;
            theData.leavefromdate = theEmpleavedetailOngoing.leavefromdate;
            theData.leavetodate = theEmpleavedetailOngoing.leavetodate;
            theData.starttime = theForminstance.submittime;
            theData.workflowinstanceid = theForminstance.workflowinstanceid;
            theData.workflowstatus = theWorkflowinstance.workflowstatus;
            theData.workflowstep = theWorkflowinstancedetail.workflowstep;
            theData.notes = theForminstancedetail.newvalue;


            theForminstancedetail = db.forminstancedetail.Where(
                    e => (e.forminstanceid == theWorkflowinstance.forminstanceid &
                          e.fieldcode == "leavecode" &
                          e.tablecode == "empleavedata" &
                          e.tablecode == "empleavedata")
                    ).FirstOrDefault();
            if (theForminstancedetail == null)
            {
                HttpResponseMessage responseMsg = new HttpResponseMessage(HttpStatusCode.NotFound);
                responseMsg.Content = new StringContent("forminstancedetail record was not found");
                IHttpActionResult response = ResponseMessage(responseMsg);
                return response;
            }

            leavetype theLeavetype = db.leavetype.Find(theForminstancedetail.newvalue);
            if (theLeavetype == null)
            {
                HttpResponseMessage responseMsg = new HttpResponseMessage(HttpStatusCode.NotFound);
                responseMsg.Content = new StringContent("leavetype record was not found");
                IHttpActionResult response = ResponseMessage(responseMsg);
                return response;
            }
            theData.leavetype_big5 = theLeavetype.big5;
            theData.leavetype_chinese = theLeavetype.chinese;
            theData.leavetype_english = theLeavetype.english;
            theData.leavetype_japanese = theLeavetype.japanese;
            return Ok(theData);
        }

        // GET api/empleavedata/5/LV01/2015-03-01/2015-03-01
        /// <summary>
        /// Retrieves list of items to be approved by given approver
        /// </summary>
        /// <param name="approver">Employee ID, who will approve items</param>
        public IQueryable<WFData> GetApproveListByApprover(int approver)
        {
            List<WFData> theList = new List<WFData>();
            PropertyInfo[] properties1 = typeof(workflowinstancedetail).GetProperties();
            foreach (var pos in db.workflowinstancedetail
                .Include(c => c.Workflowinstance)
                .OrderBy(c => c.workflowinstanceid)
                .ThenBy(c => c.workflowstep)
                .Where(c => (c.approver == approver & c.stepstatus == 1)))
            {
                WFData theData = new WFData();
                workflowinstance theWorkflowinstance = db.workflowinstance.Find(pos.workflowinstanceid);
                if (theWorkflowinstance == null)
                {
                    continue;
                }
                forminstancedetail theForminstancedetail = db.forminstancedetail.Where(
                    e => (e.forminstanceid == theWorkflowinstance.forminstanceid &
                          e.fieldcode == "leavecode" &
                          e.tablecode == "empleavedata")
                    ).FirstOrDefault();
                if (theForminstancedetail == null)
                {
                    continue;
                }

                leavetype theLeavetype = db.leavetype.Find(theForminstancedetail.newvalue);
                if (theLeavetype == null)
                {
                    continue;
                }

                theForminstancedetail = db.forminstancedetail.Where(
                   e => (e.forminstanceid == theWorkflowinstance.forminstanceid &
                         e.fieldcode == "empid" &
                         e.tablecode == "empleavedata")
                   ).FirstOrDefault();
                if (theForminstancedetail == null)
                {
                    continue;
                }
                theData.empid = int.Parse(theForminstancedetail.newvalue);
                theData.forminstanceid = pos.Workflowinstance.forminstanceid;
                theData.workflowinstanceid = pos.workflowinstanceid;
                theData.workflowstatus = pos.Workflowinstance.workflowstatus;
                theData.leavetype_chinese = theLeavetype.chinese;
                theData.leavetype_english = theLeavetype.english;
                theData.leavetype_big5 = theLeavetype.big5;
                theData.leavetype_japanese = theLeavetype.japanese;
                theData.workflowstep = pos.workflowstep;
                theData.starttime = pos.starttime;

                empleavedetail_ongoing theEmpleavedetailOngoing = db.empleavedetail_ongoing.Where(
                    e => (e.forminstanceid == theWorkflowinstance.forminstanceid &
                          e.leavecode.CompareTo(theLeavetype.leavecode) == 0)
                    ).FirstOrDefault();
                if (theEmpleavedetailOngoing != null)
                {
                    theData.leavedays = theEmpleavedetailOngoing.leavedays;
                    theData.leavefromdate = theEmpleavedetailOngoing.leavefromdate;
                    theData.leavetodate = theEmpleavedetailOngoing.leavetodate;
                }
                //foreach (PropertyInfo property1 in properties1)
                //{
                //    var value = property1.GetValue(pos);
                //    property1.SetValue(theData, value);
                //}
                theList.Add(theData);
            }
            return theList.AsQueryable();
        }


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

        /// <summary>
        /// Adds new Leave application records
        /// Input: LeaveData application
        /// </summary>
        /// <param name="page">Page number of the data, default is 0</param>
        /// <param name="pageSize">Pagesize of the data, default is int.MaxValue</param>
        [HttpPut, ActionName("PutLeave")]
        [ResponseType(typeof (WFData))]
        public async Task<IHttpActionResult> PutLeave()
        {
            string jsonContent = Request.Content.ReadAsStringAsync().Result;

            WFData theData = JsonConvert.DeserializeObject<WFData>(jsonContent);
            empleavedata theEmpleavedata = new empleavedata();
            theEmpleavedata.empid = theData.empid;
            

            empleavedetail_ongoing theEmpleavedetailOngoing = db.empleavedetail_ongoing.Where(
                    e => (e.forminstanceid == theData.forminstanceid &
                          e.empid == theData.empid &
                          e.leavefromdate == theData.leavefromdate)
                    ).FirstOrDefault();
            if (theEmpleavedetailOngoing == null)
            {
                return NotFound();
            }

            
            List<forminstancedetail> theList = 
            db.forminstancedetail.Where(
                    e => (e.forminstanceid == theData.forminstanceid &
                          e.tablecode == "empleavedata")
                    ).ToList();
            PropertyInfo[] properties = theEmpleavedata.GetType().GetProperties();
            if (theList != null)
            {
                
                //foreach (PropertyInfo prop in properties)
                //{
                //    if (theList.Find(x=>x.fieldcode.Contains(prop.Name)) != null)
                //    {
                //        prop.SetValue(theEmpleavedata, theList.Find(x => x.fieldcode.Contains(prop.Name)).newvalue);
                //    }
                //}
                theEmpleavedata.empid = int.Parse(theList.Find(x => x.fieldcode.Contains("empid")).newvalue);
                theEmpleavedata.fromdateafternoon = int.Parse(theList.Find(x => x.fieldcode.Contains("fromdateafternoon")).newvalue) == 0 ? false : true;
                theEmpleavedata.fromdatemorning = int.Parse(theList.Find(x => x.fieldcode.Contains("fromdatemorning")).newvalue) == 0 ? false : true;
                theEmpleavedata.leavecode = theList.Find(x => x.fieldcode.Contains("leavecode")).newvalue;
                theEmpleavedata.leavedays = double.Parse(theList.Find(x => x.fieldcode.Contains("leavedays")).newvalue);
                theEmpleavedata.leavefromdate = DateTime.Parse(theList.Find(x => x.fieldcode.Contains("leavefromdate")).newvalue);
                theEmpleavedata.leavefromtime = DateTime.Parse(theList.Find(x => x.fieldcode.Contains("leavefromtime")).newvalue);
                theEmpleavedata.leavehours = double.Parse(theList.Find(x => x.fieldcode.Contains("leavehours")).newvalue);
                theEmpleavedata.leavetodate = DateTime.Parse(theList.Find(x => x.fieldcode.Contains("leavetodate")).newvalue);
                theEmpleavedata.leavetotime = DateTime.Parse(theList.Find(x => x.fieldcode.Contains("leavetotime")).newvalue);
                theEmpleavedata.notes = theList.Find(x => x.fieldcode.Contains("notes")).newvalue;
                theEmpleavedata.prebirthdate = DateTime.Parse(theList.Find(x => x.fieldcode.Contains("prebirthdate")).newvalue);
                theEmpleavedata.referencedays = float.Parse(theList.Find(x => x.fieldcode.Contains("referencedays")).newvalue);
                theEmpleavedata.specifydate = DateTime.Parse(theList.Find(x => x.fieldcode.Contains("specifydate")).newvalue);
                theEmpleavedata.todateafternoon = int.Parse(theList.Find(x => x.fieldcode.Contains("todateafternoon")).newvalue) == 0 ? false : true;
                theEmpleavedata.todatemorning = int.Parse(theList.Find(x => x.fieldcode.Contains("todatemorning")).newvalue) == 0 ? false : true;
                theEmpleavedata.cancel = int.Parse(theList.Find(x => x.fieldcode.Contains("cancel")).newvalue) == 0 ? false : true;
            }
            db.empleavedata.Add(theEmpleavedata);
            await db.SaveChangesAsync();
            db.empleavedetail_ongoing.Remove(theEmpleavedetailOngoing);
            await db.SaveChangesAsync();

            workflowinstancedetail theWorkflowinstancedetail = db.workflowinstancedetail.Where(
                e => (e.workflowinstanceid == theData.workflowinstanceid &
                      e.workflowstep == 1)
                ).FirstOrDefault();
            if (theWorkflowinstancedetail != null)
            {

                db.workflowinstancedetail.Attach(theWorkflowinstancedetail);

                DbEntityEntry<workflowinstancedetail> entry = db.Entry(theWorkflowinstancedetail);
                theWorkflowinstancedetail.stepstatus = 2;

                entry.Property("stepstatus").IsModified = true;
            }

            
            
            await db.SaveChangesAsync();

            return Ok(theEmpleavedata);

        }

        /// <summary>
        /// Adds new Leave application records
        /// Input: LeaveData application
        /// </summary>
        /// <param name="page">Page number of the data, default is 0</param>
        /// <param name="pageSize">Pagesize of the data, default is int.MaxValue</param>
        [HttpPost, ActionName("PostLeave")]
        [ResponseType(typeof(WFData))]
        public async Task<IHttpActionResult> PostLeave()
        {
            string jsonContent = Request.Content.ReadAsStringAsync().Result;
            LeaveApplication theLeaveApplication = JsonConvert.DeserializeObject<LeaveApplication>(jsonContent);

            //List<WFData> theList = JsonConvert.DeserializeObject<List<WFData>> (jsonContent);

            workflowinstance theWorkflowinstance = db.workflowinstance.OrderBy(e => e.workflowinstanceid).FirstOrDefault();
            theWorkflowinstance.applicationtype = "LeaveApply";
            theWorkflowinstance.applicationversion = 5;
            theWorkflowinstance.forminstanceid = 0;
            theWorkflowinstance.workflowcode = "Mleave";
            theWorkflowinstance.workflowstatus = 1;
            theWorkflowinstance.applicant = theLeaveApplication.empid;
            db.workflowinstance.Add(theWorkflowinstance);
            await db.SaveChangesAsync();


            forminstance theForminstance = db.forminstance.OrderBy(e => e.forminstanceid).FirstOrDefault();

            theForminstance.applicationtype = "LeaveApply";
            theForminstance.applicationversion = 5;
            theForminstance.workflowinstanceid = theWorkflowinstance.workflowinstanceid;
            theForminstance.payrollgroupid = 0;
            theForminstance.formcode = "sf_wf_LeaveDataInput";
            theForminstance.formstatus = 1;
            theForminstance.applicant = theLeaveApplication.empid;
            theForminstance.submittime = DateTime.UtcNow;
            theForminstance.delegateapplicant = 0;
            theForminstance.formparameters = ";EmpID=" + theLeaveApplication.empid.ToString();
            theForminstance.rowflags = "0_sf_wf_LeaveDataInput(+)";
            db.forminstance.Add(theForminstance);
            await db.SaveChangesAsync();
            theWorkflowinstance.forminstanceid = theForminstance.forminstanceid;
            db.Entry(theWorkflowinstance).State = EntityState.Modified;
            await db.SaveChangesAsync();


            forminstancedetail theForminstancedetail = new forminstancedetail();//db.forminstancedetail.OrderBy(e => e.forminstanceid).FirstOrDefault();
            theForminstancedetail.forminstanceid = theForminstance.forminstanceid;
            theForminstancedetail.subpayrollgroupid = 0;
            theForminstancedetail.subformcode = "sf_wf_LeaveDataInput";
            theForminstancedetail.rowindex = 1;
            theForminstancedetail.userid = 28;
            theForminstancedetail.updated = false;
            theForminstancedetail.tablecode = "__key";
            theForminstancedetail.fieldcode = "autoid";
            db.forminstancedetail.Add(theForminstancedetail);
            await db.SaveChangesAsync();

            theForminstancedetail = new forminstancedetail();//db.forminstancedetail.OrderBy(e => e.forminstanceid).FirstOrDefault();
            theForminstancedetail.forminstanceid = theForminstance.forminstanceid;
            theForminstancedetail.subpayrollgroupid = 0;
            theForminstancedetail.subformcode = "sf_wf_LeaveDataInput";
            theForminstancedetail.rowindex = 1;
            theForminstancedetail.userid = 28;
            theForminstancedetail.updated = false;
            theForminstancedetail.tablecode = "__key";
            theForminstancedetail.fieldcode = "empid";
            theForminstancedetail.newvalue = theLeaveApplication.empid.ToString();
            db.forminstancedetail.Add(theForminstancedetail);
            await db.SaveChangesAsync();


            theForminstancedetail = new forminstancedetail();//db.forminstancedetail.OrderBy(e => e.forminstanceid).FirstOrDefault();
            theForminstancedetail.forminstanceid = theForminstance.forminstanceid;
            theForminstancedetail.subpayrollgroupid = 0;
            theForminstancedetail.subformcode = "sf_wf_LeaveDataInput";
            theForminstancedetail.rowindex = 1;
            theForminstancedetail.userid = 28;
            theForminstancedetail.updated = false;
            theForminstancedetail.tablecode = "__key";
            theForminstancedetail.fieldcode = "leavecode";
            theForminstancedetail.newvalue = theLeaveApplication.leavecode;
            db.forminstancedetail.Add(theForminstancedetail);
            await db.SaveChangesAsync();


            theForminstancedetail = new forminstancedetail();//db.forminstancedetail.OrderBy(e => e.forminstanceid).FirstOrDefault();
            theForminstancedetail.forminstanceid = theForminstance.forminstanceid;
            theForminstancedetail.subpayrollgroupid = 0;
            theForminstancedetail.subformcode = "sf_wf_LeaveDataInput";
            theForminstancedetail.rowindex = 1;
            theForminstancedetail.userid = 28;
            theForminstancedetail.updated = false;
            theForminstancedetail.tablecode = "__key";
            theForminstancedetail.fieldcode = "leavefromdate";
            theForminstancedetail.newvalue = theLeaveApplication.leavefromdate.ToString();
            db.forminstancedetail.Add(theForminstancedetail);
            await db.SaveChangesAsync();


            theForminstancedetail = new forminstancedetail();//db.forminstancedetail.OrderBy(e => e.forminstanceid).FirstOrDefault();
            theForminstancedetail.forminstanceid = theForminstance.forminstanceid;
            theForminstancedetail.subpayrollgroupid = 0;
            theForminstancedetail.subformcode = "sf_wf_LeaveDataInput";
            theForminstancedetail.rowindex = 1;
            theForminstancedetail.userid = 28;
            theForminstancedetail.updated = false;
            theForminstancedetail.tablecode = "__key";
            theForminstancedetail.fieldcode = "leavefromtime";
            theForminstancedetail.newvalue = theLeaveApplication.leavefromtime.ToString();
            db.forminstancedetail.Add(theForminstancedetail);
            await db.SaveChangesAsync();


            theForminstancedetail = new forminstancedetail();//db.forminstancedetail.OrderBy(e => e.forminstanceid).FirstOrDefault();
            theForminstancedetail.forminstanceid = theForminstance.forminstanceid;
            theForminstancedetail.subpayrollgroupid = 0;
            theForminstancedetail.subformcode = "sf_wf_LeaveDataInput";
            theForminstancedetail.rowindex = 1;
            theForminstancedetail.userid = 28;
            theForminstancedetail.updated = false;
            theForminstancedetail.tablecode = "__key";
            theForminstancedetail.fieldcode = "leavetotime";
            theForminstancedetail.newvalue = theLeaveApplication.leavetotime.ToString();
            db.forminstancedetail.Add(theForminstancedetail);
            await db.SaveChangesAsync();

            theForminstancedetail = new forminstancedetail();//db.forminstancedetail.OrderBy(e => e.forminstanceid).FirstOrDefault();
            theForminstancedetail.forminstanceid = theForminstance.forminstanceid;
            theForminstancedetail.subpayrollgroupid = 0;
            theForminstancedetail.subformcode = "sf_wf_LeaveDataInput";
            theForminstancedetail.rowindex = 1;
            theForminstancedetail.userid = 28;
            theForminstancedetail.updated = false;
            theForminstancedetail.tablecode = "empleavedata";

            theForminstancedetail.fieldcode = "autoid";
            theForminstancedetail.newvalue = "0";
            db.forminstancedetail.Add(theForminstancedetail);
            await db.SaveChangesAsync();

            theForminstancedetail = new forminstancedetail();//db.forminstancedetail.OrderBy(e => e.forminstanceid).FirstOrDefault();
            theForminstancedetail.forminstanceid = theForminstance.forminstanceid;
            theForminstancedetail.subpayrollgroupid = 0;
            theForminstancedetail.subformcode = "sf_wf_LeaveDataInput";
            theForminstancedetail.rowindex = 1;
            theForminstancedetail.userid = 28;
            theForminstancedetail.updated = false;
            theForminstancedetail.tablecode = "empleavedata";
            theForminstancedetail.fieldcode = "cancel";
            theForminstancedetail.newvalue = "0";
            db.forminstancedetail.Add(theForminstancedetail);
            await db.SaveChangesAsync();

            theForminstancedetail = new forminstancedetail();//db.forminstancedetail.OrderBy(e => e.forminstanceid).FirstOrDefault();
            theForminstancedetail.forminstanceid = theForminstance.forminstanceid;
            theForminstancedetail.subpayrollgroupid = 0;
            theForminstancedetail.subformcode = "sf_wf_LeaveDataInput";
            theForminstancedetail.rowindex = 1;
            theForminstancedetail.userid = 28;
            theForminstancedetail.tablecode = "empleavedata";
            theForminstancedetail.updated = true;
            theForminstancedetail.fieldcode = "empid";
            theForminstancedetail.newvalue = theLeaveApplication.empid.ToString();
            db.forminstancedetail.Add(theForminstancedetail);
            await db.SaveChangesAsync();

            theForminstancedetail = new forminstancedetail();//db.forminstancedetail.OrderBy(e => e.forminstanceid).FirstOrDefault();
            theForminstancedetail.forminstanceid = theForminstance.forminstanceid;
            theForminstancedetail.subpayrollgroupid = 0;
            theForminstancedetail.subformcode = "sf_wf_LeaveDataInput";
            theForminstancedetail.rowindex = 1;
            theForminstancedetail.userid = 28;
            theForminstancedetail.tablecode = "empleavedata";
            theForminstancedetail.updated = true;


            theForminstancedetail = new forminstancedetail();//db.forminstancedetail.OrderBy(e => e.forminstanceid).FirstOrDefault();
            theForminstancedetail.forminstanceid = theForminstance.forminstanceid;
            theForminstancedetail.subpayrollgroupid = 0;
            theForminstancedetail.subformcode = "sf_wf_LeaveDataInput";
            theForminstancedetail.rowindex = 1;
            theForminstancedetail.userid = 28;
            theForminstancedetail.tablecode = "empleavedata";
            theForminstancedetail.updated = true;
            theForminstancedetail.fieldcode = "fromdateafternoon";
            theForminstancedetail.newvalue = "1";
            db.forminstancedetail.Add(theForminstancedetail);
            await db.SaveChangesAsync();

            theForminstancedetail = new forminstancedetail();//db.forminstancedetail.OrderBy(e => e.forminstanceid).FirstOrDefault();
            theForminstancedetail.forminstanceid = theForminstance.forminstanceid;
            theForminstancedetail.subpayrollgroupid = 0;
            theForminstancedetail.subformcode = "sf_wf_LeaveDataInput";
            theForminstancedetail.rowindex = 1;
            theForminstancedetail.userid = 28;
            theForminstancedetail.tablecode = "empleavedata";
            theForminstancedetail.updated = true;
            theForminstancedetail.fieldcode = "fromdatemorning";
            theForminstancedetail.newvalue = "0";
            db.forminstancedetail.Add(theForminstancedetail);
            await db.SaveChangesAsync();

            theForminstancedetail = new forminstancedetail();//db.forminstancedetail.OrderBy(e => e.forminstanceid).FirstOrDefault();
            theForminstancedetail.forminstanceid = theForminstance.forminstanceid;
            theForminstancedetail.subpayrollgroupid = 0;
            theForminstancedetail.subformcode = "sf_wf_LeaveDataInput";
            theForminstancedetail.rowindex = 1;
            theForminstancedetail.userid = 28;
            theForminstancedetail.tablecode = "empleavedata";
            theForminstancedetail.updated = true;
            theForminstancedetail.fieldcode = "leavecode";
            theForminstancedetail.newvalue = theLeaveApplication.leavecode;
            db.forminstancedetail.Add(theForminstancedetail);
            await db.SaveChangesAsync();

            theForminstancedetail = new forminstancedetail();//db.forminstancedetail.OrderBy(e => e.forminstanceid).FirstOrDefault();
            theForminstancedetail.forminstanceid = theForminstance.forminstanceid;
            theForminstancedetail.subpayrollgroupid = 0;
            theForminstancedetail.subformcode = "sf_wf_LeaveDataInput";
            theForminstancedetail.rowindex = 1;
            theForminstancedetail.userid = 28;
            theForminstancedetail.tablecode = "empleavedata";
            theForminstancedetail.updated = true;
            theForminstancedetail.fieldcode = "leavedays";
            theForminstancedetail.newvalue = theLeaveApplication.leavedays.ToString();
            db.forminstancedetail.Add(theForminstancedetail);
            await db.SaveChangesAsync();

            theForminstancedetail = new forminstancedetail();//db.forminstancedetail.OrderBy(e => e.forminstanceid).FirstOrDefault();
            theForminstancedetail.forminstanceid = theForminstance.forminstanceid;
            theForminstancedetail.subpayrollgroupid = 0;
            theForminstancedetail.subformcode = "sf_wf_LeaveDataInput";
            theForminstancedetail.rowindex = 1;
            theForminstancedetail.userid = 28;
            theForminstancedetail.tablecode = "empleavedata";
            theForminstancedetail.updated = true;
            theForminstancedetail.fieldcode = "leavefromdate";
            theForminstancedetail.newvalue = theLeaveApplication.leavefromtime.ToString();
            db.forminstancedetail.Add(theForminstancedetail);
            await db.SaveChangesAsync();


            theForminstancedetail = new forminstancedetail();//db.forminstancedetail.OrderBy(e => e.forminstanceid).FirstOrDefault();
            theForminstancedetail.forminstanceid = theForminstance.forminstanceid;
            theForminstancedetail.subpayrollgroupid = 0;
            theForminstancedetail.subformcode = "sf_wf_LeaveDataInput";
            theForminstancedetail.rowindex = 1;
            theForminstancedetail.userid = 28;
            theForminstancedetail.tablecode = "empleavedata";
            theForminstancedetail.updated = true;
            theForminstancedetail.fieldcode = "leavefromtime";
            theForminstancedetail.newvalue = theLeaveApplication.leavefromtime.ToString();
            db.forminstancedetail.Add(theForminstancedetail);
            await db.SaveChangesAsync();


            theForminstancedetail = new forminstancedetail();//db.forminstancedetail.OrderBy(e => e.forminstanceid).FirstOrDefault();
            theForminstancedetail.forminstanceid = theForminstance.forminstanceid;
            theForminstancedetail.subpayrollgroupid = 0;
            theForminstancedetail.subformcode = "sf_wf_LeaveDataInput";
            theForminstancedetail.rowindex = 1;
            theForminstancedetail.userid = 28;
            theForminstancedetail.tablecode = "empleavedata";
            theForminstancedetail.updated = true;
            theForminstancedetail.fieldcode = "leavehours";
            theForminstancedetail.newvalue = theLeaveApplication.leavehours.ToString();
            db.forminstancedetail.Add(theForminstancedetail);
            await db.SaveChangesAsync();

            theForminstancedetail = new forminstancedetail();//db.forminstancedetail.OrderBy(e => e.forminstanceid).FirstOrDefault();
            theForminstancedetail.forminstanceid = theForminstance.forminstanceid;
            theForminstancedetail.subpayrollgroupid = 0;
            theForminstancedetail.subformcode = "sf_wf_LeaveDataInput";
            theForminstancedetail.rowindex = 1;
            theForminstancedetail.userid = 28;
            theForminstancedetail.tablecode = "empleavedata";
            theForminstancedetail.updated = true;
            theForminstancedetail.fieldcode = "leavetodate";
            theForminstancedetail.newvalue = theLeaveApplication.leavetodate.ToString();
            db.forminstancedetail.Add(theForminstancedetail);
            await db.SaveChangesAsync();

            theForminstancedetail = new forminstancedetail();//db.forminstancedetail.OrderBy(e => e.forminstanceid).FirstOrDefault();
            theForminstancedetail.forminstanceid = theForminstance.forminstanceid;
            theForminstancedetail.subpayrollgroupid = 0;
            theForminstancedetail.subformcode = "sf_wf_LeaveDataInput";
            theForminstancedetail.rowindex = 1;
            theForminstancedetail.userid = 28;
            theForminstancedetail.tablecode = "empleavedata";
            theForminstancedetail.updated = true;
            theForminstancedetail.fieldcode = "leavetotime";
            theForminstancedetail.newvalue = theLeaveApplication.leavetotime.ToString();
            db.forminstancedetail.Add(theForminstancedetail);
            await db.SaveChangesAsync();

            theForminstancedetail = new forminstancedetail();//db.forminstancedetail.OrderBy(e => e.forminstanceid).FirstOrDefault();
            theForminstancedetail.forminstanceid = theForminstance.forminstanceid;
            theForminstancedetail.subpayrollgroupid = 0;
            theForminstancedetail.subformcode = "sf_wf_LeaveDataInput";
            theForminstancedetail.rowindex = 1;
            theForminstancedetail.userid = 28;
            theForminstancedetail.tablecode = "empleavedata";
            theForminstancedetail.updated = true;
            theForminstancedetail.fieldcode = "notes";
            theForminstancedetail.newvalue = theLeaveApplication.notes;
            db.forminstancedetail.Add(theForminstancedetail);
            await db.SaveChangesAsync();

            theForminstancedetail = new forminstancedetail();//db.forminstancedetail.OrderBy(e => e.forminstanceid).FirstOrDefault();
            theForminstancedetail.forminstanceid = theForminstance.forminstanceid;
            theForminstancedetail.subpayrollgroupid = 0;
            theForminstancedetail.subformcode = "sf_wf_LeaveDataInput";
            theForminstancedetail.rowindex = 1;
            theForminstancedetail.userid = 28;
            theForminstancedetail.tablecode = "empleavedata";
            theForminstancedetail.updated = true;
            theForminstancedetail.fieldcode = "prebirthdate";
            if (jsonContent.Contains("\"" + "prebirthdate" + "\":"))
            {
                theForminstancedetail.newvalue = theLeaveApplication.prebirthdate.ToString();
            }
            else
            {
                theForminstancedetail.newvalue = DateTime.UtcNow.ToString();
            }
            
            db.forminstancedetail.Add(theForminstancedetail);
            await db.SaveChangesAsync();

            theForminstancedetail = new forminstancedetail();//db.forminstancedetail.OrderBy(e => e.forminstanceid).FirstOrDefault();
            theForminstancedetail.forminstanceid = theForminstance.forminstanceid;
            theForminstancedetail.subpayrollgroupid = 0;
            theForminstancedetail.subformcode = "sf_wf_LeaveDataInput";
            theForminstancedetail.rowindex = 1;
            theForminstancedetail.userid = 28;
            theForminstancedetail.tablecode = "empleavedata";
            theForminstancedetail.updated = true;
            theForminstancedetail.fieldcode = "referencedays";
            theForminstancedetail.newvalue = theLeaveApplication.referencedays.ToString();
            db.forminstancedetail.Add(theForminstancedetail);
            await db.SaveChangesAsync();

            theForminstancedetail = new forminstancedetail();//db.forminstancedetail.OrderBy(e => e.forminstanceid).FirstOrDefault();
            theForminstancedetail.forminstanceid = theForminstance.forminstanceid;
            theForminstancedetail.subpayrollgroupid = 0;
            theForminstancedetail.subformcode = "sf_wf_LeaveDataInput";
            theForminstancedetail.rowindex = 1;
            theForminstancedetail.userid = 28;
            theForminstancedetail.tablecode = "empleavedata";
            theForminstancedetail.updated = true;
            theForminstancedetail.fieldcode = "specifydate";
            theForminstancedetail.newvalue = theLeaveApplication.specifydate.ToString();
            db.forminstancedetail.Add(theForminstancedetail);
            await db.SaveChangesAsync();

            theForminstancedetail = new forminstancedetail();//db.forminstancedetail.OrderBy(e => e.forminstanceid).FirstOrDefault();
            theForminstancedetail.forminstanceid = theForminstance.forminstanceid;
            theForminstancedetail.subpayrollgroupid = 0;
            theForminstancedetail.subformcode = "sf_wf_LeaveDataInput";
            theForminstancedetail.rowindex = 1;
            theForminstancedetail.userid = 28;
            theForminstancedetail.tablecode = "empleavedata";
            theForminstancedetail.updated = true;
            theForminstancedetail.fieldcode = "todateafternoon";
            theForminstancedetail.newvalue = theLeaveApplication.todateafternoon.ToString();
            db.forminstancedetail.Add(theForminstancedetail);
            await db.SaveChangesAsync();

            theForminstancedetail = new forminstancedetail();//db.forminstancedetail.OrderBy(e => e.forminstanceid).FirstOrDefault();
            theForminstancedetail.forminstanceid = theForminstance.forminstanceid;
            theForminstancedetail.subpayrollgroupid = 0;
            theForminstancedetail.subformcode = "sf_wf_LeaveDataInput";
            theForminstancedetail.rowindex = 1;
            theForminstancedetail.userid = 28;
            theForminstancedetail.tablecode = "empleavedata";
            theForminstancedetail.updated = true;
            theForminstancedetail.fieldcode = "todatemorning";
            theForminstancedetail.newvalue = theLeaveApplication.todatemorning.ToString();
            db.forminstancedetail.Add(theForminstancedetail);
            await db.SaveChangesAsync();

            empposition theEmpposition = db.empposition.Find(theLeaveApplication.empid);
            if (theEmpposition == null)
            {
                return NotFound();
            }
            empposition theEmppositionS = db.empposition.Find(theEmpposition.supervisorempid);

            workflowinstancedetail theWorkflowinstancedetail = new workflowinstancedetail();
            theWorkflowinstancedetail.workflowinstanceid = theWorkflowinstance.workflowinstanceid;
            theWorkflowinstancedetail.approver = theEmppositionS.empid;
            theWorkflowinstancedetail.approvalpositioncode = theEmppositionS.positioncode;
            theWorkflowinstancedetail.positionid = theEmpposition.positionid;
            
            
            db.workflowinstancedetail.Add(theWorkflowinstancedetail);
            await db.SaveChangesAsync();

            
            empleavedetail_ongoing theempleavedetail_ongoing = JsonConvert.DeserializeObject<empleavedetail_ongoing>(jsonContent);
            Type type = theempleavedetail_ongoing.GetType();
            PropertyInfo[] properties = type.GetProperties();
            theempleavedetail_ongoing.forminstanceid = theForminstance.forminstanceid;
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
            }


            WFData theData = new WFData();
            theData.forminstanceid = theForminstance.forminstanceid;
            theData.workflowinstanceid = theWorkflowinstance.workflowinstanceid;
            theData.empid = theLeaveApplication.empid;
            theData.leavedays = theLeaveApplication.leavedays;
            theData.leavefromdate = theLeaveApplication.leavefromdate;
            theData.leavetodate = theLeaveApplication.leavetodate;
            theData.starttime = theWorkflowinstancedetail.starttime;
            theData.workflowstatus = theWorkflowinstance.workflowstatus;
            theData.workflowstep = theWorkflowinstancedetail.workflowstep;
            
            return CreatedAtRoute("DefaultApi", null, theData);

        }
       
    }
}