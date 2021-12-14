using Tools.PushNotifications.Settings;

namespace Tools.PushNotifications
{
    internal interface INotificationScheduler
    {
        void ScheduleNotification(NotificationData notificationData);
    }
}
