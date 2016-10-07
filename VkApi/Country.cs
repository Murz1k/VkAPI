using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace VkApi
{
    public class Country
    {
        [XmlElement(ElementName = "id")]
        public int id { get; set; }
        [XmlElement(ElementName = "title")]
        public string title { get; set; }
    }
}
