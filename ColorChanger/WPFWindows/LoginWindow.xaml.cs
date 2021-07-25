using ColorChanger.JsonData;
using System;
using System.Text.RegularExpressions;
using System.Windows;

namespace ColorChanger.WPFWindows
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void btnLogIn_Click(object sender, RoutedEventArgs e)
        {
            if (Regex.IsMatch(tbUsername.Text.Trim(), @"^\w+$"))
            {
                if (Regex.IsMatch(tbToken.Text.Trim(), @"^oauth:\w{30}$", RegexOptions.IgnoreCase))
                {
                    JsonController.AppSettings.Account.Username = tbUsername.Text.Trim().ToLower();
                    JsonController.AppSettings.Account.OAuthToken = tbToken.Text.Trim().ToLower();
                    JsonController.AppSettings.Account.Channels = new() { tbUsername.Text.ToLower() };
                    JsonController.SaveSettings();
                    Close();
                }
                else
                {
                    _ = MessageBox.Show("The given token is invalid, please try again!", "Invalid OAuth token", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                _ = MessageBox.Show("The given username is invalid, please try again!", "Invalid username", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
