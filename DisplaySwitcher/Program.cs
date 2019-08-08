using System;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DisplaySwitcher
{
    class Program
    {
        public static string configDirLocation = @"C:\Users\john\AppData\Roaming\johnfg10\DisplaySwitcher";
        public static string configLocation = configDirLocation + @"\config.json";

        

        public static DisplaySwitcherConfig Config = new DisplaySwitcherConfig();
        
        
        static void Main(string[] args)
        {
            Console.WriteLine("init");
            if (!File.Exists(configLocation))
            {
                if (!Directory.Exists(configDirLocation))
                {
                    Directory.CreateDirectory(configDirLocation);
                }
                
                using (var textStream = File.CreateText(configLocation))
                {
                    textStream.Write(JsonConvert.SerializeObject(Config));
                }
                Console.WriteLine("Test");
            }
            else
            {
                Config = JsonConvert.DeserializeObject<DisplaySwitcherConfig>(File.ReadAllText(configLocation));
            }
            
            Console.WriteLine(JsonConvert.SerializeObject(Config));

            if (Config.CurrentStatus == DisplaySwitcherEnum.Unknown || Config.CurrentStatus == Config.StatusTwo)
            {
                Config.StatusOne.FormatScreenChange().Cmd();
                Config.CurrentStatus = Config.StatusOne;
            }
            else
            {
                Config.StatusTwo.FormatScreenChange().Cmd();
                Config.CurrentStatus = Config.StatusTwo;
            }
            Console.WriteLine(Config.CurrentStatus);
            File.WriteAllText(configLocation, JsonConvert.SerializeObject(Config));
            
            Environment.Exit(0);
        }
        
    }
}