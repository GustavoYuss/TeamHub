const express = require('express');
const cors = require('cors');
const Sequelize = require('sequelize');
const jwt = require('jsonwebtoken');

class Server {

    constructor(){
        this.app = express();
        this.port = process.env.PORT;
        this.middleware();
        this.routes();
    }

    middleware(){
        this.app.use(cors());
        this.app.use(express.json());
        this.app.use(express.static('public'));
        this.app.use((req, res, next) =>  {
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
    routes(){
        this.app.use('/TeamHub/Task', require('../Routes/TaskRoute'));
    }
    listen(){
        this.app.listen(this.port, ()=>{
            console.log(`Servidor escuchando en puerto ${this.port}`)
        });
    }
}

module.exports = Server;