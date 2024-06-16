const { createLogger, format, transports } = require('winston');
const path = require('path');
const fs = require('fs');


const logDir = path.join(__dirname, '../Logs');
if (!fs.existsSync(logDir)) {
    fs.mkdirSync(logDir);
}

const logger = createLogger({
    format: format.combine(
        format.timestamp(),
        format.simple(),
        format.json(),
        format.prettyPrint()
    ),
    transports: [
        new transports.File({
            maxsize: 5120000, 
            maxFiles: 5,
            filename: path.join(logDir, 'log-api.log')
        }),
    ]
});

logger.on('error', (err) => {
    console.error('Error en el logger:', err);
});

module.exports = logger;
