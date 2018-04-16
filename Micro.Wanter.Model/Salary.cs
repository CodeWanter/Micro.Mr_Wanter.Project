namespace Micro.Wanter.Model
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Salary")]
    public partial class Salary
    {
        public int id { get; set; }
        //Ӧ��
        public decimal? TotalSalary { get; set; }
        //ʵ��
        public decimal? FinalSalary { get; set; }
        //�۳�
        public decimal? DeductedSalary { get; set; }
        //������
        public decimal? AccumulationFund { get; set; }
        //�ۿ�
        public decimal? LeaveSalary { get; set; }
        //����
        public DateTime SalaryTime { get; set; }
        public DateTime CreateTime { get; set; }
        //��˾
        public string Company { get; set; }
        //��ע
        public string Summary { get; set; }
        //�û�id
        public int UserId { get; set; }

    }
}
