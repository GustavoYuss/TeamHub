const amqp = require('amqplib');
const logger = require('../Controller/logger'); 

async function saveUserAction(userAction) {
    try {
        const connection = await amqp.connect('amqp://172.16.0.11');
        const channel = await connection.createChannel();

        const queueName = 'Prueba';
        await channel.assertQueue(queueName, { durable: true });

        const mensaje = JSON.stringify(userAction);

        channel.sendToQueue(queueName, Buffer.from(mensaje), { persistent: true });

        console.log('Mensaje enviado correctamente a la cola.');
        await channel.close();
        await connection.close();
    } catch (error) {
        logger.error(err);
    }
}

module.exports = saveUserAction;
