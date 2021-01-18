CREATE DATABASE  IF NOT EXISTS `arsenal` /*!40100 DEFAULT CHARACTER SET utf8 */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `arsenal`;
-- MySQL dump 10.13  Distrib 8.0.19, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: arsenal
-- ------------------------------------------------------
-- Server version	8.0.19

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `delivery`
--

DROP TABLE IF EXISTS `delivery`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `delivery` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `Date` date NOT NULL,
  `PN` int NOT NULL,
  `Storage ID` int NOT NULL,
  `Item ID` int NOT NULL,
  PRIMARY KEY (`ID`,`PN`,`Storage ID`,`Item ID`),
  KEY `fk_delivery_loaders1_idx` (`PN`),
  KEY `fk_delivery_storage1_idx` (`Storage ID`,`Item ID`),
  CONSTRAINT `fk_delivery_loaders1` FOREIGN KEY (`PN`) REFERENCES `loader` (`PN`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_delivery_storage1` FOREIGN KEY (`Storage ID`, `Item ID`) REFERENCES `storage` (`ID`, `Item ID`)
) ENGINE=InnoDB AUTO_INCREMENT=23 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `delivery`
--

LOCK TABLES `delivery` WRITE;
/*!40000 ALTER TABLE `delivery` DISABLE KEYS */;
INSERT INTO `delivery` VALUES (1,'2020-02-01',1,1,1),(2,'2020-05-05',3,2,2),(3,'2020-06-05',2,3,2),(4,'2020-11-15',3,4,4),(5,'2020-11-15',3,5,2),(6,'2020-11-16',3,6,3),(7,'2020-11-16',19,7,1),(8,'2020-11-16',2,8,4),(9,'2020-11-16',2,9,1),(10,'2020-11-16',3,10,4),(11,'2020-11-16',2,11,4),(12,'2020-11-16',3,12,4),(13,'2020-11-16',1,13,4),(14,'2020-11-16',3,14,3),(15,'2020-11-16',2,15,4),(16,'2020-11-16',1,16,1),(17,'2020-11-16',1,17,1),(18,'2020-11-16',3,18,1),(19,'2020-11-16',1,19,1),(20,'2020-12-24',3,20,5),(21,'2020-12-24',2,21,5),(22,'2020-12-24',1,22,5);
/*!40000 ALTER TABLE `delivery` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `history`
--

DROP TABLE IF EXISTS `history`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `history` (
  `Entrance Num` int NOT NULL,
  `ActionID` int NOT NULL,
  `PN` int NOT NULL,
  `Date` datetime NOT NULL,
  `Actions` varchar(200) DEFAULT NULL,
  PRIMARY KEY (`Entrance Num`,`ActionID`),
  KEY `fk_history_operators1_idx` (`PN`),
  CONSTRAINT `fk_history_operators1` FOREIGN KEY (`PN`) REFERENCES `operator` (`PN`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `history`
--

LOCK TABLES `history` WRITE;
/*!40000 ALTER TABLE `history` DISABLE KEYS */;
INSERT INTO `history` VALUES (2,1,1,'2020-11-15 18:34:22','Вход в систему'),(3,1,9,'2020-11-15 18:36:31','Вход в систему'),(4,1,9,'2020-11-15 18:39:06','Вход в систему'),(4,2,9,'2020-11-15 18:41:07','Добавлен новый оператор Андрей Авдонов Олегович'),(5,1,10,'2020-11-15 18:47:29','Вход в систему'),(6,1,9,'2020-11-15 18:58:53','Вход в систему'),(6,2,9,'2020-11-15 19:01:47','Добавлено примечание: Надо работать.....'),(7,1,1,'2020-11-15 20:07:43','Вход в систему'),(8,1,1,'2020-11-15 20:11:07','Вход в систему'),(8,2,1,'2020-11-15 20:12:03','Добавлен новый грузчик Дима Петров '),(8,3,1,'2020-11-15 20:12:39','Уволен грузчик Дима Петров '),(9,1,1,'2020-11-15 20:16:36','Вход в систему'),(9,2,1,'2020-11-15 20:16:45','Уволен грузчик Алексей \"Алёша\" Русин'),(10,1,1,'2020-11-15 20:20:47','Вход в систему'),(10,2,1,'2020-11-15 20:21:20','Уволен грузчик Алексей \"Алёша\" Русин'),(11,1,1,'2020-11-15 21:27:33','Вход в систему'),(12,1,1,'2020-11-15 21:30:29','Вход в систему'),(13,1,9,'2020-11-15 21:50:53','Вход в систему'),(14,1,1,'2020-11-15 22:03:39','Вход в систему'),(14,2,1,'2020-11-15 22:04:55','Добавлен предмет: DM53'),(15,1,9,'2020-11-15 22:07:01','Вход в систему'),(15,2,9,'2020-11-15 22:07:19','Добавлено примечание: Ух-ты'),(16,1,9,'2020-11-15 22:34:11','Вход в систему'),(17,1,1,'2020-11-15 22:36:50','Вход в систему'),(18,1,1,'2020-11-15 22:39:05','Вход в систему'),(19,1,1,'2020-11-15 22:49:04','Вход в систему'),(20,1,1,'2020-11-15 22:54:00','Вход в систему'),(21,1,1,'2020-11-15 22:56:01','Вход в систему'),(22,1,9,'2020-11-15 22:58:07','Вход в систему'),(23,1,9,'2020-11-15 23:05:58','Вход в систему'),(24,1,1,'2020-11-15 23:08:30','Вход в систему'),(25,1,9,'2020-11-15 23:36:53','Вход в систему'),(25,2,9,'2020-11-15 23:38:38','Добавлен новый оператор Григорий Мишин Александрович'),(26,1,11,'2020-11-15 23:41:14','Вход в систему'),(27,1,11,'2020-11-15 23:42:20','Вход в систему'),(27,2,11,'2020-11-15 23:42:43','Самоувольнение'),(28,1,9,'2020-11-16 00:16:18','Вход в систему'),(28,2,9,'2020-11-16 00:17:03','Оформлена новая поставка DM53 10'),(29,1,9,'2020-11-16 00:30:14','Вход в систему'),(30,1,1,'2020-11-16 00:31:09','Вход в систему'),(31,1,9,'2020-11-16 00:32:58','Вход в систему'),(32,1,9,'2020-11-16 00:33:56','Вход в систему'),(33,1,9,'2020-11-16 00:37:30','Вход в систему'),(33,2,9,'2020-11-16 00:37:50','Оформлена новая поставка DM53 100 шт.'),(33,3,9,'2020-11-16 00:38:20','Оформлена новая поставка 7.62 1000 шт.'),(33,4,9,'2020-11-16 00:39:11','Оформлена новая поставка РПГ-0 10 шт.'),(34,1,1,'2020-11-16 00:51:45','Вход в систему'),(35,1,1,'2020-11-16 00:54:21','Вход в систему'),(36,1,9,'2020-11-16 01:04:51','Вход в систему'),(36,2,9,'2020-11-16 01:05:27','Добавлено примечание: Я проснулся!'),(36,3,9,'2020-11-16 01:06:20','Добавлен новый оператор Паша Пантелеев 123'),(36,4,9,'2020-11-16 01:07:18','Уволен оператор Паша Пантелеев 123'),(36,5,9,'2020-11-16 01:08:42','Добавлен новый грузчик Александр Кузьмин '),(36,6,9,'2020-11-16 01:09:44','Оформлена новая поставка Калаш 1000 шт.'),(37,1,1,'2020-11-16 01:53:08','Вход в систему'),(37,2,1,'2020-11-16 01:53:42','Уволен грузчик Александр Кузьмин '),(38,1,1,'2020-11-16 01:59:55','Вход в систему'),(38,2,1,'2020-11-16 02:00:59','Добавлен новый грузчик Человек Работающий '),(38,3,1,'2020-11-16 02:01:13','Уволен грузчик Человек Работающий '),(39,1,1,'2020-11-16 02:10:59','Вход в систему'),(39,2,1,'2020-11-16 02:11:39','Добавлен новый грузчик Нечеловеческий Демонический Работник'),(39,3,1,'2020-11-16 02:11:59','Уволен грузчик Нечеловеческий Демонический Работник'),(40,1,1,'2020-11-16 02:17:53','Вход в систему'),(41,1,1,'2020-11-16 02:21:44','Вход в систему'),(42,1,1,'2020-11-16 02:24:13','Вход в систему'),(42,2,1,'2020-11-16 02:24:27','Оформлена выгрузка Калаш 1000 шт.'),(43,1,1,'2020-11-16 02:25:21','Вход в систему'),(44,1,1,'2020-11-16 02:28:20','Вход в систему'),(44,2,1,'2020-11-16 02:28:50','Оформлена выгрузка 7.62 240 шт.'),(45,1,1,'2020-11-16 02:31:27','Вход в систему'),(45,2,1,'2020-11-16 02:31:38','Оформлена новая поставка DM53 30 шт.'),(45,3,1,'2020-11-16 02:32:04','Оформлена выгрузка DM53 30 шт.'),(46,1,1,'2020-11-16 02:34:54','Вход в систему'),(46,2,1,'2020-11-16 02:35:05','Оформлена новая поставка Калаш 123 шт.'),(47,1,1,'2020-11-16 02:36:45','Вход в систему'),(48,1,1,'2020-11-16 02:41:24','Вход в систему'),(48,2,1,'2020-11-16 02:41:33','Оформлена новая поставка DM53 123 шт.'),(48,3,1,'2020-11-16 02:41:39','Оформлена выгрузка DM53 123 шт.'),(49,1,1,'2020-11-16 02:46:38','Вход в систему'),(49,2,1,'2020-11-16 02:46:46','Оформлена новая поставка DM53 132 шт.'),(49,3,1,'2020-11-16 02:46:49','Оформлена выгрузка DM53 132 шт.'),(50,1,1,'2020-11-16 02:51:42','Вход в систему'),(50,2,1,'2020-11-16 02:51:51','Оформлена новая поставка DM53 444 шт.'),(50,3,1,'2020-11-16 02:51:55','Оформлена выгрузка DM53 444 шт.'),(51,1,1,'2020-11-16 02:54:42','Вход в систему'),(51,2,1,'2020-11-16 02:54:50','Оформлена новая поставка DM53 1 шт.'),(51,3,1,'2020-11-16 02:54:54','Оформлена выгрузка DM53 1 шт.'),(52,1,1,'2020-11-16 03:38:53','Вход в систему'),(53,1,1,'2020-11-16 03:41:07','Вход в систему'),(54,1,1,'2020-11-16 03:48:44','Вход в систему'),(55,1,1,'2020-11-16 03:57:59','Вход в систему'),(56,1,1,'2020-11-16 04:10:32','Вход в систему'),(56,2,1,'2020-11-16 04:10:51','Оформлена новая поставка РПГ-0 1 шт.'),(56,3,1,'2020-11-16 04:10:58','Оформлена выгрузка РПГ-0 1 шт.'),(56,4,1,'2020-11-16 04:11:11','Добавлен новый грузчик 123 123 132'),(56,5,1,'2020-11-16 04:11:19','Уволен грузчик 123 123 132'),(57,1,1,'2020-11-16 04:12:11','Вход в систему'),(57,2,1,'2020-11-16 04:12:33','Проведена новая инвенторизация 14 РПГ-0 -1'),(58,1,1,'2020-11-16 04:17:52','Вход в систему'),(58,2,1,'2020-11-16 04:18:52','Оформлена выгрузка РПГ-0 -1 шт.'),(59,1,1,'2020-11-16 04:24:16','Вход в систему'),(59,2,1,'2020-11-16 04:24:24','Оформлена выгрузка РПГ-0 -1 шт.'),(59,3,1,'2020-11-16 04:24:29','Оформлена выгрузка 7.62 1000 шт.'),(59,4,1,'2020-11-16 04:24:40','Оформлена выгрузка РПГ-0 -1 шт.'),(60,1,1,'2020-11-16 04:27:33','Вход в систему'),(60,2,1,'2020-11-16 04:27:38','Оформлена выгрузка РПГ-0 -1 шт.'),(61,1,1,'2020-11-16 09:27:38','Вход в систему'),(61,2,1,'2020-11-16 09:27:49','Оформлена новая поставка DM53 1 шт.'),(61,3,1,'2020-11-16 09:27:54','Оформлена выгрузка DM53 1 шт.'),(61,4,1,'2020-11-16 09:28:29','Проведена новая инвенторизация 9 Калаш 120'),(62,1,1,'2020-11-16 09:30:39','Вход в систему'),(62,2,1,'2020-11-16 09:30:55','Проведена новая инвенторизация 9 Калаш 117'),(62,3,1,'2020-11-16 09:31:39','Оформлена выгрузка DM53 1 шт.'),(63,1,1,'2020-11-16 09:40:34','Вход в систему'),(63,2,1,'2020-11-16 09:40:55','Оформлена новая поставка Калаш 11 шт.'),(63,3,1,'2020-11-16 09:40:59','Оформлена выгрузка Калаш 11 шт.'),(64,1,1,'2020-11-16 09:44:33','Вход в систему'),(64,2,1,'2020-11-16 09:44:47','Проведена новая инвенторизация 9 Калаш 116'),(64,3,1,'2020-11-16 09:45:09','Оформлена новая поставка Калаш 12 шт.'),(64,4,1,'2020-11-16 09:45:13','Оформлена выгрузка Калаш 12 шт.'),(65,1,1,'2020-11-16 09:48:08','Вход в систему'),(65,2,1,'2020-11-16 09:48:18','Оформлена новая поставка Калаш 12 шт.'),(65,3,1,'2020-11-16 09:48:23','Оформлена выгрузка Калаш 12 шт.'),(66,1,9,'2020-11-16 09:56:37','Вход в систему'),(66,2,9,'2020-11-16 09:57:44','Добавлен новый грузчик Главный Работнмик '),(66,3,9,'2020-11-16 09:57:59','Уволен грузчик Главный Работнмик '),(66,4,9,'2020-11-16 09:59:27','Проведена новая инвенторизация 4 DM53 99'),(67,1,9,'2020-11-16 10:06:33','Вход в систему'),(67,2,9,'2020-11-16 10:07:05','Оформлена новая поставка Калаш 100000 шт.'),(67,3,9,'2020-11-16 10:07:48','Оформлена выгрузка Калаш 100000 шт.'),(68,1,1,'2020-12-14 15:02:24','Вход в систему'),(69,1,1,'2020-12-14 15:08:59','Вход в систему'),(70,1,1,'2020-12-14 15:14:58','Вход в систему'),(71,1,1,'2020-12-21 10:17:20','Вход в систему'),(72,1,1,'2020-12-21 10:19:22','Вход в систему'),(73,1,1,'2020-12-21 10:21:15','Вход в систему'),(74,1,1,'2020-12-21 10:23:24','Вход в систему'),(75,1,1,'2020-12-21 10:32:37','Вход в систему'),(76,1,1,'2020-12-21 10:37:05','Вход в систему'),(77,1,1,'2020-12-21 10:45:33','Вход в систему'),(78,1,1,'2020-12-24 01:22:47','Вход в систему'),(79,1,1,'2020-12-24 01:34:09','Вход в систему'),(80,1,1,'2020-12-24 01:36:36','Вход в систему'),(81,1,1,'2020-12-24 01:38:32','Вход в систему'),(82,1,1,'2020-12-24 01:39:32','Вход в систему'),(83,1,1,'2020-12-24 01:40:52','Вход в систему'),(84,1,1,'2020-12-24 01:43:56','Вход в систему'),(85,1,1,'2020-12-24 01:50:54','Вход в систему'),(86,1,1,'2020-12-24 01:53:52','Вход в систему'),(87,1,1,'2020-12-24 01:55:05','Вход в систему'),(88,1,1,'2020-12-24 01:56:06','Вход в систему'),(89,1,1,'2020-12-24 01:58:13','Вход в систему'),(90,1,1,'2020-12-24 01:59:53','Вход в систему'),(91,1,1,'2020-12-24 02:01:53','Вход в систему'),(92,1,1,'2020-12-24 02:05:11','Вход в систему'),(93,1,1,'2020-12-24 02:08:35','Вход в систему'),(94,1,1,'2020-12-24 12:08:48','Вход в систему'),(94,2,1,'2020-12-24 12:09:28','Добавлен предмет 12.7'),(94,3,1,'2020-12-24 12:09:41','Оформлена новая поставка 12.7 1000 шт.'),(95,1,1,'2020-12-24 12:12:48','Вход в систему'),(95,2,1,'2020-12-24 12:13:58','Проведена новая инвенторизация 20 12.7 500'),(95,3,1,'2020-12-24 12:14:15','Оформлена новая поставка 12.7 100 шт.'),(96,1,1,'2020-12-24 12:17:56','Вход в систему'),(96,2,1,'2020-12-24 12:18:10','Оформлена выгрузка 12.7 100 шт.'),(97,1,1,'2020-12-24 12:18:49','Вход в систему'),(97,2,1,'2020-12-24 12:19:09','Проведена новая инвенторизация 20 12.7 450'),(98,1,1,'2020-12-24 12:24:00','Вход в систему'),(98,2,1,'2020-12-24 12:24:39','Оформлена новая поставка 12.7 120 шт.'),(98,3,1,'2020-12-24 12:25:18','Оформлена выгрузка 12.7 120 шт.'),(99,1,1,'2020-12-24 13:04:41','Вход в систему'),(100,1,1,'2020-12-24 13:09:21','Вход в систему'),(101,1,1,'2020-12-24 13:29:39','Вход в систему'),(102,1,1,'2020-12-24 13:32:06','Вход в систему'),(103,1,1,'2020-12-24 13:33:46','Вход в систему'),(104,1,1,'2020-12-24 13:45:41','Вход в систему'),(105,1,1,'2020-12-24 13:51:45','Вход в систему'),(106,1,1,'2020-12-24 14:05:50','Вход в систему'),(107,1,1,'2020-12-28 22:46:43','Вход в систему'),(108,1,1,'2020-12-28 23:06:27','Вход в систему'),(109,1,1,'2020-12-28 23:09:09','Вход в систему'),(110,1,1,'2020-12-29 00:45:49','Вход в систему'),(111,1,1,'2020-12-29 00:54:28','Вход в систему'),(112,1,1,'2020-12-29 00:58:36','Вход в систему'),(113,1,1,'2020-12-29 01:03:30','Вход в систему');
/*!40000 ALTER TABLE `history` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `inventarisation`
--

DROP TABLE IF EXISTS `inventarisation`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `inventarisation` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `Date` date NOT NULL,
  `Storage ID` int NOT NULL,
  `Item ID` int NOT NULL,
  `Nominal Quantity` int NOT NULL,
  `Real Quantity` int NOT NULL,
  PRIMARY KEY (`ID`,`Storage ID`,`Item ID`),
  KEY `fk_inventarisations_storage1_idx` (`Storage ID`,`Item ID`),
  CONSTRAINT `fk_inventarisations_storage1` FOREIGN KEY (`Storage ID`, `Item ID`) REFERENCES `storage` (`ID`, `Item ID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `inventarisation`
--

LOCK TABLES `inventarisation` WRITE;
/*!40000 ALTER TABLE `inventarisation` DISABLE KEYS */;
INSERT INTO `inventarisation` VALUES (1,'2020-05-12',1,1,1100,1000),(3,'2020-11-16',14,3,1,2),(4,'2020-11-16',9,1,123,120),(5,'2020-11-16',9,1,120,117),(6,'2020-11-16',9,1,117,116),(7,'2020-11-16',4,4,100,99),(8,'2020-12-24',20,5,1000,500),(9,'2020-12-24',20,5,500,450);
/*!40000 ALTER TABLE `inventarisation` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `issue`
--

DROP TABLE IF EXISTS `issue`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `issue` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `Date` date NOT NULL,
  `PN` int NOT NULL,
  `Storage ID` int NOT NULL,
  `Item ID` int NOT NULL,
  PRIMARY KEY (`ID`,`PN`,`Storage ID`,`Item ID`),
  KEY `fk_issue_loaders1_idx` (`PN`),
  KEY `fk_issue_storage1_idx` (`Storage ID`,`Item ID`),
  CONSTRAINT `fk_issue_loaders1` FOREIGN KEY (`PN`) REFERENCES `loader` (`PN`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_issue_storage1` FOREIGN KEY (`Storage ID`, `Item ID`) REFERENCES `storage` (`ID`, `Item ID`)
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `issue`
--

LOCK TABLES `issue` WRITE;
/*!40000 ALTER TABLE `issue` DISABLE KEYS */;
INSERT INTO `issue` VALUES (1,'2020-07-05',1,3,2),(2,'2020-11-16',2,7,1),(3,'2020-11-16',1,2,2),(4,'2020-11-16',1,8,4),(5,'2020-11-16',1,10,4),(6,'2020-11-16',2,11,4),(7,'2020-11-16',3,12,4),(8,'2020-11-16',3,13,4),(9,'2020-11-16',2,14,3),(10,'2020-11-16',1,16,1),(11,'2020-11-16',1,17,1),(12,'2020-11-16',3,18,1),(13,'2020-11-16',1,19,1),(14,'2020-12-24',2,21,5),(15,'2020-12-24',2,22,5);
/*!40000 ALTER TABLE `issue` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `item`
--

DROP TABLE IF EXISTS `item`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `item` (
  `Item ID` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(45) NOT NULL,
  `Properities` varchar(100) DEFAULT NULL,
  `Manufacturer` varchar(45) DEFAULT NULL,
  `Type ID` int NOT NULL,
  PRIMARY KEY (`Item ID`),
  KEY `fk_items_types1_idx` (`Type ID`),
  CONSTRAINT `fk_items_types1` FOREIGN KEY (`Type ID`) REFERENCES `type` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `item`
--

LOCK TABLES `item` WRITE;
/*!40000 ALTER TABLE `item` DISABLE KEYS */;
INSERT INTO `item` VALUES (1,'Калаш','Боевое, Русское','Таганский Нефтянной Завод',1),(2,'7.62','Боевое','ОПГ Березка',2),(3,'РПГ-0','Смешное, Украинское','Детский сад \"Алеша Попович\"',1),(4,'DM53','Танковое, Немецкое','Rheinmetall',2),(5,'12.7','Боевое','США',2);
/*!40000 ALTER TABLE `item` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `loader`
--

DROP TABLE IF EXISTS `loader`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `loader` (
  `PN` int NOT NULL AUTO_INCREMENT,
  `First Name` varchar(45) NOT NULL,
  `Last Name` varchar(45) NOT NULL,
  `Patronymic` varchar(45) DEFAULT NULL,
  `Date of Birth` date NOT NULL,
  `passport ID` int NOT NULL,
  `Hiring date` date NOT NULL,
  `Dismissal date` date DEFAULT NULL,
  PRIMARY KEY (`PN`),
  UNIQUE KEY `PN_UNIQUE` (`PN`)
) ENGINE=InnoDB AUTO_INCREMENT=24 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `loader`
--

LOCK TABLES `loader` WRITE;
/*!40000 ALTER TABLE `loader` DISABLE KEYS */;
INSERT INTO `loader` VALUES (1,'Андрей','Морозов','Химик','2001-01-21',516772,'2020-01-24',NULL),(2,'Саша','Иеронимас',NULL,'1987-06-15',737321,'2020-01-26',NULL),(3,'Паша','Техник',NULL,'1984-12-12',732661,'2020-01-27',NULL),(17,'Алексей','\"Алёша\"','Русин','2004-01-01',321341,'2020-01-30','2020-11-15'),(18,'Дима','Петров',NULL,'1990-02-15',551512,'2020-11-15','2020-11-15'),(19,'Александр','Кузьмин',NULL,'1990-07-06',415151,'2020-11-16','2020-11-16'),(20,'Человек','Работающий',NULL,'1994-01-06',515215,'2020-11-16','2020-11-16'),(21,'Нечеловеческий','Демонический','Работник','2019-03-31',415415,'2020-11-16','2020-11-16'),(22,'123','123','132','2020-11-16',132131,'2020-11-16','2020-11-16'),(23,'Главный','Работнмик',NULL,'1965-02-12',515124,'2020-11-16','2020-11-16');
/*!40000 ALTER TABLE `loader` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `operator`
--

DROP TABLE IF EXISTS `operator`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `operator` (
  `PN` int NOT NULL AUTO_INCREMENT,
  `First Name` varchar(45) NOT NULL,
  `Last Name` varchar(45) NOT NULL,
  `Patronymic` varchar(45) DEFAULT NULL,
  `Date of Birth` date NOT NULL,
  `passport ID` int NOT NULL,
  `Hiring date` date NOT NULL,
  `Dismissal date` date DEFAULT NULL,
  `Login` varchar(45) NOT NULL,
  `Password` varchar(45) NOT NULL,
  PRIMARY KEY (`PN`),
  UNIQUE KEY `PN_UNIQUE` (`PN`),
  UNIQUE KEY `Login_UNIQUE` (`Login`),
  KEY `fk_operators_security1_idx` (`Login`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `operator`
--

LOCK TABLES `operator` WRITE;
/*!40000 ALTER TABLE `operator` DISABLE KEYS */;
INSERT INTO `operator` VALUES (1,'Вася','Агент',NULL,'2000-01-01',415151,'2020-01-20',NULL,'123','123'),(3,'Юрий','Головлев','Юльевич','1997-01-23',525145,'2020-11-14','2020-11-15','yura@mail.ru','dura'),(9,'Данила','Сырков',NULL,'2002-07-05',415613,'2020-11-15',NULL,'SIRdanya','hello'),(10,'Андрей','Авдонов','Олегович','2002-09-01',737373,'2020-11-15',NULL,'Lortcraft','vitayAK'),(11,'Григорий','Мишин','Александрович','1984-01-04',415161,'2020-11-15','2020-11-15','gma','delete'),(12,'Паша','Пантелеев','123','1999-02-19',151511,'2020-11-16','2020-11-16','zefirka','111');
/*!40000 ALTER TABLE `operator` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `storage`
--

DROP TABLE IF EXISTS `storage`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `storage` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `Item ID` int NOT NULL,
  `Quantity` int NOT NULL,
  PRIMARY KEY (`ID`,`Item ID`),
  KEY `fk_storage_items_idx` (`Item ID`),
  CONSTRAINT `fk_storage_items` FOREIGN KEY (`Item ID`) REFERENCES `item` (`Item ID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=23 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `storage`
--

LOCK TABLES `storage` WRITE;
/*!40000 ALTER TABLE `storage` DISABLE KEYS */;
INSERT INTO `storage` VALUES (1,1,1000),(2,2,240),(3,2,100),(4,4,99),(5,2,1000),(6,3,10),(7,1,1000),(8,4,30),(9,1,116),(10,4,123),(11,4,132),(12,4,444),(13,4,1),(14,3,-1),(15,4,1),(16,1,11),(17,1,12),(18,1,12),(19,1,100000),(20,5,450),(21,5,100),(22,5,120);
/*!40000 ALTER TABLE `storage` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `type`
--

DROP TABLE IF EXISTS `type`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `type` (
  `ID` int NOT NULL,
  `Type` varchar(45) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `type`
--

LOCK TABLES `type` WRITE;
/*!40000 ALTER TABLE `type` DISABLE KEYS */;
INSERT INTO `type` VALUES (1,'Орудие'),(2,'Патрон');
/*!40000 ALTER TABLE `type` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2020-12-29  1:20:41
