using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FirebaseServiceCaller
{
    class LoadData
    {
        private static JObject GetJsonObject()
        {
            JObject jo;
            
            using (StreamReader sr = new StreamReader(File.OpenRead(Constants.CONFIG_FILE_PATH)))
            {
                using (JsonTextReader reader = new JsonTextReader(sr))
                {
                    jo = (JObject)JToken.ReadFrom(reader);
                }
            }

            return jo;
        }

        public static string GetJsonString(DeviceData data)
        {
            return JsonConvert.SerializeObject(data.Body);
        }

        public static DeviceData LoadJsonData(string device)
        {
            var jo = GetJsonObject();
            return JsonConvert.DeserializeObject<DeviceData>(jo[device].ToString());
        }

        public static Response LoadJsonResponse(string response)
        {
            var jo = GetJsonObject();
            return JsonConvert.DeserializeObject<Response>(response);
        }
    }
}
