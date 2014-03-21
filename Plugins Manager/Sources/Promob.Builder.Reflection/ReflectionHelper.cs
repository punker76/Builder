
using System;
using System.Collections.Generic;
using System.Reflection;
namespace Promob.Builder.Reflection
{
    public static class ReflectionHelper
    {
        #region Public Methods

        public static List<Type> GetSubClasses(Type parentType, Assembly assembly)
        {
            if (assembly == null)
                assembly = parentType.Assembly;

            Type[] types = assembly.GetTypes();

            List<Type> ret = new List<Type>();

            for (int i = 0; i < types.Length; i++)
            {
                Type t = types[i];

                if (t.IsSubclassOf(parentType))
                    ret.Add(t);
            }

            return ret;
        }

        public static List<Type> GetSubClasses(Type parentType)
        {
            return GetSubClasses(parentType, null);
        }

        #endregion
    }
}
