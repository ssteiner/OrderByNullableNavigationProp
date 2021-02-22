using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Reflection;

namespace ConsoleApp3.Extensions
{
    public static class QueryExtensions
    {
        public static IQueryable<T> Order<T>(this IQueryable<T> query, string propertyName, bool isAscending) where T: class, IReferenceClass
        {
            if (string.IsNullOrEmpty(propertyName))
                return query;
            var prop = typeof(T).GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
            if (prop != null)
            {
                var propType = prop.PropertyType;
                if (propertyName.EndsWith("Id"))
                {
                    var referencePropertyName = propertyName[0..^2];
                    var referenceProp = typeof(T).GetProperty(referencePropertyName, BindingFlags.Public | BindingFlags.Instance);
                    if (referenceProp != null) // include this in query
                    {
                        var fullQuery = query as IQueryable<TestClass>;
                        var fullPropertyName = $"{nameof(TestClass.ReferenceOne)}.{nameof(ReferenceClassOne.Name)}";
                        if (propType == typeof(Guid?))
                            fullPropertyName = $"np({fullPropertyName})";
                        if (isAscending)
                            fullQuery = fullQuery.OrderBy(fullPropertyName);
                        else
                            fullQuery = fullQuery.OrderBy(fullPropertyName, "descending");
                        query = fullQuery as IQueryable<T>;
                    }
                }
                else
                {
                    if (isAscending)
                        query = query.OrderBy(propertyName);
                    else
                        query = query.OrderBy(propertyName, "descending");
                }
            }
            return query;
        }


        //public static IOrderedQueryable<TSource> OrderBy<TSource>(this IQueryable<TSource> query, string propertyName)
        //{
        //    var entityType = typeof(TSource);

        //    // Create x=>x.PropName
        //    var propertyInfo = entityType.GetProperty(propertyName);
        //    if (propertyInfo.DeclaringType != entityType)
        //    {
        //        propertyInfo = propertyInfo.DeclaringType.GetProperty(propertyName);
        //    }

        //    // If we try to order by a property that does not exist in the object return the list
        //    if (propertyInfo == null)
        //    {
        //        return (IOrderedQueryable<TSource>)query;
        //    }

        //    var arg = Expression.Parameter(entityType, "x");
        //    var property = Expression.MakeMemberAccess(arg, propertyInfo);
        //    var selector = Expression.Lambda(property, new ParameterExpression[] { arg });

        //    // Get System.Linq.Queryable.OrderBy() method.
        //    var method = typeof(Queryable).GetMethods()
        //         .Where(m => m.Name == "OrderBy" && m.IsGenericMethodDefinition)
        //         .Where(m => m.GetParameters().ToList().Count == 2) // ensure selecting the right overload
        //         .Single();

        //    //The linq's OrderBy<TSource, TKey> has two generic types, which provided here
        //    MethodInfo genericMethod = method.MakeGenericMethod(entityType, propertyInfo.PropertyType);

        //    /* Call query.OrderBy(selector), with query and selector: x=> x.PropName
        //      Note that we pass the selector as Expression to the method and we don't compile it.
        //      By doing so EF can extract "order by" columns and generate SQL for it. */
        //    return (IOrderedQueryable<TSource>)genericMethod.Invoke(genericMethod, new object[] { query, selector });
        //}

        //public static IOrderedQueryable<TSource> OrderByDescending<TSource>(this IQueryable<TSource> query, string propertyName)
        //{
        //    var entityType = typeof(TSource);

        //    // Create x=>x.PropName
        //    var propertyInfo = entityType.GetProperty(propertyName);
        //    if (propertyInfo.DeclaringType != entityType)
        //    {
        //        propertyInfo = propertyInfo.DeclaringType.GetProperty(propertyName);
        //    }

        //    // If we try to order by a property that does not exist in the object return the list
        //    if (propertyInfo == null)
        //    {
        //        return (IOrderedQueryable<TSource>)query;
        //    }

        //    var arg = Expression.Parameter(entityType, "x");
        //    var property = Expression.MakeMemberAccess(arg, propertyInfo);
        //    var selector = Expression.Lambda(property, new ParameterExpression[] { arg });

        //    // Get System.Linq.Queryable.OrderBy() method.
        //    var method = typeof(Queryable).GetMethods()
        //         .Where(m => m.Name == "OrderByDescending" && m.IsGenericMethodDefinition)
        //         .Where(m => m.GetParameters().ToList().Count == 2) // ensure selecting the right overload
        //         .Single();

        //    //The linq's OrderBy<TSource, TKey> has two generic types, which provided here
        //    MethodInfo genericMethod = method.MakeGenericMethod(entityType, propertyInfo.PropertyType);

        //    /* Call query.OrderBy(selector), with query and selector: x=> x.PropName
        //      Note that we pass the selector as Expression to the method and we don't compile it.
        //      By doing so EF can extract "order by" columns and generate SQL for it. */
        //    return (IOrderedQueryable<TSource>)genericMethod.Invoke(genericMethod, new object[] { query, selector });
        //}

    }
}
