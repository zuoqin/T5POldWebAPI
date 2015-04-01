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
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using T5PWebAPI.Models;
using WebGrease.Css.Ast.Selectors;

namespace T5PWebAPI.Controllers
{
    public class EmployeeData
    {
        private T5PWebAPIContext db = new T5PWebAPIContext();
        public emphrDTO Emphr;
        public emp_sick_cur Empsick;
        public empanlv Empanlv;

        public bool SetData(emphr theEmphr=null)
        {
            if (theEmphr != null)
            {
                Emphr = new emphrDTO();
                PropertyInfo[] properties1 = typeof(emphrDTO).GetProperties();
                PropertyInfo[] properties2 = typeof(emphr).GetProperties();


                foreach (PropertyInfo property1 in properties1)
                {
                    PropertyInfo theProperty = Array.Find(properties2, p => p.Name.CompareTo(property1.Name) == 0);
                    var value = theProperty.GetValue(theEmphr);
                    property1.SetValue(Emphr, value);
                }                
            }
            if (theEmphr.Empanlv != null)
            {
                Empanlv = new empanlv();
                PropertyInfo[] properties1 = typeof(empanlv).GetProperties();
                PropertyInfo[] properties2 = typeof(empanlv).GetProperties();


                foreach (PropertyInfo property1 in properties1)
                {
                    PropertyInfo theProperty = Array.Find(properties2, p => p.Name.CompareTo(property1.Name) == 0);
                    var value = theProperty.GetValue(theEmphr.Empanlv);
                    property1.SetValue(Empanlv, value);
                }
            }
            if (theEmphr.EmpSick != null)
            {
                Empsick = new emp_sick_cur();
                PropertyInfo[] properties1 = typeof(emp_sick_cur).GetProperties();
                PropertyInfo[] properties2 = typeof(emp_sick_cur).GetProperties();


                foreach (PropertyInfo property1 in properties1)
                {
                    PropertyInfo theProperty = Array.Find(properties2, p => p.Name.CompareTo(property1.Name) == 0);
                    var value = theProperty.GetValue(theEmphr.EmpSick);
                    property1.SetValue(Empsick, value);
                }
            }
            return true;
        }
    }
    public class EmployeeController : ApiController
    {
        private T5PWebAPIContext db = new T5PWebAPIContext();
        // GET api/employee/5
        [ResponseType(typeof(EmployeeData))]
        public async Task<IHttpActionResult> GetEmployee(int id)
        {
            emphr theEmphr = await db.emphrs.Include(b => b.Empanlv).Include(b => b.EmpSick).SingleOrDefaultAsync(b => b.empid == id);
            if (theEmphr == null)
            {
                return NotFound();
            }
            //var json = new JavaScriptSerializer().Serialize(theEmphr);
            //File.WriteAllText( @"e:\\JSON.txt", json);
            EmployeeData theData = new EmployeeData();
            theData.SetData(theEmphr);
            return Ok(theData);
        }


        /// <summary>
        /// Adds new Leave application records
        /// Input: LeaveData application
        /// </summary>
        /// <param name="page">Page number of the data, default is 0</param>
        /// <param name="pageSize">Pagesize of the data, default is int.MaxValue</param>
        [ResponseType(typeof (WFData))]
        public async Task<IHttpActionResult> PostEmployee()
        {
            return Ok();
        }




    }
}
