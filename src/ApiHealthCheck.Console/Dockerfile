FROM mcr.microsoft.com/dotnet/runtime:5.0
COPY src/ApiHealthCheck.ConsoleApp/bin/publish/ ./app
WORKDIR /app
ENTRYPOINT ["dotnet", "ApiHealthCheck.ConsoleApp.dll"]