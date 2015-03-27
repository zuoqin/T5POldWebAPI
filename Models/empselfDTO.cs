using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T5PWebAPI.Models
{
    public class empselfDTO
    {
        public int empid { get; set; }
        public string passport1 { get; set; }
        public emp_sick_cur EmpSickCur { get; set; }
        public empanlv Empanlv { get; set; }
    }
}