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

    public class Group
    {
        [XmlElement("id")]
        public int id { get; set; }
        [XmlElement("name")]
        public string name { get; set; }
        [XmlElement("members_count")]
        public uint count { get; set; }
        [XmlElement("is_closed")]
        public uint is_closed { get; set; }
        public static async Task<Response> SearchAsync(string text, int offset = 0, int count = 20, string fields = "")
        {
            string url = "https://api.vk.com/method/groups.search.xml?q=" + text + "&sort=2&offset=" + offset + "&count=" + count + "&v=5.42&access_token=" + VkApi.Token + (fields == "" ? "" : "&fields=" + fields);
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
        public static Response Search(string text, int offset = 0, int count = 20, string fields = "")
        {
            string url = "https://api.vk.com/method/groups.search.xml?q=" + text + "&sort=2&offset=" + offset + "&count=" + count + "&v=5.42&access_token=" + VkApi.Token + (fields == "" ? "" : "&fields=" + fields);
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
        public static Response GetMembers(string group_id, int offset)
        {
            WebRequest request = WebRequest.Create("https://api.vk.com/method/groups.getMembers.xml?group_id=" + group_id + "&fields=photo_id,sex,city,country,has_photo&sort=id_asc&offset=" + offset + "&v=5.42&access_token=" + VkApi.Token);
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
                            Response members = (Response)Serializer.Deserialize(stringreader);
                            return members;
                        }
                    }
                }
            }
        }
        public async static Task<Response> GetMembersAsync(string group_id, int offset)
        {
            WebRequest request = WebRequest.Create("https://api.vk.com/method/groups.getMembers.xml?group_id=" + group_id + "&fields=photo_id,sex,city,country,has_photo&sort=id_asc&offset=" + offset + "&v=5.42&access_token=" + VkApi.Token);
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
                            Response members = (Response)Serializer.Deserialize(stringreader);
                            return members;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Асинхронно возвращает список сообществ указанного пользователя.
        /// </summary>
        /// <exception cref="ResponseException" />
        /// <param name="extended"></param>
        /// <param name="filter"></param>
        /// <param name="fields"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <returns>Возвращает список сообществ указанного пользователя.</returns>
        public async static Task<Group> GetAsync(int user_id = 1, int extended = 0, string filter = "admin, editor, moder, groups, publics, events", string fields = "city", int offset = 0, int count = 1000)
        {
            WebRequest request = WebRequest.Create("https://api.vk.com/method/groups.get.xml?user_id=" + user_id + "&extended=" + extended + "&filter=" + filter + "&fields=" + fields + "&offset=" + offset + "&count=" + count + "&v=5.42");
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
                        XmlSerializer Serializer = new XmlSerializer(typeof(Group));
                        using (StringReader stringreader = new StringReader(html))
                        {
                            Group members = (Group)Serializer.Deserialize(stringreader);
                            return members;
                        }
                    }
                }
            }
        }
    }
}
