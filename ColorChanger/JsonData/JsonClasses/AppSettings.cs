using System.Collections.Generic;

namespace ColorChanger.JsonData.JsonClasses
{
    public class AppSettings
    {
        public bool AutoConnect { get; set; }

        public bool StartWithWindwos { get; set; }

        public Account Account { get; set; }

        public List<string> Colors { get; set; }
    }
}
