# Api health check

![.NET](https://github.com/Arnab-Developer/api-health-check/workflows/.NET/badge.svg?branch=main)

This is a small utility app to check api health in a perticular time interval. Add the details of the apis in configuration (like appsettings) to check their health. This is a background task application with .NET (with DI, logging, configuration) to check api health.

To write tasks which runs in background, I always used Windows Task Scheduler or Windows Service. But ASP.NET Core is now more cross platform and these solutions are only for Windows. So recently I found a more cross platform solution which is ASP.NET Core background tasks.

For hosting I have below options:

* I can use a VM and deploy my app as a self contained application in a folder. Run the exe file in it.
* I can use a VM and install .NET 5 runtime just to run a console application. Deploy my application in a folder. I can open a terminal and use `dotnet myapp.dll` to run it.
* I can put the app in a container and deploy in azure container instance or app service or kubernetes. I can put the container image in Docker Hub or Azure Container registry.

## Solution structure

* Core functionality to check api health in class lib.
* Console app to host the background task to check api health using the core functionality in the class lib.
* Unit test.

## Tech stack

* .NET 5
* C# 9
* xUnit for unit testing
* Visual Studio 2019

## Contribution

If you want to contribute in this repo then create an issue and let me know how you want to contribute.
