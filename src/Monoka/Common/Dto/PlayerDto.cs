using System;

namespace Monoka.Common.Dto
{
    public struct PlayerDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsReady { get; set; }
        public bool IsAdmin { get; set; }
    }
}