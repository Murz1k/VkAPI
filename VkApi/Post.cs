using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace VkApi
{
    public class Post
    {
        [XmlElement("id")]
        public int id { get; set; }
        [XmlElement("comments")]
        public Comment comments { get; set; }
    }
}
