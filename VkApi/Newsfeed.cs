using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VkApi
{
    public class Newsfeed
    {
        public static Response Unsubscribe(int owner_id, int item_id, string type)
        {
            string url = "https://api.vk.com/method/newsfeed.unsubscribe.xml?type=" + type + "&owner_id=" + owner_id + "&item_id=" + item_id + "&v=5.42&access_token=" + VkApi.Token;
            Response members = Response.Request(url);
            return members;
        }
    }
}
