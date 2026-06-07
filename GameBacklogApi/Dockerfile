# Lightweight image for base
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

# SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["GameBacklogApi.csproj", "./"]
# Installs all NuGet dependancies
RUN dotnet restore "./GameBacklogApi.csproj"

COPY . .
# The following command builds the specified csproj file in the release configuration to make compiler optimisations and removes debugging info. Then we output all the .dll files and dependencies to /app/build
RUN dotnet build "GameBacklogApi.csproj" -c Release -o /app/build

# Publishes application to /app/publish for deployment.
FROM build AS publish
RUN dotnet publish "GameBacklogApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Tell Docker how to start the app
ENTRYPOINT ["dotnet", "GameBacklogApi.dll"]