using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharp_WebSocket
{
    class Program
    {
        static async void ConnectAndSend()
        {
            ClientWebSocket client = new ClientWebSocket();
            Uri uri = new Uri("wss://WEB_SOCKET_ENDPOINT");

            var cts = new CancellationTokenSource();
            await client.ConnectAsync(uri, cts.Token);

            while (client.State != WebSocketState.Open)
            {
                await Task.Delay(100);
            }
            Console.WriteLine("WebSocket connected!");

            try
            {
                string message = "{\"action\":\"sendmessage\", \"data\":\"hello C#\"}";
                byte[] sendBytes = UTF8Encoding.UTF8.GetBytes(message);
                var sendBuffer = new ArraySegment<byte>(sendBytes);
                cts = new CancellationTokenSource();
                await client.SendAsync(sendBuffer, WebSocketMessageType.Text, true, cts.Token);
            }
            catch
            {
            }
            finally
            {
                Console.WriteLine("Data sent!");
            }

            try
            {
                cts = new CancellationTokenSource();
                await client.CloseAsync(WebSocketCloseStatus.NormalClosure, "Exit", cts.Token);
            }
            catch
            {
            }
            finally
            {
                Console.WriteLine("WebSocket closed!");
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Press Enter to Exit.");
            ConnectAndSend();
            Console.ReadKey();
        }
    }
}
