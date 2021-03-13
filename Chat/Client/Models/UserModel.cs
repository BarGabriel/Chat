using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Client.Models
{
    public class UserModel
    {
        public delegate void OnMessageReceivedDelegate(Message newMessage);
        public event OnMessageReceivedDelegate OnMessageReceived;

        public delegate void CleanUpDelegate();
        public event CleanUpDelegate CleanUp;

        private TcpClient Socket { get; set; }
        private string UserName { get; set; }

        private Thread ReaderThread { get; set; }

        public UserModel(TcpClient socket, string userName)
        {
            Socket = socket;
            UserName = userName;
            ReaderThread = new Thread(() => Reader());
            ReaderThread.Start();
        }

        public void SendMessage(string message)
        {
            byte[] buffer = new byte[1024];
            buffer = Encoding.ASCII.GetBytes(message + '\0');
            Socket.GetStream().Write(buffer, 0, buffer.Length);
        }

        private void Reader()
        {
            while (true)
            {
                try
                {
                    byte[] buffer = new byte[1024];
                    Socket.GetStream().Read(buffer, 0, 1024);
                    var data = Encoding.ASCII.GetString(buffer).TrimEnd('\0').Split(':');
                    Message newMessage = new Message(data[0], data[1]);
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        OnMessageReceived(newMessage);
                    });
                }
                catch (Exception)
                {
                    CleanUp();
                }               
            }
        }

        public void CloseConnection()
        {
            ReaderThread.Abort();
            Socket.Close();
        }
    }
}
