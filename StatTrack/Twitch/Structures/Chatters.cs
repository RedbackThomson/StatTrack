using System.Collections.Generic;
using Newtonsoft.Json;

namespace StatTrack.Twitch.Structures
{
    public class Chatters
    {
        [JsonProperty("moderators")]
        public HashSet<string> Moderators { get; set; }

        [JsonProperty("staff")]
        public HashSet<string> Staff { get; set; }

        [JsonProperty("admin")]
        public HashSet<string> Admins { get; set; }

        [JsonProperty("global_mods")]
        public HashSet<string> GlobalMods { get; set; }

        [JsonProperty("viewers")]
        public HashSet<string> Viewers { get; set; }
    }

    public class ChattersWrapper : ITwitchStructure
    {
        [JsonProperty("chatter_count")]
        public int ChatterCount { get; set; }

        [JsonProperty("chatters")]
        public Chatters Chatters { get; set; }
    }
}
