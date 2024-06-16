const {response} = require("express");
const fileDAO = require("../DataAccessObjects/FileDAO");
const UserActionDTO = require("../DTOs/UserAction");
const saveUserAction = require("../Controller/LogService");
const logger = require('../Controller/logger'); 

const getFilesByProject = async (req, res = response) => {
    const { IdProject } = req.params;
    try {
        const idUserClaim = parseInt(req.user.IdUser, 10);
        const idSessionClaim = parseInt(req.user.IdSession, 10);
        const message = new UserActionDTO(idUserClaim, idSessionClaim, "Consultar archivos de un proyecto");
        saveUserAction(message);

        const fileList = await fileDAO.getFilesByProject(IdProject);
        res.json(fileList);
    } catch (err) {
        logger.error(err);
        res.status(500);
    }
};

const getFilesByExtension = async (req, res = response) => {
    const { IdExtension,  IdProject } = req.body;
    const file = { IdExtension,  IdProject };
    try {
        const idUserClaim = parseInt(req.user.IdUser, 10);
        const idSessionClaim = parseInt(req.user.IdSession, 10);
        const message = new UserActionDTO(idUserClaim, idSessionClaim, "Consultar archivos de una extension especifica de un proyecto");
        saveUserAction(message);
        const fileList = await fileDAO.getFilesByExtension(file);
        res.json(fileList);
    }catch(err) {
        logger.error(err);
        res.status(500);
    }
};

module.exports = {
    getFilesByProject,
    getFilesByExtension
};