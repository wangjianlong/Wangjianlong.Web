-- --------------------------------------------------------
-- 主机:                           192.168.31.10
-- 服务器版本:                        5.1.73-community - MySQL Community Server (GPL)
-- 服务器操作系统:                      Win64
-- HeidiSQL 版本:                  9.4.0.5125
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


-- 导出 fitment 的数据库结构
CREATE DATABASE IF NOT EXISTS `fitment` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `fitment`;

-- 导出  表 fitment.city 结构
CREATE TABLE IF NOT EXISTS `city` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) NOT NULL,
  `ParentID` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 正在导出表  fitment.city 的数据：~0 rows (大约)
DELETE FROM `city`;
/*!40000 ALTER TABLE `city` DISABLE KEYS */;
/*!40000 ALTER TABLE `city` ENABLE KEYS */;

-- 导出  表 fitment.fitment 结构
CREATE TABLE IF NOT EXISTS `fitment` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) NOT NULL,
  `Number` varchar(255) DEFAULT NULL,
  `Address` varchar(255) DEFAULT NULL,
  `CreateTime` datetime NOT NULL,
  `UserID` int(11) NOT NULL DEFAULT '0',
  `CityID` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID`),
  KEY `UserID` (`UserID`),
  KEY `CityID` (`CityID`),
  CONSTRAINT `FK_fitment_city` FOREIGN KEY (`CityID`) REFERENCES `city` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 正在导出表  fitment.fitment 的数据：~0 rows (大约)
DELETE FROM `fitment`;
/*!40000 ALTER TABLE `fitment` DISABLE KEYS */;
/*!40000 ALTER TABLE `fitment` ENABLE KEYS */;

-- 导出  表 fitment.fitmentitem 结构
CREATE TABLE IF NOT EXISTS `fitmentitem` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `ProjectID` int(11) NOT NULL DEFAULT '0',
  `Number` double NOT NULL DEFAULT '0',
  `PositionID` int(11) NOT NULL DEFAULT '0',
  `NewOld` double NOT NULL DEFAULT '1',
  PRIMARY KEY (`ID`),
  KEY `ProjectID` (`ProjectID`),
  CONSTRAINT `FK_fitmentitem_project` FOREIGN KEY (`ProjectID`) REFERENCES `project` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 正在导出表  fitment.fitmentitem 的数据：~0 rows (大约)
DELETE FROM `fitmentitem`;
/*!40000 ALTER TABLE `fitmentitem` DISABLE KEYS */;
/*!40000 ALTER TABLE `fitmentitem` ENABLE KEYS */;

-- 导出  视图 fitment.item_project_position 结构
-- 创建临时表以解决视图依赖性错误
CREATE TABLE `item_project_position` (
	`ID` INT(11) NOT NULL,
	`ProjectID` INT(11) NOT NULL,
	`Title` VARCHAR(1023) NOT NULL COLLATE 'utf8_general_ci',
	`Name` VARCHAR(1023) NOT NULL COLLATE 'utf8_general_ci',
	`Price` INT(11) NOT NULL,
	`Unit` VARCHAR(50) NOT NULL COLLATE 'utf8_general_ci',
	`Number` DOUBLE NOT NULL,
	`NewOld` DOUBLE NOT NULL,
	`PositionID` INT(11) NOT NULL,
	`positionName` VARCHAR(1023) NOT NULL COLLATE 'utf8_general_ci',
	`FitmentID` INT(11) NOT NULL,
	`Category` BIT(4) NOT NULL
) ENGINE=MyISAM;

-- 导出  表 fitment.position 结构
CREATE TABLE IF NOT EXISTS `position` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(1023) NOT NULL,
  `FitmentID` int(11) NOT NULL DEFAULT '0',
  `Category` bit(4) NOT NULL DEFAULT b'0',
  PRIMARY KEY (`ID`),
  KEY `FK_position_fitment` (`FitmentID`),
  CONSTRAINT `FK_position_fitment` FOREIGN KEY (`FitmentID`) REFERENCES `fitment` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 正在导出表  fitment.position 的数据：~0 rows (大约)
DELETE FROM `position`;
/*!40000 ALTER TABLE `position` DISABLE KEYS */;
/*!40000 ALTER TABLE `position` ENABLE KEYS */;

-- 导出  表 fitment.project 结构
CREATE TABLE IF NOT EXISTS `project` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Title` varchar(1023) NOT NULL,
  `Name` varchar(1023) NOT NULL,
  `Price` int(11) NOT NULL DEFAULT '0',
  `Unit` varchar(50) NOT NULL,
  `CityID` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID`),
  KEY `CityID` (`CityID`),
  CONSTRAINT `FK_project_city` FOREIGN KEY (`CityID`) REFERENCES `city` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 正在导出表  fitment.project 的数据：~0 rows (大约)
DELETE FROM `project`;
/*!40000 ALTER TABLE `project` DISABLE KEYS */;
/*!40000 ALTER TABLE `project` ENABLE KEYS */;

-- 导出  表 fitment.secure 结构
CREATE TABLE IF NOT EXISTS `secure` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `UserID` int(11) NOT NULL DEFAULT '0',
  `LoginTime` datetime NOT NULL,
  `LogoutTime` datetime DEFAULT NULL,
  `Address` varchar(50) NOT NULL,
  `HostName` varchar(50) NOT NULL,
  `Browser` varchar(255) NOT NULL,
  `Version` varchar(255) NOT NULL,
  `Platform` varchar(255) NOT NULL,
  `Type` varchar(255) NOT NULL,
  `Message` varchar(255) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `UserID` (`UserID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 正在导出表  fitment.secure 的数据：~0 rows (大约)
DELETE FROM `secure`;
/*!40000 ALTER TABLE `secure` DISABLE KEYS */;
/*!40000 ALTER TABLE `secure` ENABLE KEYS */;

-- 导出  表 fitment.user 结构
CREATE TABLE IF NOT EXISTS `user` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `UserName` varchar(255) NOT NULL,
  `Password` varchar(255) NOT NULL,
  `DisplayName` varchar(255) NOT NULL,
  `Role` bit(4) NOT NULL DEFAULT b'0',
  `Approve` bit(1) NOT NULL DEFAULT b'0',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

-- 正在导出表  fitment.user 的数据：~2 rows (大约)
DELETE FROM `user`;
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` (`ID`, `UserName`, `Password`, `DisplayName`, `Role`, `Approve`) VALUES
	(1, 'admin', '21232f297a57a5a743894a0e4a801fc3', '管理员', b'0011', b'1'),
	(2, 'wjl', 'e10adc3949ba59abbe56e057f20f883e', '汪建龙', b'0010', b'1');
/*!40000 ALTER TABLE `user` ENABLE KEYS */;

-- 导出  视图 fitment.item_project_position 结构
-- 移除临时表并创建最终视图结构
DROP TABLE IF EXISTS `item_project_position`;
CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`%` VIEW `item_project_position` AS SELECT 
fitmentitem.ID,
fitmentitem.ProjectID,
project.Title,
project.Name,
project.Price,
project.Unit,
fitmentitem.Number,
fitmentitem.NewOld,
fitmentitem.PositionID,
position.Name as positionName,
position.FitmentID,
position.Category
from (fitmentitem inner join position on fitmentitem.PositionID=position.ID) inner join project on fitmentitem.ProjectID=project.ID ;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
