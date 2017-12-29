using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;

public static class RestfulApiHandler
{
    #region Common Methods - Restful API
    private static object HttpWebRequest_Get<T>(string url)
    {
        try
        {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "GET";

            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception(String.Format("Server error (HTTP {0}: {1}).",
                                        response.StatusCode,
                                        response.StatusDescription));
                DataContractJsonSerializer dataContractSerializer = new DataContractJsonSerializer(typeof(T));
                using (var responseStream = response.GetResponseStream())
                {
                    var rtnValue = (T)dataContractSerializer.ReadObject((responseStream));
                    return rtnValue;
                }
            }
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    public static object Get<TI, TO>(string url)
    {
        object retVal = null;
        try
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {
                    retVal = response.Content.ReadAsAsync<TO>().Result;
                }

                return retVal;
            }
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    public static object Post<TI, TO>(string url, object obj = null)
    {
        try
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.PostAsJsonAsync(url, obj).Result;

                if (response.IsSuccessStatusCode)
                {
                    dynamic respContent = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);

                    TO retVal = respContent;

                    return retVal;
                }

                return null;
            }
        }
        catch (Exception e)
        {
            throw e;
        }
    }
    #endregion
}
