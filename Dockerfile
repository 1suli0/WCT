FROM mcr.microsoft.com/dotnet/core/sdk:3.1 as build-image
WORKDIR /app

COPY . .
RUN dotnet restore

RUN dotnet publish -c Release -o /out/

WORKDIR /out

ENV ASPNETCORE_URLS=http://+:5000
ENTRYPOINT [ "dotnet", "WCT.API.dll" ]