using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace T5PWebAPI.Models
{
    class LeaveApplication
    {
        //public int empid;
        public int empid { get; set; }
        public string leavecode { get; set; }
        public float leavedays { get; set; }
        public DateTime leavefromdate { get; set; }
        public DateTime leavefromtime { get; set; }
        public float leavehours { get; set; }
        public DateTime leavetodate { get; set; }
        public DateTime leavetotime { get; set; }
        public string notes { get; set; }
        public DateTime prebirthdate { get; set; }
        public float referencedays { get; set; }
        public DateTime specifydate { get; set; }
        public float todateafternoon { get; set; }
        public float todatemorning { get; set; }
    }

    [Table("empanlv")]
    public class empanlv
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int empid { get; set; }
        public int total_year_index { get; set; }
        public int year_index { get; set; }
        /// <summary>
        /// Year begin date
        /// 年度开始日期
        /// </summary>
        public Nullable<System.DateTime> year_begin_date { get; set; }
        /// <summary>
        /// Annual Leave Year End
        /// 年假年结束日
        /// </summary>
        public Nullable<System.DateTime> year_end_date { get; set; }
        /// <summary>
        /// Month Begin Date
        /// 月开始日
        /// </summary>
        public Nullable<System.DateTime> month_begin_date { get; set; }
        /// <summary>
        /// Month End Date
        /// 月末日
        /// </summary>
        public System.DateTime month_end_date { get; set; }
        /// <summary>
        /// Annual Leave Type
        /// 年假类型
        /// </summary>
        public byte anlvtype { get; set; }
        /// <summary>
        /// Annual Leave Policy Class
        /// 年假政策类别
        /// </summary>
        public string anlvcalcclass { get; set; }
        /// <summary>
        /// Month Full Date
        /// 新进员工月度满勤日
        /// </summary>
        public Nullable<System.DateTime> month_full_date { get; set; }
        /// <summary>
        /// Is New Staff?
        /// 是否新员工
        /// </summary>
        public bool is_new_staff { get; set; }
        /// <summary>
        /// Is Quit Staff?
        /// 是否离职员工
        /// </summary>
        public bool is_quit_staff { get; set; }
        public bool is_year_begin { get; set; }
        public bool is_year_end { get; set; }
        public decimal diff { get; set; }
        public decimal year0_prev { get; set; }
        public decimal year1_prev { get; set; }
        public decimal year2_prev { get; set; }
        public decimal year3_prev { get; set; }
        public decimal year4_prev { get; set; }
        public decimal year5_prev { get; set; }
        public decimal exprv_prev { get; set; }
        public decimal total_prev { get; set; }
        public decimal unlock_occur { get; set; }
        public Nullable<System.DateTime> unlock_effectivedate { get; set; }
        public decimal lock_occur { get; set; }
        public Nullable<System.DateTime> lock_effectivedate { get; set; }
        public decimal unlock_canuse_occur { get; set; }
        public decimal lock_canuse_occur { get; set; }
        public decimal adjust_canuse_occur { get; set; }
        public decimal year0_canuse_occur { get; set; }
        public decimal year0_first { get; set; }
        public decimal year1_first { get; set; }
        public decimal year2_first { get; set; }
        public decimal year3_first { get; set; }
        public decimal year4_first { get; set; }
        public decimal year5_first { get; set; }
        public decimal exprv_first { get; set; }
        public decimal total_first { get; set; }
        public decimal year0_used { get; set; }
        public decimal year1_used { get; set; }
        public decimal year2_used { get; set; }
        public decimal year3_used { get; set; }
        public decimal year4_used { get; set; }
        public decimal year5_used { get; set; }
        public decimal exprv_used { get; set; }
        public decimal total_used { get; set; }
        public decimal year0_current { get; set; }
        public decimal year1_current { get; set; }
        public decimal year2_current { get; set; }
        public decimal year3_current { get; set; }
        public decimal year4_current { get; set; }
        public decimal year5_current { get; set; }
        public decimal exprv_current { get; set; }
        public decimal total_current { get; set; }
        public decimal month_onlyoccur { get; set; }
        public decimal quit_yeartotal { get; set; }
        public decimal quit_diff { get; set; }
        public decimal quit_balance { get; set; }
        public decimal overdraw_balance { get; set; }
        public decimal expire_balance { get; set; }
        public decimal expire_yearend { get; set; }
        public decimal expire_thismonth { get; set; }
        public decimal year0_next { get; set; }
        public decimal year1_next { get; set; }
        public decimal year2_next { get; set; }
        public decimal year3_next { get; set; }
        public decimal year4_next { get; set; }
        public decimal year5_next { get; set; }
        public decimal exprv_next { get; set; }
        public decimal total_next { get; set; }
        public decimal canceldays { get; set; }
        public Nullable<System.DateTime> calc_time { get; set; }
        public decimal beforechange { get; set; }
        public decimal afterchange { get; set; }
        public Nullable<System.DateTime> effectivedate { get; set; }
        public Nullable<System.DateTime> neweffectivedate { get; set; }
        public decimal dx { get; set; }
        public decimal dused { get; set; }
        public decimal dxx { get; set; }
        public double serviceyear { get; set; }
        public decimal workhour { get; set; }
        public decimal accrual { get; set; }
        public decimal adjust_canuse_cur { get; set; }
        public decimal adjust_canuse_used { get; set; }
        public decimal adjust_canuse_next { get; set; }
        public decimal ytd_taken { get; set; }
        public decimal ytd_entitlement { get; set; }
        public decimal year_nottaken { get; set; }
        public decimal year_entitlement { get; set; }
        public int processuser { get; set; }
        public Nullable<System.DateTime> processdate { get; set; }
        public decimal month_adl_taken { get; set; }
        public decimal ytd_adl_taken { get; set; }
        public decimal year_adl_taken { get; set; }
        public decimal month_cfd_carried { get; set; }
        public decimal ytd_cfd_carried { get; set; }
        public decimal year_cfd_carried { get; set; }
        public decimal month_cfd_taken { get; set; }
        public decimal ytd_cfd_taken { get; set; }
        public decimal year_cfd_taken { get; set; }
        public decimal c_diff { get; set; }
        public decimal c_year0_prev { get; set; }
        public decimal c_year1_prev { get; set; }
        public decimal c_year2_prev { get; set; }
        public decimal c_year3_prev { get; set; }
        public decimal c_year4_prev { get; set; }
        public decimal c_year5_prev { get; set; }
        public decimal c_exprv_prev { get; set; }
        public decimal c_total_prev { get; set; }
        public decimal c_unlock_occur { get; set; }
        public Nullable<System.DateTime> c_unlock_effectivedate { get; set; }
        public decimal c_lock_occur { get; set; }
        public Nullable<System.DateTime> c_lock_effectivedate { get; set; }
        public decimal c_unlock_canuse_occur { get; set; }
        public decimal c_lock_canuse_occur { get; set; }
        public decimal c_adjust_canuse_occur { get; set; }
        public decimal c_year0_canuse_occur { get; set; }
        public decimal c_year0_first { get; set; }
        public decimal c_year1_first { get; set; }
        public decimal c_year2_first { get; set; }
        public decimal c_year3_first { get; set; }
        public decimal c_year4_first { get; set; }
        public decimal c_year5_first { get; set; }
        public decimal c_exprv_first { get; set; }
        public decimal c_total_first { get; set; }
        public decimal c_year0_used { get; set; }
        public decimal c_year1_used { get; set; }
        public decimal c_year2_used { get; set; }
        public decimal c_year3_used { get; set; }
        public decimal c_year4_used { get; set; }
        public decimal c_year5_used { get; set; }
        public decimal c_exprv_used { get; set; }
        public decimal c_total_used { get; set; }
        public decimal c_year0_current { get; set; }
        public decimal c_year1_current { get; set; }
        public decimal c_year2_current { get; set; }
        public decimal c_year3_current { get; set; }
        public decimal c_year4_current { get; set; }
        public decimal c_year5_current { get; set; }
        public decimal c_exprv_current { get; set; }
        public decimal c_total_current { get; set; }
        public decimal c_month_onlyoccur { get; set; }
        public decimal c_quit_yeartotal { get; set; }
        public decimal c_quit_diff { get; set; }
        public decimal c_quit_balance { get; set; }
        public decimal c_overdraw_balance { get; set; }
        public decimal c_expire_balance { get; set; }
        public decimal c_expire_yearend { get; set; }
        public decimal c_expire_thismonth { get; set; }
        public decimal c_year0_next { get; set; }
        public decimal c_year1_next { get; set; }
        public decimal c_year2_next { get; set; }
        public decimal c_year3_next { get; set; }
        public decimal c_year4_next { get; set; }
        public decimal c_year5_next { get; set; }
        public decimal c_exprv_next { get; set; }
        public decimal c_total_next { get; set; }
        public Nullable<System.DateTime> c_exprv_expiredate { get; set; }
        public decimal f_diff { get; set; }
        public decimal f_year0_prev { get; set; }
        public decimal f_year1_prev { get; set; }
        public decimal f_year2_prev { get; set; }
        public decimal f_year3_prev { get; set; }
        public decimal f_year4_prev { get; set; }
        public decimal f_year5_prev { get; set; }
        public decimal f_exprv_prev { get; set; }
        public decimal f_total_prev { get; set; }
        public decimal f_unlock_occur { get; set; }
        public Nullable<System.DateTime> f_unlock_effectivedate { get; set; }
        public decimal f_lock_occur { get; set; }
        public Nullable<System.DateTime> f_lock_effectivedate { get; set; }
        public decimal f_unlock_canuse_occur { get; set; }
        public decimal f_lock_canuse_occur { get; set; }
        public decimal f_adjust_canuse_occur { get; set; }
        public decimal f_year0_canuse_occur { get; set; }
        public decimal f_year0_first { get; set; }
        public decimal f_year1_first { get; set; }
        public decimal f_year2_first { get; set; }
        public decimal f_year3_first { get; set; }
        public decimal f_year4_first { get; set; }
        public decimal f_year5_first { get; set; }
        public decimal f_exprv_first { get; set; }
        public decimal f_total_first { get; set; }
        public decimal f_year0_used { get; set; }
        public decimal f_year1_used { get; set; }
        public decimal f_year2_used { get; set; }
        public decimal f_year3_used { get; set; }
        public decimal f_year4_used { get; set; }
        public decimal f_year5_used { get; set; }
        public decimal f_exprv_used { get; set; }
        public decimal f_total_used { get; set; }
        public decimal f_year0_current { get; set; }
        public decimal f_year1_current { get; set; }
        public decimal f_year2_current { get; set; }
        public decimal f_year3_current { get; set; }
        public decimal f_year4_current { get; set; }
        public decimal f_year5_current { get; set; }
        public decimal f_exprv_current { get; set; }
        public decimal f_total_current { get; set; }
        public decimal f_month_onlyoccur { get; set; }
        public decimal f_quit_yeartotal { get; set; }
        public decimal f_quit_diff { get; set; }
        public decimal f_quit_balance { get; set; }
        public decimal f_overdraw_balance { get; set; }
        public decimal f_expire_balance { get; set; }
        public decimal f_expire_yearend { get; set; }
        public decimal f_expire_thismonth { get; set; }
        public decimal f_year0_next { get; set; }
        public decimal f_year1_next { get; set; }
        public decimal f_year2_next { get; set; }
        public decimal f_year3_next { get; set; }
        public decimal f_year4_next { get; set; }
        public decimal f_year5_next { get; set; }
        public decimal f_exprv_next { get; set; }
        public decimal f_total_next { get; set; }
        public Nullable<System.DateTime> f_exprv_expiredate { get; set; }
        public Nullable<decimal> ytd_balance { get; set; }
        public Nullable<decimal> year_balance { get; set; }
        public Nullable<decimal> month_taken { get; set; }
        public Nullable<decimal> year_taken { get; set; }
        public Nullable<decimal> year_adl_out { get; set; }
        public Nullable<decimal> year_cfd_in { get; set; }
        public Nullable<decimal> year_cfd_cancel { get; set; }
        public Nullable<decimal> exprv_expired { get; set; }
        public decimal next_year_entitlement { get; set; }
        public decimal next_year_taken { get; set; }
        public decimal next_year_balance { get; set; }
        public Nullable<System.DateTime> cfd_expiredate { get; set; }
        public Nullable<System.DateTime> next_cfd_expiredate { get; set; }
        public Nullable<decimal> cfd_rest { get; set; }
        public Nullable<decimal> cfd_expire { get; set; }
    }
}