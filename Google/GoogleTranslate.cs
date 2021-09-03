using Google.Cloud.Translate.V3;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordTranslator.Google
{
    class GoogleTranslate : IGoogleTranslate
    {
        private string _googleAuthPath = @"GoogleCloudKey_discord-translator.json";

        private GoogleAuth _googleAuth;

        public GoogleTranslate()
        {
            _googleAuth = new GoogleAuthLoader().LoadAuth(_googleAuthPath);
        }

        public string Translate(string input, string language)
        {
            string translatedMessage;
            TranslationServiceClient client = TranslationServiceClient.Create();
            try
            {
                TranslateTextRequest request = new TranslateTextRequest()
                {
                    Contents = { input },
                    TargetLanguageCode = language,
                    Parent = $"projects/{_googleAuth.project_id}"
                };
                TranslateTextResponse response = client.TranslateText(request);
                // response.Translations will have one entry, because request.Contents has one entry.
                Translation translation = response.Translations[0];
                translatedMessage = translation.TranslatedText;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }

            return translatedMessage;
        }
    }
}
