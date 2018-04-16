namespace Micro.Wanter.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SalaryModel : DbContext
    {
        public SalaryModel()
            : base("name=SalaryModel")
        {
        }

        public virtual DbSet<S_Log_Access> S_Log_Access { get; set; }
        public virtual DbSet<S_User> S_User { get; set; }
        public virtual DbSet<Salary> Salary { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<S_User>()
                .Property(e => e.UserName)
                .IsFixedLength();

            modelBuilder.Entity<Salary>()
                .Property(e => e.TotalSalary)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Salary>()
                .Property(e => e.FinalSalary)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Salary>()
                .Property(e => e.DeductedSalary)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Salary>()
                .Property(e => e.AccumulationFund)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Salary>()
                .Property(e => e.LeaveSalary)
                .HasPrecision(18, 0);
        }
    }
}
