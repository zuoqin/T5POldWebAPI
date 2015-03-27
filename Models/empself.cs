using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace T5PWebAPI.Models
{
    [Table("empself")]
    public class empself
    {
        /// <summary>
        /// Employee ID
        /// 员工序号
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int empid { get; set; }
        /// <summary>
        /// Age
        /// 年龄
        /// </summary>
        public string age { get; set; }
        /// <summary>
        /// Base Salary
        /// 基本薪资
        /// </summary>
        public double @base { get; set; }
        /// <summary>
        /// Visa No. [USA]
        /// 签证号码[USA]
        /// </summary>
        public string passport1 { get; set; }
        /// <summary>
        /// Date of Issue [USA]
        /// 签发日期[USA]
        /// </summary>
        public Nullable<System.DateTime> dateofissue1 { get; set; }
        /// <summary>
        /// Date of Expiry [USA]
        /// 有效期至[USA]
        /// </summary>
        public Nullable<System.DateTime> dateofexpiry1 { get; set; }
        /// <summary>
        /// Visa No. [Europe]
        /// 签证号码[Europe]
        /// </summary>
        public string passport2 { get; set; }
        /// <summary>
        /// Date of Issue [Europe]
        /// 有效期至[Europe]
        /// </summary>
        public Nullable<System.DateTime> dateofissue2 { get; set; }
        /// <summary>
        /// Date of Expiry [Europe]
        /// 签发日期[Europe]
        /// </summary>
        public Nullable<System.DateTime> dateofexpiry2 { get; set; }
        /// <summary>
        /// Visa No. [U.K.]
        /// 签证号码[U.K.]
        /// </summary>
        public string passport3 { get; set; }
        /// <summary>
        /// Date of Issue [U.K.]
        /// 签发日期[U.K.]
        /// </summary>
        public Nullable<System.DateTime> dateofissue3 { get; set; }
        /// <summary>
        /// Date of Expiry [U.K.]
        /// 有效期至[U.K.]
        /// </summary>
        public Nullable<System.DateTime> dateofexpiry3 { get; set; }
        /// <summary>
        /// Comp. Insurance
        /// 是否缴纳综合保险
        /// </summary>
        public bool Compr_insurance { get; set; }
        /// <summary>
        /// Employee Category
        /// 员工类别
        /// </summary>
        public string empcategory { get; set; }
        /// <summary>
        /// Course round flag
        /// 课程计划表
        /// </summary>
        public int courseroundflag { get; set; }
        /// <summary>
        /// Actual Work Days
        /// 实际工作天数
        /// </summary>
        public double actualworkdays { get; set; }
        /// <summary>
        /// Roster Work Days
        /// 排班天数
        /// </summary>
        public double rosterworkdays { get; set; }
        /// <summary>
        /// Integration Actual Work
        /// 综合实际工作小时
        /// </summary>
        public int integratedhour { get; set; }
        /// <summary>
        /// Late Arrive Times
        /// 迟到次数
        /// </summary>
        public int latetimes { get; set; }
        /// <summary>
        /// Late Hours
        /// 迟到时间
        /// </summary>
        public double latehour { get; set; }
        /// <summary>
        /// Early Leave Time
        /// 早退次数
        /// </summary>
        public int earlyleavetimes { get; set; }
        /// <summary>
        /// Early Leaving Hour
        /// 早退小时
        /// </summary>
        public double earlyleavehour { get; set; }
        /// <summary>
        /// Absent Times
        /// 缺勤次数
        /// </summary>
        public int absenttimes { get; set; }
        /// <summary>
        /// Absent Hour
        /// 缺勤小时
        /// </summary>
        public double absenthour { get; set; }
        /// <summary>
        /// Over Time Work Time
        /// 加班次数
        /// </summary>
        public int ottimes { get; set; }
        /// <summary>
        /// Actual Work Hour
        /// 实际工时
        /// </summary>
        public double actualworkhour { get; set; }
        /// <summary>
        /// Calendar days
        /// 日历天数
        /// </summary>
        public int calendardays { get; set; }
        /// <summary>
        /// Special Period Employee
        /// 三期人员
        /// </summary>
        public bool specialperiod { get; set; }
        /// <summary>
        /// 418 Effective Date
        /// 418生效日
        /// </summary>
        public Nullable<System.DateTime> effectivedate418 { get; set; }
        /// <summary>
        /// Varibale Rate (EURO)
        /// 变化率 (EURO)
        /// </summary>
        public double fxrateuro { get; set; }
        /// <summary>
        /// Yearly Target Bonus
        /// 年度目标奖金
        /// </summary>
        public double yeartargetbonus { get; set; }
        /// <summary>
        /// Soeicty Month
        /// 社会工作月数
        /// </summary>
        public int societysvmonth { get; set; }
        /// <summary>
        /// Varibale Rate (GBP)
        /// 
        /// </summary>
        public double fxrate { get; set; }
        // Navigation property
        [ForeignKey("empid")]
        public virtual emp_sick_cur EmpSickCur { get; set; }
        [ForeignKey("empid")]
        public virtual empanlv Empanlv { get; set; }
    
    }
}