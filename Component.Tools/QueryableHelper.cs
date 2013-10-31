using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.ComponentModel;

namespace Component.Tools
{
    public static class QueryableHelper<T>
    {
        public static readonly ConcurrentDictionary<string, LambdaExpression> Cache = new ConcurrentDictionary<string, LambdaExpression>();
        public static IOrderedQueryable<T> OrderBy(IQueryable<T> source, string propertyName, ListSortDirection sortDirection)
        {
            dynamic keySelector = GetLambdaExpression(propertyName);
            return sortDirection == ListSortDirection.Ascending
                ? Queryable.OrderBy(source, keySelector)
                : Queryable.OrderByDescending(source, keySelector);
        }
        public static IOrderedQueryable<T> ThenBy(IOrderedQueryable<T> source, string propertyName, ListSortDirection sortDirection)
        {
            dynamic keySelector = GetLambdaExpression(propertyName);
            return sortDirection == ListSortDirection.Ascending
                ? Queryable.OrderBy(source, keySelector)
                : Queryable.OrderByDescending(source, keySelector);
        }
        private static LambdaExpression GetLambdaExpression(string propertyName)
        {
            if (Cache.ContainsKey(propertyName))
            {
                return Cache[propertyName];
            }
            ParameterExpression param = Expression.Parameter(typeof(T));
            MemberExpression body = Expression.Property(param, propertyName);
            LambdaExpression keySelector = Expression.Lambda(body, param);
            Cache[propertyName] = keySelector;
            return keySelector;
        }
    }
}
