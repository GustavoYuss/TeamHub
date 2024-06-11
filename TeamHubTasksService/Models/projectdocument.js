const Sequelize = require('sequelize');
module.exports = function(sequelize, DataTypes) {
  return sequelize.define('projectdocument', {
    IdProjectDocument: {
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
    IdDocument: {
      type: DataTypes.INTEGER,
      allowNull: true,
      references: {
        model: 'document',
        key: 'IdDocument'
      }
    }
  }, {
    sequelize,
    tableName: 'projectdocument',
    timestamps: false,
    indexes: [
      {
        name: "PRIMARY",
        unique: true,
        using: "BTREE",
        fields: [
          { name: "IdProjectDocument" },
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
        name: "IdDocument",
        using: "BTREE",
        fields: [
          { name: "IdDocument" },
        ]
      },
    ]
  });
};
