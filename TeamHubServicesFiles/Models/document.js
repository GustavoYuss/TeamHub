const Sequelize = require('sequelize');
module.exports = function(sequelize, DataTypes) {
  return sequelize.define('document', {
    IdDocument: {
      autoIncrement: true,
      type: DataTypes.INTEGER,
      allowNull: false,
      primaryKey: true
    },
    Name: {
      type: DataTypes.STRING(50),
      allowNull: true
    },
    Path: {
      type: DataTypes.STRING(250),
      allowNull: true
    },
    Extension: {
      type: DataTypes.INTEGER,
      allowNull: true,
      references: {
        model: 'extension',
        key: 'IdExtension'
      }
    },
    IdProject: {
      type: DataTypes.INTEGER,
      allowNull: false,
      references: {
        model: 'project',
        key: 'IdProject'
      }
    }
  }, {
    sequelize,
    tableName: 'document',
    timestamps: false,
    indexes: [
      {
        name: "PRIMARY",
        unique: true,
        using: "BTREE",
        fields: [
          { name: "IdDocument" },
        ]
      },
      {
        name: "document_extension_idx",
        using: "BTREE",
        fields: [
          { name: "Extension" },
        ]
      },
      {
        name: "document_project_idx",
        using: "BTREE",
        fields: [
          { name: "IdProject" },
        ]
      },
    ]
  });
};
