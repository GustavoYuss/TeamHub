const { document } = require('../Models');

class FileDAO {
    static async saveNewFile(file) {
        return await document.create(file);
    }
}

module.exports = FileDAO;