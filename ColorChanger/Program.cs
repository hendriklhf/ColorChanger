using ColorChanger.Twitch;
using System;

namespace ColorChanger
{
    public static class Program
    {
        private static void Main()
        {
            _ = new ChatClient();
            while (true)
            {
                Console.ReadLine();
            }
        }
    }
}
