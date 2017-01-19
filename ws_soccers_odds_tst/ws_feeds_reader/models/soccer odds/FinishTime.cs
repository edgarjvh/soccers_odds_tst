using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace ws_feeds_reader.models.socccer_odds
{
    public class FinishTime
    {
        [XmlAttribute("score")]
        public string Score { get; set; }
    }
}