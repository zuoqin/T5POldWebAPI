using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T5PWebAPI.Models
{
    public class emppositionDTO
    {
        public int empid { get; set; }
        public string positioncode { get; set; }
        public Nullable<System.DateTime> positionstartdate { get; set; }
        public int supervisorempid { get; set; }
    }
}