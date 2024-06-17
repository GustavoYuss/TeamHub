const { where } = require('sequelize');
const { document } = require('../Models');

class FileDAO {
    static async saveNewFile(file) {
        try{
            const result = await document.create(file);
            return result;
        }catch (err){
            throw err;
        }
    }

    static async getFile(fileId) {
        try{
            let result = null;
            const file = await document.findOne({
                where :  { IdDocument: fileId}
            });

            if (file){
                result = file;
            }

            return result;
        }catch (err){
            throw err;
        }
    }

    static async deleteFile(fileId) {
        try {
            const result = await document.destroy({
                where: { IdDocument: fileId }
            });
            return result;
        } catch (err) {
            throw err;
        }
    }
}

module.exports = FileDAO;