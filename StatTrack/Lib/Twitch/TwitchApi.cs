using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StatTrack.Lib.Twitch.Structures;

namespace StatTrack.Lib.Twitch
{
    internal class TwitchApi
    {
        private readonly TwitchApiSettings _settings;

        private readonly Dictionary<TwitchApiEndpoint, Func<Task<ITwitchStructure>>> _getters;
        public Dictionary<TwitchApiEndpoint, Func<Task<ITwitchStructure>>> Getters { get{return _getters;} } 

        public TwitchApi(TwitchApiSettings settings)
        {
            _settings = settings;
            _getters = new Dictionary<TwitchApiEndpoint, Func<Task<ITwitchStructure>>>
            {
                {TwitchApiEndpoint.Chatters, GetChatters},
                {TwitchApiEndpoint.Followers, GetFollowers}
            };
        }

        /// <summary>
        /// Gets the channel's chatters
        /// </summary>
        /// <returns>The API chatters object</returns>
        public async Task<ITwitchStructure> GetChatters()
        {
            var url = TwitchApiEndpointUrls.GetChatters(_settings.Channel);
            return await DeserializeApiCall<ChattersWrapper>(url);
        }

        /// <summary>
        /// Gets the channel's followers
        /// </summary>
        /// <returns>The API followers object</returns>
        public async Task<ITwitchStructure> GetFollowers()
        {
            var url = TwitchApiEndpointUrls.GetFollowers(_settings.Channel);
            return await DeserializeApiCall<FollowsWrapper>(url);
        }

        /// <summary>
        /// Gets the current UTC Unix timestamp as a string
        /// </summary>
        /// <returns>The current UTC Unix timestamp</returns>
        private static string GetTimestamp()
        {
            var span = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0);
            return ((long)span.TotalSeconds).ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Makes an API call and deserializes the object
        /// </summary>
        /// <typeparam name="T">The type of the return object</typeparam>
        /// <param name="url">The URL of the API call</param>
        /// <returns>The deserialized API object</returns>
        private static async Task<T> DeserializeApiCall<T>(string url)
        {
            url += "?_=" + GetTimestamp();
            var apiResponse = await ApiCall(url);
            return JsonConvert.DeserializeObject<T>(apiResponse);
        }

        private static async Task<string> ApiCall(string url)
        {
            var client = new HttpClient();

            return await client.GetStringAsync(url);
        }
    }
}
