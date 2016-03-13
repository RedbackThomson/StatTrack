namespace StatTrack.UI.Services
{
    public interface ISettings
    {
        object this[string propertyName] { get; set; }
        void Save();
    }
}
