FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env

WORKDIR /app

# Copy the CSPROJ file and restore any dependencies (via NUGET)
COPY *.csproj ./
RUN dotnet restore

# Copy the project files and build our release
COPY . ./
RUN dotnet public -C Release -o out
COPY token.txt /bin/Release/net5.0/
COPY GoogleCloudKey_discord-translator.json /bin/Release/net5.0/

# Generic runtime image
FROM mcr.microsoft.com/dotnet/aspnet:3.1

WORKDIR /app

COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "DiscordTranslator.dll"]
