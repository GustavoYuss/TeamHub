const Sequelize = require('sequelize');
module.exports = function(sequelize, DataTypes) {
  return sequelize.define('curso', {
    ID: {
      autoIncrement: true,
      type: DataTypes.INTEGER,
      allowNull: false,
      primaryKey: true
    },
    Clave: {
      type: DataTypes.STRING(10),
      allowNull: true
    },
    Nombre: {
      type: DataTypes.STRING(200),
      allowNull: true
    },
    Descripcion: {
      type: DataTypes.STRING(1000),
      allowNull: true
    },
    NoHoras: {
      type: DataTypes.INTEGER,
      allowNull: true
    },
    FechaInicio: {
      type: DataTypes.DATE,
      allowNull: true
    },
    FechaTermino: {
      type: DataTypes.DATE,
      allowNull: true
    },
    Costo: {
      type: DataTypes.DECIMAL(6,2),
      allowNull: true
    },
    Instructor: {
      type: DataTypes.STRING(100),
      allowNull: true
    }
  }, {
    sequelize,
    tableName: 'curso',
    timestamps: false,
    indexes: [
      {
        name: "PRIMARY",
        unique: true,
        using: "BTREE",
        fields: [
          { name: "ID" },
        ]
      },
    ]
  });
};
