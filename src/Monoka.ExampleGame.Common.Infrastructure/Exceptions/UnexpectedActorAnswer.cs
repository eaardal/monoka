using System;

namespace Monoka.ExampleGame.Common.Infrastructure.Exceptions
{
    public class UnexpectedActorAnswer : Exception
    {
        public UnexpectedActorAnswer(string message) : base(message)
        {
            
        }
    }
}