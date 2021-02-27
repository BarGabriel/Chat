using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    public class Message
    {
        public string From { get; set; }
        public string Data { get; set; }

        public Message(string from, string data)
        {
            From = from;
            Data = data;
        }
    }
}
