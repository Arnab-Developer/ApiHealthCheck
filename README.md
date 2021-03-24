# Api health check
[![CI CD](https://github.com/Arnab-Developer/api-health-check/actions/workflows/ci-cd.yml/badge.svg)](https://github.com/Arnab-Developer/api-health-check/actions/workflows/ci-cd.yml)

This is a background task application with .NET (with DI, logging, configuration) to check api health.

This is a small utility app to check api health in a perticular time interval. Add the details of the apis 
in configuration (like appsettings) to check their health status. This app use an Azure Application Insights to 
log api health status.

This app is in a docker container and stored in DockerHub.

https://hub.docker.com/r/45862391/apihealthcheck

## Contribution
Please read the contribution related things [here](https://github.com/Arnab-Developer/api-health-check/blob/main/Contribution.md).