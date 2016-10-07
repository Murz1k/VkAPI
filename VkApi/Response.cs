using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace VkApi
{

    [XmlRoot("response")]
    public class Response
    {
        [XmlElement(ElementName = "user")]
        public List<User> users { get; set; }
        [XmlElement(ElementName = "photo")]
        public List<Photos> photos { get; set; }
        [XmlElement(ElementName = "comment_id")]
        public int comment_id { get; set; }
        [XmlElement(ElementName = "post_id")]
        public int post_id { get; set; }

        [XmlElement("count")]
        public int count { get; set; }
        [XmlElement("items")]
        public Item items { get; set; }
        [XmlElement("likes")]
        public int likes { get; set; }
        public string url { get; set; }
        internal static  async Task<Response> RequestAsync(string url)
        {
            WebRequest request = WebRequest.Create(url);
            using (WebResponse response = await request.GetResponseAsync())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string html = await reader.ReadToEndAsync();
                        if (html.Contains("error"))
                        {
                            Error error = new Error(html);
                            VkApi.GetLastError = error;
                            throw new ResponseException(String.Format("Ошибка: ({0}): {1}", error.error_code, error.error_msg));
                        }
                        XmlSerializer Serializer = new XmlSerializer(typeof(Response));
                        using (StringReader stringreader = new StringReader(html))
                        {
                            Response members = (Response)Serializer.Deserialize(stringreader);
                            members.url = url;
                            return members;
                        }
                    }
                }
            }
        }
        internal static Response Request(string url)
        {
            WebRequest request = WebRequest.Create(url);
            using (WebResponse response = request.GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string html = reader.ReadToEnd();
                        if (html.Contains("error"))
                        {
                            Error error = new Error(html);
                            VkApi.GetLastError = error;
                            throw new ResponseException(String.Format("Ошибка: ({0}): {1}", error.error_code, error.error_msg));
                        }
                        XmlSerializer Serializer = new XmlSerializer(typeof(Response));
                        using (StringReader stringreader = new StringReader(html))
                        {
                            Response members = (Response)Serializer.Deserialize(stringreader);
                            members.url = url;
                            return members;
                        }
                    }
                }
            }
        }
    }
}
