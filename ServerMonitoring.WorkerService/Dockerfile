FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ServerMonitoring.WorkerService/ServerMonitoring.WorkerService.csproj", "ServerMonitoring.WorkerService/"]
COPY ["ServerMonitoring.Application/ServerMonitoring.Application.csproj", "ServerMonitoring.Application/"]
COPY ["ServerMonitoring.Domain/ServerMonitoring.Domain.csproj", "ServerMonitoring.Domain/"]
COPY ["ServerMonitoring.Infrastructure/ServerMonitoring.Infrastructure.csproj", "ServerMonitoring.Infrastructure/"]
RUN dotnet restore "ServerMonitoring.WorkerService/ServerMonitoring.WorkerService.csproj"
COPY . .
WORKDIR "/src/ServerMonitoring.WorkerService"
RUN dotnet build "ServerMonitoring.WorkerService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ServerMonitoring.WorkerService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ServerMonitoring.WorkerService.dll"]
