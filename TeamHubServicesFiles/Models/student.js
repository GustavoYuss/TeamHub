const Sequelize = require('sequelize');
module.exports = function(sequelize, DataTypes) {
  return sequelize.define('student', {
    IdStudent: {
      autoIncrement: true,
      type: DataTypes.INTEGER,
      allowNull: false,
      primaryKey: true
    },
    Name: {
      type: DataTypes.STRING(15),
      allowNull: true
    },
    MiddleName: {
      type: DataTypes.STRING(15),
      allowNull: true
    },
    LastName: {
      type: DataTypes.STRING(15),
      allowNull: true
    },
    SurName: {
      type: DataTypes.STRING(15),
      allowNull: true
    },
    Email: {
      type: DataTypes.STRING(50),
      allowNull: true
    },
    Password: {
      type: DataTypes.STRING(200),
      allowNull: true
    },
    ProDocumentImage: {
      type: DataTypes.STRING(250),
      allowNull: true
    }
  }, {
    sequelize,
    tableName: 'student',
    timestamps: false,
    indexes: [
      {
        name: "PRIMARY",
        unique: true,
        using: "BTREE",
        fields: [
          { name: "IdStudent" },
        ]
      },
    ]
  });
};
