using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    public class UserModel
    {
        public TcpClient Socket { get; set; }
        public string UserName { get; set; }

        public UserModel(TcpClient socket, string userName)
        {
            Socket = socket;
            UserName = userName;
        }
    }
}
