using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordTranslator.Google
{
    public interface IGoogleTranslate
    {
        string Translate(string input, string language);
    }
}
