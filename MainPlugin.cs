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
            AccountHooks.AccountCreate += OnRegister;

        }

        private void OnPostInitialize(EventArgs args)
        {
            ConfigUtils.LoadConfig();
            TShock.Log.ConsoleInfo($"[RegisterAllServers] 已加载 {ConfigUtils.config.Servers.Count} 个服务器");
        }

        private void OnRegister(AccountCreateEventArgs e)
        {
            var username = e.Account.Name;
            var userpwd = e.Account.Password;
            var group = e.Account.Group;
            foreach (var server in ConfigUtils.config.Servers)
            {
                server.CreateToken();
                server.CreateUser(username,userpwd,group);
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
