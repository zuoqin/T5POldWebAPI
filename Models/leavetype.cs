using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace T5PWebAPI.Models
{
    [Table("leavetype")]
    public partial class leavetype
    {
        public int payrollgroupid { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string leavecode { get; set; }
        public string english { get; set; }
        public string chinese { get; set; }
        public string big5 { get; set; }
        public string category { get; set; }
        public string japanese { get; set; }
        public bool exclude_holiday { get; set; }
        public int leaveapppolicy { get; set; }
        public bool exclude_weekend { get; set; }
        public string exclude_weekend_not_apply_to { get; set; }
        public string exclude_holiday_not_apply_to { get; set; }
        public string imgfile { get; set; }
        public bool orgcalendarshow { get; set; }
        public bool apply_to_each_day { get; set; }
        public bool exclude_ph { get; set; }
        public string exclude_ph_not_apply_to { get; set; }
    }

}