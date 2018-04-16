using Micro.Sql.Service;
using System.Data.Entity;

namespace LZCX.Eamian.DAL
{
    /// <summary>
    /// 数据库上下文
    /// </summary>
    public class DbContextEntity : DbContext
    {
        public DbContextEntity()
           : base("name=SQLModel")
        {
        }
        public DbSet<MainRCJournalInfo> MainRCJournalInfo { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
