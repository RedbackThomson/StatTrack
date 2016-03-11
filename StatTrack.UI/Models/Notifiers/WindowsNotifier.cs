using Windows.UI.Notifications;

namespace StatTrack.UI.Models.Notifiers
{
    public class WindowsNotifier : INotifier
    {
        private const string AppId = "StatTrack";

        private static ToastNotification NewNotification(string text)
        {
            var template = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText02);
            var stringElements = template.GetElementsByTagName("text");
            stringElements[0].AppendChild(template.CreateTextNode(AppId));
            stringElements[1].AppendChild(template.CreateTextNode(text));

            return new ToastNotification(template);
        }

        public void NotifyNew(string format, params object[] args)
        {
            ToastNotificationManager.CreateToastNotifier(AppId)
                .Show(NewNotification(string.Format(format, args)));
        }
    }
}
