using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace T5PWebAPI.Models
{
    [Table("empposition")]
    public class empposition
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int empid { get; set; }
        public string positioncode { get; set; }
        //public Nullable<System.DateTime> positionstartdate { get; set; }
        public DateTime? positionstartdate { get; set; }
        public int supervisorempid { get; set; }
        public string title_english { get; set; }
        public string title_chinese { get; set; }
        public string title_big5 { get; set; }
        public string jobdescription { get; set; }
        public string changereason { get; set; }
        public string changenote { get; set; }
        public string positioncode1 { get; set; }
        //public Nullable<System.DateTime> positionstartdate1 { get; set; }
        public DateTime? positionstartdate1 { get; set; }
        public int supervisorempid1 { get; set; }
        public string title1_english { get; set; }
        public string title1_chinese { get; set; }
        public string title1_big5 { get; set; }
        public string jobdescription1 { get; set; }
        public string changereason1 { get; set; }
        public string changenote1 { get; set; }
        public string positioncode2 { get; set; }
        //public Nullable<System.DateTime> positionstartdate2 { get; set; }
        public DateTime? positionstartdate2 { get; set; }
        public int supervisorempid2 { get; set; }
        public string title2_english { get; set; }
        public string title2_chinese { get; set; }
        public string title2_big5 { get; set; }
        public string jobdescription2 { get; set; }
        public string changereason2 { get; set; }
        public string changenote2 { get; set; }
        public string approvalposition { get; set; }
        public bool assistant { get; set; }
        public bool assistant1 { get; set; }
        public bool assistant2 { get; set; }
        public int positionid { get; set; }
        public int positionid1 { get; set; }
        public int positionid2 { get; set; }
        public string title_japanese { get; set; }
        public string title1_japanese { get; set; }
        public string title2_japanese { get; set; }

    }
}