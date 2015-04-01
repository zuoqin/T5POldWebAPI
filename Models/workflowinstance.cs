using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace T5PWebAPI.Models
{
    public class workflowinstance
    {
        public string applicationtype { get; set; }
        public double applicationversion { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int workflowinstanceid { get; set; }
        public int forminstanceid { get; set; }
        public string workflowcode { get; set; }
        public byte workflowstatus { get; set; }
        public string errormsg { get; set; }
        public int applicant { get; set; }
        public Nullable<System.DateTime> submittime { get; set; }
        public Nullable<System.DateTime> endtime { get; set; }
        public bool sendmail { get; set; }
        public int delegateapplicant { get; set; }
        public bool skipnextstep { get; set; }
        public string monitorempid { get; set; }
        public bool monitorsendmail { get; set; }
        public bool monitorapp { get; set; }
    }
}