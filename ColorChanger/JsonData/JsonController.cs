using ColorChanger.JsonData.JsonClasses;
using ColorChanger.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace ColorChanger.JsonData
{
    public static class JsonController
    {
        public static AppSettings AppSettings { get; set; }

        public static void LoadSettings()
        {
            AppSettings = JsonSerializer.Deserialize<AppSettings>(File.ReadAllText(Resources.AppSettingsPath));
        }

        public static void SaveSettings()
        {
            File.WriteAllText(Resources.AppSettingsPath, JsonSerializer.Serialize(AppSettings, new() { WriteIndented = true }));
        }

        public static void ResetSettings()
        {
            AppSettings.Account.Username = "";
            AppSettings.Account.OAuthToken = "";
            AppSettings.Account.Channels = null;
            AppSettings.Colors = Resources.DefaultColors.Split().ToList();
            AppSettings.AutoConnect = false;
            AppSettings.StartWithWindows = false;
            SaveSettings();
        }

        private static List<string> GetChannelsFromChatterinoSettings()
        {
            List<string> result = new();
            try
            {
                string chatterinoSettingsDirectory = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\Chatterino2\\Settings\\window-layout.json";
                JsonElement chatterinoTabs = JsonSerializer.Deserialize<JsonElement>(File.ReadAllText(chatterinoSettingsDirectory)).GetProperty("windows")[0].GetProperty("tabs");
                for (int i = 0; i < chatterinoTabs.GetArrayLength(); i++)
                {
                    JsonElement tabSettings = chatterinoTabs[i].GetProperty("splits2");
                    try
                    {
                        if (tabSettings.GetProperty("data").GetProperty("type").GetString() == "twitch")
                        {
                            result.Add(tabSettings.GetProperty("data").GetProperty("name").GetString());
                        }
                    }
                    catch (Exception)
                    {
                        try
                        {
                            JsonElement tabItems = tabSettings.GetProperty("items");
                            for (int j = 0; j < tabItems.GetArrayLength(); j++)
                            {
                                if (tabItems[j].GetProperty("data").GetProperty("type").GetString() == "twitch")
                                {
                                    result.Add(tabItems[j].GetProperty("data").GetProperty("name").GetString());
                                }
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
            return result.Distinct().ToList();
        }

        private static bool IsNullOrEmpty(List<string> channels)
        {
            return channels == null || channels.Count == 0;
        }
    }
}
