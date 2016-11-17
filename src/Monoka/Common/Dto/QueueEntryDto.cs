using System;
using System.Collections.Generic;

namespace Monoka.Common.Dto
{
    public class QueueEntryDto
    {
        public Guid SessionId { get; set; }
        public DateTime Timestamp { get; set; }
        public int ActionCode { get; set; }
        public IEnumerable<object> ActionData { get; set; }
        public Guid PlayerId { get; set; }
    }
}
