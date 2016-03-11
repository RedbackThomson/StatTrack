using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatTrack
{
    public interface IStatTrack : INotifyPropertyChanged
    {
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
