using Discord.Commands;
using DiscordTranslator.Google;
using Google.Cloud.Translate.V3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordTranslator.Commands
{
    public class Translate : ModuleBase<SocketCommandContext>
    {
        private readonly IGoogleTranslate _translate;

        public Translate(IGoogleTranslate translate)
        {
            _translate = translate;
        }

        [Command("translate")]
        [Summary("Translates input to selected language.")]
        public async Task TranslateAsync([Summary("The language you want to be translated.")] string language, [Summary("The text you want to be translated.")] string input)
        {
            var mentions = GetMentions(input);

            string responseMessage = _translate.Translate(input, language);

            if (responseMessage == null)
            {
                await ReplyAsync("Shit... Google Cloud Translation V3 API fucked up!");

                return;
            }

            if (mentions != null)
                responseMessage = InsertMentions(responseMessage, mentions);

            await ReplyAsync($"{Context.User.Mention} said: {responseMessage}");
            await Context.Message.DeleteAsync();
        }

        // Getting mentions from message cause translator is messing things up
        private static string?[] GetMentions(string input)
        {
            // Using string?[] array instead of List because it's unecessary when I can resize the array
            string?[] mentions = new string?[0];
            string message = input;

            int i = 0;
            string[] words = message.Split(" ");
            foreach(string word in words)
            {
                if (word.Contains("<@!"))
                {
                    int startingMention = message.IndexOf('<');
                    Array.Resize(ref mentions, mentions.Length + 1);
                    mentions[i++] = word;
                    message = message.Remove(startingMention, word.Length);
                }
            }

            return mentions;
        }

        // Inserting the Mentions on the place of previous broken mentions
        private static string InsertMentions(string input, string[] mentions)
        {
            string message = input;

            foreach(string mention in mentions)
            {
                int startingOfSubstring = message.IndexOf("&lt;@!");
                int substringLength = message.IndexOf("&gt;") + 4 - startingOfSubstring;

                string substring = String.Empty;
                try
                {
                    substring = message.Substring(startingOfSubstring, substringLength);
                }catch
                {
                    break;
                }

                message = message.Replace(substring, mention);
            }

            return message;
        }
    }
}
