@echo off

REM Cambiar al directorio TeamHubServiceFileRest y construir la imagen Docker
cd TeamHubServicesFiles
docker build -t grpcfileservice .
IF %ERRORLEVEL% NEQ 0 EXIT /B %ERRORLEVEL%

REM Cambiar al directorio TeamHubServiceFileRest y construir la imagen Docker
cd TeamHubLogService
docker build -t logservice .
IF %ERRORLEVEL% NEQ 0 EXIT /B %ERRORLEVEL%

REM Cambiar al directorio TeamHubServiceProjects y construir la imagen Docker
cd ..\TeamHubServiceProjects
docker build -t projectservice .
IF %ERRORLEVEL% NEQ 0 EXIT /B %ERRORLEVEL%

REM Cambiar al directorio TeamHubServicesFiles y construir la imagen Docker
cd ..\TeamHubServiceFileRest
docker build -t fileservice .
IF %ERRORLEVEL% NEQ 0 EXIT /B %ERRORLEVEL%

REM Cambiar al directorio TeamHubServiceUser y construir la imagen Docker
cd ..\TeamHubServiceUser
docker build -t userservice .
IF %ERRORLEVEL% NEQ 0 EXIT /B %ERRORLEVEL%

REM Cambiar al directorio TeamHubTaskService y construir la imagen Docker
cd ..\TeamHubTasksService
docker build -t taskservice .
IF %ERRORLEVEL% NEQ 0 EXIT /B %ERRORLEVEL%

REM Cambiar al directorio TeamsHubWebClient y construir la imagen Docker
cd ..\TeamsHubWebClient
docker build -t webclient .
IF %ERRORLEVEL% NEQ 0 EXIT /B %ERRORLEVEL%

REM Cambiar al directorio TeamsHubAPIGateway y construir la imagen Docker
cd ..\TeamsHubAPIGateway
docker build -t apigateway .
IF %ERRORLEVEL% NEQ 0 EXIT /B %ERRORLEVEL%

REM Cambiar al directorio TeamHubSessionsServices y construir la imagen Docker
cd ..\TeamHubSessionsServices
docker build -t sessionservice .
IF %ERRORLEVEL% NEQ 0 EXIT /B %ERRORLEVEL%

REM Cambiar al directorio TeamHubDataBase y construir la imagen Docker
cd ..\TeamHubDataBase
docker build -t teamhub_db .
IF %ERRORLEVEL% NEQ 0 EXIT /B %ERRORLEVEL%

REM Ejecutar docker-compose build y docker-compose up
docker-compose build
IF %ERRORLEVEL% NEQ 0 EXIT /B %ERRORLEVEL%
docker-compose up
