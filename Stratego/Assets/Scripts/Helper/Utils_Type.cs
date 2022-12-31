using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public static partial class Utils
{
    public static IEnumerable<Type> GetTypesAssignableFrom(System.Type rootType)
    {
        foreach (var assemb in System.AppDomain.CurrentDomain.GetAssemblies())
        {
            foreach (var tp in assemb.GetTypes())
            {
                if (rootType.IsAssignableFrom(tp)) yield return tp;
            }
        }
    }

    public static IEnumerable<Type> FindDerivedTypes(Assembly assembly, Type baseType)
    {
        return assembly.GetTypes().Where(t => t != baseType && baseType.IsAssignableFrom(t));
    }

    public static string GetTypeString<T>() => typeof(T).ToString().Split(".").Last().ToString();
}