using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;

using Component.Tools;

namespace Component.Data
{
    /// <summary>
    /// 单元操作实现
    /// </summary>
    public abstract class UnitOfWorkContextBase : IUnitOfWorkContext
    {
        /// <summary>
        /// 获取 当前使用的数据访问上下文对象
        /// </summary>
        protected abstract DbContext Context { get; }
        public DbSet<TEntity> Set<TEntity>() where TEntity : EntityBase
        {
            return Context.Set<TEntity>();
        }

        public void RegisterNew<TEntity>(TEntity entity) where TEntity : EntityBase
        {
            EntityState state = Context.Entry(entity).State;
            if (state == EntityState.Detached)
            {
                Context.Entry(entity).State = EntityState.Added;
            }
            IsCommitted = false;
        }

        public void RegisterNew<TEntity>(IEnumerable<TEntity> entities) where TEntity : EntityBase
        {

            try
            {
                Context.Configuration.AutoDetectChangesEnabled = false;
                foreach (var item in entities)
                {
                    RegisterNew(item);
                }
            }
            finally
            {
                Context.Configuration.AutoDetectChangesEnabled = true;
            }
        }

        public void RegisterModified<TEntity>(TEntity entity) where TEntity : EntityBase
        {
            if (Context.Entry(entity).State == EntityState.Modified)
            {
                Context.Set<TEntity>().Attach(entity);
            }
            Context.Entry(entity).State = EntityState.Modified;
            IsCommitted = false;
        }
        public void RegisterModified<TEntity>(IEnumerable<TEntity> entities) where TEntity : EntityBase
        {
            try
            {
                Context.Configuration.AutoDetectChangesEnabled = false;
                foreach (var item in entities)
                {
                    RegisterModified(item);
                }
            }
            finally
            {
                Context.Configuration.AutoDetectChangesEnabled = true;
            }
        }

        public void RegisterDeleted<TEntity>(TEntity entity) where TEntity : EntityBase
        {
            Context.Entry(entity).State = EntityState.Deleted;
            IsCommitted = false;
        }

        public void RegisterDeleted<TEntity>(IEnumerable<TEntity> entities) where TEntity : EntityBase
        {
            try
            {
                Context.Configuration.AutoDetectChangesEnabled = false;
                foreach (var item in entities)
                {
                    RegisterDeleted(item);
                }
            }
            finally
            {
                Context.Configuration.AutoDetectChangesEnabled = true;
            }
        }

        /// <summary>
        /// 获取 当前单元操作是否已被提交
        /// </summary>
        public bool IsCommitted
        {
            get;
            private set;
        }

        public int Commit()
        {
            if (IsCommitted)
            {
                return 0;
            }
            try
            {
                int result = Context.SaveChanges();
                IsCommitted = true;
                return result;
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException != null && e.InnerException.InnerException is SqlException)
                {
                    SqlException sqlEx = e.InnerException.InnerException as SqlException;
                    //string msg = DataHelper.GetSqlExceptionMessage(sqlEx.Number);
                    //throw PublicHelper.ThrowDataAccessException("提交数据更新时发生异常：" + msg, sqlEx);
                }
                throw;
            }
        }
        /// <summary>
        /// 把当前单元操作回滚成未提交状态
        /// </summary>
        public void Rollback()
        {
            IsCommitted = false;
        }

        public void Dispose()
        {
            if (!IsCommitted)
            {
                Commit();
            }
            Context.Dispose();
        }
    }
}
