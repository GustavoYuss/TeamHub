const { where } = require('sequelize');
const { document } = require('../Models');

class FileDAO {
    static async saveNewFile(file) {
        return await document.create(file);
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
            console.error(err);
        }
    }

    static async deleteFile(fileId) {
        try {
            const result = await document.destroy({
                where: { IdDocument: fileId }
            });
            return result;
        } catch (err) {
            console.error(err);
        }
    }
}

module.exports = FileDAO;