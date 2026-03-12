using System;

namespace Com.Game.Level.Domain.Definitions
{
    internal readonly struct LevelId : IEquatable<LevelId>
    {
        internal Guid Value { get; }

        internal LevelId(Guid value)
        {
            Value = value;
        }

        internal static LevelId New()
        {
            return new LevelId(Guid.NewGuid());
        }

        internal static LevelId From(Guid value)
        {
            return new LevelId(value);
        }

        public override string ToString()
        {
            return Value.ToString();
        }
        
        public override bool Equals(object obj)
        {
            return obj is LevelId other && Value.Equals(other.Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public bool Equals(LevelId other)
        {
            return Value.Equals(other.Value);
        }
    }
}