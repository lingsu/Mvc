using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.Data.Entity;

using Component.Data;


namespace Core.Data.Context
{
    [Export(typeof(IUnitOfWork))]
    public class DemoUnitOfWorkContext:UnitOfWorkContextBase
    {
        /// <summary>
        /// 获取或设置 当前使用的数据访问上下文对象    
        /// </summary>
        protected override DbContext Context
        {
            get { return DemoDbContext; }
        }

        /// <summary>
        /// 获取或设置 默认的Demo项目数据访问上下文对象
        /// </summary>
        [Import(typeof(DbContext))]
        public DemoDbContext DemoDbContext { get; set; }
    }
}
