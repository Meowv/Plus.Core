using MongoDB.Driver;
using Plus.Domain.Entities;
using Plus.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Plus.MongoDB
{
    internal static class MongoDbContextHelper
    {
        public static IEnumerable<Type> GetEntityTypes(Type dbContextType)
        {
            return
                from property in dbContextType.GetTypeInfo().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                where
                    ReflectionHelper.IsAssignableToGenericType(property.PropertyType, typeof(IMongoCollection<>)) &&
                    typeof(IEntity).IsAssignableFrom(property.PropertyType.GenericTypeArguments[0])
                select property.PropertyType.GenericTypeArguments[0];
        }
    }
}