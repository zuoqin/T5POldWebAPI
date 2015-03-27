using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace T5PWebAPI.Models
{
    [Table("empleavedetail_ongoing")]
    public class empleavedetail_ongoing
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(Order = 10)]
        public long forminstanceid { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(Order = 20)]
        public int rowindex { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(Order = 30)]
        public int empid { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(Order = 40)]
        public string leavecode { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(Order = 50)]
        public System.DateTime? leavefromdate { get; set; }
        public System.DateTime? leavetodate { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(Order = 60)]
        public System.DateTime? leavefromtime { get; set; }
        public System.DateTime? leavetotime { get; set; }
        public bool fromdatemorning { get; set; }
        public bool fromdateafternoon { get; set; }
        public bool todatemorning { get; set; }
        public bool todateafternoon { get; set; }
        public double leavehours { get; set; }
        public double applyleavehours { get; set; }
        public double leavedays { get; set; }
        public double referencedays { get; set; }
        public DateTime? specifydate { get; set; }
        public DateTime? prebirthdate { get; set; }
        public string datetype { get; set; }
    }
}