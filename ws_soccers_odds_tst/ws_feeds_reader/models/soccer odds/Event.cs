using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace ws_feeds_reader.models.socccer_odds
{
    public class Event
    {
        [XmlAttribute("eventid")]
        public string EventId { get; set; }

        [XmlAttribute("type")]
        public string Type { get; set; }

        [XmlAttribute("minute")]
        public string Minute { get; set; }

        [XmlAttribute("extra_min")]
        public string ExtraMin { get; set; }

        [XmlAttribute("team")]
        public string Team { get; set; }

        [XmlAttribute("player")]
        public string Player { get; set; }

        [XmlAttribute("result")]
        public string Result { get; set; }

        [XmlAttribute("playerId")]
        public string PlayerId { get; set; }

        [XmlAttribute("assist")]
        public string Assist { get; set; }

        [XmlAttribute("assistid")]
        public string AssistId { get; set; }
    }
}