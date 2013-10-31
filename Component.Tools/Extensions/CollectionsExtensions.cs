using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.ComponentModel;

namespace Component.Tools.Extensions
{
    public static class CollectionsExtensions
    {
        #region IEnumerable的扩展

        #endregion

        #region IQueryable的扩展

        /// <summary>
        ///     把IQueryable[T]集合按指定属性与排序方式进行排序
        /// </summary>
        /// <typeparam name="T">动态类型</typeparam>
        /// <param name="source">要排序的数据集</param>
        /// <param name="propertyName">排序属性名</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns>排序后的数据集</returns>
        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string propertyName, ListSortDirection sortDirection = ListSortDirection.Ascending)
        {
            return QueryableHelper<T>.OrderBy(source, propertyName, sortDirection);
        }

        /// <summary>
        ///     把IQueryable[T]集合按指定属性排序条件进行排序
        /// </summary>
        /// <typeparam name="T">动态类型</typeparam>
        /// <param name="source">要排序的数据集</param>
        /// <param name="sortCondition">列表属性排序条件</param>
        /// <returns></returns>
        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, PropertySortCondition sortCondition)
        {
            return source.OrderBy(sortCondition.PropertyName, sortCondition.ListSortDirection);
        }

        /// <summary>
        ///     把IOrderedQueryable[T]集合继续按指定属性排序方式进行排序
        /// </summary>
        /// <typeparam name="T">动态类型</typeparam>
        /// <param name="source">要排序的数据集</param>
        /// <param name="propertyName">排序属性名</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        public static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> source, string propertyName,ListSortDirection sortDirection = ListSortDirection.Ascending)
        {
            return QueryableHelper<T>.ThenBy(source, propertyName, sortDirection);
        }

        /// <summary>
        ///     把IOrderedQueryable[T]集合继续指定属性排序方式进行排序
        /// </summary>
        /// <typeparam name="T">动态类型</typeparam>
        /// <param name="source">要排序的数据集</param>
        /// <param name="sortCondition">排序方向</param>
        /// <returns></returns>
        public static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> source, PropertySortCondition sortCondition)
        {
            return source.ThenBy(sortCondition.PropertyName, sortCondition.ListSortDirection);
        }

        public static IQueryable<T> Where<T>(this IQueryable<T> source,int pageIndex,int pageSize,out int total,Expression<Func<T,bool>> predicate=null,PropertySortCondition[] sortConditions=null) where T:EntityBase
        {
            if (sortConditions ==null || sortConditions.Length==0)
            {
                source = source.OrderByDescending(m => m.AddDate);
            }
            else
            {
                int count = 0;
                IOrderedQueryable<T> orderSource = null;
                foreach (var sortCondition in sortConditions)
                {
                    orderSource = count == 0
                        ? source.OrderBy(sortCondition.PropertyName, sortCondition.ListSortDirection)
                        : orderSource.ThenBy(sortCondition.PropertyName, sortCondition.ListSortDirection);
                    count++;
                }
                source = orderSource;
            }

            if (predicate != null)
            {
                total = source.Count(predicate);
                return source.Where(predicate).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }
            total = source.Count();
            return source.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }
        #endregion
    }
}
