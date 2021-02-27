using Client.Models;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModels
{
    public class ChatViewModel : ViewModelBase
    {
        private UserModel User { get; set; }

        private ObservableCollection<Message> _messages;
        public ObservableCollection<Message> Messages
        {
            get { return _messages; }
            set
            {
                _messages = value;
                RaisePropertyChanged(() => Messages);
            }
        }

        public ChatViewModel(UserModel user)
        {
            User = user;
            Messages = new ObservableCollection<Message>();
        }
    }
}
