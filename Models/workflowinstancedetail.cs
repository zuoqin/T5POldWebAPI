using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace T5PWebAPI.Models
{
    public class WFData
    {
        public int workflowinstanceid { get; set; }
        public int forminstanceid { get; set; }
        public int empid { get; set; }
        public string leavetype_chinese { get; set; }
        public string leavetype_english { get; set; }
        public string leavetype_big5 { get; set; }
        public string leavetype_japanese { get; set; }
        public byte workflowstatus { get; set; }
        public int workflowstep { get; set; }
        public Nullable<System.DateTime> starttime { get; set; }
        public System.DateTime? leavefromdate { get; set; }
        public System.DateTime? leavetodate { get; set; }
        public double leavedays { get; set; }
        public string notes { get; set; }
    }
    [Table("workflowinstancedetail")]
    public class workflowinstancedetail
    {
        public workflowinstancedetail()
        {
            workflowstep = 1;
            starttime = DateTime.UtcNow;
            approvesn = Guid.NewGuid().ToString();
            stepstatus = 1;
            defaultemp = false;
            autoapprove = false;
            delegateapprove = false;
            approvalnote = "";
            allapprove = false;
            inconsult = false;
            consultquestion = "";
            isruntimeapprover = false;
            isnextruntimeapprover = false;
            nextallapprove = false;
            approvergroup = 0;
            groupallapprove = false;
            supervisorapprover = 0;
            supervisorapprove = false;
            skipall = false;
            monitor = 0;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(Order = 10)]
        public int workflowinstanceid { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(Order = 20)]
        public int workflowstep { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(Order = 30)]
        public int approver { get; set; }
        public int delegateapprover { get; set; }
        public string approvalpositioncode { get; set; }
        public int expireday { get; set; }
        public int informaheadday { get; set; }
        public bool informdaily { get; set; }
        public Nullable<System.DateTime> starttime { get; set; }
        public Nullable<System.DateTime> endtime { get; set; }
        public Nullable<System.DateTime> checktime { get; set; }
        public byte stepstatus { get; set; }
        public bool defaultemp { get; set; }
        public bool autoapprove { get; set; }
        public bool delegateapprove { get; set; }
        public string approvalnote { get; set; }
        public string approvesn { get; set; }
        public bool allapprove { get; set; }
        public bool inconsult { get; set; }
        public string consultquestion { get; set; }
        public bool isruntimeapprover { get; set; }
        public bool isnextruntimeapprover { get; set; }
        public bool nextallapprove { get; set; }
        public byte approvergroup { get; set; }
        public bool groupallapprove { get; set; }
        public int supervisorapprover { get; set; }
        public bool supervisorapprove { get; set; }
        public bool skipall { get; set; }
        public int monitor { get; set; }
        public int positionid { get; set; }
        [ForeignKey("workflowinstanceid")]
        public virtual workflowinstance Workflowinstance { get; set; }
    }
}