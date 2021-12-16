using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegisterAllServers
{
    public class ServerInfo
    {
        public string IP { get; set; }
        public int Port { get; set; }
        public string Token { get; set; }
        public ServerInfo(string ip,int port,string token)
        {
            IP = ip;
            Port = port;
            Token = token;
        }
        public string ConvertToUrl(bool https=false) 
        {
            string url = "http://" + IP + ':' + Port.ToString();
            if (https)
            {
                 url = "https://" + IP + ':' + Port.ToString();
            }  
            return url;
        }
    }
}
