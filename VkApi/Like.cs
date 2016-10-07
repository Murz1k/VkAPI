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
    public class Likes
    {
        [XmlElement("liked")]
        public int liked { get; set; }
        [XmlElement("copied")]
        public int copied { get; set; }
        [XmlElement("user_likes")]
        public int user_likes { get; set; }
        [XmlElement("count")]
        public int count { get; set; }
        /// <summary>
        /// Асинхронно добавляет указанный объект в список "Мне нравится" текущего пользователя, и возвращает текущее количество пользователей, которые добавили данный объект в свой список "Мне нравится".
        /// </summary>
        /// <exception cref="ResponseException" />
        /// <param name="owner_id"></param>
        /// <param name="item_id"></param>
        /// <param name="token"></param>
        /// <returns>Возвращает текущее количество пользователей, которые добавили данный объект в свой список "Мне нравится".</returns>
        public static async Task<int> AddAsync(string type, int owner_id, int item_id)
        {
            WebRequest request = WebRequest.Create("https://api.vk.com/method/likes.add.xml?type=" + type + "&owner_id=" + owner_id + "&item_id=" + item_id + "&v=5.42&access_token=" + VkApi.Token);
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
                            return resp.likes;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Добавляет указанный объект в список "Мне нравится" текущего пользователя, и возвращает текущее количество пользователей, которые добавили данный объект в свой список "Мне нравится".
        /// </summary>
        /// <exception cref="ResponseException" />
        /// <param name="owner_id"></param>
        /// <param name="item_id"></param>
        /// <param name="token"></param>
        /// <returns>Возвращает текущее количество пользователей, которые добавили данный объект в свой список "Мне нравится".</returns>
        public static int Add(string type, int owner_id, int item_id)
        {
            WebRequest request = WebRequest.Create("https://api.vk.com/method/likes.add.xml?type=" + type + "&owner_id=" + owner_id + "&item_id=" + item_id + "&v=5.42&access_token=" + VkApi.Token);
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
                            return resp.likes;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Асинхронно проверяет, находится ли объект в списке "Мне нравится" заданного пользователя, и возвращает объект с полями liked и copied.
        /// </summary>
        /// <exception cref="ResponseException" />
        /// <param name="type"></param>
        /// <param name="owner_id"></param>
        /// <param name="item_id"></param>
        /// <returns>Возвращает объект с полями liked и copied.</returns>
        public static async Task<Likes> IsLikedAsync(string type, int user_id, int owner_id, string item_id)
        {
            WebRequest request = WebRequest.Create("https://api.vk.com/method/likes.isLiked.xml?type=" + type + "&user_id=" + user_id + "&item_id=" + item_id + "&owner_id=" + owner_id + "&v=5.42&access_token=" + VkApi.Token);
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
                        XmlSerializer Serializer = new XmlSerializer(typeof(Likes));
                        using (StringReader stringreader = new StringReader(html))
                        {
                            Likes resp = (Likes)Serializer.Deserialize(stringreader);
                            return resp;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Проверяет, находится ли объект в списке "Мне нравится" заданного пользователя, и возвращает объект с полями liked и copied.
        /// </summary>
        /// <exception cref="ResponseException" />
        /// <param name="type"></param>
        /// <param name="owner_id"></param>
        /// <param name="item_id"></param>
        /// <returns>Возвращает объект с полями liked и copied.</returns>
        public static Likes IsLiked(string type, int user_id, int owner_id, string item_id)
        {
            WebRequest request = WebRequest.Create("https://api.vk.com/method/likes.isLiked.xml?type=" + type + "&user_id=" + user_id + "&type=photo&item_id=" + item_id + "&owner_id=" + owner_id + "&v=5.42&access_token=" + VkApi.Token);
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
                        XmlSerializer Serializer = new XmlSerializer(typeof(Likes));
                        using (StringReader stringreader = new StringReader(html))
                        {
                            Likes resp = (Likes)Serializer.Deserialize(stringreader);
                            return resp;
                        }
                    }
                }
            }
        }

    }
}
