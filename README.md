# API health check
[![CI CD](https://github.com/Arnab-Developer/api-health-check/actions/workflows/ci-cd.yml/badge.svg)](https://github.com/Arnab-Developer/api-health-check/actions/workflows/ci-cd.yml)
![Docker Image Version (latest by date)](https://img.shields.io/docker/v/45862391/apihealthcheck)

This is a background task application with .NET (with DI, logging, configuration) to check API health. A small utility 
app to check API health in a perticular time interval. Add the details of the APIs in configuration (like appsettings) 
to check their health status. It uses an Azure Application Insights to log API health status.

## How to use
This app uses some example APIs for health check. If you want to run the app as is then follow the below steps.

- Create an Azure Application Insights resource. 
- Update the appsettings.json with the reference of the Application Insights.
```
"ApplicationInsights": {
  "Key": "",
  "LogLevel": {
    "Default": "Information",
    "Microsoft": "Error"
  }
```
- Create a Azure Container Instence with the dockerhub image. https://hub.docker.com/r/45862391/apihealthcheck
- You can find the health check logs in Application Insights.

If you want to test different APIs then follow the below steps.

- Update the appsettings.json with API details to check their health.
```
"ApiDetails": [
  {
    "Name": "Microsoft",
    "Url": "https://microsoft.com",
    "Credential": {
    "UserName": "",
    "Password": ""
    },
    "IsEnable": true
  }
]
```
- Create an Azure Application Insights resource. 
- Update the appsettings.json with the reference of the Application Insights.
```
"ApplicationInsights": {
  "Key": "",
  "LogLevel": {
    "Default": "Information",
    "Microsoft": "Error"
  }
```
- Build the app and create docker image.
- Store the docker image in dockerhub.
- Create a Azure Container Instence with the dockerhub image.
- You can find the health check logs in Application Insights.

## Docker image
This app is in a docker container and stored in dockerhub.

https://hub.docker.com/r/45862391/apihealthcheck

## Contribution
Please read the contribution related things [here](https://github.com/Arnab-Developer/api-health-check/blob/main/Contribution.md).