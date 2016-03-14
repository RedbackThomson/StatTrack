namespace StatTrack.UI.Services
{
    public interface ISettings
    {
        object this[string propertyName] { get; set; }
        void Save();
    }

    class Settings : ISettings
    {
        public object this[string propertyName]
        {
            get { return Properties.Settings.Default[propertyName]; }
            set { Properties.Settings.Default[propertyName] = value; }
        }

        public void Save()
        {
            Properties.Settings.Default.Save();
        }
    }
}
