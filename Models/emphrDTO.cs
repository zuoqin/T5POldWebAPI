using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T5PWebAPI.Models
{
    public class emphrDTO
    {
        public int empid { get; set; }
        public string empcode { get; set; }
        public int payrollgroupid { get; set; }
        public string pinyin { get; set; }
        public string english { get; set; }
    }
}