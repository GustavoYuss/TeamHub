const { newFile } = require('../Helpers/FileHelper');

const SaveFile = (req, res) => {
    console.log(req.request);
    newFile(req.request);
    res(null, {response:200});
};

module.exports = {
    SaveFile
};

