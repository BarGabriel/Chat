using Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client.MvvmLightMessages
{
    public class OpenChatView
    {
        public UserModel User { get; set; }

        public OpenChatView(UserModel user)
        {
            User = user;
        }
    }
}
