-- SQL Dump of wcLog.mdb
-- generated by MDB Viewer 2.2.7
-- optimized for MySQL

SET NAMES 'UTF8';



DROP TABLE IF EXISTS `_STRUTTURA_`;

CREATE TABLE `_STRUTTURA_` (
    `F_STT_ID` INTEGER PRIMARY KEY,
    `F_STT_DATCRE` DATETIME,
    `F_STT_DATAGG` DATETIME,
    `F_STT_VERSIO` INTEGER,
    `F_STT_OLDVER` INTEGER,
    `F_STT_ARCPUB` BOOLEAN,
    `F_STT_TEMPOR` BOOLEAN,
    `F_STT_ARCPRI` BOOLEAN,
    `F_STT_ESTERN` BOOLEAN
) CHARACTER SET 'UTF8';

INSERT INTO `_STRUTTURA_`(`F_STT_ID`,`F_STT_DATCRE`,`F_STT_DATAGG`,`F_STT_VERSIO`,`F_STT_OLDVER`,`F_STT_ARCPUB`,`F_STT_TEMPOR`,`F_STT_ARCPRI`,`F_STT_ESTERN`)
VALUES(1,NULL,NULL,101,0,TRUE,TRUE,FALSE,FALSE);



DROP TABLE IF EXISTS `Log`;

CREATE TABLE `Log` (
    `F_LOG_CODLOG` INTEGER,
    `F_LOG_DATLOG` DATETIME,
    `F_LOG_CODUTE` INTEGER,
    `F_LOG_USERWP` VARCHAR(50),
    `F_LOG_USERWN` VARCHAR(50),
    `F_LOG_NOMEPC` VARCHAR(50),
    `F_LOG___TIPO` INTEGER,
    `F_LOG___NOTE` LONGTEXT
) CHARACTER SET 'UTF8';



