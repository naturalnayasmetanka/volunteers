# См. статью по ссылке https://aka.ms/customizecontainer, чтобы узнать как настроить контейнер отладки и как Visual Studio использует этот Dockerfile для создания образов для ускорения отладки.

# Этот этап используется при запуске из VS в быстром режиме (по умолчанию для конфигурации отладки)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# Этот этап используется для сборки проекта службы
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Volunteers.API/Volunteers.API.csproj", "src/Volunteers.API/"]
COPY ["src/Volunteers.Application/Volunteers.Application.csproj", "src/Volunteers.Application/"]
COPY ["src/Volunteers.Domain/Volunteers.Domain.csproj", "src/Volunteers.Domain/"]
COPY ["src/Volunteers.Infrastructure/Volunteers.Infrastructure.csproj", "src/Volunteers.Infrastructure/"]
RUN dotnet restore "./src/Volunteers.API/Volunteers.API.csproj"
COPY . .
WORKDIR "/src/src/Volunteers.API"
RUN dotnet build "./Volunteers.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Этот этап используется для публикации проекта службы, который будет скопирован на последний этап
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Volunteers.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Этот этап используется в рабочей среде или при запуске из VS в обычном режиме (по умолчанию, когда конфигурация отладки не используется)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Volunteers.API.dll"]