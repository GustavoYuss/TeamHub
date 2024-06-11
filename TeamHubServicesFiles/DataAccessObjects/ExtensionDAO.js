const { where } = require('sequelize');
const { extension } = require('../Models');

class ExtensionDAO{
    
    static async createNewExtension(extensionAux) {
        const newExtencionDB = await extension.create(extensionAux);
        return newExtencionDB.IdExtension;
    }

    static async getExtensionId(extensionAux){
        let result = 0;
        const extensionDB = await extension.findOne({
            where : {Extension: extensionAux}
        });

        if (extensionDB){
            result = extensionDB.IdExtension;
        }
        
        return result;
    }
}

module.exports = ExtensionDAO;