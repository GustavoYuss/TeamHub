const { 
    newFile,
    DeleteFile,
    DownloadFile 
} = require('../Helpers/FileHelper');

const SaveFile = (req, res) => {
    newFile(req.request);
    res(null, {response:200});
};

const DeleteFileSystem = (req, res) => {
    DeleteFile(req.request);
    res(null, {response:200});
};

const DownloadFileSystem = async (req, res) => {
    try{
        const fileData = await DownloadFile(req.request);
        console.log(fileData);
        res(null, {fileContent:fileData});
    }catch (err) {
        console.error(err);
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

