using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Ex3.Models
{
    public class ClientTCP
    {
        private NetworkStream stream;
        private TcpClient tcpClient;
        private List<string> commands;
        public bool isConnected { get; set; }
        public Dictionary<string, string> pathCommands = new Dictionary<string, string>()
        {
            { "Lon", "get /position/longitude-deg\r\n" },
            { "Lat", "get /position/latitude-deg\r\n" },
            { "Rudder", "get /controls/flight/rudder\r\n" },
            { "Throttle", "get /controls/engines/current-engine/throttle\r\n" }
        };

        private static ClientTCP instance = null;
        public static ClientTCP Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ClientTCP();
                }
                return instance;
            }
        }

        public ClientTCP()
        {
            commands = new List<string>();
            tcpClient = new TcpClient();
        }

        public void ConnectClientTCP(string IP, int port)
        {
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(IP), port);
            tcpClient.Connect(iPEndPoint);
            isConnected = true;          
        }

        public double SendSingelCommand(string commandKey)
        {
            if (!tcpClient.Connected)
            {
                return 0;
            }
            string commandValue = pathCommands[commandKey];
            stream = tcpClient.GetStream();
            byte[] sendCommand = Encoding.ASCII.GetBytes(commandValue);
            stream.Write(sendCommand, 0, sendCommand.Length); // send to the server
            byte[] data = new byte[1024];
            int dataBytes = stream.Read(data, 0, data.Length);
            string toBild = Encoding.ASCII.GetString(data, 0, dataBytes);
            toBild = toBild.Split('=')[1].Split(' ')[1].Split('\'')[1];
            return double.Parse(toBild);
        }

        public void StopConnection()
        {
            tcpClient.Close();
        }

    }
}