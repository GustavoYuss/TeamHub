const cors = require('cors');
const os = require('os');
const express = require('express');
const dotenv = require('dotenv');
const jwt = require('jsonwebtoken');
const logger = require('../Controller/logger'); 
dotenv.config();

class Server {
    constructor() {
        this.app = express();
        this.port = process.env.PORT;
        this.middleware();
        this.routes();
    }

    middleware() {
        this.app.use(cors());
        this.app.use(express.json());
        this.app.use(express.static('public'));
        this.app.use((req, res, next) => {
            const authHeader = req.headers['authorization'];
            const token = authHeader && authHeader.split(' ')[1];
            if (token == null) return res.sendStatus(401);
            jwt.verify(token, process.env.SECRETORPRIVATEKEY, (err, user) => {
                if (err) {
                    console.error('Token verification error:', err);
                    return res.sendStatus(403);
                }
                req.user = user;
                next();
            });
        });
    }

    routes() {
        this.app.use('/TeamHub/File', require('../Routes/FileRoute'));
    }

    listen() {
        const address = this.getNetworkAddress();
        this.app.listen(this.port, () => {
            console.log(`Servidor escuchando en http://${address}:${this.port}`);
        });
    }

    getNetworkAddress() {
        const interfaces = os.networkInterfaces();
        for (const name of Object.keys(interfaces)) {
            for (const iface of interfaces[name]) {
                if (iface.family === 'IPv4' && !iface.internal) {
                    return iface.address;
                }
            }
        }
        return 'localhost';
    }
}

module.exports = Server;
