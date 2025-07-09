using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UtilBox
{
    public static class ReflectionUtils
    {
        public static IEnumerable<PropertyInfo> GetProperties(object obj)
        {
            return obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
        }

        public static object? GetPropertyValue(object obj, string propertyName)
        {
            var prop = obj.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
            return prop?.GetValue(obj);
        }

        public static bool SetPropertyValue(object obj, string propertyName, object? value)
        {
            var prop = obj.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
            if (prop != null && prop.CanWrite)
            {
                prop.SetValue(obj, value);
                return true;
            }
            return false;
        }

        public static T? GetAttribute<T>(MemberInfo member) where T : Attribute
        {
            return member.GetCustomAttribute<T>();
        }

        public static IEnumerable<T> GetAttributes<T>(MemberInfo member) where T : Attribute
        {
            return member.GetCustomAttributes<T>();
        }

        public static object? CreateInstance(Type type, params object[] args)
        {
            return Activator.CreateInstance(type, args);
        }

        public static IEnumerable<Type> GetTypesWithAttribute<T>(Assembly assembly) where T : Attribute
        {
            return assembly.GetTypes()
                .Where(t => t.GetCustomAttribute<T>() != null);
        }

        public static IEnumerable<Type> GetImplementingTypes<T>(Assembly assembly)
        {
            var targetType = typeof(T);
            return assembly.GetTypes().Where(t => targetType.IsAssignableFrom(t) && t.IsClass && !t.IsAbstract);
        }

    }
}
