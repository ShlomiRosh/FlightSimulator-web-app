using Ex3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace Ex3.Controllers
{
    public class SimulatorController : Controller
    {
        // GET: Simulator
        public ActionResult Home()
        {
            return View();
        }

        public ActionResult Display(string ip, int port, int interval)
        {
            stopConnection();
            if (!System.Net.IPAddress.TryParse(ip, out System.Net.IPAddress address)) //task 4
            {
                return ShowData(ip, port, interval);
            }
            else
            {
                return DoDisplay(ip, port, interval);
            }
            
        }

        [HttpGet]
        public ActionResult DoDisplay(string ip, int port, int interval)
        {
            Session["IP"] = ip;
            Session["PORT"] = port;
            Session["INTERVAL"] = interval;
            return View("Display");
        }

        [HttpGet]
        public ActionResult SaveData(string ip, int port, int interval, int time, string name)
        {
            stopConnection();
            Session["IP"] = ip;
            Session["PORT"] = port;
            Session["INTERVAL"] = interval;
            Session["TIME"] = time;
            Session["NAME"] = name;
            return View();
        }

        [HttpPost]
        public ActionResult GetDataPosition(string ip, int port, int interval)
        {

            //startConnection(ip, port);
            //Position position = new Position();
            //position.Lon = ClientTCP.Instance.SendSingelCommand("Lon");
            //position.Lat = ClientTCP.Instance.SendSingelCommand("Lat");
            //return Json(position, JsonRequestBehavior.AllowGet);
            /* TRY RANDOM */
            Random rnd = new Random();
            Position position = new Position();
            position.Lon = rnd.NextDouble() * 40;
            position.Lat = rnd.NextDouble() * 40;
            return Json(position, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult FileDataSave(string ip, int port, int interval, int time, string name)
        {
            
           //startConnection(ip, port);
           //Position position = new Position();
           //position.Lon = ClientTCP.Instance.SendSingelCommand("Lon");
           //position.Lat = ClientTCP.Instance.SendSingelCommand("Lat");
           //position.Rudder = ClientTCP.Instance.SendSingelCommand("Rudder");
           //position.Throttle = ClientTCP.Instance.SendSingelCommand("Throttle");          
            WriteRead write = WriteRead.Instance;
            if (!WriteRead.Instance.PathCreated)
            {                
                write.CreatePath(name);
            }
            //write.WriteData(position.Lon, position.Lat, position.Rudder, position.Throttle);
            //return Json(position, JsonRequestBehavior.AllowGet);
            /* TRY RANDOM */
            Random rnd = new Random();
            Position position = new Position();
            position.Lon = rnd.NextDouble() * 40;
            position.Lat = rnd.NextDouble() * 40;
            position.Rudder = rnd.NextDouble() * 4;
            position.Throttle = rnd.NextDouble() * 4;
            write.WriteData(position.Lon, position.Lat, position.Rudder, position.Throttle);
            return Json(position, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ShowData(string ip, int port, int interval)
        {
            Session["NAME"] = ip;
            Session["TIME"] = port;
            Session["INTERVAL"] = interval;
            return View("ShowData");
        }

        [HttpPost]
        public ActionResult ShowFileData(string ip, int port, int interval)
        {
            if (!WriteRead.Instance.PathCreated)
            {
                WriteRead.Instance.CreatePath(ip);
            }
            if (!WriteRead.Instance.ArrInitialized)
            {
                WriteRead.Instance.initializArrToRead();
            }

            int stop = WriteRead.Instance.ReadData();
            Position position = new Position();
            position = WriteRead.Instance.Position;
            if (stop == 0)
            {
                WriteRead.Instance.close();
            }
            return Json(new { position, stop }, JsonRequestBehavior.AllowGet);
        }

        private void stopConnection()
        {
            if (ClientTCP.Instance.isConnected)
            {
                ClientTCP.Instance.StopConnection();
            }
        }

        private void startConnection(string ip, int port)
        {
            if (!ClientTCP.Instance.isConnected)
            {
                ClientTCP.Instance.ConnectClientTCP(ip, port);
            }
        }
        /* If I want to use this XML structure of the base */
        //private string ToXml(Position position)
        //{
        //    //Initiate XML stuff
        //    StringBuilder sb = new StringBuilder();
        //    XmlWriterSettings settings = new XmlWriterSettings();
        //    XmlWriter writer = XmlWriter.Create(sb, settings);

        //    writer.WriteStartDocument();
        //    //writer.WriteStartElement("Position");

        //    position.ToXml(writer);

        //    //writer.WriteEndElement();
        //    writer.WriteEndDocument();
        //    writer.Flush();
        //    return sb.ToString();
        //}


        //[HttpPost]
        //public string GetPosition()
        //{
        //    Random rnd = new Random();
        //    Position position = new Position();
        //    position.Lon = rnd.NextDouble() * 40;
        //    position.Lat = rnd.NextDouble() * 40;
        //    position.Rudder = rnd.NextDouble() * 1;
        //    position.Throttle = rnd.NextDouble() * 1;

        //    return ToXml(position);
        //}

    }
}