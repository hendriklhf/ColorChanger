using System.Collections.Generic;

namespace ColorChanger.JsonData.JsonClasses
{
    public class AccountSettings
    {
        public string Username { get; set; }

        public string OAuthToken { get; set; }

        public List<string> Channels { get; set; }
    }
}
