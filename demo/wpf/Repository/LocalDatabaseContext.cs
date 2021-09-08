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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LocalTestEntity>();
        }
        /// <summary>
        /// 本地测试类
        /// </summary>
        public virtual DbSet<LocalTestEntity> LocalTest { get; set; }
    }
    public class LocalDatabaseContextFactory : IDesignTimeDbContextFactory<LocalDatabaseContext>
    {
        public LocalDatabaseContext CreateDbContext(string[] args)
        {
            SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_e_sqlcipher());
            var optionsBuilder = new DbContextOptionsBuilder<LocalDatabaseContext>();
            optionsBuilder.UseSqlite("Data Source=test.db;Version=3;Password=2020");
            return new LocalDatabaseContext(optionsBuilder.Options);
        }
    }
}
