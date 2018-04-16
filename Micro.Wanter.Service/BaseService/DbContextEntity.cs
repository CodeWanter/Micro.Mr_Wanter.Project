using Micro.Wanter.Model;
using System.Data.Entity;

namespace Micro.Wanter.Service
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
            : base("name=DbContextEntity")
        {
        }

        /// <summary>
        /// 生成用户表
        /// </summary>
        public DbSet<S_User> S_User { get; set; }
        public DbSet<S_Log_Access> S_Log_Access { get; set; }
        public DbSet<Salary> Salary { get; set; }
    }
}
