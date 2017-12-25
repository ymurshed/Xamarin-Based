using System;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace FirebaseServiceCaller
{
    class Program
    {
        private static DeviceData data;

        static void Main(string[] args)
        {
            string device = Constants.DeviceName.LG.ToString();
            data = LoadData.LoadJsonData(device);

            string input = Constants.INPUT_FROM;

            if (input == "File")
                InputFromFile();
            else
                InputFromConsole();

            ServiceCall.FCMServiceCall(data);
            Console.ReadLine();
        }

        private static void InputFromConsole()
        {
            Console.WriteLine("...Firebase Cloud Messaging Service Calling...");
            Console.WriteLine("===============================================");

            Console.WriteLine("Enter Notification Title: ");
            var title = Console.ReadLine();
            Console.WriteLine();

            Console.WriteLine("Enter Notification Body: ");
            var body = Console.ReadLine();
            Console.WriteLine();

            Console.WriteLine("Please wait for response...");
            Console.WriteLine();

            data.Body.notification.title = title;
            data.Body.notification.body = body;
        }

        private static void InputFromFile()
        {
            int i;
            data.Body.notification.title = data.Body.notification.body = "";

            string[] newInput;
            string[] lines = System.IO.File.ReadAllLines(Constants.INPUT_FILE_PATH);
            
            for (i = 0; i < lines.Length; i++)
            {
                if (lines[i].Equals(""))
                    break;

                if (i == 0)
                    data.Body.notification.title = lines[i];
                else
                    data.Body.notification.body += lines[i] + "\n";
            }

            int newLength = lines.Length - ++i;

            if (newLength <= 0)
            {
                System.IO.File.WriteAllText(Constants.INPUT_FILE_PATH, string.Empty);
            }
            else
            {
                newInput = new string[newLength];
                Array.Copy(lines, i, newInput, 0, lines.Length - i);

                System.IO.File.WriteAllLines(Constants.INPUT_FILE_PATH, newInput);
            }
        }
    }
}
