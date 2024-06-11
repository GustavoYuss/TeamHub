const fs = require('fs');
const path = require('path');

const { 
    saveNewFile 
} = require('../Controller/FileController');

const getFolderPath = (folderName) => {
    const endFolderPath = path.resolve(__dirname, '../ProjectsFiles/' + folderName);
    const res = endFolderPath;

    if (!fs.existsSync(endFolderPath)) {
        fs.mkdirSync(path.resolve(endFolderPath));     
    } 

    return res;
};

const newFile = (req) => { 
    const projectPath = getFolderPath(req.projectName);
    const filePath = projectPath + `/${req.fileName}`
    reMakeFile(req.fileString, filePath, (err, statuscode) => {
        if (err) {
            console.error('Error al guardar el archivo:', err);
        } else {
            console.log('Archivo guardado exitosamente:', statuscode);
            const Name = req.fileName;
            const Path = projectPath;
            const Extension = req.extension;
            const IdProject = req.projectName;
            const file = {Name, Path,Extension, IdProject};
            saveNewFile(file);
        }
    });
}

const reMakeFile = (fileString, rutaArchivo, callback) => {
    fs.writeFile(rutaArchivo, fileString, err => {
        if (err) {
            return callback(err);
        }
        callback(null, 'Archivo recreado exitosamente.');
    });
}


module.exports = {
    newFile
};