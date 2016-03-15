using System;

namespace StatTrack.Lib.Twitch.Structures
{
    [AttributeUsage(AttributeTargets.Property)]
    public class Graphable : Attribute
    {
        private readonly bool _graphable;
        public string Name { get; set; }
        public string GraphableProperty { get; set; }

        public Graphable(string name)
        {
            _graphable = true;
            Name = name;
        }

        public Graphable(string name, string graphableProperty) : this(name)
        {
            GraphableProperty = graphableProperty;
        }


        public Graphable(bool graphable)
        {
            _graphable = graphable;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class HasGraphables : Attribute
    {
        public string Name { get; set; }

        public HasGraphables(string name)
        {
            Name = name;
        }
    }
}
