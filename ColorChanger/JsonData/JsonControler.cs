using ColorChanger.JsonData.JsonClasses;
using ColorChanger.Properties;
using System.Text.Json;

namespace ColorChanger.JsonData
{
    public class JsonControler
    {
        public Settings Settings { get; }

        public JsonControler()
        {
            Settings = JsonSerializer.Deserialize<Settings>(Resources.Settings);
        }
    }
}
