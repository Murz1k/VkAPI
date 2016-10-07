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

    public class Wall
    {
        public static int Post(int owner_id, string message)
        {
            string url = "https://api.vk.com/method/wall.post.xml?owner_id=" + owner_id  + "&message=" + message + "&v=5.42&access_token=" + VkApi.Token;
            Response members = Response.Request(url);
            return members.post_id;
        }
        public async static Task<int> PostAsync(int owner_id,string message)
        {
            string url = "https://api.vk.com/method/wall.post.xml?owner_id=" + owner_id  + "&message=" + message + "&v=5.42&access_token=" + VkApi.Token;
            Response members = await Response.RequestAsync(url);
            return members.post_id;
        }
        public static Response GetComments(int owner_id, int post_id, int offset = 0, int count = 100)
        {
            string url = "https://api.vk.com/method/wall.getComments.xml?owner_id=" + owner_id + "&post_id=" + post_id + "&count=" + count + "&offset=" + offset + "&v=5.42&access_token=" + VkApi.Token;
            return Response.Request(url);
        }
        public static async Task<Response> GetCommentsAsync(int owner_id, int post_id, int offset = 0, int count = 100)
        {
            string url = "https://api.vk.com/method/wall.getComments.xml?owner_id=" + owner_id + "&post_id=" + post_id + "&count=" + count + "&offset=" + offset + "&v=5.42&access_token=" + VkApi.Token;
            return await Response.RequestAsync(url);
        }
        public static int AddComment(int owner_id, int post_id, string text, string attachments)
        {
            string url = "https://api.vk.com/method/wall.addComment.xml?attachments=" + attachments + "&owner_id=" + owner_id + "&post_id=" + post_id + "&text=" + text + "&v=5.42&access_token=" + VkApi.Token;
            Response members = Response.Request(url);
            return members.comment_id;
        }
        public static Response DeleteComment(int owner_id, int comment_id)
        {
            string url = "https://api.vk.com/method/wall.deleteComment.xml?comment_id=" + comment_id + "&owner_id=" + owner_id  + VkApi.Token;
            Response members = Response.Request(url);
            return members;
        }
        public static async Task<int> AddCommentAsync(int owner_id, int post_id, string text, string attachments)
        {
            string url = "https://api.vk.com/method/wall.addComment.xml?attachments=" + attachments + "&owner_id=" + owner_id + "&post_id=" + post_id + "&text=" + text + "&v=5.42&access_token=" + VkApi.Token;
            Response members = await Response.RequestAsync(url);
            return members.comment_id;
        }
        public static Response Get(int owner_id, int offset, int count)
        {
            string url = "https://api.vk.com/method/wall.get.xml?owner_id=" + owner_id + "&offset=" + offset + "&count=" + count + "&v=5.42";
            return Response.Request(url);
        }
        public static async Task<Response> GetAsync(int owner_id, int offset, int count)
        {
            string url = "https://api.vk.com/method/wall.get.xml?owner_id=" + owner_id + "&offset=" + offset + "&count=" + count + "&v=5.42";
            return await Response.RequestAsync(url);
        }
    }
}
