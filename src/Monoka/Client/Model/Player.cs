using System;
using Microsoft.Xna.Framework;

namespace Monoka.Client.Model
{
    public class Player
    {
        public enum PlayerNr { One, Two }

        public PlayerNr Nr { get; set; }
        public string Name { get; private set; }
        public Color Color { get; set; }
        public bool IsLocalPlayer { get; set; }
        public Guid Id { get; set; }
        public bool IsReady { get; set; }

        public Player(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
