# Base aspnet core image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim-arm64v8 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Build step container image
FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster-arm64v8 AS build
# Setup working directory /build
WORKDIR /src
# Copy projects
COPY web-app.sln ./
COPY Core/*.csproj ./Core/
COPY Infrastructure/*.csproj ./Infrastructure/
COPY Web/*.csproj ./Web/
# Run projects restore cmd
RUN dotnet restore -r linux-arm64
COPY . .
# Build projects
WORKDIR "/src/Core"
RUN dotnet build -c Release -o /app -r linux-arm64
WORKDIR "/src/Infrastructure"
RUN dotnet build -c Release -o /app -r linux-arm64
WORKDIR "/src/Web"
RUN dotnet build -c Release -o /app -r linux-arm64

# Publish step container image
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish -r linux-arm64

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# ! Do not change port 88 !
EXPOSE 88
# !
ENTRYPOINT ["dotnet", "Web.dll"]
