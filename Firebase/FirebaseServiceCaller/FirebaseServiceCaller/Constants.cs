using System;
using System.Collections.Generic;
using System.Text;

namespace FirebaseServiceCaller
{
    public class Constants
    {
        public enum DeviceName { LG, SAMSUNG, NEXUS }

        public const int RETRY_COUNT = 3;
        public const string INPUT_FROM = "File";
        public const string CONFIG_FILE_PATH = "DataConfig.json";
        public const string INPUT_FILE_PATH = "Notification.txt";
        public const string FIREBASE_URL = "https://fcm.googleapis.com/fcm/send";
    }
}
