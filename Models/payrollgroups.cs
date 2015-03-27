using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace T5PWebAPI.Models
{
    public class payrollgroups
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        //[Required]
        public int payrollgroupid{ get; set; } //1
        //[Required]
        [StringLength(100)]
        public string english { get; set; } //2
        //[Required]
        [StringLength(100)]
        public string chinese { get; set; }  //3
        
        [StringLength(100)]
        public string big5 { get; set; } //4
        //[Required]
        [StringLength(100)]
        public string bank_english { get; set; }  //5
        //[Required]
        [StringLength(100)]
        public string bank_chinese { get; set; }   //6
        //[Required]
        [StringLength(100)]
        public string bank_big5 { get; set; }   //7
        //[Required]
        [StringLength(50)]
        public string bankid { get; set; } //8
        //[Required]
        [StringLength(50)]
        public string taxnumber { get; set; }  //9
        //[Required]
        public bool use_gl { get; set; } //10
        //[Required]
        public bool gl_reverse { get; set; }   //11
        //[Required]
        public bool use_costcenter { get; set; }  //12
        //[Required]
        public bool use_negative { get; set; }  //13
        //[Required]
        public bool daily_piece { get; set; }   //14
        //[Required]
        public bool use_piece_quality { get; set; }   //15
        //[Required]
        public bool use_piece_quantity { get; set; }   //16
        //[Required]
        public int pay_curyear { get; set; }    //17
        
        public DateTime? pay_curyearbegin { get; set; }  //18
        
        public DateTime? pay_curyearend { get; set; }  //19
        
        public DateTime? pay_curperiodbegin { get; set; }   //20
        public DateTime? pay_curperiodend { get; set; }   //21
        public DateTime? attbegin { get; set; }   //22
        public DateTime? attend { get; set; }   //23
        //[Required]
        [StringLength(7)]
        public string restday { get; set; }    //24
        //[Required]
        public int gl_length { get; set; }    //25
	    //[Required]
        [StringLength(100)]
        public string gl_mask { get; set; }   ///26
	    //[Required]
        [StringLength(100)]
        public string gl_sample { get; set; }   //27
	    //[Required]
        [StringLength(100)]
        public string gl_taxexpense { get; set; }    //28
	    //[Required]
        [StringLength(100)]
        public string gl_taxcoexpense { get; set; }   //29
	    //[Required]
        [StringLength(100)]
        public string gl_taxpayable { get; set; }   //30
	    //[Required]
        [StringLength(100)]
        public string gl_vacationaccrued { get; set; }   ///31
        //[Required]
        [StringLength(100)]
        public string gl_vacationexpense { get; set; }    //32
        //[Required]
        [StringLength(100)]
        public string gl_vacationpayable { get; set; }   //33
        //[Required]
        public Double calendardays { get; set; }   ///34
        //[Required]
        public Double workdays { get; set; }  //35
        //[Required]
        public Double dayspermonth { get; set; }   //36
        //[Required]
        public Double hoursperday { get; set; }   //37
        //[Required]
        public bool viewsalary { get; set; }  //38
        //[Required]
        public bool use_dailydata { get; set; }
        //[Required]
        public bool includeholiday { get; set; }
	    //[Required]
        public bool UsePieceWork { get; set; }
        //[Required]
        public bool usempf { get; set; }
        //[Required]
        public Double mpfmaxbase { get; set; }
        //[Required]
        public Double mpfminbase { get; set; }
	    //[Required]
        public Double mpfmaxday { get; set; }
        //[Required]
        public Double mpfminday { get; set; }
        //[Required]
        public Double mpfper { get; set; }
        //[Required]
        public Double mpffreedays { get; set; }
	    //[Required]
        public Double mpfdelaydays { get; set; }
        //[Required]
        public Double femaleretireage { get; set; }
        //[Required]
        public Double maleretireage { get; set; }
        //[Required]
        public bool use_new_ordinance { get; set; }
        //[Required]
        public bool use_pay_general_holiday { get; set; }
        //[Required]
        public bool use_pay_annual_leave { get; set; }
	    //[Required]
        [StringLength(20)]
        public string statutory_anlv_overdraw_item { get; set; }
	    //[Required]
        [StringLength(20)]
        public string company_anlv_overdraw_item { get; set; }
	    //[Required]
        public bool use_pay_lieu_notice { get; set; }
	    //[Required]
        [StringLength(20)]
        public string ot_compare_field { get; set; }
	    //[Required]
        public Double ot_compare_factor { get; set; }
	    //[Required]
        public Byte dailywage_decimaltype { get; set; }
        //[Required]
        public Byte mon_days_type { get; set; }
	    //[Required]
        [StringLength(100)]
        public string mon_pay_field { get; set; }
	    //[Required]
        public int fixed_mon_days { get; set; }
	    //[Required]
        public Byte less_lieu_notice { get; set; }
	    //[Required]
        public Byte less_annual_leave { get; set; }
        //[Required]
        public Byte less_other { get; set; }
        //[Required]
        public Byte use_dailywage_sick { get; set; }
        //[Required]
        public Byte use_dailywage_maternity { get; set; }
	    //[Required]
        [StringLength(20)]
        public string sick_lv_ded_item { get; set; }
	    //[Required]
        [StringLength(20)]
        public string maternity_lv_ded_item { get; set; }
	    //[Required]
        [StringLength(20)]
        public string other_lv_ded_item { get; set; }
	    //[Required]
        public bool payyearoverdraw { get; set; }
        //[Required]
        public bool anlv_payyearoverdraw { get; set; }
        //[Required]
        public bool use_pay_statutory_holiday { get; set; }
        //[Required]
        public Double cpfobasemax { get; set; }
        //[Required]
        public Double cpfabasemax { get; set; }
        //[Required]
        public Double cpfvcmax { get; set; }
        //[Required]
        public bool use_pthk_ats { get; set; }
	    //[Required]
        [StringLength(10)]
        public string uenno { get; set; }
	    //[Required]
        [StringLength(10)]
        public string nricno { get; set; }
	    //[Required]
        [StringLength(10)]
        public string finno { get; set; }
	    //[Required]
        [StringLength(100)]
        public string bank_japanese { get; set; }
	    //[Required]
        [StringLength(100)]
        public string japanese { get; set; }
	    //[Required]
        public bool ptholiday { get; set; }
        //[Required]
        public bool otbaseprorate { get; set; }
	    //[Required]
        [StringLength(100)]
        public string otmonitorfield { get; set; }
	    //[Required]
        public bool lvbaseprorate { get; set; }
	    //[Required]
        [StringLength(100)]
        public string lvmonitorfield { get; set; }
	    //[Required]
        [StringLength(20)]
        public string iwl_lv_ded_item { get; set; }
	    //[Required]
        [StringLength(100)]
        public string iwlexclude_field { get; set; }
        //[Required]
        public Double iwl_ot_compare_factor { get; set; }
        //[Required]
        public Byte iwl_mon_days_type { get; set; }
        //[Required]
        public int iwl_fix_mon_days { get; set; }
        //[Required]
        public Byte iwl_firstgettype { get; set; }
	    //[Required]
        [StringLength(50)]
        public string iwl_firstgetfield { get; set; }
        //[Required]
        public Byte holiday_paymonth { get; set; }
        //[Required]
        public bool usemacaodaw { get; set; }
        //[Required]
        public bool lvpaybydaily { get; set; }
        //[Required]
        public int attprdoffset_minute { get; set; }
        //[Required]
        public bool mmbcalc { get; set; }
	    //[Required]
        [StringLength(100)]
        public string mmbquery { get; set; }
	    //[Required]
        [StringLength(100)]
        public string mmbbaseitem { get; set; }
        public bool multiplepayment { get; set; }
	    //[Required]
        [StringLength(100)]
        public string pt_pay_field { get; set; }
        public bool useiitreport { get; set; }
	    //[Required]
        [StringLength(20)]
        public string noticedatetype { get; set; }
	    //[Required]
        [StringLength(50)]
        public string countrycode { get; set; }
        //[Required]
        public Byte methodsvcyear { get; set; }
        //[Required]
        public int createuser { get; set; }
	    DateTime? createdate { get; set; }
        //[Required]
        public int moduser { get; set; }
        public DateTime? moddate { get; set; }
	    //[Required]
        [StringLength(50)]
        public string payrollgroupcode { get; set; }
	    //[Required]
        [StringLength(50)]
        public string mpferid { get; set; }
	    //[Required]
        [StringLength(50)]
        public string mpfername { get; set; }
	    //[Required]
        [StringLength(50)]
        public string mpferparticipationno { get; set; }
	    //[Required]
        [StringLength(50)]
        public string mpfpaycenterid { get; set; }
	    //[Required]
        [StringLength(50)]
	    string mpfpaycentername { get; set; }
	    //[Required]
        [StringLength(50)]
        public string mpfschemeid { get; set; }
	    //[Required]
        [StringLength(50)]
        public string mpfschemename { get; set; }
        //[Required]
        public decimal mpf_industry_scheme_daily_min_income { get; set; }
        //[Required]
        public decimal mpf_industry_scheme_daily_max_income { get; set; }
        //[Required]
        public decimal mpf_industry_scheme_contribute_percentage { get; set; }
	    //[Required]
        [StringLength(200)]
        public string daily_work_data_source_table { get; set; }
        //[Required]
        public int minimum_continues_work_months { get; set; }
	    //[Required]
        [StringLength(50)]
        public string parttime_daw_policy { get; set; }
        //[Required]
        public int parttime_daw_period_count { get; set; }
        //[Required]
        public bool usesalarylockworkflow { get; set; }
	    //[Required]
        [StringLength(50)]
        public string orsopaycenterid { get; set; }
	    //[Required]
        [StringLength(50)]
        public string orsopolicyid { get; set; }
	    //[Required]
        [StringLength(50)]
        public string orsoschemeid { get; set; }
	    //[Required]
        [StringLength(100)]
        public string orsoschemename { get; set; }















        //public payrollgroups(payrollgroups thegroup)
        //{
        //    if( thegroup.big5 == null )
        //    {
        //        thegroup.big5 = "";
        //    }
        //}
        public payrollgroups()
        {
            english = "";
            chinese = "";
            big5 = "";
            bank_english ="";
            bank_chinese ="";
            bank_big5 = "";
            bankid = "";
            taxnumber = "";
            use_gl = false;
            gl_reverse = false;
            use_costcenter = false;
            use_negative = false;
            daily_piece = false;
            use_piece_quality = false;
            use_piece_quantity = false;
            pay_curyear = 0;
            restday = "0000011";
            gl_length = 0;
            gl_mask = "";
            gl_sample  = "";
            gl_taxexpense = "";
            gl_taxcoexpense  = "";
            gl_taxpayable = "";
            gl_vacationaccrued = "";
            gl_vacationexpense = "";
            gl_vacationpayable = "";
            calendardays = 31;
            workdays = 22;
            dayspermonth = 0;
            hoursperday = 0;
            viewsalary = false;
            use_dailydata = false;
            includeholiday = false;
            UsePieceWork = false;
            usempf = false;
            mpfmaxbase = 0;
            mpfminbase = 0;
            mpfmaxday = 0;
            mpfminday = 0;
            mpfper = 0;
            mpffreedays = 0;
            mpfdelaydays = 0;
            femaleretireage = 0;
            maleretireage = 0;
            use_new_ordinance  = false;
            use_pay_general_holiday = false;
            use_pay_annual_leave = false;
            statutory_anlv_overdraw_item = "";
            company_anlv_overdraw_item = "";
            use_pay_lieu_notice = false;
            ot_compare_field  = "";
            ot_compare_factor = 0;
            dailywage_decimaltype = 0;
            mon_days_type = 0;
            mon_pay_field  = "";
            fixed_mon_days= 0;
            less_lieu_notice = 0;
            less_annual_leave = 0;
            less_other = 0;
            use_dailywage_sick  = 0;
            use_dailywage_maternity = 0;
            sick_lv_ded_item  = "";
            maternity_lv_ded_item = "";
            other_lv_ded_item = "";
            payyearoverdraw  = false;
            anlv_payyearoverdraw = false;
            use_pay_statutory_holiday = false;
            cpfobasemax = 0;
            cpfabasemax = 0;
            cpfvcmax = 0;
            use_pthk_ats = false;
            uenno = "";
            nricno = "";
            finno = "";
            bank_japanese = "";
            japanese = "";
            ptholiday  = false;
            otbaseprorate = false;
            otmonitorfield = "";
            lvbaseprorate = false;
            lvmonitorfield = "";
            iwl_lv_ded_item = "";
            iwlexclude_field = "";
            iwl_ot_compare_factor = 0;
            iwl_mon_days_type = 0;
            iwl_fix_mon_days = 0;
            iwl_firstgettype = 0;
            iwl_firstgetfield = "";
            holiday_paymonth = 0;
            usemacaodaw = false;
            lvpaybydaily = false;
            attprdoffset_minute = 0;
            mmbcalc  = false;
            mmbquery = "";
            mmbbaseitem = "";
            multiplepayment =  false;
            pt_pay_field = "";
            useiitreport = false;
            noticedatetype = "";
            countrycode  = "";
            methodsvcyear = 0;
            createuser = 0;
            
            moduser  = 0;
            
            payrollgroupcode  = "";
            mpferid = "";
            mpfername  = "";
            mpferparticipationno = "";
            mpfpaycenterid  = "";
            mpfpaycentername  = "";
            mpfschemeid  = "";
            mpfschemename = "";
            mpf_industry_scheme_daily_min_income  = 0;
            mpf_industry_scheme_daily_max_income = 0;
            mpf_industry_scheme_contribute_percentage  = 0;
            daily_work_data_source_table  = "";
            minimum_continues_work_months  = 0;
            parttime_daw_policy = "";
            parttime_daw_period_count  = 0;
            usesalarylockworkflow = false;
            orsopaycenterid = "";
            orsopolicyid = "";
            orsoschemeid = "";
            orsoschemename = "";
        }
    }
}