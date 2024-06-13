const { 
    newFile,
    DeleteFile 
} = require('../Helpers/FileHelper');

const SaveFile = (req, res) => {
    console.log(req.request);
    newFile(req.request);
    res(null, {response:200});
};

const DeleteFileSystem = (req, res) => {
    console.log("Controller:" + req.request.idFile);
    DeleteFile(req.request);
    res(null, {response:200});
};

module.exports = {
    SaveFile,
    DeleteFileSystem
};

