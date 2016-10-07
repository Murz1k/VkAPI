using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace VkApi
{

    public class Item
    {
        [XmlElement("id")]
        public int id { get; set; }
        [XmlElement("user")]
        public List<User> Users { get; set; }
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
        [XmlElement("post")]
        public List<Post> post { get; set; }
        [XmlElement("group")]
        public List<Group> group { get; set; }
        [XmlElement("photo")]
        public List<Photos> photos { get; set; }
        [XmlElement("comment")]
        public List<Comment> comment { get; set; }
    }
}
