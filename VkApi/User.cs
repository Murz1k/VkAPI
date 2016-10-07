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

    public class User
    {
        [XmlElement(ElementName = "id")]
        public int id { get; set; }
        [XmlElement(ElementName = "first_name")]
        public string First_Name { get; set; }
        [XmlElement(ElementName = "last_name")]
        public string Last_Name { get; set; }
        [XmlElement(ElementName = "has_photo")]
        public int has_photo { get; set; }
        [XmlElement(ElementName = "sex")]
        public int sex { get; set; }
        [XmlElement(ElementName = "bdate")]
        public string bdate { get; set; }
        [XmlElement(ElementName = "deactivated")]
        public string deactivated { get; set; }
        [XmlElement(ElementName = "photo_id")]
        public string photo_id { get; set; }
        [XmlElement(ElementName = "city")]
        public City city { get; set; }
        [XmlElement(ElementName = "country")]
        public Country country { get; set; }
        public static async Task<Response> GetAsync(int user_ids, string fields)
        {
            WebRequest request = WebRequest.Create("https://api.vk.com/method/users.get.xml?user_ids=" + user_ids + "&fields=" + fields + "&v=5.42");
            using (WebResponse response = await request.GetResponseAsync())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string html = await reader.ReadToEndAsync();
                        if (html.IndexOf("error") > -1)
                        {
                            Error error = new Error(html);
                            throw new ResponseException(String.Format("Ошибка: ({0}): {1}", error.error_code, error.error_msg));
                        }
                        XmlSerializer Serializer = new XmlSerializer(typeof(Response));
                        using (StringReader stringreader = new StringReader(html))
                        {
                            Response resp = (Response)Serializer.Deserialize(stringreader);
                            return resp;
                        }
                    }
                }
            }
        }
    }
}
