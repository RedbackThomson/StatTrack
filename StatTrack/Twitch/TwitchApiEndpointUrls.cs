namespace StatTrack.Twitch
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

        public static TwitchApiEndpoint Chatters = new TwitchApiEndpoint(_chatters);
        public static TwitchApiEndpoint Followers = new TwitchApiEndpoint(_followers);

        private TwitchApiEndpoint(string url)
        {
            Url = url;
        }

        public string Url { get; set; }
    }
}
