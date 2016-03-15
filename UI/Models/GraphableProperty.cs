using System.Reflection;
using StatTrack.Lib.Twitch.Structures;

namespace StatTrack.UI.Models
{
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
            return Property.Equals(obj);
        }
    }
}
