FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
RUN adduser -D -u 1000 appuser  # Create a non-root user
USER appuser                    # Switch to non-root user
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
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
ENTRYPOINT ["dotnet", "Aurum.dll"]
