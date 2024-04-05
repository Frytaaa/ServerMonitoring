using Microsoft.AspNetCore.Mvc;
using Tinkerforge;

namespace ServerMonitoring.Controllers
{
    public class ServerMonitoringController : Controller
    {
        private static string HOST = "localhost";
        private static int PORT = 4223;
        private static string UID1 = "68WXq6";
        private static string UID2 = "62D7kk";
        private static string UID3 = "6nCVXX";


        static void Main()
        {
            IPConnection ipcon = new IPConnection();
            BrickMaster master = new BrickMaster(UID1, ipcon);

            ipcon.Connect(HOST, PORT);

        }
    }
}
