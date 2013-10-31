using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

using Core.Models;

namespace Core.Data.Context
{
    [Export(typeof(DbContext))]
    public class DemoDbContext:DbContext
    {
        public DemoDbContext()
            : base("default") { }

        public DemoDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString) { }

        #region 属性
        DbSet<Product> Products { get; set; }
        DbSet<Category> Category { get; set; }
        DbSet<LoginLog> LoginLogs { get; set; }
        DbSet<Member> Members { get; set; }
        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            //移除一对多的级联删除约定，想要级联删除可以在 EntityTypeConfiguration<TEntity>的实现类中进行控制
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            //多对多启用级联删除约定，不想级联删除可以在删除前判断关联的数据进行拦截
            //modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }

    }
}
