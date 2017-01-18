using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace ws_feeds_reader.models.soccer_odds
{
    public class Category
    {
        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("gid")]
        public string GID { get; set; }

        [XmlAttribute("file_group")]
        public string FileGroup { get; set; }

        [XmlAttribute("iscup")]
        public string IsCup { get; set; }

        [XmlElement("matches")]
        public Matches Matches { get; set; }

        public Category()
        {
            Matches = new Matches();
        }
    }
}