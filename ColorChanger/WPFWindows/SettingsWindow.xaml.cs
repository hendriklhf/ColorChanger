using ColorChanger.JsonData;
using HLE.Strings;
using System;
using System.Linq;
using System.Windows;

namespace ColorChanger.WPFWindows
{
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
            tbChannels.Text = GenerateChannelString();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            JsonController.AppSettings.Account.Channels = tbChannels.Text.Matches(@"\w+").Distinct().ToList();
            if (!JsonController.AppSettings.Account.Channels.Contains(JsonController.AppSettings.Account.Username)
                || JsonController.AppSettings.Account.Channels == null
                || JsonController.AppSettings.Account.Channels.Count == 0)
            {
                JsonController.AppSettings.Account.Channels = new() { JsonController.AppSettings.Account.Username };
            }
            JsonController.SaveSettings();
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnRetrieveChatterinoChannels_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                JsonController.AppSettings.Account.Channels = JsonController.GetChannelsFromChatterinoSettings();
            }
            catch (Exception)
            {

            }
            JsonController.SaveSettings();
            tbChannels.Text = GenerateChannelString();
        }

        private string GenerateChannelString()
        {
            string result = string.Empty;
            JsonController.AppSettings.Account.Channels.ForEach(c => result += $"{c} ");
            return result.Trim();
        }
    }
}
