using System;
using System.Collections.Generic;
using System.Linq;

namespace Monoka.ExampleGame.Common.Infrastructure.Extensions
{
    public static class LinqExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> list, Action<T> action)
        {
            foreach (var item in list)
            {
                action(item);
            }
        }

        public static IEnumerable<T> Tail<T>(this IEnumerable<T> list)
        {
            var enumerable = list as T[] ?? list.ToArray();
            return enumerable.Except(new[] {enumerable.First()});
        }
    }
}
