using LoriCMS.Domain.AggregatesModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;

namespace LindAgile.Web
{
    /// <summary>
    /// LindDb这个数据库的上下文对象
    /// </summary>
    public partial class LindContext : DbContext
    {
        public LindContext()
            : base("DefaultConnection")
        {
            Database.SetInitializer<LindContext>(new LindInitializer());


            this.Configuration.AutoDetectChangesEnabled = true;//对多对多，一对多进行curd操作时需要为true
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;//禁止动态拦截System.Data.Entity.DynamicProxies.
        }

        public DbSet<Work_Item> Work_Item { get; set; }
        public DbSet<Work_Project> Work_Project { get; set; }
        public DbSet<Work_User> Work_User { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
    /// <summary>
    /// 数据库初始化
    /// </summary>
    public class LindInitializer : CreateDatabaseIfNotExists<LindContext>
    {
        protected override void Seed(LindContext context)
        {
            context.Work_Item.Add(new Work_Item
            {
                Content = new LoriCMS.Domain.AggregatesModel.Content { Title = "test", Detail = "infomation" },
                EndTime = DateTime.Now.AddDays(1),
                Schedule = 60,
                StartTime = DateTime.Now,
                UserId = 1
            });
            context.SaveChanges();

        }
    }

}
