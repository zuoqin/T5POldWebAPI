using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T5PWebAPI.Models
{
    public class positionDTO
    {
        public string positioncode { get; set; }
        public string english { get; set; }
        public string chinese { get; set; }
        public int orgid { get; set; }
        public int positionid { get; set; }
    }
}