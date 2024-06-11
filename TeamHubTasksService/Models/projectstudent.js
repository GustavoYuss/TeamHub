const Sequelize = require('sequelize');
module.exports = function(sequelize, DataTypes) {
  return sequelize.define('projectstudent', {
    IdProjectStudent: {
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
    tableName: 'projectstudent',
    timestamps: false,
    indexes: [
      {
        name: "PRIMARY",
        unique: true,
        using: "BTREE",
        fields: [
          { name: "IdProjectStudent" },
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
        name: "IdStudent",
        using: "BTREE",
        fields: [
          { name: "IdStudent" },
        ]
      },
    ]
  });
};
