using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FirebaseServiceCaller
{
    public class ServiceCall
    {
        public static void FCMServiceCall(DeviceData data)
        {
            int retry = 0;

            try
            {
                while (retry < Constants.RETRY_COUNT)
                {
                    var httpWebRequest = (HttpWebRequest)WebRequest.Create(Constants.FIREBASE_URL);
                    httpWebRequest.Headers.Add("Authorization", "key=" + data.Header.authorization);
                    httpWebRequest.ContentType = data.Header.contenttype;
                    httpWebRequest.Method = "POST";

                    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    {
                        string json = LoadData.GetJsonString(data);

                        streamWriter.Write(json);
                        streamWriter.Flush();
                        streamWriter.Close();
                    }

                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = LoadData.LoadJsonResponse(streamReader.ReadToEnd());

                        if (result.success && !result.failure)
                        {
                            Console.WriteLine("Notification successfully sent :)");
                            break;
                        }
                        else
                        {
                            retry++;
                            if (retry == Constants.RETRY_COUNT)
                            {
                                Console.WriteLine("Notification not sent :(");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("FCM service call error occurred! Details: " + ex.ToString());
            }
        }
    }
}
