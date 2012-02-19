using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Common.Core.Pattern
{
    public static class ReflectionHelper
    {
        public static object[] GetAttributes<TAttribute>(this Type type)
        {
            if (type != null)
            {
                object[] attributes = type.GetCustomAttributes(typeof(TAttribute), true);
                return attributes;
            }

            return null;
        }

        public static object[] GetAttributes<TAttribute>(this object source)
        {
            if (source != null)
            {
                object [] attributes = source.GetType().GetCustomAttributes(typeof(TAttribute), true);
                return attributes;
            }

            return null;
        }
    }
}
