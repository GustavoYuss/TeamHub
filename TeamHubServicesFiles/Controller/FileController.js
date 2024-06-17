const fileDAO = require('../DataAccessObjects/FileDAO');
const extensionDAO = require('../DataAccessObjects/ExtensionDAO');
const fs = require('fs').promises;
const path = require('path');
const logger = require("../Controller/logger");

const saveNewFile = async (req) => {
    try {
        const extensionName = req.Extension;
        let extension = await extensionDAO.getExtensionId(req.Extension);

        if (extension == 0){
            console.log(extensionName);
            const Extension = extensionName;
            const newExtencion = { Extension };
            extension = await extensionDAO.createNewExtension(newExtencion);    
        }

        req.Extension = extension;
        await fileDAO.saveNewFile(req);
    } catch(err) {
        throw err;
    }
}

const deleteFile = async (req) => {
    try {
        console.log("LLEGO AQUI DEDEDEDE");
        const file = await fileDAO.getFile(req);
        await destroyFileSystem(file);
        await fileDAO.deleteFile(req);
    }catch(err) {
        throw err;
    }
}

const destroyFileSystem = async (req) => {
    if (req) {
        const filePath = path.join(req.Path, req.Name);

        try {
            await fs.unlink(filePath);
            console.log(`File ${filePath} was deleted successfully.`);
        } catch (err) {
            if (err.code === 'ENOENT') {
                console.error(`File ${filePath} does not exist.`);
            } else {
                throw err;
            }
        }
    } else {
        throw new Error("Request object is missing.");
    }
};


const getFilePath = async (fileId) => {
    try {
        const file = await fileDAO.getFile(fileId);
        if (!file) {
            throw new Error('File not found');
        }
        const filePath = path.join(file.Path, file.Name);
        return filePath;
    } catch (err) {
        throw err;
    }
}

module.exports = {
    saveNewFile,
    deleteFile,
    getFilePath
};