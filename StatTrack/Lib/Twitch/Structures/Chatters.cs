using System.Collections.Generic;
using Newtonsoft.Json;

namespace StatTrack.Lib.Twitch.Structures
{
    public class Chatters
    {
        [JsonProperty("moderators")]
        [Graphable("Moderators", "Count")]
        public HashSet<string> Moderators { get; set; }

        [JsonProperty("staff")]
        [Graphable("Staff", "Count")]
        public HashSet<string> Staff { get; set; }

        [JsonProperty("admin")]
        [Graphable("Admins", "Count")]
        public HashSet<string> Admins { get; set; }

        [JsonProperty("global_mods")]
        [Graphable("Global Mods", "Count")]
        public HashSet<string> GlobalMods { get; set; }

        [JsonProperty("viewers")]
        [Graphable("Viewers", "Count")]
        public HashSet<string> Viewers { get; set; }
    }

    public class ChattersWrapper : ITwitchStructure
    {
        [JsonProperty("chatter_count")]
        public int ChatterCount { get; set; }

        [JsonProperty("chatters")]
        [HasGraphables("Chat")]
        public Chatters Chatters { get; set; }
    }
}
