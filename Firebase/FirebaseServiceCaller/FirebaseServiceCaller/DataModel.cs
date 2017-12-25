using System;
using System.Collections.Generic;
using System.Text;

namespace FirebaseServiceCaller
{
    public class notification
    {
        public string body { get; set; }
        public string title { get; set; }
        public string content_available { get; set; }
        public string priority { get; set; }
    }

    public class Header
    {
        public string authorization { get; set; }
        public string contenttype { get; set; }
    }

    public class Body
    {
        public string to { get; set; }
        public notification notification { get; set; }
    }

    public class Response
    {
        public bool success { get; set; }
        public bool failure { get; set; }
    }

    public class DeviceData
    {
        public Header Header { get; set; }
        public Body Body { get; set; }
        public Body Response { get; set; }
    }
}
