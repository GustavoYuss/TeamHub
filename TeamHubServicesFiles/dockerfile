FROM node AS build-env
WORKDIR /FilegRPCAPI

COPY . ./

RUN npm install

FROM node
WORKDIR /FilegRPCAPI
COPY --from=build-env /FilegRPCAPI/ .
ENTRYPOINT [ "node", "App.js"]
