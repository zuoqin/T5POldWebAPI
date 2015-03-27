using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T5PWebAPI.Models
{
    public class empleavedataDTO
    {
        public int empid { get; set; }
        public string leavecode { get; set; }
        public System.DateTime? leavefromdate { get; set; }
        public System.DateTime? leavetodate { get; set; }
        public System.DateTime? leavefromtime { get; set; }
        public System.DateTime? leavetotime { get; set; }
    }
}