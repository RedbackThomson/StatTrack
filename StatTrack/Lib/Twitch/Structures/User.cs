using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;

namespace StatTrack.Lib.Twitch.Structures
{
    public class User
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("bio")]
        public string Bio { get; set; }

        [JsonProperty("logo")]
        public string Logo { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("_id")]
        public int Id { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdateAt { get; set; }
    }
}
