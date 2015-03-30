using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T5PWebAPI.Models
{
    public class empselfDTO
    {
        public int empid { get; set; }
        public double @base { get; set; }
        public string age { get; set; }
        public Nullable<System.DateTime> dateofissue1 { get; set; }
        public Nullable<System.DateTime> dateofexpiry1 { get; set; }
        public string passport1 { get; set; }
        public string passport2 { get; set; }
        public Nullable<System.DateTime> dateofissue2 { get; set; }
        public Nullable<System.DateTime> dateofexpiry2 { get; set; }
        public string passport3 { get; set; }
        public Nullable<System.DateTime> dateofissue3 { get; set; }
        public Nullable<System.DateTime> dateofexpiry3 { get; set; }
        public bool Compr_insurance { get; set; }
        public string empcategory { get; set; }
        public double actualworkdays { get; set; }
        public emp_sick_cur EmpSickCur { get; set; }
        public empanlv Empanlv { get; set; }
    }
}