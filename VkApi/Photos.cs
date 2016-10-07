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

    public class Photos
    {
        /// <summary>
        /// Возвращает список фотографий в альбоме.
        /// </summary>
        /// <exception cref="ResponseException" />
        /// <param name="owner_id"></param>
        /// <param name="token"></param>
        /// <returns>Возвращает список фотографий в альбоме.</returns>
        public async static Task<Response> GetAsync(int owner_id)
        {
            WebRequest request = WebRequest.Create("https://api.vk.com/method/photos.get.xml?owner_id=" + owner_id + "&album_id=profile&extended=1&v=5.42&access_token=" + VkApi.Token);
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
        public async static Task<Response> GetByIdAsync(string photo_id)
        {
            WebRequest request = WebRequest.Create("https://api.vk.com/method/photos.getById.xml?photos=" + photo_id + "&extended=1&v=5.42");
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
        public static Response Get(int owner_id)
        {
            WebRequest request = WebRequest.Create("https://api.vk.com/method/photos.get.xml?owner_id=" + owner_id + "&album_id=profile&extended=1&v=5.42&access_token=" + VkApi.Token);
            using (WebResponse response = request.GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string html = reader.ReadToEnd();
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
        [XmlElement("owner_id")]
        public int owner_id { get; set; }
        [XmlElement("id")]
        public int id { get; set; }
        [XmlElement("photo_130")]
        public string url1 { get; set; }
        [XmlElement("photo_604")]
        public string url2 { get; set; }
        [XmlElement("photo_807")]
        public string url3 { get; set; }
        [XmlElement("photo_1280")]
        public string url4 { get; set; }
        [XmlElement("photo_2560")]
        public string url5 { get; set; }
        [XmlElement("likes")]
        public Likes likes { get; set; }
    }
}
