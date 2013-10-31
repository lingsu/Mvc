using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.ComponentModel.Composition;
using System.Data.Entity;

using Component.Tools;

namespace Component.Data
{
    public abstract class EFRepositoryBase<TEntity>:IRepository<TEntity> where TEntity:EntityBase
    {
        /// <summary>
        /// 获取 仓储上下文的实例
        /// </summary>
        [Import]
        public IUnitOfWork UnitOfWork { get; set; }
        protected IUnitOfWorkContext EFContext
        {
            get
            {
                if (UnitOfWork is IUnitOfWorkContext)
                {
                    return UnitOfWork as IUnitOfWorkContext;
                }
                throw new DataAccessException(string.Format("数据仓储上下文对象类型不正确，应为IUnitOfWorkContext，实际为 {0}", UnitOfWork.GetType().Name));
            }
        }
        public virtual IQueryable<TEntity> Entities
        {
            get { return EFContext.Set<TEntity>(); }
        }

        public int Insert(TEntity entity, bool isSave = true)
        {
             EFContext.RegisterNew(entity);
             return isSave ? EFContext.Commit() : 0;
        }

        public int Insert(IEnumerable<TEntity> entities, bool isSave = true)
        {
            EFContext.RegisterNew(entities);
            return isSave ? EFContext.Commit() : 0;
        }

        public int Delete(object id, bool isSave = true)
        {
            TEntity entity = EFContext.Set<TEntity>().Find(id);
            return entity != null ? Delete(entity) : 0;
        }

        public int Delete(TEntity entity, bool isSave = true)
        {
            EFContext.RegisterDeleted(entity);
            return isSave ? EFContext.Commit() : 0;
        }

        public int Delete(IEnumerable<TEntity> entities, bool isSave = true)
        {
            EFContext.RegisterDeleted(entities);
            return isSave ? EFContext.Commit() : 0;
        }

        public int Delete(Expression<Func<TEntity, bool>> predicate, bool isSave = true)
        {
            List<TEntity> entities = EFContext.Set<TEntity>().Where(predicate).ToList();
            return entities.Count > 0 ? Delete(entities) : 0;
        }

        public int Update(TEntity entity, bool isSave = true)
        {
            EFContext.RegisterModified(entity);
            return isSave ? EFContext.Commit() : 0;
        }
        public int Update(IEnumerable<TEntity> entities, bool isSave = true)
        {
            EFContext.RegisterModified(entities);
            return isSave ? EFContext.Commit() : 0;
        }

        public TEntity GetByKey(object key)
        {
            return EFContext.Set<TEntity>().Find(key);
        }
    }
}
