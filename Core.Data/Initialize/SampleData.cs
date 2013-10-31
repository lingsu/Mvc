using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

using Core.Data.Context;
using Core.Models;

namespace Core.Data.Initialize
{
    /// <summary>
    /// 数据库初始化策略
    /// </summary>
    public class SampleData:CreateDatabaseIfNotExists<DemoDbContext>
    {
        protected override void Seed(DemoDbContext context)
        {
            //base.Seed(context);
            Member member = new Member { UserName = "admin", Password = "123456", NickName = "admin", Email = "570678569@qq.com" };
            DbSet<Member> memberSet = context.Set<Member>();
            memberSet.Add(member);
            context.SaveChanges();
        }
    }
}
