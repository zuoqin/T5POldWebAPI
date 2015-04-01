using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace T5PWebAPI.Models
{
    public class forminstance
    {
        public string applicationtype { get; set; }
        public double applicationversion { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int forminstanceid { get; set; }
        public int workflowinstanceid { get; set; }
        public int payrollgroupid { get; set; }
        public string formcode { get; set; }
        public byte formstatus { get; set; }
        public int applicant { get; set; }
        public Nullable<System.DateTime> submittime { get; set; }
        public int delegateapplicant { get; set; }
        public string formparameters { get; set; }
        public string rowflags { get; set; }
        public string workflowinfo_english { get; set; }
        public string workflowinfo_chinese { get; set; }
        public string workflowinfo_big5 { get; set; }
        public string workflowinfo_japanese { get; set; }
        public bool submitall { get; set; }
        public string otherpara { get; set; }
    }
}