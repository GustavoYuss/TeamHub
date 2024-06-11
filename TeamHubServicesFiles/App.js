require('dotenv').config();

const ServerApp = require('./Settings/Server');
//const Server = require('./Settings/Server');

const servidor = new ServerApp();

servidor.listen();