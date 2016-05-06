using System;
using System.Collections.Generic;
using Windows.UI.Notifications;
using NotificationsExtensions.Toasts;

namespace StatTrack.UI.Services
{
    public interface INotifier
    {
        void ShowNewNotification(string text);
        void ClearNotifications();
    }

    public class Notifier : INotifier
    {
        private readonly ISettings _settings;

        private const string AppTitle = "StatTrack";
        private const string AppId = "StatTrack";

        private readonly List<ToastNotification> _notifications; 

        public Notifier(ISettings settings)
        {
            _settings = settings;
            _notifications = new List<ToastNotification>();
        }

        public void ShowNewNotification(string text)
        {
            ClearNotifications();

            var xmlDoc = new Windows.Data.Xml.Dom.XmlDocument();
            xmlDoc.LoadXml(GetToastContent(text).GetContent());

            var toast = new ToastNotification(xmlDoc)
            {
                ExpirationTime = DateTime.Now.AddMilliseconds(_settings.UpdatePeriod)
            };
            _notifications.Add(toast);
            ToastNotificationManager.CreateToastNotifier(AppId).Show(toast);
        }

        public void ClearNotifications()
        {
            foreach (var toast in _notifications)
                ToastNotificationManager.CreateToastNotifier(AppId).Hide(toast);
            _notifications.Clear();
        }

        private ToastContent GetToastContent(string content)
        {
            var visual = new ToastVisual()
            {
                TitleText = new ToastText()
                {
                    Text = AppTitle
                },
                BodyTextLine1 = new ToastText()
                {
                    Text = content
                }
            };

            return new ToastContent()
            {
                Visual = visual,
                Audio = new ToastAudio {Silent = true}
            };
        }
    }
}
