const { where } = require('sequelize');
const { document } = require('../Models/Index');

class FileDAO{
    static async getFilesByProject(IdProject){
        return await document.findAll({
            where: {
                IdProject : IdProject
            },
            attributes: ['IdDocument', 'Name', 'Extension']
        });
    }

    static async getFilesByExtension(file){
        return await document.findAll({
            where: {
                Extension : file.IdExtension,
                IdProject : file.IdProject 
            },
            attributes: ['IdDocument', 'Name', 'Extension']
        });
    }
}


module.exports = FileDAO;