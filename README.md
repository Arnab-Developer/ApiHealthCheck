# Api health check
[![CI CD](https://github.com/Arnab-Developer/api-health-check/actions/workflows/ci-cd.yml/badge.svg)](https://github.com/Arnab-Developer/api-health-check/actions/workflows/ci-cd.yml)
![Docker Image Version (latest by date)](https://img.shields.io/docker/v/45862391/apihealthcheck)

This is a background task application with .NET (with DI, logging, configuration) to check api health. A small utility 
app to check api health in a perticular time interval. Add the details of the apis in configuration (like appsettings) 
to check their health status. It uses an Azure Application Insights to log api health status.

## How to use
- Update the appsettings.json with api details to check their health.
- Create a Azure Application Insights and update the appsettings.json with the reference of the Application Insights.
- Create a Azure Container Instence with the dockerhub image.

You can find the logs in Application Insights.

## Docker image
This app is in a docker container and stored in dockerhub.

https://hub.docker.com/r/45862391/apihealthcheck

## Contribution
Please read the contribution related things [here](https://github.com/Arnab-Developer/api-health-check/blob/main/Contribution.md).