using Mapbox.Utils;
using System;
using System.Xml;
using System.Xml.Serialization;

namespace Geoduck
{
    [XmlRoot("gpx", Namespace = Constants.xmlRootNamespace)]
    public class GpxStructure
    {
        public Vector2d Coordinates
        {
            get => new Vector2d(double.Parse(wpt.lat), double.Parse(wpt.lon));
        }
        public Waypoint wpt;
    }

    public class Waypoint
    {
        [XmlAttribute] public string lat;
        [XmlAttribute] public string lon;

        [XmlElement("name")]
        public string code;

        [XmlElement("cache", Namespace = Constants.xmlNamespace)]
        public CacheDetails details;
    }

    public class CacheDetails
    {
        [XmlElement("name", Namespace = Constants.xmlNamespace)]
        public string name;

        [XmlElement("type", Namespace = Constants.xmlNamespace)]
        public string type;

        [XmlElement("container", Namespace = Constants.xmlNamespace)]
        public string container;

        [XmlElement("difficulty", Namespace = Constants.xmlNamespace)]
        public float difficulty;

        [XmlElement("terrain", Namespace = Constants.xmlNamespace)]
        public float terrain;

        [XmlElement("encoded_hints", Namespace = Constants.xmlNamespace)]
        public string hint;

        /*[XmlArrayItem("logs", Namespace = Constants.xmlNamespace)]
        public List<Log> logs;*/
    }

    public class Log
    {
        [XmlElement("date", Namespace = Constants.xmlNamespace)]
        public DateTime date;

        [XmlElement("type", Namespace = Constants.xmlNamespace)]
        public string type;
    }
}
