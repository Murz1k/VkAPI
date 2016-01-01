using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

    public class VkApi
    {
        private string redirect_uri = "https://vk.com";
        private string display = "popup";
        private string scope = null;
        private static string token = null;
        private static int id = 0;
        private int ApplicationId = 0;
        public string Scope { internal get { return scope; } set 
        { 
            scope = value;
            Url = String.Format("https://oauth.vk.com/authorize?client_id={0}&display={1}&redirect_uri={2}&scope={3}&response_type={4}&v={5}", ApplicationId, display, redirect_uri, scope, response_type, v);
        } }
        private string response_type = "token";
        private string v = "5.42";
        public string Url { get; internal set; }
        public VkApi(int applicationId)
        {
            ApplicationId = applicationId;
            }
        public static int IdStatic { internal get { return id; } set { id = value; } }
        public int Id { get { return id; } set { id = value; } }
        public static string TokenStatic { internal get { return token; } set { token = value; } }
        public string Token { internal get { return token; } set { token = value; } }

    }
    [XmlRoot("response")]
    public class Stats
    {
        public static async Task<bool> TrackVisitorAsync()
        {
            string html = "https://api.vk.com/method/stats.trackVisitor?v=5.42&access_token=" + VkApi.TokenStatic;
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
        public static bool TrackVisitor()
        {
            string html = "https://api.vk.com/method/stats.trackVisitor?v=5.42&access_token=" + VkApi.TokenStatic;
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
    [XmlRoot("response")]
    public class Response
    {
        [XmlElement(ElementName = "user")]
        public List<Users> users { get; set; }
        [XmlElement(ElementName = "photo")]
        public List<Photos> photos { get; set; }

        [XmlElement("count")]
        public int count { get; set; }
        [XmlElement("items")]
        public Items items { get; set; }
        [XmlElement("likes")]
        public int likes { get; set; }
    }
    [XmlRoot("response")]
    public class Groups
    {
        [XmlElement("count")]
        public int count { get; set; }
        [XmlElement("items")]
        public List<Items> items { get; set; }
        public static Members GetMembers(string group_id, int offset)
        {
            WebRequest request = WebRequest.Create("https://api.vk.com/method/groups.getMembers.xml?group_id=" + group_id + "&fields=photo_id,sex,city,country,has_photo    &sort=id_asc&offset=" + offset + "&v=5.42&access_token=" + VkApi.TokenStatic);
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
                        XmlSerializer Serializer = new XmlSerializer(typeof(Members));
                        using (StringReader stringreader = new StringReader(html))
                        {
                            Members members = (Members)Serializer.Deserialize(stringreader);
                            return members;
                        }
                    }
                }
            }
        }
        public async static Task<Members> GetMembersAsync(string group_id, int offset)
        {
            WebRequest request = WebRequest.Create("https://api.vk.com/method/groups.getMembers.xml?group_id=" + group_id + "&fields=photo_id,sex,city,country,has_photo&sort=id_asc&offset=" + offset + "&v=5.42&access_token=" + VkApi.TokenStatic);
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
                        XmlSerializer Serializer = new XmlSerializer(typeof(Members));
                        using (StringReader stringreader = new StringReader(html))
                        {
                            Members members = (Members)Serializer.Deserialize(stringreader);
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
        public async static Task<Groups> GetAsync(int user_id = 1,int extended = 0, string filter = "admin, editor, moder, groups, publics, events", string fields = "city", int offset = 0, int count = 1000)
        {
            WebRequest request = WebRequest.Create("https://api.vk.com/method/groups.get.xml?user_id=" + user_id + "&extended=" + extended + "&filter=" + filter + "&fields=" + fields + "&offset=" + offset + "&count=" + count+"&v=5.42");
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
                        XmlSerializer Serializer = new XmlSerializer(typeof(Groups));
                        using (StringReader stringreader = new StringReader(html))
                        {
                            Groups members = (Groups)Serializer.Deserialize(stringreader);
                            return members;
                        }
                    }
                }
            }
        }
    }
    [XmlRoot("response")]
    public class Members
    {
        [XmlElement("count")]
        public int count { get; set; }
        [XmlElement("items")]
        public Items items { get; set; }
    }
    public class Items
    {
        [XmlElement("id")]
        public int id { get; set; }
        [XmlElement("user")]
        public List<Users> Users { get; set; }
        [XmlElement("name")]
        public string name { get; set; }
        [XmlElement("screen_name")]
        public string screen_name { get; set; }
        [XmlElement("photo_50")]
        public string photo_50 { get; set; }
        [XmlElement("photo_100")]
        public string photo_100 { get; set; }
        [XmlElement("photo_200")]
        public string photo_200 { get; set; }
        [XmlElement("photo")]
        public List<Photos> photos { get; set; }
    }
    [Serializable]
    public class ResponseException : ApplicationException
    {
        public ResponseException() { }

        public ResponseException(string message) : base(message) { }

        public ResponseException(string message, Exception inner) : base(message, inner) { }

        protected ResponseException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
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
            WebRequest request = WebRequest.Create("https://api.vk.com/method/photos.get.xml?owner_id=" + owner_id + "&album_id=profile&extended=1&v=5.42&access_token=" + VkApi.TokenStatic);
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
            WebRequest request = WebRequest.Create("https://api.vk.com/method/photos.get.xml?owner_id=" + owner_id + "&album_id=profile&extended=1&v=5.42&access_token=" + VkApi.TokenStatic);
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
            WebRequest request = WebRequest.Create("https://api.vk.com/method/likes.add.xml?type=" + type + "&owner_id=" + owner_id + "&item_id=" + item_id + "&v=5.42&access_token=" + VkApi.TokenStatic);
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
            WebRequest request = WebRequest.Create("https://api.vk.com/method/likes.add.xml?type=" + type + "&owner_id=" + owner_id + "&item_id=" + item_id + "&v=5.42&access_token=" + VkApi.TokenStatic);
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
        public static async Task<Likes> IsLikedAsync(string type, int owner_id, string item_id)
        {
            WebRequest request = WebRequest.Create("https://api.vk.com/method/likes.isLiked.xml?type="+type+"&user_id=" + VkApi.IdStatic + "&type=photo&item_id="+item_id+"&owner_id=" + owner_id + "&v=5.42&access_token=" + VkApi.TokenStatic);
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
        public static Likes IsLiked(string type, int owner_id, string item_id)
        {
            WebRequest request = WebRequest.Create("https://api.vk.com/method/likes.isLiked.xml?type=" + type + "&user_id=" + VkApi.IdStatic + "&type=photo&item_id=" + item_id + "&owner_id=" + owner_id + "&v=5.42&access_token=" + VkApi.TokenStatic);
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
    public class Users
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
    public class Country
    {
        [XmlElement(ElementName = "id")]
        public int id { get; set; }
        [XmlElement(ElementName = "title")]
        public string title { get; set; }
    }
    public class City
    {
        [XmlElement(ElementName = "id")]
        public int id { get; set; }
        [XmlElement(ElementName = "title")]
        public string title { get; set; }
    }
    [XmlRoot("error")]
    public class Error
    {
        private Error error = null;

        private string GetError(Error error)
        {
            string text = "Неизвестная ошибка";
            switch (error.error_code)
            {
                case 1: text = "Произошла неизвестная ошибка"; break;
                case 2: text = "Приложение выключено."; break;
                case 3: text = "Передан неизвестный метод."; break;
                case 4: text = "Неверная подпись."; break;
                case 5: text = "Авторизация пользователя не удалась."; break;
                case 6: text = "Слишком много запросов в секунду."; break;
                case 7: text = "Нет прав для выполнения этого действия."; break;
                case 8: text = "Неверный запрос."; break;
                case 9: text = "Слишком много однотипных действий."; break;
                case 10: text = "Произошла внутренняя ошибка сервера."; break;
                case 11: text = "В тестовом режиме приложение должно быть выключено или пользователь должен быть залогинен."; break;
                case 14: text = "Требуется ввод кода с картинки (Captcha)."; break;
                case 15: text = "Доступ запрещён."; break;
                case 16: text = "Требуется выполнение запросов по протоколу HTTPS, т.к. пользователь включил настройку, требующую работу через безопасное соединение."; break;
                case 17: text = "Требуется валидация пользователя."; break;
                case 20: text = "Данное действие запрещено для не Standalone приложений."; break;
                case 21: text = "Данное действие разрешено только для Standalone и Open API приложений."; break;
                case 23: text = "Метод был выключен."; break;
                case 24: text = "Требуется подтверждение со стороны пользователя."; break;
                case 100: text = "Один из необходимых параметров был не передан или неверен."; break;
                case 101: text = "Неверный API ID приложения."; break;
                case 113: text = "Неверный идентификатор пользователя."; break;
                case 150: text = "Неверный timestamp."; break;
                case 200: text = "Доступ к альбому запрещён."; break;
                case 201: text = "Доступ к аудио запрещён."; break;
                case 203: text = "Доступ к группе запрещён."; break;
                case 300: text = "Альбом переполнен."; break;
                case 500: text = "Действие запрещено. Вы должны включить переводы голосов в настройках приложения."; break;
                case 600: text = "Нет прав на выполнение данных операций с рекламным кабинетом."; break;
                case 603: text = "Произошла ошибка в работе с рекламным кабинетом."; break;
            }
            return text;
        }
        public Error()
        {

        }
        public Error(string text)
        {
            XmlSerializer xmlSerializer1 = new XmlSerializer(typeof(Error));
            error = (Error)xmlSerializer1.Deserialize(new StringReader(text));
            error_code = error.error_code;
            error_msg = GetError(error);
        }
        [XmlElement("error_code")]
        public int error_code { get; set; }
        [XmlElement("error_msg")]
        public string error_msg { get; set; }

        [XmlElement("captcha_sid")]
        public string id { get; set; }
        [XmlElement("captcha_img")]
        public string url { get; set; }
    }
    public class Audio
    {
        [XmlElement("id")]
        public int id { get; set; }
        [XmlElement("owner_id")]
        public int owner_id { get; set; }
        [XmlElement("artist")]
        public string artist { get; set; }
        [XmlElement("title")]
        public string title { get; set; }
        [XmlElement("duration")]
        public int duration { get; set; }
        [XmlElement("date")]
        public int date { get; set; }
        [XmlElement("url")]
        public string url { get; set; }
        [XmlElement("lyrics_id")]
        public int lyrics_id { get; set; }
        [XmlElement("genre_id")]
        public int genre_id { get; set; }
    }
public class AudioCollection
{
    [XmlElement("audio")]
    public List<Audio> audio { get; set; }
}
[XmlRoot("response")]
public class AudioResponse
{

    [XmlElement("count")]
    public int count { get; set; }
    [XmlElement("items")]
    public AudioCollection collection;
    /// <summary>
    /// Асинхронно возвращает список аудиозаписей пользователя или сообщества.
    /// </summary>
    /// <exception cref="ResponseException" />
    /// <param name="owner_id"></param>
    /// <param name="item_id"></param>
    /// <param name="token"></param>
    /// <returns>Возвращает список аудиозаписей пользователя или сообщества.</returns>
    public static async Task<AudioResponse> GetAsync(int owner_id = 1, int album_id = 0, int album_ids = 0, int need_user = 0, int offset = 0, int count = 0)
    {
        WebRequest request = WebRequest.Create("https://api.vk.com/method/audio.get.xml?owner_id=" + owner_id + "&album_id=" + album_id + "&album_ids=" + album_ids + "&need_user=" + need_user + "&offset=" + offset + "&count=" + count + "&v=5.42&access_token=" + VkApi.TokenStatic);
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
                    XmlSerializer Serializer = new XmlSerializer(typeof(AudioResponse));
                    using (StringReader stringreader = new StringReader(html))
                    {
                        AudioResponse resp = (AudioResponse)Serializer.Deserialize(stringreader);
                        return resp;
                    }
                }
            }
        }
    }
}
