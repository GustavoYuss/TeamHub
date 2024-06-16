const { where } = require('sequelize');
const { extension } = require('../Models');

class ExtensionDAO{
    
    static async createNewExtension(extensionAux) {
        try{
            const newExtencionDB = await extension.create(extensionAux);
            return newExtencionDB.IdExtension;
        }catch (err){
            throw err;
        }
    }

    static async getExtensionId(extensionAux){
        try{
            let result = 0;
            const extensionDB = await extension.findOne({
                where : {Extension: extensionAux}
            });
            
            if (extensionDB){
                result = extensionDB.IdExtension;
            }
            
            return result;
        }catch (err){
            throw err;
        }
    }
}

module.exports = ExtensionDAO;