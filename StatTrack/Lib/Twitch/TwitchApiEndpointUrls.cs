using System;
using System.Collections.Generic;
using StatTrack.Lib.Twitch.Structures;

namespace StatTrack.Lib.Twitch
{
    internal class TwitchApiEndpointUrls
    {
        /// <summary>
        /// Gets the /chatters API endpoint url
        /// </summary>
        /// <param name="username">The username of the channel</param>
        /// <returns>The endpoint url</returns>
        public static string GetChatters(string username)
        {
            return string.Format(TwitchApiEndpoint.Chatters.Url, username);
        }

        /// <summary>
        /// Gets the /followers API endpoint url
        /// </summary>
        /// <param name="channel">The username of the channel</param>
        /// <returns>The endpoint url</returns>
        public static string GetFollowers(string channel)
        {
            return string.Format(TwitchApiEndpoint.Followers.Url, channel);
        }
    }

    public class TwitchApiEndpoint
    {
        private const string TmiBase = "https://tmi.twitch.tv/";
        private const string ApiBase = "https://api.twitch.tv/kraken";

        private const string _chatters = TmiBase + "group/user/{0}/chatters";
        private const string _followers = ApiBase + "/channels/{0}/follows";

        public static TwitchApiEndpoint Chatters = new TwitchApiEndpoint(_chatters, "Chatters", typeof(ChattersWrapper));
        public static TwitchApiEndpoint Followers = new TwitchApiEndpoint(_followers, "Followers", typeof(FollowsWrapper));

        public static List<TwitchApiEndpoint> GetAllEndpoints()
        {
            return new List<TwitchApiEndpoint>
            {
                Chatters,
                Followers
            };
        }

        private TwitchApiEndpoint(string url, string name, Type returnType)
        {
            Url = url;
            Name = name;
            ReturnStructure = returnType;
        }

        public string Url { get; private set; }
        public string Name { get; private set; }
        public Type ReturnStructure { get; private set; }
    }
}
