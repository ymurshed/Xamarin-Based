using System;
using Android.App;
using Android.Content;
using Android.Util;
using Firebase.Messaging;

namespace ChromeNotifier.Droid
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class MyFirebaseMessagingService : FirebaseMessagingService
    {
        const string TAG = "MyFirebaseMsgService";
        string KEY_TEXT_REPLY = "key_text_reply";

        public override void OnMessageReceived(RemoteMessage message)
        {
            Log.Debug(TAG, "From: " + message.From);
            Log.Debug(TAG, "Notification Message Body: " + message.GetNotification().Body);
            Console.WriteLine("From: " + message.From);
            Console.WriteLine("Notification Message Body: " + message.GetNotification().Body);

            var v = message.GetNotification().ToString();
            SendNotification(message.GetNotification().Title, message.GetNotification().Body);
        }

        private Notification.Builder OldStyleNotification(string messageBody)
        {
            var intent = new Intent(this, typeof(MainActivity));
            intent.AddFlags(ActivityFlags.ClearTop);
            var pendingIntent = PendingIntent.GetActivity(this, 0, intent, PendingIntentFlags.OneShot);

            // use System.currentTimeMillis() to have a unique ID for the pending intent
            PendingIntent pIntent = PendingIntent.GetActivity(this, (int)DateTime.UtcNow.Millisecond, intent, 0);

#pragma warning disable CS0618 // Type or member is obsolete
            return new Notification.Builder(this)
                                            .SetSmallIcon(Resource.Drawable.abc_ic_star_black_48dp)
                                            .SetContentTitle("FCM Message")
                                            .SetContentText(messageBody)
                                            .SetAutoCancel(true)
                                            .AddAction(Resource.Drawable.icon, "Reply", pIntent)
#pragma warning restore CS0618 // Type or member is obsolete
                                            .SetContentIntent(pendingIntent);
        }

        void SendNotification(string title, string messageBody)
        {
            var intent = new Intent(this, typeof(MainActivity));
            intent.AddFlags(ActivityFlags.ClearTop);
            var pendingIntent = PendingIntent.GetActivity(this, 0, intent, PendingIntentFlags.OneShot);

            // use System.currentTimeMillis() to have a unique ID for the pending intent
            PendingIntent pIntent = PendingIntent.GetActivity(this, (int)DateTime.UtcNow.Millisecond, intent, 0);

            // Create the reply action and add the remote input
            //Notification.Action replyAction = new Notification.Action(Resource.Drawable.abc_btn_borderless_material, "Start", pIntent);

            // Create the snooze action and add the remote input.
            //Notification.Action snoozeAction = new Notification.Action(Resource.Drawable.abc_btn_borderless_material, "Snooze", pIntent);

            var notificationBuilder = new Notification.Builder(this)
                                            .SetSmallIcon(Resource.Drawable.abc_ic_star_black_48dp)
                                            .SetContentTitle(title)
                                            .SetContentText(messageBody)
                                            .SetAutoCancel(true)
                                            .SetContentIntent(pendingIntent);

            var notificationManager = NotificationManager.FromContext(this);
            notificationManager.Notify(0, notificationBuilder.Build());
        }
    }
}