using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordTranslator.Google
{
    public class SetupGoogleAuth : ISetUpGoogleAuth
    {
        public object AuthImplicit(string projectId)
        {
            // If you don't specify credentials when constructing the client, the client library will look for the credentials in the environment.
            var credential = GoogleCredential.GetApplicationDefault();
            var storage = StorageClient.Create(credential);
            // Make an authenticated API request.
            var buckets = storage.ListBuckets(projectId);
            foreach(var bucket in buckets)
            {
                Console.WriteLine(bucket.Name);
            }
            return null;
        }
    }
}
