using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace T5PWebAPI.Models
{
    public class empleavedata_cancel
    {
        /// <summary>
        /// 休假记录序号
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long leaveid { get; set; }
        /// <summary>
        /// 员工序号
        /// </summary>
        public int empid { get; set; }
        /// <summary>
        /// 休假类型
        /// </summary>
        public string leavecode { get; set; }
        /// <summary>
        /// 休假开始日
        /// </summary>
        public System.DateTime leavefromdate { get; set; }
        /// <summary>
        /// 休假结束日
        /// </summary>
        public System.DateTime leavetodate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public System.DateTime leavefromtime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public System.DateTime leavetotime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double leavehours { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double applyleavehours { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string notes { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string requestid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double leavedays { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? specifydate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double referencedays { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public byte paytype { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool post { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool fromdatemorning { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool fromdateafternoon { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool todatemorning { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool todateafternoon { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool org_approved { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int org_createuser { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? org_createdate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int org_moduser { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? org_moddate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int org_processuser { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? org_processdate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool cancel { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int canceluser { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? canceldate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string cancelnote { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool cancelapproved { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int cancelapprover { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? cancelapprovedate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sourcetype { get; set; }
    }
}