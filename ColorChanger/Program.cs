using ColorChanger.Twitch;
using System;

namespace ColorChanger
{
    public static class Program
    {
        private static void Main()
        {
            _ = new TwitchBot();
            while (true)
            {
                Console.ReadLine();
            }
        }
    }
}
