using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace T5PWebAPI.Models
{
    [Table("position")]
    public class position
    {
        public string positioncode { get; set; }
        public string english { get; set; }
        public string chinese { get; set; }
        public string big5 { get; set; }
        public string jd { get; set; }
        public string japanese { get; set; }
        public int orgid { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int positionid { get; set; }
        public string corpjobcode { get; set; }
        public int reporttoposition { get; set; }
        public int nextposition1 { get; set; }
        public int nextposition2 { get; set; }
        public int nextposition3 { get; set; }
        public string localtitle_english { get; set; }
        public string localtitle_chinese { get; set; }
        public string localtitle_big5 { get; set; }
        public string localtitle_japanese { get; set; }
        public string externaltitle_english { get; set; }
        public string externaltitle_chinese { get; set; }
        public string externaltitle_big5 { get; set; }
        public string externaltitle_japanese { get; set; }
        public int budgetheadcount { get; set; }
        public int actualheadcount { get; set; }
        public bool approval { get; set; }
    }
}