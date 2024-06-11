const Sequelize = require('sequelize');
module.exports = function(sequelize, DataTypes) {
  return sequelize.define('taskstudent', {
    IdTaskStudent: {
      autoIncrement: true,
      type: DataTypes.INTEGER,
      allowNull: false,
      primaryKey: true
    },
    IdTask: {
      type: DataTypes.INTEGER,
      allowNull: true,
      references: {
        model: 'tasks',
        key: 'IdTask'
      }
    },
    IdStudent: {
      type: DataTypes.INTEGER,
      allowNull: true,
      references: {
        model: 'student',
        key: 'IdStudent'
      }
    }
  }, {
    sequelize,
    tableName: 'taskstudent',
    timestamps: false,
    indexes: [
      {
        name: "PRIMARY",
        unique: true,
        using: "BTREE",
        fields: [
          { name: "IdTaskStudent" },
        ]
      },
      {
        name: "IdTask",
        using: "BTREE",
        fields: [
          { name: "IdTask" },
        ]
      },
      {
        name: "IdStudent",
        using: "BTREE",
        fields: [
          { name: "IdStudent" },
        ]
      },
    ]
  });
};
