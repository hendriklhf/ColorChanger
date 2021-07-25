using ColorChanger.JsonData;
using ColorChanger.Twitch;
using System.Windows;

namespace ColorChanger.WPFWindows
{
    public partial class MainWindow : Window
    {
        private ChatClient _chatClient;

        public MainWindow()
        {
            JsonController.LoadSettings();
            SetUpChatClient();
            AddEvents();
            InitializeComponent();
            SetUpUI();
        }

        private void SetUpChatClient()
        {
            if (string.IsNullOrEmpty(JsonController.AppSettings.Account.Username) || string.IsNullOrEmpty(JsonController.AppSettings.Account.OAuthToken))
            {
                _ = new LoginWindow().ShowDialog();
            }
            _chatClient = new();
        }

        private void AddEvents()
        {
            _chatClient.TwitchClient.OnConnected += (sender, e) =>
            {
                listBoxLogs.Dispatcher.Invoke(() => listBoxLogs.Items.Add("Connected"));
            };
            _chatClient.TwitchClient.OnDisconnected += (sender, e) =>
            {
                listBoxLogs.Dispatcher.Invoke(() => listBoxLogs.Items.Add("Disconnected"));
            };
            _chatClient.TwitchClient.OnConnectionError += (sender, e) =>
            {
                _chatClient.Disconnect();
                SetUpChatClient();
                AddEvents();
                _chatClient.Connect();
            };
            _chatClient.TwitchClient.OnJoinedChannel += (sender, e) =>
            {
                listBoxLogs.Dispatcher.Invoke(() => listBoxLogs.Items.Add($"Joined channel {e.Channel}"));
            };
            _chatClient.OnColorChanged += (sender, e) =>
            {
                listBoxLogs.Dispatcher.Invoke(() => listBoxLogs.Items.Add($"Changed color to {e.Color}"));
            };
        }

        private void SetUpUI()
        {
            listBoxLogs.Items.Clear();
            lblUsername.Content = JsonController.AppSettings.Account.Username;
            btnConnect.Visibility = Visibility.Visible;
        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            _chatClient.Disconnect();
            JsonController.ResetSettings();
            SetUpChatClient();
            AddEvents();
            SetUpUI();
            Show();
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settings = new();
            settings.Closed += (sender, e) =>
            {
                _chatClient.Disconnect();
                SetUpChatClient();
                AddEvents();
                SetUpUI();
                _chatClient.Connect();
            };
            settings.Show();
        }

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            btnConnect.Visibility = Visibility.Hidden;
            _chatClient.Connect();
        }
    }
}
