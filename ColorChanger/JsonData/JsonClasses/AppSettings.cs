using System.Collections.Generic;

namespace ColorChanger.JsonData.JsonClasses
{
    public class AppSettings
    {
        public AccountSettings AccountSettings { get; set; }

        public List<string> Colors { get; set; }
    }
}
