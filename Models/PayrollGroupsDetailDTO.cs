using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T5PWebAPI.Models
{
    public class PayrollGroupsDetailDTO
    {
        public int payrollgroupid { get; set; }
        public string english { get; set; }
        public string chinese { get; set; }
        public string big5 { get; set; }
        public string bank_english { get; set; }
        public string bank_chinese { get; set; }
        public string bank_big5 { get; set; }
        public string bankid { get; set; }
        public string taxnumber { get; set; }
        public bool use_gl { get; set; }
        public bool gl_reverse { get; set; }
        public bool use_costcenter { get; set; }
        public bool use_negative { get; set; }
        public bool daily_piece { get; set; }
        public bool use_piece_quality { get; set; }
        public bool use_piece_quantity { get; set; }
        public int pay_curyear { get; set; }
        public DateTime pay_curyearbegin { get; set; }
        public DateTime pay_curyearend { get; set; }
        public DateTime pay_curperiodbegin { get; set; }
        public DateTime pay_curperiodend { get; set; }
        public DateTime attbegin { get; set; }
        public DateTime attend { get; set; }
        public string restday { get; set; }
        public int gl_length { get; set; }
        public string gl_mask { get; set; }
        public string gl_sample { get; set; }
        public string gl_taxexpense { get; set; }
        public string gl_taxcoexpense { get; set; }
        public string gl_taxpayable { get; set; }
        public string gl_vacationaccrued { get; set; }
        public string gl_vacationexpense { get; set; }
        public string gl_vacationpayable { get; set; }
        public Double calendardays { get; set; }
        public Double workdays { get; set; }
        public Double dayspermonth { get; set; }
        public Double hoursperday { get; set; }
        public bool viewsalary { get; set; }
        public bool use_dailydata { get; set; }
        public bool includeholiday { get; set; }
        public bool UsePieceWork { get; set; }
        public bool usempf { get; set; }
        public Double mpfmaxbase { get; set; }
        public Double mpfminbase { get; set; }
        public Double mpfmaxday { get; set; }
        public Double mpfminday { get; set; }
        public Double mpfper { get; set; }
        public Double mpffreedays { get; set; }
        public Double mpfdelaydays { get; set; }
        public Double femaleretireage { get; set; }
        public Double maleretireage { get; set; }
        public bool use_new_ordinance { get; set; }
        public bool use_pay_general_holiday { get; set; }
        public bool use_pay_annual_leave { get; set; }
        public string statutory_anlv_overdraw_item { get; set; }
        public string company_anlv_overdraw_item { get; set; }
        public bool use_pay_lieu_notice { get; set; }
        public string ot_compare_field { get; set; }
        public Double ot_compare_factor { get; set; }
        public Byte dailywage_decimaltype { get; set; }
        public Byte mon_days_type { get; set; }
        public string mon_pay_field { get; set; }
        public int fixed_mon_days { get; set; }
        public Byte less_lieu_notice { get; set; }
        public Byte less_annual_leave { get; set; }
        public Byte less_other { get; set; }
        public Byte use_dailywage_sick { get; set; }
        public Byte use_dailywage_maternity { get; set; }
        public string sick_lv_ded_item { get; set; }
        public string maternity_lv_ded_item { get; set; }
        public string other_lv_ded_item { get; set; }
        public bool payyearoverdraw { get; set; }
        public bool anlv_payyearoverdraw { get; set; }
        public bool use_pay_statutory_holiday { get; set; }
        public Double cpfobasemax { get; set; }
        public Double cpfabasemax { get; set; }
        public Double cpfvcmax { get; set; }
        public bool use_pthk_ats { get; set; }
        public string uenno { get; set; }
        public string nricno { get; set; }
        public string finno { get; set; }
        public string bank_japanese { get; set; }
        public string japanese { get; set; }
        public bool ptholiday { get; set; }
        public bool otbaseprorate { get; set; }
        public string otmonitorfield { get; set; }
        public bool lvbaseprorate { get; set; }
        public string lvmonitorfield { get; set; }
        public string iwl_lv_ded_item { get; set; }
        public string iwlexclude_field { get; set; }
        public Double iwl_ot_compare_factor { get; set; }
        public Byte iwl_mon_days_type { get; set; }
        public int iwl_fix_mon_days { get; set; }
        public Byte iwl_firstgettype { get; set; }
        public string iwl_firstgetfield { get; set; }
        public Byte holiday_paymonth { get; set; }
        public bool usemacaodaw { get; set; }
        public bool lvpaybydaily { get; set; }
        public int attprdoffset_minute { get; set; }
        public bool mmbcalc { get; set; }
        public string mmbquery { get; set; }
        public string mmbbaseitem { get; set; }
        public bool multiplepayment { get; set; }
        public string pt_pay_field { get; set; }
        public bool useiitreport { get; set; }
        public string noticedatetype { get; set; }
        public string countrycode { get; set; }
        public Byte methodsvcyear { get; set; }
        public int createuser { get; set; }
        DateTime createdate { get; set; }
        public int moduser { get; set; }
        public DateTime? moddate { get; set; }
        public string payrollgroupcode { get; set; }
        public string mpferid { get; set; }
        public string mpfername { get; set; }
        public string mpferparticipationno { get; set; }
        public string mpfpaycenterid { get; set; }
        string mpfpaycentername { get; set; }
        public string mpfschemeid { get; set; }
        public string mpfschemename { get; set; }
        public decimal mpf_industry_scheme_daily_min_income { get; set; }
        public decimal mpf_industry_scheme_daily_max_income { get; set; }
        public decimal mpf_industry_scheme_contribute_percentage { get; set; }
        public string daily_work_data_source_table { get; set; }
        public int minimum_continues_work_months { get; set; }
        public string parttime_daw_policy { get; set; }
        public int parttime_daw_period_count { get; set; }
        public bool usesalarylockworkflow { get; set; }
        public string orsopaycenterid { get; set; }
        public string orsopolicyid { get; set; }
        public string orsoschemeid { get; set; }
        public string orsoschemename { get; set; }    
    
    }
}