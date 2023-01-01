using System;
using System.Collections.Generic;
using System.Linq;

public static partial class Utils
{
    private static System.Random rng = new Random();

    public static List<T> GetValues<T>() where T : Enum
    {
        var result = new List<T>();
        foreach (T type in Enum.GetValues(typeof(T)))
        {
            result.Add(type);
        }

        return result;
    }

    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public static bool In<T>(this T val, params T[] values) where T : struct => values.Contains(val);

    public static Dictionary<TValue, TKey> Reverse<TKey, TValue>(this IDictionary<TKey, TValue> source)
    {
        var dictionary = new Dictionary<TValue, TKey>();
        foreach (var entry in source)
        {
            if (!dictionary.ContainsKey(entry.Value))
                dictionary.Add(entry.Value, entry.Key);
        }
        return dictionary;
    }
}