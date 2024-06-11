FROM node AS build-env
WORKDIR /TaskAPI

COPY . ./

RUN npm install

FROM node
WORKDIR /TaskAPI
COPY --from=build-env /TaskAPI/ .
ENTRYPOINT [ "node", "App.js"]
