FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env

WORKDIR /app

# Copy the CSPROJ file and restore any dependencies (via NUGET)
COPY *.csproj ./
RUN dotnet restore

# Copy the project files and build our release
COPY . ./
RUN dotnet publish -c Release -o out

# Generic runtime image
FROM mcr.microsoft.com/dotnet/runtime:5.0

WORKDIR /app

COPY --from=build-env /app/out .
COPY ./token.txt /app/token.txt
COPY ./GoogleCloudKey_discord-translator.json /app/GoogleCloudKey_discord-translator.json
ENTRYPOINT ["dotnet", "DiscordTranslator.dll"]
