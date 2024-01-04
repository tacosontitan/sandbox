FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release

# Install Node.js
RUN curl -fsSL https://deb.nodesource.com/setup_14.x | bash - \
    && apt-get install -y \
        nodejs \
    && rm -rf /var/lib/apt/lists/*

WORKDIR /src
COPY ["src/Sandbox.Presentation.React/Sandbox.Presentation.React.csproj", "src/Sandbox.Presentation.React/"]
RUN dotnet restore "src/Sandbox.Presentation.React/Sandbox.Presentation.React.csproj"
COPY . .
WORKDIR "/src/src/Sandbox.Presentation.React"
RUN dotnet build "Sandbox.Presentation.React.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Sandbox.Presentation.React.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sandbox.Presentation.React.dll"]
