using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatTrack.Lib.Twitch;

namespace StatTrack.Lib
{
    public interface IStatTrack : INotifyPropertyChanged
    {
        /// <summary>
        /// Keeps track of all of the API responses
        /// </summary>
        ApiResponseHandlers Handlers { get; }

        /// <summary>
        /// The list of all endpoints that should be tracked
        /// </summary>
        HashSet<TwitchApiEndpoint> Trackers { get; }

        /// <summary>
        /// Determines whether the tracker information is able to start tracking
        /// </summary>
        /// <returns>True if all the configuration is correct</returns>
        bool CanStartMonitoring();

        /// <summary>
        /// Starts monitoring the API endpoints
        /// </summary>
        void StartMonitoring();

        /// <summary>
        /// Stops monitoring the API endpoints
        /// </summary>
        void StopMonitoring();

        /// <summary>
        /// Gets the current settings
        /// </summary>
        /// <returns>The current settings</returns>
        StatTrackSettings GetSettings();

        /// <summary>
        /// Returns whether the stat track is monitoring
        /// </summary>
        /// <returns>True if the stat track is monitoring</returns>
        bool IsMonitoring();
    }
}
