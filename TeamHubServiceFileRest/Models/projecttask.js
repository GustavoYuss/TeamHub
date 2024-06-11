const Sequelize = require('sequelize');
module.exports = function(sequelize, DataTypes) {
  return sequelize.define('projecttask', {
    IdProjectTask: {
      autoIncrement: true,
      type: DataTypes.INTEGER,
      allowNull: false,
      primaryKey: true
    },
    IdProject: {
      type: DataTypes.INTEGER,
      allowNull: true,
      references: {
        model: 'project',
        key: 'IdProject'
      }
    },
    IdTask: {
      type: DataTypes.INTEGER,
      allowNull: true,
      references: {
        model: 'task',
        key: 'IdTask'
      }
    }
  }, {
    sequelize,
    tableName: 'projecttask',
    timestamps: false,
    indexes: [
      {
        name: "PRIMARY",
        unique: true,
        using: "BTREE",
        fields: [
          { name: "IdProjectTask" },
        ]
      },
      {
        name: "IdProject",
        using: "BTREE",
        fields: [
          { name: "IdProject" },
        ]
      },
      {
        name: "IdTask",
        using: "BTREE",
        fields: [
          { name: "IdTask" },
        ]
      },
    ]
  });
};
