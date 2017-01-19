using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace ws_feeds_reader.models.socccer_odds
{
    public class Odds
    {
        [XmlElement("type")]
        public List<OddType> Types { get; set; }

        public Odds()
        {
            Types = new List<OddType>();
        }
    }
}