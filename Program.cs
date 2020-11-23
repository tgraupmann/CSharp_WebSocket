// Ref: https://archive.codeplex.com/?p=websocket4net
// Ref: https://stackoverflow.com/questions/5420656/unable-to-read-data-from-the-transport-connection-an-existing-connection-was-f

using SuperSocket.ClientEngine;
using System;
using System.Net;
using System.Threading;
using WebSocket4Net;

namespace CSharp_WebSocket
{
    class Program
    {
        private static WebSocket _mWebsocket = null;

        private static void OnWebSocket_Opened(object sender, EventArgs e)
        {
            _mWebsocket.Send("Hello World!");
        }

        private static void OnWebSocket_Error(object sender, ErrorEventArgs e)
        {
            Console.WriteLine("WebSocket Error: {0}", e.Exception);
        }

        private static void OnWebSocket_Closed(object sender, EventArgs e)
        {
            Console.WriteLine("WebSocket Closed");
        }

        private static void OnWebSocket_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            Console.WriteLine("WebSocket MessageReceived: {0}", e);
        }

        static void Main(string[] args)
        {
            System.Net.ServicePointManager.Expect100Continue = false;
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            _mWebsocket = new WebSocket("wss://THE_ENDPOINT");
            _mWebsocket.Opened += new EventHandler(OnWebSocket_Opened);
            _mWebsocket.Error += new EventHandler<ErrorEventArgs>(OnWebSocket_Error);
            _mWebsocket.Closed += new EventHandler(OnWebSocket_Closed);
            _mWebsocket.MessageReceived += new EventHandler<MessageReceivedEventArgs>(OnWebSocket_MessageReceived);

            _mWebsocket.Open();

            while (true)
            {
                Thread.Sleep(0);
            }
        }
    }
}
