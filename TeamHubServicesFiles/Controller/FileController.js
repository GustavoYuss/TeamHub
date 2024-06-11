const fileDAO = require('../DataAccessObjects/FileDAO');
const extensionDAO = require('../DataAccessObjects/ExtensionDAO');

const saveNewFile = async (req) => {
    try {
        const extensionName = req.Extension;
        console.log("ID EXTENSION = ", extensionName);
        let extension = await extensionDAO.getExtensionId(req.Extension);

        if (extension == 0){
            console.log(extensionName);
            const Extension = extensionName;
            const newExtencion = { Extension };
            extension = await extensionDAO.createNewExtension(newExtencion);    
        }

        req.Extension = extension;
        console.log(`ExtensionID: ${extension}`);
        await fileDAO.saveNewFile(req);
    } catch(err) {
        console.log(err);
    }
}

module.exports = {
    saveNewFile
};