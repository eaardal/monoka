using System;

namespace Monoka.Common.Infrastructure.Exceptions
{
    public class MonokaException : Exception
    {
        public MonokaException(string message) : base(message)
        {
            
        }
    }
}