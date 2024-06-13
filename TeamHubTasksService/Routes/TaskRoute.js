const {Router} = require('express');

const {
    createTask,
    getAllTaskByDate,
    updateTask,
    deleteTask,
    getAllTaskCompleteByProject,
    getTasksByProject
} = require('../Controller/TaskController');

const router = Router();

router.post('/', createTask);
router.get('/get', getAllTaskByDate);
router.get('/:IdProject', getTasksByProject);
router.get('/getAll', getAllTaskCompleteByProject);
router.post('/up', updateTask);
router.delete('/:IdTask', deleteTask);

module.exports = router;