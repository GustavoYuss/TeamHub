const amqp = require('amqplib');

async function saveUserAction(userAction) {
    try {
        // Establece la conexión con RabbitMQ
        const connection = await amqp.connect('amqp://172.16.0.11');
        const channel = await connection.createChannel();

        // Declara la cola
        const queueName = 'Prueba';
        await channel.assertQueue(queueName, { durable: true });

        // Convierte el objeto userAction a formato JSON
        const mensaje = JSON.stringify(userAction);

        // Envía el mensaje a la cola
        await channel.sendToQueue(queueName, Buffer.from(mensaje), { persistent: true });

        console.log('Mensaje enviado correctamente a la cola.');

        // Cierra la conexión
        await channel.close();
        await connection.close();
    } catch (error) {
        console.error('Error al enviar el mensaje:', error);
    }
}

module.exports = saveUserAction;
