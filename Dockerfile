FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["WebApi/WebApi.csproj", "WebApi/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]

RUN dotnet restore "WebApi/WebApi.csproj"
RUN dotnet restore "Infrastructure/Infrastructure.csproj"
RUN dotnet restore "Application/Application.csproj"

COPY . .
WORKDIR "/src/WebApi"
RUN dotnet build "WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

RUN dotnet tool install -g dotnet-ef
ENV PATH $PATH:/root/.dotnet/tools

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "WebApi.dll"]
#ENTRYPOINT ["/bin/sh", "-c", "dotnet ef database update && dotnet WebApi.dll"]
ENTRYPOINT ["/bin/sh", "-c", "dotnet WebApi.dll"]