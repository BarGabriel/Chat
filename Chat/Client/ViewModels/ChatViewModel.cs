using Client.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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

        private string _textToSend;
        public string TextToSend
        {
            get { return _textToSend; }
            set
            {
                _textToSend = value;
                RaisePropertyChanged(() => TextToSend);
            }
        }

        public RelayCommand SendCommand { get; set; }

        public ChatViewModel(UserModel user)
        {
            User = user;
            User.OnMessageReceived += new UserModel.OnMessageReceivedDelegate(OnMessageReceivedHandler);
            Messages = new ObservableCollection<Message>();
            SendCommand = new RelayCommand(Send);
        }

        private void Send()
        {
            if (TextToSend != null && TextToSend != "")
            {
                User.SendMessage(TextToSend);
            }
            TextToSend = String.Empty;
        }

        private void OnMessageReceivedHandler(Message newMessage)
        {
            Application.Current.Dispatcher.Invoke(() => 
            {
                Messages.Add(newMessage);
            });
        }
    }
}
