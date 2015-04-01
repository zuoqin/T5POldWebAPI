using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using T5PWebAPI.Models;

namespace T5PWebAPI.Controllers
{
    public class WorkflowinstancedetailController : ApiController
    {
        private T5PWebAPIContext db = new T5PWebAPIContext();
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
                theData.empid = pos.approver;
                theData.forminstanceid = pos.Workflowinstance.forminstanceid;
                theData.workflowinstanceid = pos.workflowinstanceid;
                //foreach (PropertyInfo property1 in properties1)
                //{
                //    var value = property1.GetValue(pos);
                //    property1.SetValue(theData, value);
                //}
                theList.Add(theData);
            }
            return theList.AsQueryable();
        }
        
        
        /*public IQueryable<workflowinstancedetail> Getworkflowinstancedetailbyapprover(int approver)
        {
            List<workflowinstancedetail> theList = new List<workflowinstancedetail>();
            PropertyInfo[] properties1 = typeof(workflowinstancedetail).GetProperties();
            foreach (var pos in db.workflowinstancedetail.OrderBy(c => c.workflowinstanceid).ThenBy(c => c.workflowstep).Where(c=>  (c.approver==approver & c.stepstatus == 1)   )  )
            {
                workflowinstancedetail theData = new workflowinstancedetail();

                foreach (PropertyInfo property1 in properties1)
                {
                    var value = property1.GetValue(pos);
                    property1.SetValue(theData, value);
                }
                theList.Add(theData);
            }
            return theList.AsQueryable();
        }*/


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


    }
}
