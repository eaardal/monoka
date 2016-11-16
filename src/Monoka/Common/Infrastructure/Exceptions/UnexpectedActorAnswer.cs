using System;

namespace Monoka.Common.Infrastructure.Exceptions
{
    public class UnexpectedActorAnswer : Exception
    {
        public UnexpectedActorAnswer(string message) : base(message)
        {
            
        }
    }
}