using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RegisterAllServers
{
    public class ServerInfo
    {
        public string IP { get; set; }
        public int Port { get; set; }
        public string Token { get; set; }
        public string RestUser { get; set; }
        public string RestPassword { get; set; }
        public ServerInfo(string ip,int port,string token,string userName,string userPwd)
        {
            IP = ip;
            Port = port;
            Token = token;
            RestUser = userName;
            RestPassword = userPwd;
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
        public JObject GetHttp(string url)
        {
            var client = new HttpClient();
            return JObject.Parse(client.GetAsync(url).Result.Content.ReadAsStringAsync().Result);
        }
        public JObject CreateUser(string userName,string userPwd,string groupName)
        {
            if (TokenTest())
            {
                string url = ConvertToUrl();
                url += $"/v2/users/create?user={userName}&password={userPwd}&group={groupName}&token={Token}";
                return GetHttp(url);
            }
            return null;
        }
        public JObject DeleteUser(string userName) 
        {
            if (TokenTest())
            {
                string url = ConvertToUrl();
                url += $"/v2/users/destroy?user={userName}&type=name&token={Token}";
                return GetHttp(url);
            }
            return null;
        }
        public bool TokenTest() 
        {
            string url = ConvertToUrl() + $"/tokentest?token={Token}";
            var response = GetHttp(url);
            return response["status"].ToString() == "200";
        
        }
        public void CreateToken() 
        {
            if (string.IsNullOrEmpty(Token))
            {
                string tokenUrl = ConvertToUrl() + $"/v2/token/create?username={RestUser}&password={RestPassword}";
                var getToken = GetHttp(tokenUrl);
                Token = getToken["token"].ToString();
            }
        }
    }
}
