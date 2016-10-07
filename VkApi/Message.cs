using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VkApi
{
    public class Message
    {
        public static Response Send(int user_id, string text)
        {
            string url = "https://api.vk.com/method/messages.send.xml?user_id=" + user_id + "&text=" + text + "&v=5.42&token="+VkApi.Token;
            return Response.Request(url);
        }
    }
}
