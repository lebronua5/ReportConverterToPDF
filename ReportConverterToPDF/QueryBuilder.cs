using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;

namespace ReportConverterToPDF
{
    static class QueryBuilder
    {
        static string _apiBaseUri = "https://fzcore.fozzy.lan:9999/Production.v2/REST";

        public static T MakeRequest<T>(string httpMethod, string route, string json, string idVCMH = null)
        {
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create($"{_apiBaseUri}/{route}");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = httpMethod;

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(json);
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var content = streamReader.ReadToEnd();
                    var result = JsonConvert.DeserializeObject<T>(content);

                    return result;
                }
            }
            catch (Exception ex)
            {
                dynamic jsonObject = JsonConvert.DeserializeObject(json);
                string exception = JsonConvert.SerializeObject(new
                {
                    sid = jsonObject.sid,
                    operationName = FozzyCoreOperationNames.SetReportPdf,
                    request = $"<data id=\"{idVCMH}\" url_pdf=\"\" error=\"{ex.ToString()}\"/>"
                });

                var result = QueryBuilder.MakeRequest<dynamic>("POST", "Execute", exception);
                return default(T);
            }
        }
    }
}
