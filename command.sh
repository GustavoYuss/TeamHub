#!/bin/bash

cd TeamHubSericeFileRest
docker build  -t grpcfileservice .

cd ../TeamHubServiceProjects
docker build  -t projectservice .

cd ../TeamHubServicesFiles
docker build  -t fileservice .

cd ../TeamHubServiceUser
docker build  -t userservice .

cd ../TeamHubTaskService
docker build  -t taskservice .

cd ../TeamsHubWebClient
docker build  -t webclient .

cd ../TeamsHubAPIGateway
docker build  -t apigateway .

cd ../TeamHubSessionsServices
docker build  -t sessionservice .

docker-compose build
docker-compose up