const fs = require('fs');
const path = require('path');
const util = require('util');
const readFileAsync = util.promisify(fs.readFile);


const { 
    saveNewFile,
    deleteFile,
    getFilePath 
} = require('../Controller/FileController');

const getFolderPath = (folderName) => {
    try {
        const endFolderPath = path.resolve(__dirname, '../ProjectsFiles/' + folderName);
        const res = endFolderPath;
    
        if (!fs.existsSync(endFolderPath)) {
            fs.mkdirSync(path.resolve(endFolderPath));     
        } 
    
        return res;
    } catch (err){
        throw err;
    }
};

const newFile = (req) => {
    try {
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
    }catch (err){
        throw err;
    }
}

const DeleteFile = (req) => {
    try{
        const idFile = req.idFile;
        deleteFile(idFile)
    }catch (err){
        throw err;
    }
}

const DownloadFile = async (req) => {
    try {
        const fileId = req.idFile;
        const filePath = await getFilePath(fileId);

        if (!fs.existsSync(filePath)) {
            throw new Error('File not found');
        }

        const fileData = await readFileAsync(filePath);

        return fileData;
    } catch (err) {
        throw err; 
    }
};

const reMakeFile = (fileString, rutaArchivo, callback) => {
    try{
        fs.writeFile(rutaArchivo, fileString, err => {
            if (err) {
                return callback(err);
            }
            callback(null, 'Archivo recreado exitosamente.');
        });
    }catch (err){
        throw err;
    }
}




module.exports = {
    newFile,
    DeleteFile,
    DownloadFile
};