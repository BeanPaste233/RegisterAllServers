using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using TerrariaApi.Server;
using TShockAPI;
using TShockAPI.Hooks;

namespace RegisterAllServers
{
    [ApiVersion(2,1)]
    public class MainPlugin : TerrariaPlugin
    {
        public MainPlugin(Main game) : base(game)
        {
        }

        public override string Name => "RegisterAllServers";

        public override Version Version => Assembly.GetExecutingAssembly().GetName().Version;

        public override string Author => "豆沙";

        public override string Description => "一键注册多服";

        public override void Initialize()
        {
            ServerApi.Hooks.GamePostInitialize.Register(this,OnPostInitialize);
            PlayerHooks.PlayerCommand += OnRegister;
            GeneralHooks.ReloadEvent += OnReload;

        }

        private void OnReload(ReloadEventArgs e)
        {
            ConfigUtils.LoadConfig();
            TShock.Log.ConsoleInfo("[RegisterAllServers] 配置文件已重载");
        }

        private void OnPostInitialize(EventArgs args)
        {
            ConfigUtils.LoadConfig();
            TShock.Log.ConsoleInfo($"[RegisterAllServers] 已加载 {ConfigUtils.config.Servers.Count} 个服务器");
        }

        private void OnRegister(PlayerCommandEventArgs e)
        {
            if (e.CommandName=="register")
            {
                var username = e.Player.Name;
                var userpwd = e.Parameters[0];
                var group = TShock.Config.Settings.DefaultRegistrationGroupName;
                TShock.Log.ConsoleInfo(userpwd);
                foreach (var server in ConfigUtils.config.Servers)
                {
                    server.CreateToken();
                    if (server.CreateUser(username, userpwd, group)["status"].ToString() == "200")
                    {
                        TShock.Log.ConsoleInfo($"[{username}] 成功在 [{server.ConvertToUrl()}] 注册");
                    }
                }
            } 
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
