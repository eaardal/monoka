using System.Collections.Generic;
using System.Linq;

namespace Monoka.Common.Infrastructure.Extensions
{
    public static class StringExtensions
    {
        public static bool EndsWithAny(this string str, IEnumerable<string> phrases)
        {
            return phrases.Any(str.EndsWith);
        }
    }
}
