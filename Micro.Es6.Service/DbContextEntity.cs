using Micro.Es6.Model;
using System.Data.Entity;

namespace Micro.Es6.Service
{
    /// <summary>
    /// 数据库上下文
    /// </summary>
    public class DbContextEntity : DbContext
    {
        //public DbContextEntity()
        //{
        //    //启动数据自动迁移
        //    Database.SetInitializer(new MigrateDatabaseToLatestVersion<DbContextEntity, Migrations.Configuration>());
        //}

        public DbContextEntity()
            : base("name=ESDBContextEntity")
        {
        }

        public DbSet<MainRCJournalInfo> MainRCJournalInfo { get; set; }
    }
}
