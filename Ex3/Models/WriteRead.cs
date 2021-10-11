using System;
using System.IO;
using System.Web;

namespace Ex3.Models
{
    public class WriteRead
    {
        
        private static WriteRead s_instace = null;
        private int i;
        private string path = "";
        private string[] lines;
        public static WriteRead Instance
        {
            get
            {
                if (s_instace == null)
                {
                    s_instace = new WriteRead();
                }
                return s_instace;
            }
        }

        public bool PathCreated { get; set; }
        public bool ArrInitialized { get; set; }
        public Position Position { get; private set; }

        public WriteRead()
        {
            Position = new Position();
            i = 0;
        }

        public const string SCENARIO_FILE = "~/App_Data/{0}.txt";

        public void CreatePath(string name)
        {
            path = HttpContext.Current.Server.MapPath(String.Format(SCENARIO_FILE, name));
            PathCreated = true;
        }

        public void WriteData(double lon, double lat, double rudder, double throttle)
        {
            Position.Lon = lon;
            Position.Lat = lat;
            Position.Rudder = rudder;
            Position.Throttle = throttle;

            if (!File.Exists(path))
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(path, true))
                {
                    file.WriteLine((Position.Lon).ToString() + '|' + (Position.Lat).ToString() + '|' 
                        + (Position.Rudder).ToString()  + '|' + (Position.Throttle).ToString());
                }
            } 
            else
            {
                using (StreamWriter stream = System.IO.File.AppendText(path))
                {
                    stream.WriteLine((Position.Lon).ToString() + '|' + (Position.Lat).ToString() + '|'
                        + (Position.Rudder).ToString() + '|' + (Position.Throttle).ToString());
                }
            }
        }

        public void initializArrToRead()
        {
            lines = System.IO.File.ReadAllLines(path); // reading all the lines of the file
            ArrInitialized = true;
        }

        public void close()
        {
            s_instace = null;
            ArrInitialized = false;
        }

        public int ReadData()
        {
            if (lines.Length > i) 
            {
                string ownLine = lines[i++];
                string[] position = ownLine.Split('|');
                Position.Lon = Convert.ToDouble(position[0]);
                Position.Lat = Convert.ToDouble(position[1]);
                Position.Rudder = Convert.ToDouble(position[2]);
                Position.Throttle = Convert.ToDouble(position[3]);
                return 1;
            }
            else
            {
                return 0;
            }
        }
        
    }
}