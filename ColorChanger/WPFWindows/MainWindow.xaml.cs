using ColorChanger.JsonData;
using ColorChanger.Twitch;
using System;
using System.Windows;

namespace ColorChanger.WPFWindows
{
    public partial class MainWindow : Window
    {
        private ChatClient _chatClient;

        public MainWindow()
        {
            JsonController.LoadSettings();
            JsonController.SaveSettings();
            SetUpChatClient();
            AddEvents();
            InitializeComponent();
        }

        private void SetUpChatClient()
        {
            if (string.IsNullOrEmpty(JsonController.AppSettings.Account.Username) || string.IsNullOrEmpty(JsonController.AppSettings.Account.OAuthToken))
            {
                if (new LoginWindow().ShowDialog() != true)
                {
                    Environment.Exit(0);
                }
            }
            _chatClient = new();
        }

        private void AddEvents()
        {
            _chatClient.TwitchClient.OnConnected += (sender, e) => listBoxLogs.Items.Add("Connected");
            _chatClient.TwitchClient.OnDisconnected += (sender, e) => listBoxLogs.Items.Add("Disconnected");
            _chatClient.TwitchClient.OnJoinedChannel += (sender, e) => listBoxLogs.Items.Add($"Joined {e.Channel}");
            _chatClient.OnColorChanged += (sender, e) => listBoxLogs.Items.Add($"Color changed to {e.Color}");
        }
    }

}
