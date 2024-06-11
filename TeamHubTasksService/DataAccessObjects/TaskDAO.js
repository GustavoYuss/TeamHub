const { where } = require("sequelize");
const { tasks } = require("../Models/Index");
class TaskDAO {
  
  static async createNewTask(taskAux) {
    const existingTask = await tasks.findOne({
      where: { Name: taskAux.Name },
    });

    if (existingTask) {
      return {
        message: "La actividad ya está registrada en la base de datos.",
      };
    }

    return await tasks.create(taskAux);
  }

  static async getAllTasks() {
    return await tasks.findAll();
  }

  static async getAllProjectTasks(idProject) {
    return await tasks.findAll({
      where: {
        IdProject: idProject,
      },
    });
  }

  static async findTasksByDate(startDate, endDate) {
    console.log(startDate, endDate);
    return await tasks.findAll({
      where: {
        StartDate: startDate,
        EndDate: endDate,
      },
    });
  }

  static async updateTaskByID(taskNew) {
    try {
      const result = await tasks.update(taskNew, {
        where: { IdTask: taskNew.IdTask },
      });
  
      const [affectedCount] = result;
  
      if (affectedCount === 0) {
        return {
          message: "No se encontró ninguna tarea con ese ID para actualizar.",
        };
      }
  
      return {
        message: "La tarea se actualizó con éxito.",
        affectedCount,
      };
    } catch (error) {
      return {
        message: "Ocurrió un error al intentar actualizar la tarea.",
        error: error.message,
      };
    }
  }
  
  static async deleteTaskByName(name) {
    return await tasks.destroy({ where: { name } });
  }
}

module.exports = TaskDAO;
