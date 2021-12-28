using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TShockAPI;

namespace RegisterAllServers
{
    public static class ConfigUtils
    {
        public static string configDir = TShock.SavePath + "/RegisterAllServers";
        public static string configPath =configDir + "/config.json";
        public static Config config = new Config();
        public static void LoadConfig()
        {
            if (Directory.Exists(configDir))
            {
                if (File.Exists(configPath))
                {
                    config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(configPath));
                }
                else
                {
                    File.WriteAllText(configPath,JsonConvert.SerializeObject(config,Formatting.Indented));
                }
            }
            else
            {
                Directory.CreateDirectory(configDir);
                File.WriteAllText(configPath, JsonConvert.SerializeObject(config, Formatting.Indented));
            }
        
        }
    }
    public class Config
    {
        public List<ServerInfo> Servers= new List<ServerInfo>();
        public Config() 
        {
           
        }
    }
}
