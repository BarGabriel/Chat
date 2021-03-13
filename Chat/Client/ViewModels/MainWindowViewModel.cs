using Client.Models;
using Client.MvvmLightMessages;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Client.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private TcpClient _socket;

        public string UserName { get; set; }

        public RelayCommand loginCommand { get; set; }

        public MainWindowViewModel()
        {
            _socket = new TcpClient();
            loginCommand = new RelayCommand(() => Login().ConfigureAwait(true));
        }

        private async Task Login()
        {
            await Task.Run(() => 
            {
                if (UserName == null || UserName == "")
                {
                    MessageBox.Show("User name can not be empty.", "UserName error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    try
                    {
                        _socket.Connect("127.0.0.1", 1234);
                        if (_socket.Connected)
                        {
                            Byte[] data = System.Text.Encoding.ASCII.GetBytes(UserName + '\0');
                            NetworkStream stream = _socket.GetStream();
                            stream.Write(data, 0, data.Length);
                        }
                        Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            Messenger.Default.Send(new OpenChatView(new UserModel(_socket, UserName)));
                        }));
                    }
                    catch (SocketException)
                    {
                        MessageBox.Show("Unable to login", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            });                   
        }
    }
}
