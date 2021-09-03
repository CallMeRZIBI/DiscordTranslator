# **Discord Translator**

### Discord bot that uses Google Cloud Translate API for translating of text for international communication

## Installation
```bash
git clone https://github.com/CallMeRZIBI/DiscordTranslator.git
cd DiscordTranslator
# You have to setup translate API from google: https://cloud.google.com/translate/docs/setup
#
# In the folder where you will have the dll or exe you have to put your google cloud authentication
# and discord token in token.txt file
# Change the path to GOOGLE_APPLICATION_CREDENTIALS environment variable in Program.cs
```

## Usage

To run the bot:
```bash
dotnet run
# or build it and run the dll
dotnet build -c Release # Only once
dotnet run bin/Release/net5.0/DiscordTranslator.dll
# or if you are using windows
cd bin/Release/net5.0
DiscordTranslator.exe
# Keep in mind that you have to put token.txt and google cloud authentication in the same folder
# as the executable or dll is
```

# Hope you enjoy this ;)