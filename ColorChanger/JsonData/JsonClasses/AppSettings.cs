using System.Collections.Generic;

namespace ColorChanger.JsonData.JsonClasses
{
    public class AppSettings
    {
        public Account Account { get; set; }

        public List<string> Colors { get; set; }
    }
}
