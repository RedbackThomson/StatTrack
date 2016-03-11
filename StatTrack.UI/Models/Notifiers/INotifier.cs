namespace StatTrack.UI.Models.Notifiers
{
    public interface INotifier
    {
        /// <summary>
        /// Notifies about a new item
        /// </summary>
        /// <param name="format">The string format of the output</param>
        /// <param name="args">The format item</param>
        void NotifyNew(string format, params object[] args);
    }
}
