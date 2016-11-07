-- MySQL Script generated by MySQL Workbench
-- 11/06/16 20:28:11
-- Model: New Model    Version: 1.0
-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `mydb` DEFAULT CHARACTER SET utf8 ;
USE `mydb` ;

-- -----------------------------------------------------
-- Table `mydb`.`ACHIEVEMENT`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `ACHIEVEMENT` (
  `ACHIEVEMENT_ID` INT NOT NULL,
  `NAME` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`ACHIEVEMENT_ID`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`ACCOUNT`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `ACCOUNT` (
  `ACCOUNT_ID` INT NOT NULL,
  `USERNAME` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`ACCOUNT_ID`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`STATS`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `STATS` (
  `STATS_ID` INT NOT NULL,
  `SECONDS_PLAYED` INT NOT NULL,
  `NB_KILLED_SCARABS` INT NOT NULL,
  `NB_KILLED_BATS` INT NOT NULL,
  `NB_KILLED_SKELTALS` INT NOT NULL,
  `NB_DEATHS` INT NOT NULL,
  `KNIFE_PICKED` TINYINT(1) NOT NULL,
  `AXE_PICKED` TINYINT(1) NOT NULL,
  `FEATHER_PICKED` TINYINT(1) NOT NULL,
  `BOOTS_PICKED` TINYINT(1) NOT NULL,
  `BUBBLE_PICKED` TINYINT(1) NOT NULL,
  `ARMOR_PICKED` TINYINT(1) NOT NULL,
  `ARTEFACT1_PICKED` TINYINT(1) NOT NULL,
  `ARTEFACT2_PICKED` TINYINT(1) NOT NULL,
  `ARTEFACT3_PICKED` TINYINT(1) NOT NULL,
  `ARTEFACT4_PICKED` TINYINT(1) NOT NULL,
  `GAME_COMPLETED` TINYINT(1) NOT NULL,
  `ACCOUNT_ID` INT NOT NULL,
  PRIMARY KEY (`STATS_ID`, `ACCOUNT_ID`),
  CONSTRAINT `fk_STATS_ACCOUNT1`
    FOREIGN KEY (`ACCOUNT_ID`)
    REFERENCES `ACCOUNT` (`ACCOUNT_ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`ACCOUNT_ACHIEVEMENT`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `ACCOUNT_ACHIEVEMENT` (
  `ACCOUNT_ID` INT NOT NULL,
  `ACHIEVEMENT_ID` INT NOT NULL,
  PRIMARY KEY (`ACCOUNT_ID`, `ACHIEVEMENT_ID`),
  CONSTRAINT `fk_ACCOUNT_has_ACHIEVEMENT_ACCOUNT1`
    FOREIGN KEY (`ACCOUNT_ID`)
    REFERENCES `mydb`.`ACCOUNT` (`ACCOUNT_ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_ACCOUNT_has_ACHIEVEMENT_ACHIEVEMENT1`
    FOREIGN KEY (`ACHIEVEMENT_ID`)
    REFERENCES `ACHIEVEMENT` (`ACHIEVEMENT_ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`FUN_FACT`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `FUN_FACT` (
  `FUN_FACT_ID` INT NOT NULL,
  `DESCRIPTION` VARCHAR(250) NOT NULL,
  PRIMARY KEY (`FUN_FACT_ID`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`SETTINGS`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `SETTINGS` (
  `SETTINGS_ID` INT NOT NULL,
  `MUSIC_PLAYING` TINYINT(1) NOT NULL,
  `MUSIC_VOLUME` FLOAT NOT NULL,
  `SFX_VOLUME` FLOAT NOT NULL,
  `CONTROL_SCHEME` INT NOT NULL,
  `ACCOUNT_ID` INT NOT NULL,
  PRIMARY KEY (`SETTINGS_ID`, `ACCOUNT_ID`),
  CONSTRAINT `fk_SETTINGS_ACCOUNT1`
    FOREIGN KEY (`ACCOUNT_ID`)
    REFERENCES `ACCOUNT` (`ACCOUNT_ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
