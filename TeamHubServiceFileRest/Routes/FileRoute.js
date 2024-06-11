const {Router} = require('express');

const {
    getFilesByProject,
    getFilesByExtension
} = require('../Controller/FileController');

const router = Router();

router.get('/:IdProject', getFilesByProject);
router.get('/Extension', getFilesByExtension);

module.exports = router;