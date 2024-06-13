const fileDAO = require('../DataAccessObjects/FileDAO');
const extensionDAO = require('../DataAccessObjects/ExtensionDAO');
const fs = require('fs').promises;
const path = require('path');

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
        console.log(err);
    }
}

const deleteFile = async (req) => {
    try {
        console.log("LLEGO AQUI DEDEDEDE");
        const file = await fileDAO.getFile(req);
        destroyFileSystem(file);
        await fileDAO.deleteFile(req);
    }catch(err) {
        console.log(err);
    }
}

const destroyFileSystem = async (req) => {
    if (req) {
        const filePath = path.join(req.Path, req.Name);

        try {
            await fs.unlink(filePath);
            console.log("File successfully deleted from filesystem.");
        } catch (err) {
            console.error("Error deleting file from filesystem:", err);
        }
    }else {
        console.log("File not found in database.");
    }
}

module.exports = {
    saveNewFile,
    deleteFile
};