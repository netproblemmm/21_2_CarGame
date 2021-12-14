using Tools.PushNotifications.Settings;
using UnityEngine;

namespace Tools.PushNotifications
{
    internal class StubNotificationScheduler : INotificationScheduler
    {
        public void ScheduleNotification(NotificationData notificationData) =>
            Debug.Log($"[{GetType()}] {notificationData}");
    }
}
