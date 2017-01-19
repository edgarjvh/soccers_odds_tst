using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace ws_feeds_reader.models.socccer_odds
{
    public class OddType
    {
        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlAttribute("value")]
        public string Value { get; set; }

        [XmlElement("bookmaker")]
        public List<Bookmaker> Bookmakers { get; set; }

        public OddType()
        {
            Bookmakers = new List<Bookmaker>();
        }
    }
}