using ColorChanger.JsonData;
using System;
using System.Text.RegularExpressions;
using TwitchLib.Client;
using TwitchLib.Client.Enums;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Clients;
using TwitchLib.Communication.Enums;
using TwitchLib.Communication.Models;

namespace ColorChanger.Twitch
{
    public class ChatClient
    {
        public TwitchClient TwitchClient { get; }

        public ConnectionCredentials ConnectionCredentials { get; }

        public ClientOptions ClientOptions { get; }

        public TcpClient TcpClient { get; }

        public event EventHandler<OnColorChangedArgs> OnColorChanged;

        public static int ColorIndex
        {
            get => _index;
            private set
            {
                _index = value;
                if (_index == JsonController.AppSettings.Account.Channels.Count)
                {
                    _index = 0;
                }
            }
        }

        private static int _index = 0;

        public ChatClient()
        {
            ConnectionCredentials = new(JsonController.AppSettings.Account.Username, JsonController.AppSettings.Account.OAuthToken);
            ClientOptions = new()
            {
                ClientType = ClientType.Chat,
                ReconnectionPolicy = new(10000, 30000, 1000),
                UseSsl = true
            };
            TcpClient = new(ClientOptions);
            TwitchClient = new(TcpClient, ClientProtocol.TCP)
            {
                AutoReListenOnException = true
            };
            TwitchClient.Initialize(ConnectionCredentials, JsonController.AppSettings.Account.Channels);
            TwitchClient.OnMessageReceived += OnMessageReceived;
        }

        public void Connect()
        {
            TwitchClient.Connect();
        }

        private void OnMessageReceived(object sender, OnMessageReceivedArgs e)
        {
            if (e.ChatMessage.Username == JsonController.AppSettings.Account.Username.ToLower() && !Regex.IsMatch(e.ChatMessage.Message, @"color\s#[0-9A-Fa-f]{6}$"))
            {
                try
                {
                    string color = JsonController.AppSettings.Colors[ColorIndex];
                    TwitchClient.SendMessage(JsonController.AppSettings.Account.Username, $".color {color}");
                    ColorIndex++;
                    OnColorChanged.Invoke(this, new(color));
                }
                catch (Exception)
                {
                    ColorIndex = 0;
                }
            }
        }
    }
}
