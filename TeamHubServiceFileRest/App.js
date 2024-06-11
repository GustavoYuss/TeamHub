require('dotenv').config();

const Server = require('./Settings/Server');

const servidor = new Server();

servidor.listen();