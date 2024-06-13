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

const DeleteFile = (req) => {
    console.log("HELPER: " + req.idFile);
    const idFile = req.idFile;
    deleteFile(idFile)
}

const DownloadFile = async (req) => {
    try {
        const fileId = req.idFile;
        const filePath = await getFilePath(fileId);

        if (!fs.existsSync(filePath)) {
            throw new Error('File not found');
        }

        // Utiliza promisify para convertir readFile en una función que devuelve una promesa
        const fileData = await readFileAsync(filePath);

        // Puedes procesar los datos aquí si es necesario

        return fileData;
    } catch (err) {
        console.error('Error en DownloadFile:', err);
        throw err; // Propaga el error para que sea manejado en DownloadFileSystem
    }
};

const reMakeFile = (fileString, rutaArchivo, callback) => {
    fs.writeFile(rutaArchivo, fileString, err => {
        if (err) {
            return callback(err);
        }
        callback(null, 'Archivo recreado exitosamente.');
    });
}




module.exports = {
    newFile,
    DeleteFile,
    DownloadFile
};