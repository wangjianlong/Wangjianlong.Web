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
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

-- 正在导出表  fitment.city 的数据：~1 rows (大约)
DELETE FROM `city`;
/*!40000 ALTER TABLE `city` DISABLE KEYS */;
INSERT INTO `city` (`ID`, `Name`, `ParentID`) VALUES
	(1, '嵊州', 0);
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
) ENGINE=InnoDB AUTO_INCREMENT=498 DEFAULT CHARSET=utf8;

-- 正在导出表  fitment.project 的数据：~268 rows (大约)
DELETE FROM `project`;
/*!40000 ALTER TABLE `project` DISABLE KEYS */;
INSERT INTO `project` (`ID`, `Title`, `Name`, `Price`, `Unit`, `CityID`) VALUES
	(1, 'zbq62-sz', '座便器', 1200, '个', 1),
	(2, 'zbq11-sz', '座便器', 850, '个', 1),
	(3, 'zbq13-sz', '座便器', 2000, '个', 1),
	(4, 'zbq296-sz', '座便器', 300, '个', 1),
	(5, 'zbq534-sz', '座便器', 2300, '个', 1),
	(6, 'zbq-sz', '座便器', 650, '个', 1),
	(7, 'zbq2-sz', '座便器', 1700, '只', 1),
	(8, 'zdssm-sz', '自动伸缩门', 850, 'm', 1),
	(9, 'zsxt-sz', '装饰线条', 55, 'm', 1),
	(10, 'zsxt-sz262', '装饰线条', 22, 'm', 1),
	(11, 'zsdls-sz', '装饰大理石', 850, '㎡', 1),
	(12, 'zwq-sz', '砖围墙', 135, '㎡', 1),
	(13, 'zqt256-sz', '砖砌体', 220, 'm³', 1),
	(14, 'zqsc-sz15', '砖砌水池', 110, 'm³', 1),
	(15, 'zqsc-sz', '砖砌水池', 220, '立方', 1),
	(16, 'ztwl-sz', '铸铁围栏', 330, '㎡', 1),
	(17, 'ztwl112-sz', '铸铁围栏', 220, '㎡', 1),
	(18, 'ztgyfs20-sz', '铸铁工艺扶手', 550, 'm', 1),
	(19, 'ztgyfs202-sz', '铸铁工艺扶手', 550, 'm', 1),
	(20, 'ztfs8-sz', '铸铁扶手', 330, 'm', 1),
	(21, 'ztdm101-sz', '铸铁大门', 850, '㎡', 1),
	(22, 'ztdm8-sz', '铸铁大门', 850, '㎡', 1),
	(23, 'ztdm-sz', '铸铁大门', 550, '㎡', 1),
	(24, 'ztdm-sz163', '铸铁大门', 1200, '㎡', 1),
	(25, 'ztjx-sz', '竹踢脚线', 20, 'm', 1),
	(26, 'zdb-sz', '竹地板', 130, '㎡', 1),
	(27, 'zdb02-sz', '竹地板', 95, '㎡', 1),
	(28, 'zkblbc21-sz', '中空玻璃补差', 85, '㎡', 1),
	(29, 'zttpg-861-sz', '整体台盆柜', 4300, '组', 1),
	(30, 'zttpg', '整体台盆柜', 1200, '个', 1),
	(31, 'zttpg165-sz', '整体台盆柜', 3300, '个', 1),
	(32, 'zttpg21-sz', '整体台盆柜', 1400, '个', 1),
	(33, 'zttpg25-sz', '整体台盆柜', 300, '个', 1),
	(34, 'zttpg321-sz', '整体台盆柜', 3300, '个', 1),
	(35, 'zttpg3-sz', '整体台盆柜', 2300, '只', 1),
	(36, 'zttpg5-sz', '整体台盆柜', 1700, '套', 1),
	(37, 'zttp-sz', '整体台盆柜', 850, '套', 1),
	(38, 'ztsmlt-sz', '整体实木楼梯', 61800, '套', 1),
	(39, 'ztcg-sz', '整体橱柜', 950, 'm', 1),
	(40, 'ztcg4-sz', '整体橱柜', 1700, 'm', 1),
	(41, 'zsq-2', '真石漆', 130, '㎡', 1),
	(42, 'zsq112-sz', '真石漆', 65, '㎡', 1),
	(43, 'zyp-sz', '遮阳棚', 85, '㎡', 1),
	(44, 'zqc-sz', '沼气池', 2300, '个', 1),
	(45, 'zxhq-sz', '造型护墙', 170, '㎡', 1),
	(46, 'zx963-sz', '造型吊顶', 480, '㎡', 1),
	(47, 'zxdd11-sz', '造型吊顶', 380, '㎡', 1),
	(48, 'zxdd761-sz', '造型吊顶', 380, '㎡', 1),
	(49, 'zxdd8-sz', '造型吊顶', 330, '㎡', 1),
	(50, 'zxdd-sz', '造型吊顶', 260, '㎡', 1),
	(51, 'yzb-sz', '预制板', 130, '块', 1),
	(52, 'yg21-sz', '浴缸', 1200, '个', 1),
	(53, 'yg122-sz', '浴缸', 1200, '个', 1),
	(54, 'yg-sz', '浴缸', 330, '个', 1),
	(55, 'ypb-sz', '雨蓬板', 160, 'm', 1),
	(56, 'ymsng-sz', '有门水泥柜', 330, 'm', 1),
	(57, 'yq-sz', '油漆地面', 17, '㎡', 1),
	(58, 'yq11-sz', '油漆', 35, '㎡', 1),
	(59, 'YXSC-SZ', '隐形纱窗', 220, '㎡', 1),
	(60, 'ysgd-sz265', '艺术隔断', 330, '㎡', 1),
	(61, 'ysbl-sz', '艺术玻璃', 280, '㎡', 1),
	(62, 'ysbl486-sz', '艺术玻璃', 1400, '㎡', 1),
	(63, 'ysbl11-sz', '艺术玻璃', 510, '㎡', 1),
	(64, 'yg-sz2', '檐沟', 85, 'm', 1),
	(65, 'ysj-sz', '压水井/水泵', 400, '只', 1),
	(66, 'xcl62-sz', '宣传栏', 330, '㎡', 1),
	(67, 'xbq121-sz', '小便器', 550, '个', 1),
	(68, 'xbq101-sz', '小便器', 850, '个', 1),
	(69, 'xbq-sz', '小便器', 160, '个', 1),
	(70, 'xmm-sz', '橡木门', 550, '扇', 1),
	(71, 'xjxb-sz', '现浇小板', 85, 'm', 1),
	(72, 'xjsc-sz', '现浇水池', 270, '立方', 1),
	(73, 'xjz-sz', '现浇立柱', 3300, '根', 1),
	(74, 'xlp-sz', '洗脸盆/台盆', 130, '个', 1),
	(75, 'zdxlp-sz', '洗脸盆', 330, '只', 1),
	(76, 'gdxlp-sz', '洗脸盆', 850, '只', 1),
	(77, 'xlp123-sz', '洗脸盆', 220, '只', 1),
	(78, 'wmsng-sz', '无门水泥柜', 220, 'm', 1),
	(79, 'wnsng128-sz', '无门水泥柜', 330, 'm', 1),
	(80, 'wkblm-sz', '无框玻璃门', 550, '㎡', 1),
	(81, 'wkblm101-sz', '无框玻璃门', 850, '㎡', 1),
	(82, 'whs-sz', '文化石', 130, '㎡', 1),
	(83, 'wl-sz54', '围栏', 75, 'm', 1),
	(84, 'wjz-sz', '微晶砖', 430, '㎡', 1),
	(85, 'wqtl-sz', '外墙涂料', 35, '㎡', 1),
	(86, 'wqtl777-sz', '外墙涂料', 65, '㎡', 1),
	(87, 'wqtl5-sz', '外墙涂料', 35, '㎡', 1),
	(88, 'tbc-sz', '拖把池', 110, '只', 1),
	(89, 'tbc125-sz', '拖把池', 330, '个', 1),
	(90, 'tl157-sz', '涂料', 14, '㎡', 1),
	(91, 'tt-sz', '铜条', 12, 'm', 1),
	(92, 'tm1598-sz', '铜门', 23000, '扇', 1),
	(93, 'tm-sz', '铜门', 1700, '㎡', 1),
	(94, 'tzm-sz', '铁制门', 170, '㎡', 1),
	(95, 'tzlt8-sz', '铁制楼梯', 430, 'm', 1),
	(96, 'tzlsm-sz', '铁制拉栅门', 220, '㎡', 1),
	(97, 'tzfs-sz', '铁制扶手', 75, 'm', 1),
	(98, 'tzfdc-sz', '铁制防盗窗', 75, '㎡', 1),
	(99, 'tyfdc-sz262', '铁艺防盗窗', 130, '㎡', 1),
	(100, 'tyfdc9-sz', '铁艺防盗窗', 220, '㎡', 1),
	(101, 'tyfdc284-sz', '铁艺防盗窗', 330, '㎡', 1),
	(102, 'tdm45-sz', '铁大门', 330, '㎡', 1),
	(103, 'tdm-sz', '铁大门', 220, '㎡', 1),
	(104, 'tynyj-sz', '太阳能移机', 500, '只', 1),
	(105, 'shgy-sz', '碎花岗岩', 85, '㎡', 1),
	(106, 'snzwq-sz', '水泥砖围墙', 95, '㎡', 1),
	(107, 'snzwq2-sz', '水泥砖围墙', 120, '㎡', 1),
	(108, 'snxyb-sz', '水泥洗衣板', 330, '组', 1),
	(109, 'snhl-sz', '水泥护栏', 160, 'm', 1),
	(110, 'sndp11-sz', '水泥地坪', 65, '㎡', 1),
	(111, 'sndp-sz', '水泥地坪', 45, '㎡', 1),
	(112, 'sndl-sz', '水泥道路', 85, '㎡', 1),
	(113, 'mssc-sz', '水泥槽/磨石子水池', 85, '只', 1),
	(114, 'sms11-sz', '水磨石（拼花）', 85, '㎡', 1),
	(115, 'sms-sz', '水磨石', 55, '㎡', 1),
	(116, 'sj111-sz', '水井（大）', 2000, '座', 1),
	(117, 'sj145-sz', '水井', 1000, '座', 1),
	(118, 'sjz110-sz', '水晶柱', 280, 'm', 1),
	(119, 'sb-sz', '水泵', 400, '个', 1),
	(120, 'syz-sz', '双眼灶', 1700, '只', 1),
	(121, 'smmb-sz', '饰面门', 330, '扇', 1),
	(122, 'smm8-sz', '饰面门', 220, '扇', 1),
	(123, 'smmt-sz', '实木门套', 750, '个', 1),
	(124, 'smm584-sz', '实木门', 1400, '扇', 1),
	(125, 'smm123-sz', '实木门', 1700, '扇', 1),
	(126, 'smm-sz', '实木门', 850, '扇', 1),
	(127, 'smlt-sz', '实木楼梯地板', 850, '㎡', 1),
	(128, 'smfs123456-sz', '实木扶手', 550, 'm', 1),
	(129, 'smfs12345-sz', '实木扶手', 480, 'm', 1),
	(130, 'smfs-sz', '实木扶手', 380, 'm', 1),
	(131, 'smfs123-sz', '实木扶手', 850, 'm', 1),
	(132, 'smfs12-sz', '实木扶手', 650, 'm', 1),
	(133, 'smfs157-sz', '实木扶手', 1200, '㎡', 1),
	(134, 'smdb-sz', '实木地板', 190, '㎡', 1),
	(135, 'smdb258-sz', '实木地板', 450, '㎡', 1),
	(136, 'smdb2-sz', '实木地板', 300, '㎡', 1),
	(137, 'smdb3-sz', '实木地板', 410, '㎡', 1),
	(138, 'smdb5255-sz', '实木地板', 80, '㎡', 1),
	(139, 'smdb622-sz', '实木地板', 130, '㎡', 1),
	(140, 'smdb963-sz', '实木地板', 50, '㎡', 1),
	(141, 'smdbfs-sz', '实木带玻扶手', 480, 'm', 1),
	(142, 'smdm1-sz', '实木大门', 1700, '㎡', 1),
	(143, 'smdm-sz', '实木大门', 850, '㎡', 1),
	(144, 'smdm154-sz', '实木大门', 550, '㎡', 1),
	(145, 'smwyp-sz', '石棉瓦棚', 70, '㎡', 1),
	(146, 'sk-sz', '石坎', 270, '立方', 1),
	(147, 'sgxt-sz', '石膏线条/木线条', 17, 'm', 1),
	(148, 'sgxt-sz151', '石膏线条', 22, 'm', 1),
	(149, 'sghbx-sz', '石膏花边线条', 20, 'm', 1),
	(150, 'sgdd-sz25', '石膏吊顶', 65, '㎡', 1),
	(151, 'sgbdd-sz', '石膏板吊顶', 120, '㎡', 1),
	(152, 'scmt-sz', '石材门套', 550, 'm', 1),
	(153, 'sjlyj-sz', '升降晾衣架', 550, '套', 1),
	(154, 'sxg-sz', '上下柜', 330, 'm', 1),
	(155, 'smhq1-sz', '杉木护墙', 85, '㎡', 1),
	(156, 'smdd-sz25', '杉木吊顶', 55, '㎡', 1),
	(157, 'smdd265-sz', '杉木吊顶', 95, '㎡', 1),
	(158, 'smdd79-sz', '杉木吊顶', 135, '㎡', 1),
	(159, 'smdd-sz12458', '杉木吊顶', 65, '㎡', 1),
	(160, 'smdb1-sz', '杉木地板', 85, '㎡', 1),
	(161, 'rbbj-sz', '软包背景', 430, '㎡', 1),
	(162, 'rb136-sz', '软包', 40, '㎡', 1),
	(163, 'rb1-sz', '软包', 75, '㎡', 1),
	(164, 'rb265-sz', '软包', 20, '㎡', 1),
	(165, 'rjq74-sz', '乳胶漆', 35, '㎡', 1),
	(166, 'rjq123-sz', '乳胶漆', 40, '㎡', 1),
	(167, 'rjq2-sz', '乳胶漆', 14, '㎡', 1),
	(168, 'rjq-sz', '乳胶漆', 27, '㎡', 1),
	(169, 'rjq789-sz', '乳胶漆', 45, '㎡', 1),
	(170, 'rjq321-sz', '乳胶漆', 20, '㎡', 1),
	(171, 'rxddz-sz', '人行道地砖', 65, '㎡', 1),
	(172, 'rxdb62-sz', '人行道板', 95, '㎡', 1),
	(173, 'rxdb-sz', '人行道板', 65, '㎡', 1),
	(174, 'qzym1-sz', '轻质移门', 480, '㎡', 1),
	(175, 'qzym-sz1252', '轻质移门', 100, '㎡', 1),
	(176, 'qzym-sz', '轻质移门', 550, '㎡', 1),
	(177, 'qzym8-sz', '轻质移门', 850, '㎡', 1),
	(178, 'qzym2652-sz', '轻质移门', 150, '㎡', 1),
	(179, 'qzym-22', '轻质移门', 330, '㎡', 1),
	(180, 'qsb-sz', '青石板', 110, '㎡', 1),
	(181, 'qz112-sz', '墙纸', 40, '㎡', 1),
	(182, 'qz14-sz', '墙纸', 95, '㎡', 1),
	(183, 'qz-sz', '墙纸', 20, '㎡', 1),
	(184, 'qz17-sz', '墙纸', 130, '㎡', 1),
	(185, 'pmbj62-sz', '墙面背景', 220, '㎡', 1),
	(186, 'qb-sz', '墙布', 95, '㎡', 1),
	(187, 'qb753-sz', '墙布', 40, '㎡', 1),
	(188, 'qb235-sz', '墙布', 20, '㎡', 1),
	(189, 'qb123456-sz', '墙布', 220, '㎡', 1),
	(190, 'qhdb-sz', '强化地板', 70, '㎡', 1),
	(191, 'qhdb2-sz', '强化地板', 95, '㎡', 1),
	(192, 'pdd2525-sz', '平吊顶', 65, '㎡', 1),
	(193, 'pdd-sz26', '平吊顶', 95, '㎡', 1),
	(194, 'pdd-sz', '平吊顶', 120, '㎡', 1),
	(195, 'pdd-sz564', '平吊顶', 170, '㎡', 1),
	(196, 'pdd256-sz', '平吊顶', 160, '㎡', 1),
	(197, 'ppzbq000-sz', '品牌座便器', 3800, '个', 1),
	(198, 'ppzbq-sz', '品牌座便器', 3300, '只', 1),
	(199, 'ppzbq123-sz', '品牌座便器', 4300, '个', 1),
	(200, 'ps321-sz', '喷塑', 35, '㎡', 1),
	(201, 'pmb-sz158', '泡沫板', 12, '㎡', 1),
	(202, 'pgztjx-sz', '抛光砖踢脚线', 14, 'm', 1),
	(203, 'pgztjx12-sz', '抛光砖踢脚线', 20, 'm', 1),
	(204, 'pgztjx11-sz', '抛光砖踢脚线', 27, 'm', 1),
	(205, 'pgz1-sz', '抛光砖80*80', 130, '㎡', 1),
	(206, 'pgz-sz', '抛光砖100*100', 190, '㎡', 1),
	(207, 'pgz(ph)154-sz', '抛光砖（拼花）', 350, '㎡', 1),
	(208, 'pgzd-sz', '抛光砖', 110, '㎡', 1),
	(209, 'pgz123-sz', '抛光砖', 240, '㎡', 1),
	(210, 'pgz156-sz', '抛光砖', 130, '㎡', 1),
	(211, 'pgz354-sz', '抛光砖', 190, '㎡', 1),
	(212, 'pgz274-sz', '抛光砖', 160, '㎡', 1),
	(213, 'mqblbc5-sz', '幕墙玻璃补差', 380, '㎡', 1),
	(214, 'mqblbc-sz', '幕墙玻璃补差', 380, '㎡', 1),
	(215, 'mqblbc9-sz', '幕墙玻璃补差', 160, '㎡', 1),
	(216, 'mzl-sz', '木栅栏', 110, 'm', 1),
	(217, 'mxt14-sz', '木线条', 35, 'm', 1),
	(218, 'mxt123-sz', '木线条', 22, 'm', 1),
	(219, 'mtjx241-sz', '木踢脚线', 27, 'm', 1),
	(220, 'mtjx2-sz', '木踢脚线', 20, 'm', 1),
	(221, 'mtjx18-sz', '木踢脚线', 17, 'm', 1),
	(222, 'mtjx-sz', '木踢脚线', 10, 'm', 1),
	(223, 'msmc-sz', '木纱门窗', 110, '扇', 1),
	(224, 'mmt110-sz', '木门套（线条）', 160, '个', 1),
	(225, 'mmt001-sz', '木门套（包边）', 220, '个', 1),
	(226, 'mmct914-sz', '木门窗套', 600, '个', 1),
	(227, 'mhq-sz', '木护墙', 65, '㎡', 1),
	(228, 'mhq8-sz', '木护墙', 240, '㎡', 1),
	(229, 'mhq8520-sz', '木护墙', 170, '㎡', 1),
	(230, 'mhq2-sz', '木护墙', 130, '㎡', 1),
	(231, 'mgd-sz25', '木隔断', 45, '㎡', 1),
	(232, 'mgd2-sz', '木隔断', 95, '㎡', 1),
	(233, 'mgd-sz', '木隔断', 170, '㎡', 1),
	(234, 'mgl11-sz', '木阁楼', 220, '㎡', 1),
	(235, 'mgl-sz', '木阁楼', 280, '㎡', 1),
	(236, 'mgb12-sz', '木搁板', 220, '㎡', 1),
	(237, 'mfs-sz', '木扶手', 110, 'm', 1),
	(238, 'mfs52-sz', '木扶手', 330, 'm', 1),
	(239, 'mfs456-sz', '木扶手', 240, 'm', 1),
	(240, 'mfs11111-sz', '木扶手', 160, 'm', 1),
	(241, 'mfs101-sz', '木扶手', 300, 'm', 1),
	(242, 'mdd-sz', '木吊顶', 120, '㎡', 1),
	(243, 'mdm-sz', '木大门', 330, '㎡', 1),
	(244, 'mgs-sz', '蘑菇石', 130, '㎡', 1),
	(245, 'mgs101-sz', '蘑菇石', 190, '㎡', 1),
	(246, 'mjqhxt-sz', '描金嵌花线条', 480, 'm', 1),
	(247, 'mz1234-sz', '面砖（含线条）', 130, '㎡', 1),
	(248, 'mz963-sz', '面砖', 330, '㎡', 1),
	(249, 'mz789-sz', '面砖', 130, '㎡', 1),
	(250, 'mz11-sz', '面砖', 95, '㎡', 1),
	(251, 'mz564-sz', '面砖', 110, '㎡', 1),
	(252, 'mz456-sz', '面砖', 190, '㎡', 1),
	(253, 'mz-sz', '面砖', 65, '㎡', 1),
	(254, 'mz1478-sz', '门柱', 650, '个', 1),
	(255, 'mz1231-sz', '门柱', 1200, '个', 1),
	(256, 'mz1354-sz', '门柱', 1700, '个', 1),
	(257, 'mt5641-sz', '门套', 480, '个', 1),
	(258, 'md3-sz', '门斗', 330, '㎡', 1),
	(259, 'md-sz', '门斗', 1000, '㎡', 1),
	(260, 'md2-sz', '门斗', 1500, '㎡', 1),
	(261, 'mk12-sz', '茅坑', 330, '个', 1),
	(262, 'mjj-sz', '毛巾架', 50, '个', 1),
	(263, 'msk-sz', '马赛克', 40, '㎡', 1),
	(264, 'lmzhl-sz', '罗马柱护栏', 160, 'm', 1),
	(265, 'lmzfl23-sz', '罗马柱护栏', 240, 'm', 1),
	(266, 'lmct-sz', '罗马窗套', 330, '个', 1),
	(267, 'lshq12-sz', '铝塑护墙', 65, '㎡', 1),
	(268, 'lshq-sz', '铝塑护墙', 110, '㎡', 1),
	(269, 'lsdd158-sz', '铝塑吊顶', 65, '㎡', 1),
	(270, 'lsdd-sz48', '铝塑吊顶', 120, '㎡', 1),
	(271, 'lsdd-sz', '铝塑吊顶', 170, '㎡', 1),
	(272, 'lsb-sz25', '铝塑板', 65, '㎡', 1),
	(273, 'lsatdd-sz', '铝塑凹凸吊顶', 95, '㎡', 1),
	(274, 'lkbdd11-sz', '铝扣板吊顶', 130, '㎡', 1),
	(275, 'lkb230-sz', '铝扣板', 85, '㎡', 1),
	(276, 'lkb741-sz', '铝扣板', 130, '㎡', 1),
	(277, 'lkb842-sz', '铝扣板', 240, '㎡', 1),
	(278, 'lkb8-sz', '铝扣板', 65, '㎡', 1),
	(279, 'lkb963-sz', '铝扣板', 160, '㎡', 1),
	(280, 'lkb-sz', '铝扣板', 95, '㎡', 1),
	(281, 'lhjsm-sz', '铝合金纱门', 130, '㎡', 1),
	(282, 'lhjsm-sz22', '铝合金纱门', 330, '㎡', 1),
	(283, 'lhjsc-sz', '铝合金纱窗', 130, '㎡', 1),
	(284, 'lhjmt-sz', '铝合金门套', 380, '个', 1),
	(285, 'lhjm565-sz', '铝合金门', 160, '㎡', 1),
	(286, 'lhjm14-sz', '铝合金门', 380, '㎡', 1),
	(287, 'lhjm26-sz', '铝合金门', 110, '㎡', 1),
	(288, 'lhjgd-sz', '铝合金隔断', 220, '㎡', 1),
	(289, 'lhjthm', '铝合金弹簧门', 330, '㎡', 1),
	(290, 'lhjdm963-sz', '铝合金大门', 1400, '㎡', 1),
	(291, 'lhjdm951-sz', '铝合金大门', 430, '㎡', 1),
	(292, 'lhjdm-sz', '铝合金大门', 550, '㎡', 1),
	(293, 'lhjdm158-sz', '铝合金大门', 850, '㎡', 1),
	(294, 'lhjsgm-sz', '铝合金、塑钢门', 260, '㎡', 1),
	(295, 'lhjsgc-sz', '铝合金、塑钢窗', 260, '㎡', 1),
	(296, 'llw54-sz', '琉璃瓦', 95, '㎡', 1),
	(297, 'lygd1-sz', '淋浴隔断', 750, '㎡', 1),
	(298, 'lygd-sz', '淋浴隔断', 260, '㎡', 1),
	(299, 'lyf95-sz', '淋浴房', 300, '个', 1),
	(300, 'lyf8-sz', '淋浴房', 3300, '个', 1),
	(301, 'lyf12-sz', '淋浴房', 4100, '个', 1),
	(302, 'lyf123-sz', '淋浴房', 4800, '个', 1),
	(303, 'lyf-sz', '淋浴房', 2300, '座', 1),
	(304, 'lqw', '沥青瓦', 95, '㎡', 1),
	(305, 'lsxlp1-sz', '立式洗脸盆', 280, '个', 1),
	(306, 'lsxlp-sz', '立式台盆', 220, '个', 1),
	(307, 'lstp553-sz', '立式台盆', 330, '个', 1),
	(308, 'kddhyj-sz', '宽带/电话移机', 216, '只', 1),
	(309, 'kqnyj352-sz', '空气能移机', 500, '个', 1),
	(310, 'ktyj-sz', '空调移机', 400, '只', 1),
	(311, 'ktls-sz', '空调移机', 600, '只', 1),
	(312, 'jzmbc-sz', '卷闸门补差', 85, '㎡', 1),
	(313, 'jzm-sz', '卷闸门', 140, '㎡', 1),
	(314, 'jmmt-sz', '榉木门套/窗套', 380, '个', 1),
	(315, 'jz-sz', '镜子', 85, '块', 1),
	(316, 'jtyp-sz', '角铁雨棚', 110, '㎡', 1),
	(317, 'jy-sz', '简易', 220, '㎡', 1),
	(318, 'jkyj1-sz', '监控移机', 150, '个', 1),
	(319, 'jk87-sz', '监控', 300, '个', 1),
	(320, 'js-sz', '假山', 550, '组', 1),
	(321, 'js1-sz', '假山', 330, '组', 1),
	(322, 'js896-sz', '假山', 850, '座', 1),
	(323, 'js321-sz', '假山', 4300, '座', 1),
	(324, 'jkc265-sz', '架空层', 579, '㎡', 1),
	(325, 'jkc032-sz', '架空层', 339, '㎡', 1),
	(326, 'jkc-sz215', '架空层', 0, '㎡', 1),
	(327, 'jk4812-sz', '架空', 0, '㎡', 1),
	(328, 'jk125-sz', '架空', 755, '㎡', 1),
	(329, 'jk-sz', '架空', 688, '㎡', 1),
	(330, 'jk1-sz', '架空', 703, '㎡', 1),
	(331, 'hfc-sz', '化粪池', 2300, '只', 1),
	(332, 'hgyyd-sz', '花岗岩圆墩', 550, '个', 1),
	(333, 'hgyytsxyb9-sz', '花岗岩一体式洗衣板', 1400, '组', 1),
	(334, 'hgyytsxyb-sz', '花岗岩一体式洗衣板', 850, '组', 1),
	(335, 'hgyytbctbmkb25-sz', '花岗岩阳台板/窗台板/门口板', 130, 'm', 1),
	(336, 'hgyytbct-sz', '花岗岩阳台板/窗台板/门口板', 85, 'm', 1),
	(337, 'hgyytb-sz', '花岗岩阳台板/窗台板/门口板', 55, 'm', 1),
	(338, 'hgyctbmkb-sz', '花岗岩阳台板/窗台板', 130, 'm', 1),
	(339, 'hgyxt-sz', '花岗岩线条', 550, 'm', 1),
	(340, 'hgyxt101-sz', '花岗岩线条', 330, 'm', 1),
	(341, 'hgytj-sz', '花岗岩踢脚线', 35, 'm', 1),
	(342, 'hgytbl-sz', '花岗岩台板', 350, 'm', 1),
	(343, 'hgytb-sz', '花岗岩台板', 130, 'm', 1),
	(344, 'hgytb11-sz', '花岗岩台板', 240, 'm', 1),
	(345, 'hgytb265-sz', '花岗岩台板', 85, 'm', 1),
	(346, 'hgytb895-sz', '花岗岩台板', 550, 'm', 1),
	(347, 'hgytb25-sz', '花岗岩台板', 55, 'm', 1),
	(348, 'hgymt101-sz', '花岗岩门套', 550, 'm', 1),
	(349, 'hgylmzhl-sz', '花岗岩罗马柱护栏', 430, 'm', 1),
	(350, 'hgylmzhl8-sz', '花岗岩罗马柱护栏', 550, 'm', 1),
	(351, 'hgyhl1-sz', '花岗岩护栏', 550, 'm', 1),
	(352, 'hgyct101-sz', '花岗岩窗套', 330, 'm', 1),
	(353, 'hgycs-sz', '花岗岩侧石', 130, 'm', 1),
	(354, 'hgybz-sz', '花岗岩包柱', 650, '㎡', 1),
	(355, 'hgybfz-sz', '花岗岩包方柱', 480, '㎡', 1),
	(356, 'hgy8954-sz', '花岗岩', 190, '㎡', 1),
	(357, 'hgy11111-sz', '花岗岩', 170, '㎡', 1),
	(358, 'hgy155-sz', '花岗岩', 280, '㎡', 1),
	(359, 'hgy256-sz', '花岗岩', 480, '㎡', 1),
	(360, 'hgy789-sz', '花岗岩', 160, '㎡', 1),
	(361, 'hgydm2-sz', '花岗岩', 350, '㎡', 1),
	(362, 'hgydm3-sz', '花岗岩', 240, '㎡', 1),
	(363, 'hgydm-sz', '花岗岩', 130, '㎡', 1),
	(364, 'hq-sz25', '护墙', 65, '㎡', 1),
	(365, 'hhtm-sz', '豪华铜门', 3300, '㎡', 1),
	(366, 'hbyhl-sz', '汉白玉护栏', 950, 'm', 1),
	(367, 'hbyhl215-sz', '汉白玉护栏', 430, 'm', 1),
	(368, 'hbyhl7-sz', '汉白玉护栏', 750, 'm', 1),
	(369, 'ggz1-sz', '广告字', 110, '个', 1),
	(370, 'ggz-sz', '广告字', 220, '个', 1),
	(371, 'ggp-sz', '广告牌', 130, '㎡', 1),
	(372, 'ggp8-sz', '广告牌', 160, '㎡', 1),
	(373, 'ggb-sz', '广告布', 110, '㎡', 1),
	(374, 'gdg(j)-sz', '固定柜（酒）', 850, '㎡', 1),
	(375, 'gdg123-sz', '固定柜', 430, '㎡', 1),
	(376, 'gdg852-sz', '固定柜', 140, '㎡', 1),
	(377, 'gdg17-sz', '固定柜', 550, '㎡', 1),
	(378, 'gdg265-sz', '固定柜', 650, '㎡', 1),
	(379, 'gdg32-sz', '固定柜', 200, '㎡', 1),
	(380, 'gdg8-sz', '固定柜', 330, '㎡', 1),
	(381, 'gdg3-sz', '固定柜', 80, '㎡', 1),
	(382, 'gdg-sz', '固定柜', 380, '㎡', 1),
	(383, 'gdbl-sz', '固定玻璃', 170, '㎡', 1),
	(384, 'gzw_sz', '构筑物', 0, '㎡', 1),
	(385, 'gq-sz22', '沟渠', 190, 'm', 1),
	(386, 'gyfs-sz', '工艺扶手', 380, 'm', 1),
	(387, 'gdztcg-sz', '高档整体橱柜', 2300, 'm', 1),
	(388, 'gdzxdd-sz', '高档造型吊顶', 600, '㎡', 1),
	(389, 'gddls-sz', '高档大理石', 430, '㎡', 1),
	(390, 'gt-sz', '钢砼', 720, '立方米', 1),
	(391, 'ghbltp-sz', '钢化玻璃台盆', 850, '个', 1),
	(392, 'ghblp-sz', '钢化玻璃棚', 330, '㎡', 1),
	(393, 'ghbl-sz', '钢化玻璃', 170, '㎡', 1),
	(394, 'gbdm110-sz', '钢板大门', 850, '㎡', 1),
	(395, 'gbdm-sz', '钢板大门', 550, '㎡', 1),
	(396, 'gz-sz', '缸砖', 55, '㎡', 1),
	(397, 'gyzbq-sz', '感应座便器', 4800, '个', 1),
	(398, 'ftm23-sz', '仿铜门', 1700, '㎡', 1),
	(399, 'ftm-sz', '仿铜门', 1200, '㎡', 1),
	(400, 'fwdjjc-sz', '房屋地基基础', 32677, '座', 1),
	(401, 'fdw-sz', '防盗网', 55, '㎡', 1),
	(402, 'fdm-sz', '防盗门', 650, '扇', 1),
	(403, 'fdm9-sz', '防盗门', 1400, '扇', 1),
	(404, 'fdm11-sz', '防盗门', 950, '扇', 1),
	(405, 'fdm123-sz', '防盗门', 850, '扇', 1),
	(406, 'fdm52-sz', '防盗门', 430, '扇', 1),
	(407, 'fdm78-sz', '防盗门', 2000, '扇', 1),
	(408, 'fdm96-sz', '防盗门', 3300, '扇', 1),
	(409, 'elsdm-sz', '鹅卵石地面', 35, '㎡', 1),
	(410, 'ersdm62-sz', '鹅卵石地面', 65, '㎡', 1),
	(411, 'dk-sz', '蹲坑', 110, '个', 1),
	(412, 'dsg-sz', '电视柜', 480, '只', 1),
	(413, 'ddzz-sz', '电动装置', 1000, '个', 1),
	(414, 'dz852-sz', '地砖', 240, '㎡', 1),
	(415, 'dz14-sz', '地砖', 95, '㎡', 1),
	(416, 'dz-sz', '地砖', 65, '㎡', 1),
	(417, 'dz123-sz', '地砖', 190, '㎡', 1),
	(418, 'dz8-sz', '地砖', 130, '㎡', 1),
	(419, 'dt-sz', '地台', 380, '㎡', 1),
	(420, 'dx-sz', '灯箱', 220, '㎡', 1),
	(421, 'dst1-sz', '挡水条', 160, 'm', 1),
	(422, 'txtl101-sz', '弹性涂料', 45, '㎡', 1),
	(423, 'dyz-sz', '单眼灶', 1200, '只', 1),
	(424, 'dmhgy123-sz', '大门花岗岩', 430, '㎡', 1),
	(425, 'dmdls2-sz', '大门花岗岩', 410, '㎡', 1),
	(426, 'dmdls3-sz', '大门花岗岩', 480, '㎡', 1),
	(427, 'dmdls-sz', '大门花岗岩', 220, '㎡', 1),
	(428, 'dlsyq-sz', '大理石圆球', 2300, '个', 1),
	(429, 'dlsxt-sz', '大理石线条', 480, 'm', 1),
	(430, 'dlsxt456-sz', '大理石线条', 380, 'm', 1),
	(431, 'dlstjx-sz', '大理石踢脚线', 150, 'm', 1),
	(432, 'dlslmzhl-sz', '大理石罗马柱护栏', 750, 'm', 1),
	(433, 'dlshl-sz', '大理石护栏', 950, '米', 1),
	(434, 'dlsbj-sz', '大理石背景', 550, '㎡', 1),
	(435, 'dls(ssh)-sz', '大理石（山水画）', 40000, '块', 1),
	(436, 'dls-sz', '大理石', 550, '㎡', 1),
	(437, 'dls-sz256', '大理石', 350, '㎡', 1),
	(438, 'csc-sz', '瓷水槽/池', 110, '个', 1),
	(439, 'clx-sz', '窗帘箱', 95, 'm', 1),
	(440, 'clg1-sz', '窗帘杆', 75, 'm\nm', 1),
	(441, 'clg-sz', '窗帘杆', 50, 'm', 1),
	(442, 'cg98-sz', '橱柜', 200, 'm', 1),
	(443, 'cg-sz', '橱柜', 650, 'm', 1),
	(444, 'ckjzm-sz', '车库卷闸门', 330, '㎡', 1),
	(445, 'cs-sz15', '侧石', 65, 'm', 1),
	(446, 'cs-sz', '侧石', 45, 'm', 1),
	(447, 'cgwwh-sz', '彩钢瓦围护', 85, '㎡', 1),
	(448, 'cgwds85-sz', '彩钢瓦滴水', 95, 'm', 1),
	(449, 'cgw215-sz', '彩钢瓦', 95, '㎡', 1),
	(450, 'bxgwl85-sz', '不锈钢围栏', 220, '㎡', 1),
	(451, 'bxgwl-sz', '不锈钢围栏', 160, '㎡', 1),
	(452, 'bxgtjx-sz', '不锈钢踢脚线', 20, 'm', 1),
	(453, 'bxgtb-sz', '不锈钢台板', 330, 'm', 1),
	(454, 'nxgsmdm-sz', '不锈钢双面大门', 850, '㎡', 1),
	(455, 'bxgsmdm-sz', '不锈钢双面大门', 850, '㎡', 1),
	(456, 'bxgsc-sz', '不锈钢双槽', 550, '个', 1),
	(457, 'bxglt-sz', '不锈钢楼梯', 550, 'm', 1),
	(458, 'bxglyj-sz', '不锈钢晾衣架/杆', 55, 'm', 1),
	(459, 'bxglsm1-sz', '不锈钢拉栅门', 330, '㎡', 1),
	(460, 'bxglsm-sz', '不锈钢拉栅门', 240, '㎡', 1),
	(461, 'bxghj-sz', '不锈钢花架', 160, 'm', 1),
	(462, 'bxggyfl-sz', '不锈钢工艺扶栏', 480, '㎡', 1),
	(463, 'bxgfs1-sz', '不锈钢扶手', 160, 'm', 1),
	(464, 'bxgfs-sz', '不锈钢扶手', 130, 'm', 1),
	(465, 'bxgfs-sz215', '不锈钢扶手', 280, 'm', 1),
	(466, 'bxgfs-sz25', '不锈钢扶手', 65, 'm', 1),
	(467, 'bxgfdm110-sz', '不锈钢防盗门', 330, '㎡', 1),
	(468, 'bxgfdc5-sz', '不锈钢防盗窗', 130, '㎡', 1),
	(469, 'bxgfdc4-sz', '不锈钢防盗窗', 55, '㎡', 1),
	(470, 'bxgfdc-sz', '不锈钢防盗窗', 95, '㎡', 1),
	(471, 'bxgdc-sz', '不锈钢单槽', 280, '个', 1),
	(472, 'bxgdm-sz', '不锈钢大门', 550, '㎡', 1),
	(473, 'bxgcst-sz', '不锈钢储水桶', 1200, '只', 1),
	(474, 'bxgcg-sz', '不锈钢橱柜', 650, 'm', 1),
	(475, 'bxgblq-sz', '不锈钢避雷球', 3300, '套', 1),
	(476, 'bxgbb-sz26', '不锈钢包边', 160, '㎡', 1),
	(477, 'bxgbb-sz', '不锈钢包边', 330, '㎡', 1),
	(478, 'bldm-sz', '玻璃地面', 130, '㎡', 1),
	(479, 'blss-sz', '避雷设施', 2000, '套', 1),
	(480, 'bjzsxt-sz', '背景装饰线条', 110, 'm', 1),
	(481, 'bjq654-sz', '背景墙', 750, '㎡', 1),
	(482, 'bjq887-sz', '背景墙', 220, '㎡', 1),
	(483, 'bjq8-sz', '背景墙', 550, '㎡', 1),
	(484, 'bjq321-sz', '背景墙', 80, '㎡', 1),
	(485, 'bjq-sz', '背景墙', 380, '㎡', 1),
	(486, 'bylm_sz', '柏油路面', 130, '㎡', 1),
	(487, 'atdd256-sz', '凹凸吊顶', 120, '㎡', 1),
	(488, 'atdd-sz522', '凹凸吊顶', 65, '㎡', 1),
	(489, 'atdd-sz', '凹凸吊顶', 170, '㎡', 1),
	(490, 'sbsfsjc-sz', 'SBS防水卷材', 45, '㎡', 1),
	(491, 'pvchq-sz', 'pvc护墙', 65, '㎡', 1),
	(492, 'pvchq-sz25', 'pvc护墙', 35, '㎡', 1),
	(493, 'pvcdd12-sz', 'PVC吊顶', 35, '㎡', 1),
	(494, 'pvcdd_sz', 'PVC吊顶', 95, '㎡', 1),
	(495, 'pvcdd-sz', 'PVC吊顶', 65, '㎡', 1),
	(496, 'pvc897-sz', 'PVC吊顶', 130, '个', 1),
	(497, 'LEDgg-sz', 'LED广告', 430, 'm', 1);
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
