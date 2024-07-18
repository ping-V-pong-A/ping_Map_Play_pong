#FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
#USER $APP_UID
#WORKDIR /app
#EXPOSE 8080
#
#FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
#ARG BUILD_CONFIGURATION=Release
#WORKDIR /src
#COPY ["ping-Map_Play-pong.csproj", "./"]
#RUN dotnet restore "ping-Map_Play-pong.csproj"
#COPY . .
#WORKDIR "/src/"
#RUN dotnet build "ping-Map_Play-pong.csproj" -c $BUILD_CONFIGURATION -o /app/build
#
#FROM build AS publish
#ARG BUILD_CONFIGURATION=Release
#RUN dotnet publish "ping-Map_Play-pong.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "ping-Map_Play-pong.dll"]

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

EXPOSE 8080

COPY . ./
RUN dotnet restore "ping-Map_Play-pong.csproj"

RUN dotnet publish "ping-Map_Play-pong.csproj" -c Release -o out


# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .


ENTRYPOINT ["dotnet", "ping-Map_Play-pong.dll"]

ENV ASPNETCORE_URLS=http://+:8080/
