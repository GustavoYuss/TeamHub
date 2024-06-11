var DataTypes = require("sequelize").DataTypes;
var _document = require("./document");
var _extension = require("./extension");
var _project = require("./project");
var _projectstudent = require("./projectstudent");
var _student = require("./student");
var _studentsession = require("./studentsession");
var _tasks = require("./tasks");
var _taskstudent = require("./taskstudent");

function initModels(sequelize) {
  var document = _document(sequelize, DataTypes);
  var extension = _extension(sequelize, DataTypes);
  var project = _project(sequelize, DataTypes);
  var projectstudent = _projectstudent(sequelize, DataTypes);
  var student = _student(sequelize, DataTypes);
  var studentsession = _studentsession(sequelize, DataTypes);
  var tasks = _tasks(sequelize, DataTypes);
  var taskstudent = _taskstudent(sequelize, DataTypes);

  document.belongsTo(extension, { as: "Extension_extension", foreignKey: "Extension"});
  extension.hasMany(document, { as: "documents", foreignKey: "Extension"});
  document.belongsTo(project, { as: "IdProject_project", foreignKey: "IdProject"});
  project.hasMany(document, { as: "documents", foreignKey: "IdProject"});
  projectstudent.belongsTo(project, { as: "IdProject_project", foreignKey: "IdProject"});
  project.hasMany(projectstudent, { as: "projectstudents", foreignKey: "IdProject"});
  tasks.belongsTo(project, { as: "IdProject_project", foreignKey: "IdProject"});
  project.hasMany(tasks, { as: "tasks", foreignKey: "IdProject"});
  projectstudent.belongsTo(student, { as: "IdStudent_student", foreignKey: "IdStudent"});
  student.hasMany(projectstudent, { as: "projectstudents", foreignKey: "IdStudent"});
  studentsession.belongsTo(student, { as: "IdStudent_student", foreignKey: "IdStudent"});
  student.hasMany(studentsession, { as: "studentsessions", foreignKey: "IdStudent"});
  taskstudent.belongsTo(student, { as: "IdStudent_student", foreignKey: "IdStudent"});
  student.hasMany(taskstudent, { as: "taskstudents", foreignKey: "IdStudent"});
  taskstudent.belongsTo(tasks, { as: "IdTask_task", foreignKey: "IdTask"});
  tasks.hasMany(taskstudent, { as: "taskstudents", foreignKey: "IdTask"});

  return {
    document,
    extension,
    project,
    projectstudent,
    student,
    studentsession,
    tasks,
    taskstudent,
  };
}
module.exports = initModels;
module.exports.initModels = initModels;
module.exports.default = initModels;
