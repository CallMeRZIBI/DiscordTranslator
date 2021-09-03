
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordTranslator.Google
{
    public class GoogleAuth
    {
        public string type { get; set; }
        public string project_id { get; set; }
        public string private_key_id { get; set; }
        public string private_key { get; set; }
        public string client_email { get; set; }
        public string client_id { get; set; }
        public string auth_uri { get; set; }
        public string token_uri { get; set; }
        public string auth_provider_x509_cert_url { get; set; }
        public string client_x509_cert_url { get; set; }
    }

    public class GoogleAuthLoader : IGoogleAuthLoader
    {
        public GoogleAuth LoadAuth(string jsonPath)
        {
            GoogleAuth googleAuth;
            try
            {
                googleAuth = JsonConvert.DeserializeObject<GoogleAuth>(File.ReadAllText(jsonPath));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
            return googleAuth;
        }
    }
}
