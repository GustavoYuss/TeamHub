const path = require('path');
const grpc = require('@grpc/grpc-js')
const protoLoader = require('@grpc/proto-loader');
const { error } = require('console');
const fs = require('fs');

const PORT = 8083;
const PROTO_FILE = '../Protos/file.proto';

const packageDef = protoLoader.loadSync(path.resolve(__dirname, PROTO_FILE));
const grpcObj = grpc.loadPackageDefinition(packageDef);

const client = new grpcObj.filePackage.FileManagement(
    `127.0.0.1:${PORT}`, grpc.credentials.createInsecure()
);

const deadline = new Date();
deadline.setSeconds(deadline.getSeconds() + 5);
client.waitForReady(deadline, (err) => {
    if (err) {
        console.error(err);
        return;
    }
    onClientReady();
});

function onClientReady(){
    const file = "./CU.docx";
    dividirArchivo(file, (err,data) => {
        if (err) {
            console.error(err);
        }
        else{
            const filename =  path.basename(file);
            let extension = path.extname(file);

            client.SaveFile({projectName: 2, fileName: filename, extension: extension, fileString: data}, (err, result) =>{
                if (err){
                    console.log(err);
                    return;
                }
                console.log(result);
            });
        }
    });
}

function dividirArchivo(rutaArchivo, callback) {
    fs.readFile(rutaArchivo, (err, data) => {
        if (err) {
            return callback(err);
        }

        callback(null, data);
    });
}