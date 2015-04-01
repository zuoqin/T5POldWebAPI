using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace T5PWebAPI.Models
{
    [Table("empleavedata")]
    public class empleavedata
    {
        public empleavedata()
        {
            attachment = "";
            cancelnote = "";
            sourcetype = "";
            requestid = "";
        }
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        //[Column(Order = 10)]
        public int empid { get; set; }
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        //[Column(Order = 20)]
        public string leavecode { get; set; }
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        //[Column(Order = 30)]
        public System.DateTime? leavefromdate { get; set; }
        public System.DateTime? leavetodate { get; set; }
        public double leavehours { get; set; }
        public bool paid { get; set; }
        public string notes { get; set; }
        public string requestid { get; set; }
        public double leavedays { get; set; }
        public System.DateTime? specifydate { get; set; }
        public byte paytype { get; set; }
        public bool post { get; set; }
        public bool paythisperiod { get; set; }
        public double allowance_base { get; set; }
        public double allowance { get; set; }
        public double paypercent { get; set; }
        public double deduction_base { get; set; }
        public double deduction { get; set; }
        public double referencedays { get; set; }
        public double actualusedays { get; set; }
        public bool statutory_anlv { get; set; }
        public double actualpaydays { get; set; }
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        //[Column(Order = 40)]
        public System.DateTime? leavefromtime { get; set; }
        public System.DateTime? leavetotime { get; set; }
        public bool leavesheet { get; set; }
        public bool atspost { get; set; }
        public double applyleavehours { get; set; }
        public byte flag { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long autoid { get; set; }
        public bool approved { get; set; }
        public int create_user { get; set; }
        public int workflowflag { get; set; }
        public double partialpaydays { get; set; }
        public bool fromdatemorning { get; set; }
        public bool fromdateafternoon { get; set; }
        public bool todatemorning { get; set; }
        public bool todateafternoon { get; set; }
        public System.DateTime? createdate { get; set; }
        public int moduser { get; set; }
        public System.DateTime? moddate { get; set; }
        public int processuser { get; set; }
        public System.DateTime? processdate { get; set; }
        public System.DateTime? prebirthdate { get; set; }
        public string attachment { get; set; }
        public bool cancel { get; set; }
        public int canceluser { get; set; }
        public System.DateTime? canceldate { get; set; }
        public string cancelnote { get; set; }
        public bool cancelapproved { get; set; }
        public int cancelapprover { get; set; }
        public System.DateTime? cancelapprovedate { get; set; }
        public System.DateTime? paydate { get; set; }
        public string sourcetype { get; set; }
    
    }
}