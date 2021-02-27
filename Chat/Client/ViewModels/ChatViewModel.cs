﻿using Client.Models;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModels
{
    public class ChatViewModel : ViewModelBase
    {
        private UserModel User { get; set; }

        public ChatViewModel(UserModel user)
        {
            User = user;
        }
    }
}
