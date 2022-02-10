FROM mcr.microsoft.com/dotnet/runtime:6.0
COPY src/ApiHealthCheck.Console/bin/publish/ ./app
WORKDIR /app
ENTRYPOINT ["dotnet", "ApiHealthCheck.Console.dll"]