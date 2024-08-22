FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

EXPOSE 8080

COPY . ./
RUN dotnet restore "ping-Map_Play-pong.csproj"

RUN dotnet publish "ping-Map_Play-pong.csproj" -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .

ENTRYPOINT ["dotnet", "ping-Map_Play-pong.dll"]

ENV ASPNETCORE_URLS=http://+:8080/
