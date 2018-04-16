namespace Micro.Wanter.Model
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Salary")]
    public partial class Salary
    {
        public int id { get; set; }
        //应发
        public decimal? TotalSalary { get; set; }
        //实发
        public decimal? FinalSalary { get; set; }
        //扣除
        public decimal? DeductedSalary { get; set; }
        //公积金
        public decimal? AccumulationFund { get; set; }
        //扣款
        public decimal? LeaveSalary { get; set; }
        //日期
        public DateTime SalaryTime { get; set; }
        public DateTime CreateTime { get; set; }
        //公司
        public string Company { get; set; }
        //备注
        public string Summary { get; set; }
        //用户id
        public int UserId { get; set; }

    }
}
