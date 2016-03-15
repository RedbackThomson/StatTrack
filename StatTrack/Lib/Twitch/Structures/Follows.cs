using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace StatTrack.Lib.Twitch.Structures
{
    public class FollowsWrapper : ITwitchStructure
    {
        [JsonProperty("_total")]
        [Graphable("Followers")]
        public int Total { get; set; }

        [JsonProperty("_cursor")]
        public string Cursor { get; set; }

        [JsonProperty("follows")]
        public HashSet<Follow> Follows { get; set; }
    }

    public class Follow
    {
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("notifications")]
        public bool Notifications { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }
    }
}
