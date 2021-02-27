using Client.MvvmLightMessages;
using Client.ViewModels;
using Client.Views;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Messenger.Default.Register<OpenChatView>(this, ShowChatView);
        }

        private void ShowChatView(OpenChatView data)
        {
            var chatView = new ChatView();
            chatView.DataContext = new ChatViewModel(data.User);
            chatView.Show();
            this.Close();
        }
    }
}
