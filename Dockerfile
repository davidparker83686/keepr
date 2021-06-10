FROM mcr.microsoft.com/dotnet/core/sdk:5.0
COPY keeprserver/bin/Release/netcoreapp5.0/publish/ App/
WORKDIR /App
CMD ASPNETCORE_URLS=http://*:$PORT dotnet keeprserver.dll
