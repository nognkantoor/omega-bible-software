using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Common.Core.Application
{
    public static class ReflectionHelper
    {
        public static IEnumerable<TAttribute> GetAttributes<TAttribute>(this Type type)
        {
            if (type != null)
            {
                object[] attributes = type.GetCustomAttributes(typeof(TAttribute), true);
                return attributes.Cast<TAttribute>();
            }

            return null;
        }

        public static IEnumerable<TAttribute> GetAttributes<TAttribute>(this object source)
        {
            if (source != null)
            {
                return GetAttributes<TAttribute>(source.GetType());
            }

            return null;
        }
    }
}
