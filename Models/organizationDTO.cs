using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T5PWebAPI.Models
{
    public class organizationDTO
    {
        public int orglevel { get; set; }
        public string orgcode { get; set; }
        public string english { get; set; }
        public string chinese { get; set; }
        public string big5 { get; set; }
        public string parentorg { get; set; }
        public string japanese { get; set; }
        public int orgid { get; set; }
        public int parentorgid { get; set; }

    }
}