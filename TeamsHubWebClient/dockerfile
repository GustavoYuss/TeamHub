FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /WebClientAPI

#Copiar todos los archivos del proyecto
COPY . ./
#Restaurar los paquetes dentro del contenedor
RUN dotnet restore
#Construir el proyecto en versión release
RUN dotnet publish -c Release -o out

# Se construye la imagen
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /WebClientAPI
COPY --from=build-env /WebClientAPI/out .
ENTRYPOINT ["dotnet", "TeamsHubWebClient.dll"]