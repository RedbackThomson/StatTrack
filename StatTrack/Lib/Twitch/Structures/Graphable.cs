using System;

namespace StatTrack.Lib.Twitch.Structures
{
    /// <summary>
    /// Used to define an object that can be graphed and tracked
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class Graphable : Attribute
    {
        public string Name { get; set; }
        public string GraphableProperty { get; set; }

        public Graphable(string name)
        {
            Name = name;
        }

        public Graphable(string name, string graphableProperty) : this(name)
        {
            GraphableProperty = graphableProperty;
        }
    }

    /// <summary>
    /// Used to declare a class that contains Graphable objects
    /// </summary>
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
