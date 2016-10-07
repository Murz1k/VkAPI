using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace VkApi
{
    public class Comment
    {
        [XmlElement("id")]
        public int id { get; set; }
        [XmlElement("count")]
        public int count { get; set; }
        [XmlElement("can_post")]
        public int can_post { get; set; }
        [XmlElement("from_id")]
        public int from_id { get; set; }
    }
}
