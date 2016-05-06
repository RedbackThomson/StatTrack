using System.Reflection;
using StatTrack.Lib.Twitch.Structures;

namespace StatTrack.UI.Models
{
    /// <summary>
    /// A property that is graphable
    /// </summary>
    public class GraphableProperty
    {
        public PropertyInfo Property { get; set; }
        public Graphable Attribute { get; set; }

        public override int GetHashCode()
        {
            return Property.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            GraphableProperty prop = obj as GraphableProperty;
            return prop != null && Property.Equals(prop.Property);
        }
    }
}
