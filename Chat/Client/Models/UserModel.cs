using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client.Models
{
    public class UserModel
    {
        public delegate void OnMessageReceivedDelegate(Message newMessage);
        public event OnMessageReceivedDelegate OnMessageReceived;

        public TcpClient Socket { get; set; }
        public string UserName { get; set; }

        public UserModel(TcpClient socket, string userName)
        {
            Socket = socket;
            UserName = userName;
            new Thread(() => Reader()).Start();
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
                byte[] buffer = new byte[1024];
                Socket.GetStream().Read(buffer, 0, 1024);
                var data = Encoding.ASCII.GetString(buffer).TrimEnd('\0').Split(':');
                Message newMessage = new Message(data[0], data[1]);
                OnMessageReceived(newMessage);
            }
        }
    }
}
