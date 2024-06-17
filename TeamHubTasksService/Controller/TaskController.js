const {response} = require("express");
const taskDAO = require('../DataAccessObjects/TaskDAO');
const UserActionDTO = require("../DTOs/UserAction");
const saveUserAction = require("../Controller/LogService");

const getTasksByProject = async (req, res = response) => {
    const { IdProject } = req.params;

    if (IdProject == null || IdProject == 0) {
        return res.status(400).json({ message: 'Invalid IdProject' });
    }

    try {

        const idUserClaim = parseInt(req.user.IdUser, 10);
        const idSessionClaim = parseInt(req.user.IdSession, 10);
        const message = new UserActionDTO(idUserClaim, idSessionClaim, "Obtener tareas por proyecto");
        saveUserAction(message);
        const taskList = await taskDAO.getAllProjectTasks(IdProject);
        res.json(taskList);

      } catch (error) {

        console.error(error)
        res.status(500).json({ message: error });

    }
}

const createTask = async (req, res = response) => {
    const { Name, Description, StartDate, EndDate, IdProject, Status } = req.body;
    const task = { Name, Description, StartDate, EndDate, IdProject, Status };
    const validationError = validateTask(task);

    if (validationError) {
        return res.status(400).json({ message: validationError });
    } 

    try {

        const idUserClaim = parseInt(req.user.IdUser, 10);
        const idSessionClaim = parseInt(req.user.IdSession, 10);
        const message = new UserActionDTO(idUserClaim, idSessionClaim, "Dar de alta tarea en un proyecto");
        saveUserAction(message);
        const result = await taskDAO.createNewTask(task);

        if (result.message === 'La actividad ya está registrada en la base de datos.') {

            return res.status(409).json({ message: result.message });

        }

        res.status(201).json({ message: 'Se registró', task: result });

    } catch (error) {

        res.status(500).json({ message: 'Error al crear la tarea', error: error.message });

    }
};

const updateTask = async (req, res = response) => {

    const { IdTask, Name, Description, StartDate, EndDate, IdProject, Status } = req.body;
    const task = {IdTask, Name, Description, StartDate, EndDate, IdProject, Status };
    const validationError = validateTask(task);

    if (validationError) {
        return res.status(400).json({ message: validationError });
    } 

    try {

        const idUserClaim = parseInt(req.user.IdUser, 10);
        const idSessionClaim = parseInt(req.user.IdSession, 10);
        const message = new UserActionDTO(idUserClaim, idSessionClaim, "Actualizar una tarea en un proyecto");
        saveUserAction(message);
        const result = await taskDAO.updateTaskByID(task);
        res.status(201).json({ message: 'Se modifico la Tarea: ', task: result });

    } catch (error) {

        res.status(500).json({ message: 'Error al modificar la tarea', error: error.message });

    }
}

const deleteTask = async (req, res = response) => {
    const { IdTask } = req.params;
    const uid = req.uid;

    if (IdTask == null || IdTask == 0) {
        return res.status(400).json({ message: 'Invalid Task' });
    }

    try {

        const idUserClaim = parseInt(req.user.IdUser, 10);
        const idSessionClaim = parseInt(req.user.IdSession, 10);
        const message = new UserActionDTO(idUserClaim, idSessionClaim, "Eliminar una tarea de un proyecto");
        saveUserAction(message);
        await taskDAO.deleteTaskByID(IdTask);
        res.json({uid});

    } catch (error) {

        console.error(error);
        res.status(500).json({ message: 'Error deleting user' });

    }
}

const getAllTaskCompleteByProject = async (req, res = response) => {

    try {

        const idUserClaim = parseInt(req.user.IdUser, 10);
        const idSessionClaim = parseInt(req.user.IdSession, 10);
        const message = new UserActionDTO(idUserClaim, idSessionClaim, "Obtener todas las tareas completadas de un proyecto");
        saveUserAction(message);
        const taskList = await taskDAO.getAllTasks();
        res.json(taskList);

    } catch (error) {
        console.error(error);
        res.status(500).json({ message: error });
    }

}

const getAllTaskByDate = async (req, res = response) => {
    const {StartDate, EndDate } = req.body;

    if (!StartDate || !EndDate || !isValidDateTime(StartDate) || !isValidDateTime(EndDate)) {
        return res.status(400).json({ message: 'Invalid Dates' });
    }

    try {
        const idUserClaim = parseInt(req.user.IdUser, 10);
        const idSessionClaim = parseInt(req.user.IdSession, 10);
        const message = new UserActionDTO(idUserClaim, idSessionClaim, "Obtener todas las tareas de un proyecto por fechas");
        saveUserAction(message);
        const taskList = await taskDAO.findTasksByDate(StartDate, EndDate);
        res.json(taskList);
      } catch (error) {
        console.error(error);
        res.status(500).json({ message: error });
    }
}

function validateTask(task) {
    const { IdTask, Name, Description, StartDate, EndDate, IdProject, Status } = task;

    if (IdTask === null) {
        return 'Invalid IdTask';
    }

    if (!Name || typeof Name !== 'string' || Name.trim() === '') {
        return 'Invalid Name';
    }

    if (!Description || typeof Description !== 'string' || Description.trim() === '') {
        return 'Invalid Description';
    }

    if (!StartDate || !isValidDateTime(StartDate)) {
        return 'Invalid StartDate';
    }

    if (!EndDate || !isValidDateTime(EndDate)) {
        return 'Invalid EndDate';
    }

    if (IdProject === null || IdProject === 0) {
        return 'Invalid IdProject';
    }

    if (!Status || typeof Status !== 'string' || Status.trim() === '') {
        return 'Invalid Status';
    }

    return null; 
}

function isValidDateTime(dateTime) {
    const date = new Date(dateTime);
    return !isNaN(date.getTime());
}



module.exports = {
    createTask,
    getAllTaskByDate,
    updateTask,
    deleteTask,
    getAllTaskCompleteByProject,
    getTasksByProject
};