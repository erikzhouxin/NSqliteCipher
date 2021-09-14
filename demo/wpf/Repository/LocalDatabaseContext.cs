using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Data.SQLiteCipher;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWPFUI.SQLiteCipher.Models;

namespace TestWPFUI.SQLiteCipher.Repository
{
    /// <summary>
    /// 本地数据库环境
    /// </summary>
    public class LocalDatabaseContext : DbContext
    {
        /// <summary>
        /// 连接构造
        /// </summary>
        /// <param name="connString"></param>
        public LocalDatabaseContext(string connString) : this(GetOption(connString))
        {

        }
        /// <summary>
        /// 初始构造
        /// </summary>
        /// <param name="options"></param>
        public LocalDatabaseContext(DbContextOptions options) : base(options)
        {
            if (Database.GetPendingMigrations().Any())
            {
                Database.Migrate(); //执行迁移
            }
        }
        private static DbContextOptions GetOption(string connString)
        {
            var conn = SqliteConnectionPool.GetConnection(connString);
            var dbOptions = new DbContextOptionsBuilder<LocalDatabaseContext>().UseSqlite(conn).Options;
            return dbOptions;
        }
        /// <summary>
        /// 模型创建
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LocalTestEntity>();
        }
        /// <summary>
        /// 本地测试类
        /// </summary>
        public virtual DbSet<LocalTestEntity> LocalTest { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class LocalDatabaseContextFactory : IDesignTimeDbContextFactory<LocalDatabaseContext>
    {
        /// <summary>
        /// 创建迁移
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public LocalDatabaseContext CreateDbContext(string[] args)
        {
            var conn = SqliteConnectionPool.GetConnection("Data Source=bin\\LocalDatabaseMigration.db;Version=3;Password=2020");
            var optionsBuilder = new DbContextOptionsBuilder<LocalDatabaseContext>();
            optionsBuilder.UseSqlite(conn);
            return new LocalDatabaseContext(optionsBuilder.Options);
        }
    }
}
