﻿FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Aurum/Aurum.csproj", "Aurum/"]
RUN dotnet restore "Aurum/Aurum.csproj"
COPY . .
WORKDIR "/src/Aurum"
RUN dotnet build "Aurum.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Aurum.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

RUN mkdir -p /app/raw-seeding-data
COPY raw-seeding-data/*.csv /app/raw-seeding-data/

ENTRYPOINT ["dotnet", "Aurum.dll"]
