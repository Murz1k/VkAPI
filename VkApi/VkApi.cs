using System.IO;
using System.Net;

namespace VkApi
{
    public class VkApi
    {
        private CookieContainer _cookies;
        private string _scope;
        public static string Token;
        private int _applicationId;
        private int _userId;
        private string _v = "5.42";
        
        public VkApi(int applicationId,string scope)
        {
            _applicationId = applicationId;
            _scope = scope;
            _cookies = new CookieContainer();
        }
        
        public int User_Id { get { return _userId; } }

        public void Authorization(string email, string pass)
        {
            SendUserData(email, pass);
            Authorization();
        }

        private void SendUserData(string email, string pass)
        {
            string url = GetActionLink();
            HttpWebRequest Post = WebRequest.Create(url) as HttpWebRequest;
            Post.Method = "POST";
            Post.CookieContainer = _cookies;
            Post.ContentType = "application/x-www-form-urlencoded";
            string data = "email=" + email + "&pass=" + pass;
            using (StreamWriter writer = new StreamWriter(Post.GetRequestStream()))
            {
                writer.Write(data);
            }
            using (WebResponse response = Post.GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    //using (StreamReader reader = new StreamReader(stream))
                    //{
                    //    html = reader.ReadToEnd();
                    //}
                }
            }
            _cookies = Post.CookieContainer;
        }

        private string GetActionLink()
        {
            HttpWebRequest Get = WebRequest.Create("https://login.vk.com/?act=login") as HttpWebRequest;
            Get.CookieContainer = _cookies;
            string html;
            using (WebResponse response = Get.GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        html = reader.ReadToEnd();
                    }
                }
            }
            _cookies = Get.CookieContainer;
            html = html.Remove(0, html.IndexOf("action") + 8);
            string url = html.Remove(html.IndexOf('"'));
            return url;
        }

        private void Authorization()
        {
            string url = string.Format($"https://oauth.vk.com/authorize?client_id={0}&display=popup&redirect_uri=https://vk.com&scope={1}&response_type=token&v={2}", 5212368, _scope, _v);
            HttpWebRequest authorizationRequest = WebRequest.Create(url) as HttpWebRequest;
            authorizationRequest.CookieContainer = _cookies;
            WebResponse response1 = authorizationRequest.GetResponse();
            using (WebResponse response = authorizationRequest.GetResponse())
            {
                string html = response.ResponseUri.ToString();
                Token = html.Split('=')[1].Split('&')[0];
                _userId = int.Parse(html.Split('=')[3]);
            }
        }

        private static Error LastError = new Error();
        public static Error GetLastError { get { return LastError; } internal set { LastError = value; } }
    }
}
