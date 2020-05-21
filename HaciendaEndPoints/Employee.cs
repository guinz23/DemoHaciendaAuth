using System;
using System.Xml.Serialization;

namespace HaciendaEndPoints
{
    public class Employee
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("age")]
        public string Age { get; set; }
    }
}
