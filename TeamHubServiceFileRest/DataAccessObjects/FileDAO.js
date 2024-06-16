const { where } = require('sequelize');
const { document } = require('../Models/Index');
const logger = require('../Controller/logger'); 

class FileDAO{
    static async getFilesByProject(IdProject){
        try{
            const response = await document.findAll({
                where: {
                    IdProject : IdProject
                },
                attributes: ['IdDocument', 'Name', 'Extension']
            });
            return response;;
        }catch (err){
            logger.error(err);
        }
        return null;
    }

    static async getFilesByExtension(file){
        try{
            const response = await document.findAll({
                where: {
                    Extension : file.IdExtension,
                    IdProject : file.IdProject 
                },
                attributes: ['IdDocument', 'Name', 'Extension']
            });
            return response;;
        }catch (err){
            logger.error(err);
        }
        return null;
    }
}


module.exports = FileDAO;