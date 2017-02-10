using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace ws_feeds_reader.models.socccer_odds
{
    public class Events
    {
        [XmlElement("event")]
        public List<Event> List { get; set; }
    }
}