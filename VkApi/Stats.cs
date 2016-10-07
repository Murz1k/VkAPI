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

    public class Stats
    {
        [XmlElement("day")]
        public string day { get; set; }
        [XmlElement("viewes")]
        public uint viewes { get; set; }
        [XmlElement("visitors")]
        public uint visitors { get; set; }
        [XmlElement("subscribed")]
        public uint subscribed { get; set; }
        [XmlElement("unsubscribed")]
        public uint unsubscribed { get; set; }
        /// <summary>
        /// Возвращает статистику сообщества или приложения.
        /// </summary>
        /// <param name="group_id"></param>
        /// <param name="app_id"></param>
        /// <param name="date_from"></param>
        /// <param name="date_to"></param>
        /// <returns></returns>
        public static bool Get(uint group_id = 1, uint app_id = 0, string date_from = "2000-01-01", string date_to = "2000-01-02")
        {
            string html = "https://api.vk.com/method/stats.get?v=5.42&access_token=" + VkApi.Token;
            WebRequest request = WebRequest.Create(html);
            using (WebResponse response = request.GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        html = reader.ReadToEnd();
                        if (html.IndexOf("error") > -1)
                        {
                            Error error = new Error(html);
                            throw new ResponseException(String.Format("Ошибка: ({0}): {1}", error.error_code, error.error_msg));
                        }
                        if (html == "{\"response\":1}")
                        {
                            return true;
                        }
                        return false;
                    }
                }
            }
        }
        /// <summary>
        /// Асинхронно добавляет данные о текущем сеансе в статистику посещаемости приложения, и возращает true в случае успешной обработки данных.
        /// </summary>
        /// <returns>Возращает true в случае успешной обработки данных</returns>
        public static async Task<bool> TrackVisitorAsync()
        {
            string html = "https://api.vk.com/method/stats.trackVisitor?v=5.42&access_token=" + VkApi.Token;
            WebRequest request = WebRequest.Create(html);
            using (WebResponse response = await request.GetResponseAsync())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        html = await reader.ReadToEndAsync();
                        if (html.IndexOf("error") > -1)
                        {
                            Error error = new Error(html);
                            throw new ResponseException(String.Format("Ошибка: ({0}): {1}", error.error_code, error.error_msg));
                        }
                        if (html == "{\"response\":1}")
                        {
                            return true;
                        }
                        return false;
                    }
                }
            }
        }
        /// <summary>
        /// Добавляет данные о текущем сеансе в статистику посещаемости приложения, и возращает true в случае успешной обработки данных.
        /// </summary>
        /// <returns>Возращает true в случае успешной обработки данных</returns>
        public static bool TrackVisitor()
        {
            string html = "https://api.vk.com/method/stats.trackVisitor?v=5.42&access_token=" + VkApi.Token;
            WebRequest request = WebRequest.Create(html);
            using (WebResponse response = request.GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        html = reader.ReadToEnd();
                        if (html.IndexOf("error") > -1)
                        {
                            Error error = new Error(html);
                            throw new ResponseException(String.Format("Ошибка: ({0}): {1}", error.error_code, error.error_msg));
                        }
                        if (html == "{\"response\":1}")
                        {
                            return true;
                        }
                        return false;
                    }
                }
            }
        }
    }
}
