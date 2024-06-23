DROP SCHEMA IF EXISTS teamhub_db;
CREATE SCHEMA IF NOT EXISTS teamhub_db;

USE teamhub_db;

CREATE TABLE `student` (
  `IdStudent` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(15) DEFAULT NULL,
  `MiddleName` varchar(15) DEFAULT NULL,
  `LastName` varchar(15) DEFAULT NULL,
  `SurName` varchar(15) DEFAULT NULL,
  `Email` varchar(50) DEFAULT NULL,
  `Password` varchar(200) DEFAULT NULL,
  `ProDocumentImage` varchar(250) DEFAULT NULL,
  PRIMARY KEY (`IdStudent`)
);


CREATE TABLE `extension` (
  `IdExtension` int NOT NULL AUTO_INCREMENT,
  `Extension` varchar(45) NOT NULL,
  PRIMARY KEY (`IdExtension`)
);

CREATE TABLE `project` (
  `IdProject` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) DEFAULT NULL,
  `StartDate` date DEFAULT NULL,
  `EndDate` date DEFAULT NULL,
  `Status` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`IdProject`)
);

CREATE TABLE `document` (
  `IdDocument` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(300) DEFAULT NULL,
  `Path` varchar(250) DEFAULT NULL,
  `Extension` int DEFAULT NULL,
  `IdProject` int NOT NULL,
  PRIMARY KEY (`IdDocument`),
  KEY `document_extension_idx` (`Extension`),
  KEY `document_project_idx` (`IdProject`),
  CONSTRAINT `document_extension` FOREIGN KEY (`Extension`) REFERENCES `extension` (`IdExtension`),
  CONSTRAINT `document_project` FOREIGN KEY (`IdProject`) REFERENCES `project` (`IdProject`)
);

CREATE TABLE `projectstudent` (
  `IdProjectStudent` int NOT NULL AUTO_INCREMENT,
  `IdProject` int DEFAULT NULL,
  `IdStudent` int DEFAULT NULL,
  PRIMARY KEY (`IdProjectStudent`),
  KEY `IdProject` (`IdProject`),
  KEY `IdStudent` (`IdStudent`),
  CONSTRAINT `projectstudent_ibfk_1` FOREIGN KEY (`IdProject`) REFERENCES `project` (`IdProject`) ON DELETE CASCADE,
  CONSTRAINT `projectstudent_ibfk_2` FOREIGN KEY (`IdStudent`) REFERENCES `student` (`IdStudent`) ON DELETE CASCADE
);

CREATE TABLE `studentsession` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `IdStudent` int NOT NULL,
  `StartDate` datetime NOT NULL,
  `EndDate` datetime NOT NULL,
  `IPAdress` varchar(30) NOT NULL,
  `Token` longtext,
  PRIMARY KEY (`Id`),
  KEY `session_student_idx` (`IdStudent`),
  CONSTRAINT `session_student` FOREIGN KEY (`IdStudent`) REFERENCES `student` (`IdStudent`)
);

CREATE TABLE `tasks` (
  `IdTask` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) DEFAULT NULL,
  `Description` varchar(250) DEFAULT NULL,
  `StartDate` date DEFAULT NULL,
  `EndDate` date DEFAULT NULL,
  `IdProject` int DEFAULT NULL,
  `Status` varchar(90) DEFAULT NULL,
  PRIMARY KEY (`IdTask`),
  KEY `task_project_idx` (`IdProject`),
  CONSTRAINT `task_project` FOREIGN KEY (`IdProject`) REFERENCES `project` (`IdProject`)
);

CREATE TABLE `taskstudent` (
  `IdTaskStudent` int NOT NULL AUTO_INCREMENT,
  `IdTask` int DEFAULT NULL,
  `IdStudent` int DEFAULT NULL,
  PRIMARY KEY (`IdTaskStudent`),
  KEY `IdTask` (`IdTask`),
  KEY `IdStudent` (`IdStudent`),
  CONSTRAINT `taskstudent_ibfk_1` FOREIGN KEY (`IdTask`) REFERENCES `tasks` (`IdTask`) ON DELETE CASCADE,
  CONSTRAINT `taskstudent_ibfk_2` FOREIGN KEY (`IdStudent`) REFERENCES `student` (`IdStudent`) ON DELETE CASCADE
);
