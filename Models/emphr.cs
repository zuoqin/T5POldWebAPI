using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace T5PWebAPI.Models
{
    [Table("emphr")]
    public class emphr
    {
        /// <summary>
        /// Employee ID
        /// 员工序号
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int empid { get; set; }
        /// <summary>
        /// Employee Code
        /// 员工编码
        /// </summary>
        public string empcode { get; set; }
        /// <summary>
        /// Payroll Group ID
        /// 薪资组序号
        /// </summary>
        public int payrollgroupid { get; set; }
        /// <summary>
        /// Pinyin
        /// 拼音
        /// </summary>
        public string pinyin { get; set; }
        /// <summary>
        /// Employee Name
        /// </summary>
        public string english { get; set; }
        public string chinese { get; set; }
        public string big5 { get; set; }
        public string servicestatus { get; set; }
        public string hrstatus { get; set; }
        public string address { get; set; }
        public string note { get; set; }
        public string province { get; set; }
        public string city { get; set; }
        public string zipcode { get; set; }
        public string raddress { get; set; }
        public string rzipcode { get; set; }
        public string ethnic { get; set; }
        public string nationality { get; set; }
        public string pid { get; set; }
        public Nullable<System.DateTime> birthday { get; set; }
        public string marital { get; set; }
        public string gender { get; set; }
        public string homephone { get; set; }
        public string mobile { get; set; }
        public string businessphone { get; set; }
        public string cemail { get; set; }
        public string pemail { get; set; }
        public string zippassword { get; set; }
        public string portrait { get; set; }
        public string party { get; set; }
        //public Nullable<System.DateTime> hiredate { get; set; }
        public DateTime? hiredate { get; set; }
        //public Nullable<System.DateTime> hirevalid { get; set; }
        public DateTime? hirevalid { get; set; }
        //public Nullable<System.DateTime> hiresociety { get; set; }
        public DateTime? hiresociety { get; set; }
        public bool inprobation { get; set; }
        //public Nullable<System.DateTime> probationenddate { get; set; }
        public DateTime? probationenddate { get; set; }
        public string hiresource { get; set; }
        public string in_from { get; set; }
        public string laborbook { get; set; }
        public string dangan { get; set; }
        public string huko { get; set; }
        public string ppnumber { get; set; }
        public string ppissuecountry { get; set; }
        //public Nullable<System.DateTime> ppexpiredate { get; set; }
        public DateTime? ppexpiredate { get; set; }
        public bool adminemp { get; set; }
        public bool saemp { get; set; }
        public bool tempemp { get; set; }
        public bool expatriate { get; set; }
        public bool laborworker { get; set; }
        public string agencycode { get; set; }
        public string agencyname { get; set; }
        public string contracttype { get; set; }
        //public Nullable<System.DateTime> contractstartdate { get; set; }
        public DateTime? contractstartdate { get; set; }
        //public Nullable<System.DateTime> contractenddate { get; set; }
        public DateTime? contractenddate { get; set; }
        public string contractnumber { get; set; }
        public string contractlocation { get; set; }
        public double monthmanagefee { get; set; }
        public double pensioncontribution { get; set; }
        public double hfcontribution { get; set; }
        public double medicalpercentage { get; set; }
        //public Nullable<System.DateTime> orgstart { get; set; }
        public DateTime? orgstart { get; set; }
        public string orgcode1 { get; set; }
        public string orgcode2 { get; set; }
        public string orgcode3 { get; set; }
        public string orgcode4 { get; set; }
        public string orgcode5 { get; set; }
        public string orgcode6 { get; set; }
        public string orgcode7 { get; set; }
        public string orgcode8 { get; set; }
        public string orgcode9 { get; set; }
        public string orgcode10 { get; set; }
        public string anlvcalcclass { get; set; }
        public string sickleaveclass { get; set; }
        public string surname { get; set; }
        public string christianname { get; set; }
        public string areacode { get; set; }
        public string spousename { get; set; }
        public string spousehkid { get; set; }
        public string spouseppnumber { get; set; }
        public string taxfileid { get; set; }
        public string spouseppissuecountry { get; set; }
        public string cleaveclass { get; set; }
        public string quitlieutype { get; set; }
        public string calendarcode { get; set; }
        public string atsbasicpolicy { get; set; }
        public string atsotpolicy { get; set; }
        public string cpfno { get; set; }
        public string sg_ethnic { get; set; }
        public string nricetype { get; set; }
        //public Nullable<System.DateTime> prbegindate { get; set; }
        public DateTime? prbegindate { get; set; }
        public string japanese { get; set; }
        public int rosterflag { get; set; }
        public string carlinecode { get; set; }
        public string mealtype { get; set; }
        public string gradecode { get; set; }
        public string levelcode { get; set; }
        public string titlecode { get; set; }
        public string skilllevelcode { get; set; }
        public string atslocation { get; set; }
        public int orgid { get; set; }
        public string companysickleaveclass { get; set; }
        public string salutation { get; set; }
        public string othername { get; set; }
        [ForeignKey("empid")]
        public virtual emp_sick_cur EmpSick { get; set; }
        [ForeignKey("empid")]
        public virtual empanlv Empanlv { get; set; }
    }
}