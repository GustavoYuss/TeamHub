const { 
    newFile,
    DeleteFile,
    DownloadFile 
} = require('../Helpers/FileHelper');
const logger = require("../Controller/logger");

const SaveFile = (req, res) => {
    try{
        newFile(req.request);
        res(null, {response:200});
    }catch (err) {
        logger.error(err);
        res({
            code: grpc.status.NOT_FOUND,
            message: 'Error al subir el archivo'
        });
    }
};

const DeleteFileSystem = (req, res) => {
    try{
        DeleteFile(req.request);
        res(null, {response:200});
    }catch (err) {
        logger.error(err);
        res({
            code: grpc.status.NOT_FOUND,
            message: 'Error al eliminar el archivo'
        });
    }
};

const DownloadFileSystem = async (req, res) => {
    try{
        const fileData = await DownloadFile(req.request);
        res(null, {fileContent:fileData});
    }catch (err) {
        logger.error(err);
        res({
            code: grpc.status.NOT_FOUND,
            message: 'Error al descargar el archivo'
        });
    }
}

module.exports = {
    SaveFile,
    DeleteFileSystem,
    DownloadFileSystem
};

