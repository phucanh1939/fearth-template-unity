using System;
using System.Collections.Generic;
using System.Linq;

public static class ListExtensions
{
    public static void Shuffle<T>(this List<T> values)
    {
        Random rand = new();
        // Fisher-Yates shuffle algorithm
        for (int i = values.Count - 1; i > 0; i--)
        {
            int j = rand.Next(0, i + 1);
            (values[j], values[i]) = (values[i], values[j]);
        }
    }

    public static void Extend<T>(this List<T> values, T value, int amount)
    {
        values.AddRange(Enumerable.Repeat(value, amount));
    }
}

