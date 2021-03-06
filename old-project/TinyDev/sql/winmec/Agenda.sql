-- SQL Dump of Agenda.mdb
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
VALUES(1,NULL,NULL,219,0,TRUE,FALSE,FALSE,FALSE);



DROP TABLE IF EXISTS `Appuntamenti`;

CREATE TABLE `Appuntamenti` (
    `Id` INTEGER PRIMARY KEY,
    `Inserimento` DATETIME,
    `Appuntamento` DATETIME,
    `IdStato` INTEGER,
    `IdPriorita` INTEGER,
    `IdCategoria` INTEGER,
    `Oggetto` VARCHAR(255),
    `Testo` LONGTEXT,
    `Promemoria` DATETIME,
    `Avvisato` BOOLEAN,
    `Chiusura` DATETIME,
    `Annotazioni` LONGTEXT,
    `EmailAvviso` BOOLEAN,
    `EmailA` LONGTEXT,
    `EmailCC` LONGTEXT,
    `IdProprietario` INTEGER,
    `IdCategoriaGenerica` INTEGER,
    `idGCalendario` VARCHAR(255),
    `idGAppuntamento` VARCHAR(255),
    `Pubblico` INTEGER,
    `DataFine` DATETIME,
    `DataModifica` DATETIME,
    `DaAggiornare` INTEGER,
    `Cancellato` BOOLEAN
) CHARACTER SET 'UTF8';

INSERT INTO `Appuntamenti`(`Id`,`Inserimento`,`Appuntamento`,`IdStato`,`IdPriorita`,`IdCategoria`,`Oggetto`,`Testo`,`Promemoria`,`Avvisato`,`Chiusura`,`Annotazioni`,`EmailAvviso`,`EmailA`,`EmailCC`,`IdProprietario`,`IdCategoriaGenerica`,`idGCalendario`,`idGAppuntamento`,`Pubblico`,`DataFine`,`DataModifica`,`DaAggiornare`,`Cancellato`)
VALUES(3,'2015-11-11 15:53:00','2015-11-07 15:53:00',0,0,0,'tagliando','service cambio olio',NULL,FALSE,NULL,'',FALSE,'','',0,0,'','',2,'2015-11-07 15:53:00','2015-11-11 15:53:00',2,FALSE),
      (5,'2017-01-20 15:02:00','2017-01-20 00:00:00',1,5,10,'Prevista consegna autorizzata DY187TL - LEASYS','Prevista consegna autorizzata da LEASYS per il veicolo  FIAT Doblò dal 2005 fino al 2009  targato  DY187TL  di proprietà:\nLEASYS S.p.a.\nCorso Agnelli 200\n10135 TORINO TO\n','2017-01-19 00:00:00',FALSE,NULL,'',FALSE,'','',0,0,'','',2,'2017-01-20 00:00:00','2017-01-20 15:02:00',2,TRUE),
      (6,'2017-01-27 08:43:00','2017-01-21 00:00:00',1,5,10,'Prevista consegna autorizzata FB336SN - LEASYS','Prevista consegna autorizzata da LEASYS per il veicolo  FORD Transit Tourneo Custom dal 2012  targato  FB336SN  di proprietà:\nLEASYS S.p.a.\nCorso Agnelli 200\n10135 TORINO TO\n','2017-01-20 00:00:00',FALSE,NULL,'',FALSE,'','',0,0,'','',2,'2017-01-21 00:00:00','2017-01-27 08:43:00',2,TRUE),
      (7,'2017-01-27 08:46:00','2017-01-27 00:00:00',1,5,10,'Prevista consegna autorizzata FB339SN - LEASYS','Prevista consegna autorizzata da LEASYS per il veicolo  FORD Transit Tourneo Custom dal 2012  targato  FB339SN  di proprietà:\nLEASYS S.p.a.\nCorso Agnelli 200\n10135 TORINO TO\n','2017-01-26 00:00:00',FALSE,NULL,'',FALSE,'','',0,0,'','',2,'2017-01-27 00:00:00','2017-01-27 08:48:00',2,TRUE),
      (8,'2017-01-30 15:12:00','2017-01-30 00:00:00',1,5,10,'Prevista consegna autorizzata FB345SN - LEASYS','Prevista consegna autorizzata da LEASYS per il veicolo  FORD Transit Tourneo Custom dal 2012  targato  FB345SN  di proprietà:\nLEASYS S.p.a.\nCorso Agnelli 200\n10135 TORINO TO\n','2017-01-29 00:00:00',FALSE,NULL,'',FALSE,'','',0,0,'','',2,'2017-01-30 00:00:00','2017-01-31 09:57:00',2,TRUE),
      (9,'2017-01-30 15:12:00','2017-01-30 00:00:00',1,5,10,'Prevista consegna autorizzata FB334SN - LEASYS','Prevista consegna autorizzata da LEASYS per il veicolo  FORD Transit Tourneo Custom dal 2012  targato  FB334SN  di proprietà:\nLEASYS S.p.a.\nCorso Agnelli 200\n10135 TORINO TO\n','2017-01-29 00:00:00',FALSE,NULL,'',FALSE,'','',0,0,'','',2,'2017-01-30 00:00:00','2017-01-30 15:12:00',2,TRUE),
      (10,'2017-01-30 17:26:00','2017-02-06 00:00:00',1,5,10,'Prevista consegna autorizzata EP944YV - LEASYS','Prevista consegna autorizzata da LEASYS per il veicolo  FIAT Freemont dal 2011  targato  EP944YV  di proprietà:\nLEASYS S.p.a.\nCorso Agnelli 200\n10135 TORINO TO\n','2017-02-05 00:00:00',FALSE,NULL,'',FALSE,'','',0,0,'','',2,'2017-02-06 00:00:00','2017-02-07 10:14:00',2,TRUE),
      (11,'2017-02-01 09:56:00','2016-12-27 00:00:00',3,5,10,'Prevista consegna autorizzata FB367SP - LEASYS','Prevista consegna autorizzata da LEASYS per il veicolo  FORD Transit Tourneo Custom dal 2012  targato  FB367SP  di proprietà:\nLEASYS S.p.a.\nCorso Agnelli 200\n10135 TORINO TO\n','2016-12-26 00:00:00',FALSE,'2017-01-26 09:43:00','',FALSE,'','',0,0,'','',2,'2016-12-27 00:00:00','2017-02-01 09:56:00',2,FALSE),
      (12,'2017-02-01 10:19:00','2017-01-31 00:00:00',1,5,10,'Prevista consegna autorizzata FF482PC - LEASYS','Prevista consegna autorizzata da LEASYS per il veicolo  JEEP Renegade dal 2014  targato  FF482PC  di proprietà:\nLEASYS S.p.a.\nCorso Agnelli 200\n10135 TORINO TO\n','2017-01-30 00:00:00',FALSE,NULL,'',FALSE,'','',0,0,'','',2,'2017-01-31 00:00:00','2017-02-01 10:19:00',2,TRUE),
      (13,'2017-02-14 10:45:00','2017-02-16 00:00:00',1,5,10,'Prevista consegna autorizzata FB343SN - LEASYS','Prevista consegna autorizzata da LEASYS per il veicolo  FORD Transit dal 2006 fino al 2013  targato  FB343SN  di proprietà:\nLEASYS S.p.a.\nCorso Agnelli 200\n10135 TORINO TO\n','2017-02-15 00:00:00',FALSE,NULL,'',FALSE,'','',0,0,'','',2,'2017-02-16 00:00:00','2017-02-14 10:45:00',2,TRUE),
      (14,'2017-02-14 16:41:00','2017-02-15 15:55:00',1,5,10,'Prevista consegna autorizzata FH162DA - ATHLON','Prevista consegna autorizzata da ATHLON per il veicolo  BMW Serie 1 F20 LCI dal 03/2015  targato  FH162DA  di proprietà:\nATHLON CAR LEASE ITALY S.R.L.\nVIA CARLO PESENTI, 109\n00156  ROMA \n','2017-02-14 15:55:00',FALSE,NULL,'',FALSE,'','',0,0,'','',2,'2017-02-15 15:55:00','2017-02-14 16:41:00',2,TRUE),
      (15,'2017-02-15 10:26:00','2017-02-22 10:58:00',1,5,10,'Prevista consegna autorizzata EZ347ZF - ATHLON','Prevista consegna autorizzata da ATHLON per il veicolo  PEUGEOT 308 dal 2013  targato  EZ347ZF  di proprietà:\nATHLON CAR LEASE ITALY S.R.L.\nVIA CARLO PESENTI, 109\n00156 ROMA \n','2017-02-21 10:58:00',FALSE,NULL,'',FALSE,'','',0,0,'','',2,'2017-02-22 10:58:00','2017-02-20 12:04:00',2,TRUE),
      (16,'2017-02-15 13:57:00','2017-02-20 00:00:00',1,5,10,'Prevista consegna autorizzata FD891ZK - LEASYS','Prevista consegna autorizzata da LEASYS per il veicolo  FIAT 500 X dal 2014  targato  FD891ZK  di proprietà:\nLEASYS S.p.a.\nCorso Agnelli 200\n10135 TORINO TO\n','2017-02-19 00:00:00',FALSE,NULL,'',FALSE,'','',0,0,'','',2,'2017-02-20 00:00:00','2017-02-15 13:57:00',2,TRUE),
      (17,'2017-02-16 10:05:00','2017-02-17 00:00:00',1,5,10,'Prevista consegna autorizzata FF773EB - LEASYS','Prevista consegna autorizzata da LEASYS per il veicolo  PEUGEOT 308 dal 2013  targato  FF773EB  di proprietà:\nLEASYS S.p.a.\nCorso Agnelli 200\n10135 TORINO TO\n','2017-02-16 00:00:00',FALSE,NULL,'',FALSE,'','',0,0,'','',2,'2017-02-17 00:00:00','2017-02-16 10:06:00',2,TRUE),
      (18,'2017-02-17 09:37:00','2017-02-20 00:00:00',1,5,10,'Prevista consegna autorizzata FB338SN - LEASYS','Prevista consegna autorizzata da LEASYS per il veicolo  FORD Transit Tourneo Custom dal 2012  targato  FB338SN  di proprietà:\nLEASYS S.p.a.\nCorso Agnelli 200\n10135 TORINO TO\n','2017-02-19 00:00:00',FALSE,NULL,'',FALSE,'','',0,0,'','',2,'2017-02-20 00:00:00','2017-02-17 09:38:00',2,TRUE),
      (19,'2017-02-17 13:58:00','2017-02-21 00:00:00',1,5,10,'Prevista consegna autorizzata EX738AY - LEASYS','Prevista consegna autorizzata da LEASYS per il veicolo  FIAT Doblò dal 2009 fino al 11/2014  targato  EX738AY  di proprietà:\nLEASYS S.p.a.\nCorso Agnelli 200\n10135 TORINO TO\n','2017-02-20 00:00:00',FALSE,NULL,'',FALSE,'','',0,0,'','',2,'2017-02-21 00:00:00','2017-02-17 13:58:00',2,TRUE),
      (20,'2017-02-21 09:39:00','2017-02-22 08:33:00',1,5,10,'Prevista consegna autorizzata FC661XB - ATHLON','Prevista consegna autorizzata da ATHLON per il veicolo  PEUGEOT 308 dal 2013  targato  FC661XB  di proprietà:\nATHLON CAR LEASE ITALY S.R.L.\nVIA CARLO PESENTI, 109\n00156 ROMA \n','2017-02-21 08:33:00',FALSE,NULL,'',FALSE,'','',0,0,'','',2,'2017-02-22 08:33:00','2017-02-21 09:39:00',2,TRUE),
      (21,'2017-02-21 11:37:00','2017-02-21 00:00:00',1,5,10,'Prevista consegna autorizzata FB325SP - LEASYS','Prevista consegna autorizzata da LEASYS per il veicolo  FORD Transit Tourneo Custom dal 2012  targato  FB325SP  di proprietà:\nLEASYS S.p.a.\nCorso Agnelli 200\n10135 TORINO TO\n','2017-02-20 00:00:00',FALSE,NULL,'',FALSE,'','',0,0,'','',2,'2017-02-21 00:00:00','2017-02-21 11:37:00',2,TRUE),
      (22,'2017-02-21 17:42:00','2017-02-23 00:00:00',1,5,10,'Prevista consegna autorizzata FB367SP - LEASYS','Prevista consegna autorizzata da LEASYS per il veicolo  FORD Transit Tourneo Custom dal 2012  targato  FB367SP  di proprietà:\nLEASYS S.p.a.\nCorso Agnelli 200\n10135 TORINO TO\n','2017-02-22 00:00:00',FALSE,NULL,'',FALSE,'','',0,0,'','',2,'2017-02-23 00:00:00','2017-02-21 17:42:00',2,TRUE),
      (23,'2017-06-06 11:42:00','2017-05-18 16:04:00',1,5,10,'Prevista consegna autorizzata EX301AE - ATHLON','Prevista consegna autorizzata da ATHLON per il veicolo  PEUGEOT 5008 dal 2014 fino al 03/2017  targato  EX301AE  di proprietà:\nATHLON CAR LEASE ITALY S.R.L.\nVIA CARLO PESENTI, 109\n00156  ROMA \n','2017-05-17 16:04:00',FALSE,NULL,'',FALSE,'','',0,0,'','',2,'2017-05-18 16:04:00','2017-06-06 11:42:00',2,TRUE),
      (24,'2017-08-04 14:15:00','2017-08-07 11:30:00',1,5,10,'Prevista consegna autorizzata FG018AT - ATHLON','Prevista consegna autorizzata da ATHLON per il veicolo  RENAULT Kangoo dal 05/2013  targato  FG018AT  di proprietà:\nATHLON CAR LEASE ITALY S.R.L.\nVIA CARLO PESENTI, 109\n00156  ROMA \n','2017-08-06 11:30:00',FALSE,NULL,'',FALSE,'','',0,0,'','',2,'2017-08-07 11:30:00','2017-08-04 14:15:00',2,TRUE),
      (25,'2017-11-11 08:43:00','2017-11-06 00:00:00',1,5,10,'Prevista consegna autorizzata FB326SP - LEASYS','Prevista consegna autorizzata da LEASYS per il veicolo  FORD Transit Tourneo Custom dal 2012  targato  FB326SP  di proprietà:\nLEASYS S.p.a.\nCorso Agnelli 200\n10135 TORINO TO\n','2017-11-05 00:00:00',FALSE,NULL,'',FALSE,'','',0,0,'','',2,'2017-11-06 00:00:00','2017-11-11 08:43:00',2,TRUE),
      (26,'2017-11-23 09:18:00','2017-11-02 08:58:00',1,5,10,'Prevista consegna autorizzata EY249AN - ATHLON','Prevista consegna autorizzata da ATHLON per il veicolo  FIAT Punto dal 2012  targato  EY249AN  di proprietà:\nATHLON CAR LEASE ITALY S.R.L.\nVIA CARLO PESENTI, 109\n00156  ROMA \n','2017-11-01 08:58:00',FALSE,NULL,'',FALSE,'','',0,0,'','',2,'2017-11-02 08:58:00','2017-11-23 09:18:00',2,TRUE),
      (27,'2017-11-23 10:12:00','2017-11-16 08:42:00',1,5,10,'Prevista consegna autorizzata ET593KZ - ATHLON','Prevista consegna autorizzata da ATHLON per il veicolo  FIAT Punto dal 2005 fino al 2012  targato  ET593KZ  di proprietà:\nATHLON CAR LEASE ITALY S.R.L.\nVIA CARLO PESENTI, 109\n00156  ROMA \n','2017-11-15 08:42:00',FALSE,NULL,'',FALSE,'','',0,0,'','',2,'2017-11-16 08:42:00','2017-11-23 10:12:00',2,TRUE),
      (28,'2017-12-13 12:09:00','2017-12-14 08:30:00',1,5,2,'Prevista consegna FB342SN','Prevista consegna del veicolo  FORD Transit dal 2006 fino al 2013  targato  FB342SN  di proprietà:\nLEASYS S.p.a.\nCorso Agnelli 200\n10135 TORINO TO\n','2017-12-13 08:30:00',FALSE,NULL,'',FALSE,'','',0,0,'','',2,'2017-12-14 08:30:00','2017-12-13 12:09:00',2,TRUE),
      (29,'2018-01-05 17:43:00','2018-01-05 15:20:00',1,5,10,'Prevista consegna autorizzata ET595KZ - ATHLON','Prevista consegna autorizzata da ATHLON per il veicolo  FIAT Fiorino dal 11/2007  targato  ET595KZ  di proprietà:\nATHLON CAR LEASE ITALY S.R.L.\nVIA CARLO PESENTI, 109\n00156 ROMA \n','2018-01-04 15:20:00',FALSE,NULL,'',FALSE,'','',0,0,'','',2,'2018-01-05 15:20:00','2018-01-05 17:43:00',2,TRUE),
      (30,'2018-01-18 11:09:00','2018-01-18 09:00:00',1,5,10,'Prevista consegna autorizzata EZ825BH - ATHLON','Prevista consegna autorizzata da ATHLON per il veicolo  VOLVO V60 dal 05/2013 fino al 02/2015  targato  EZ825BH  di proprietà:\nATHLON CAR LEASE ITALY S.R.L.\nVIA CARLO PESENTI, 109\n00156 ROMA \n','2018-01-17 09:00:00',FALSE,NULL,'',FALSE,'','',0,0,'','',2,'2018-01-18 09:00:00','2018-01-18 11:09:00',2,TRUE),
      (31,'2018-01-31 18:18:00','2018-02-12 00:00:00',1,5,10,'Prevista consegna autorizzata FG750ZW - LEASYS','Prevista consegna autorizzata da LEASYS per il veicolo  JEEP Renegade dal 2014  targato  FG750ZW  di proprietà:\nLEASYS S.p.a.\nCorso Agnelli 200\n10135 TORINO TO\n','2018-02-11 00:00:00',FALSE,NULL,'',FALSE,'','',0,0,'','',2,'2018-02-12 00:00:00','2018-01-31 18:18:00',2,TRUE),
      (32,'2018-03-20 15:18:00','2018-03-09 00:00:00',1,5,10,'Prevista consegna autorizzata FJ485GL - LEASYS','Prevista consegna autorizzata da LEASYS per il veicolo  LANCIA Ypsilon dal 2011 fino al 08/2015  targato  FJ485GL  di proprietà:\nLEASYS S.p.a.\nCorso Agnelli 200\n10135 TORINO TO\n','2018-03-08 00:00:00',FALSE,NULL,'',FALSE,'','',0,0,'','',2,'2018-03-09 00:00:00','2018-03-20 15:18:00',2,TRUE),
      (33,'2018-05-04 16:16:00','2018-04-18 09:01:00',1,5,10,'Prevista consegna autorizzata EX156CV - ATHLON','Prevista consegna autorizzata da ATHLON per il veicolo  FIAT Punto dal 2012  targato  EX156CV  di proprietà:\nATHLON CAR LEASE ITALY S.R.L.\nVIA CARLO PESENTI, 109\n00156  ROMA \n','2018-04-17 09:01:00',FALSE,NULL,'',FALSE,'','',0,0,'','',2,'2018-04-18 09:01:00','2018-05-04 16:16:00',2,TRUE),
      (34,'2018-05-14 14:17:00','2018-05-16 11:12:00',1,5,10,'Prevista consegna autorizzata FC901ES - ATHLON','Prevista consegna autorizzata da ATHLON per il veicolo  FORD Focus dal 09/2014  targato  FC901ES  di proprietà:\nATHLON CAR LEASE ITALY S.R.L.\nVIA CARLO PESENTI, 109\n00156 ROMA \n','2018-05-15 11:12:00',FALSE,NULL,'',FALSE,'','',0,0,'','',2,'2018-05-16 11:12:00','2018-05-14 14:17:00',2,TRUE),
      (35,'2018-05-16 12:24:00','2018-05-16 12:24:00',1,5,0,'','',NULL,FALSE,NULL,'',FALSE,'','',0,0,'','',2,'2018-05-16 12:24:00','2018-05-16 12:24:00',2,TRUE),
      (36,'2018-09-17 08:35:00','2018-09-06 00:00:00',1,5,10,'Prevista consegna autorizzata EW874WT - LEASYS','Prevista consegna autorizzata da LEASYS per il veicolo  FIAT Panda dal 2011  targato  EW874WT  di proprietà:\nLEASYS S.p.a.\nCorso Agnelli 200\n10135 TORINO TO\n','2018-09-05 00:00:00',FALSE,NULL,'',FALSE,'','',0,0,'','',2,'2018-09-06 00:00:00','2018-09-17 08:35:00',2,FALSE);



DROP TABLE IF EXISTS `Categorie`;

CREATE TABLE `Categorie` (
    `id` INTEGER,
    `Categoria` VARCHAR(50)
) CHARACTER SET 'UTF8';

INSERT INTO `Categorie`(`id`,`Categoria`)
VALUES(1,'Revisione veicolo'),
      (2,'Prevista consegna veicolo'),
      (3,'Noleggio veicolo'),
      (999999,'Generico'),
      (4,'Scadenza certificato medico'),
      (5,'Trattative lesioni'),
      (6,'Scadenza referto medico'),
      (7,'Presunta accettazione veicolo'),
      (8,'Scadenza polizza assicurativa'),
      (9,'Effettuare preventivo'),
      (10,'Prevista consegna veicolo autorizzata'),
      (11,'Prevista consegna ricambi');



DROP TABLE IF EXISTS `CategorieGeneriche`;

CREATE TABLE `CategorieGeneriche` (
    `id` INTEGER PRIMARY KEY,
    `CategoriaGenerica` VARCHAR(200)
) CHARACTER SET 'UTF8';




DROP TABLE IF EXISTS `Priorita`;

CREATE TABLE `Priorita` (
    `id` INTEGER PRIMARY KEY,
    `Priorita` VARCHAR(50)
) CHARACTER SET 'UTF8';

INSERT INTO `Priorita`(`id`,`Priorita`)
VALUES(3,'Bassa'),
      (5,'Normale'),
      (7,'Alta');



DROP TABLE IF EXISTS `Progressivo`;

CREATE TABLE `Progressivo` (
    `Id` INTEGER PRIMARY KEY
) CHARACTER SET 'UTF8';

INSERT INTO `Progressivo`(`Id`)
VALUES(1),
      (2),
      (3),
      (4),
      (5),
      (6),
      (7),
      (8),
      (9),
      (10),
      (11),
      (12),
      (13),
      (14),
      (15),
      (16),
      (17),
      (18),
      (19),
      (20),
      (21),
      (22),
      (23),
      (24),
      (25),
      (26),
      (27),
      (28),
      (29),
      (30),
      (31),
      (32),
      (33),
      (34),
      (35),
      (36);



DROP TABLE IF EXISTS `Proprietari`;

CREATE TABLE `Proprietari` (
    `id` INTEGER,
    `Proprietario` VARCHAR(50)
) CHARACTER SET 'UTF8';




DROP TABLE IF EXISTS `Stati`;

CREATE TABLE `Stati` (
    `Id` INTEGER PRIMARY KEY,
    `Stato` VARCHAR(50)
) CHARACTER SET 'UTF8';

INSERT INTO `Stati`(`Id`,`Stato`)
VALUES(1,'Aperto'),
      (2,'In corso'),
      (3,'Completato'),
      (4,'In attesa'),
      (5,'Rinviato');


