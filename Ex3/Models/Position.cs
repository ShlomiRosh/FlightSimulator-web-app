
using System.Xml;

namespace Ex3.Models
{
    public class Position
    {
        public double Lon { get; set; }
        public double Lat { get; set; }
        public double Rudder { get; set; }
        public double Throttle { get; set; }
        /* If I want to use this XML structure of the base */
        //public void ToXml(XmlWriter writer)
        //{
        //    writer.WriteStartElement("Position");
        //    writer.WriteElementString("Lon", this.Lon.ToString());
        //    writer.WriteElementString("Lat", this.Lat.ToString());
        //    writer.WriteElementString("Rudder", this.Rudder.ToString());
        //    writer.WriteElementString("Throttle", this.Throttle.ToString());
        //    writer.WriteEndElement();
        //}
    }
}