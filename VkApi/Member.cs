using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace VkApi
{
    public class Members
    {
        [XmlElement("count")]
        public int count { get; set; }
        [XmlElement("items")]
        public Item items { get; set; }
    }
}
