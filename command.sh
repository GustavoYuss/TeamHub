#!/bin/bash

# Cambiar al directorio TeamHubServiceFiles y construir la imagen Docker
cd TeamHubServicesFiles || exit 1
docker build -t grpcfileservice .
if [ $? -ne 0 ]; then
  exit $?
fi

# Cambiar al directorio TeamHubLogService y construir la imagen Docker
cd ../TeamHubLogService || exit 1
docker build -t logservice .
if [ $? -ne 0 ]; then
  exit $?
fi

# Cambiar al directorio TeamHubServiceProjects y construir la imagen Docker
cd ../TeamHubServiceProjects || exit 1
docker build -t projectservice .
if [ $? -ne 0 ]; then
  exit $?
fi

# Cambiar al directorio TeamHubServiceFileRest y construir la imagen Docker
cd ../TeamHubServiceFileRest || exit 1
docker build -t fileservice .
if [ $? -ne 0 ]; then
  exit $?
fi

# Cambiar al directorio TeamHubServiceUser y construir la imagen Docker
cd ../TeamHubServiceUser || exit 1
docker build -t userservice .
if [ $? -ne 0 ]; then
  exit $?
fi

# Cambiar al directorio TeamHubTaskService y construir la imagen Docker
cd ../TeamHubTasksService || exit 1
docker build -t taskservice .
if [ $? -ne 0 ]; then
  exit $?
fi

# Cambiar al directorio TeamsHubWebClient y construir la imagen Docker
cd ../TeamsHubWebClient || exit 1
docker build -t webclient .
if [ $? -ne 0 ]; then
  exit $?
fi

# Cambiar al directorio TeamsHubAPIGateway y construir la imagen Docker
cd ../TeamsHubAPIGateway || exit 1
docker build -t apigateway .
if [ $? -ne 0 ]; then
  exit $?
fi

# Cambiar al directorio TeamHubSessionsServices y construir la imagen Docker
cd ../TeamHubSessionsServices || exit 1
docker build -t sessionservice .
if [ $? -ne 0 ]; then
  exit $?
fi

# Cambiar al directorio TeamHubDataBase y construir la imagen Docker
cd ../TeamHubDataBase || exit 1
docker build -t teamhub_db .
if [ $? -ne 0 ]; then
  exit $?
fi

# Ejecutar docker-compose build y docker-compose up
docker-compose build
if [ $? -ne 0 ]; then
  exit $?
fi

docker-compose up
