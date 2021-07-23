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

        public JsonController JsonController { get; } = new();

        public static int ColorIndex
        {
            get => _index;
            private set
            {
                _index = value;
                if (_index == JsonController.AppSettings.AccountSettings.Channels.Count)
                {
                    _index = 0;
                }
            }
        }

        private static int _index = 0;

        public ChatClient()
        {
            ConnectionCredentials = new(JsonController.AppSettings.AccountSettings.Username, JsonController.AppSettings.AccountSettings.OAuthToken);
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
            TwitchClient.Initialize(ConnectionCredentials, JsonController.AppSettings.AccountSettings.Channels);
            TwitchClient.OnMessageReceived += OnMessageReceived;
            TwitchClient.OnConnected += (sender, e) => Console.WriteLine("Client connected!");
            TwitchClient.OnJoinedChannel += (sender, e) => Console.WriteLine($"Joined channel {e.Channel}");
            TwitchClient.Connect();
        }

        private void OnMessageReceived(object sender, OnMessageReceivedArgs e)
        {
            if (e.ChatMessage.Username == JsonController.AppSettings.AccountSettings.Username.ToLower() && !Regex.IsMatch(e.ChatMessage.Message, @"color\s#[0-9A-Fa-f]{6}"))
            {
                try
                {
                    string color = JsonController.AppSettings.Colors[ColorIndex];
                    TwitchClient.SendMessage(JsonController.AppSettings.AccountSettings.Username, $".color {color}");
                    ColorIndex++;
                }
                catch (Exception)
                {
                    ColorIndex = 0;
                }
            }
        }
    }
}
