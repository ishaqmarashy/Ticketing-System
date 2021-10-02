
CREATE SCHEMA IF NOT EXISTS "TICKETING_SYS" DEFAULT CHARACTER SET utf8 ;
USE "TICKETING_SYS" ;

-- -----------------------------------------------------
-- Table "TICKETING_SYS"."AUTHL"
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS "AUTHL" (
  "UAUTH" VARCHAR(10) NOT NULL,
  PRIMARY KEY ("UAUTH"))
ENGINE = InnoDB;
-- -----------------------------------------------------
-- Table "TICKETING_SYS"."USERS"
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS "USERS" (
  "UNAME" VARCHAR(50) NOT NULL,
  "PASSWORD" VARCHAR(50) NOT NULL,
  "AUTH" VARCHAR(10) NOT NULL,
  PRIMARY KEY ("UNAME"),
  CONSTRAINT "fk_USERS_AUTHL"
    FOREIGN KEY ("AUTH")
    REFERENCES "AUTHL" ("UAUTH"))
ENGINE = InnoDB;

-- -----------------------------------------------------
-- Table "TICKETING_SYS"."STATES"
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS "STATES" (
  "TSTATE" VARCHAR(15) NOT NULL,
  PRIMARY KEY ("TSTATE"))
ENGINE = InnoDB;

-- -----------------------------------------------------
-- Table "TICKETING_SYS"."TICKETS"
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS "TICKETS" (
  "ID" INT NOT NULL AUTO_INCREMENT,
  "COMPLAINT" VARCHAR(300) NOT NULL,
  "STATES_TSTATE" VARCHAR(15) NOT NULL,
  PRIMARY KEY ("ID"),
  CONSTRAINT "fk_TICKETS_STATES1"
    FOREIGN KEY ("STATES_TSTATE")
    REFERENCES "STATES" ("TSTATE"))
ENGINE = InnoDB;

-- -----------------------------------------------------
-- Table "TICKETING_SYS"."UID"
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS "UID" (
  "TICKETS_ID" INT NOT NULL,
  "USERS_UNAME" VARCHAR(50) NOT NULL,
  PRIMARY KEY ("TICKETS_ID", "USERS_UNAME"),
  CONSTRAINT "fk_TICKETS_has_USERS_TICKETS1"
    FOREIGN KEY ("TICKETS_ID")
    REFERENCES "TICKETS" ("ID"),
  CONSTRAINT "fk_TICKETS_has_USERS_USERS1"
    FOREIGN KEY ("USERS_UNAME")
    REFERENCES "USERS" ("UNAME"))
ENGINE = InnoDB;

-- -----------------------------------------------------
-- View "TICKETING_SYS"."UTickets"
-- -----------------------------------------------------
CREATE VIEW UTICKETS AS  
SELECT A.UNAME, C.ID, C.COMPLAINT, C.STATES_TSTATE  
FROM USERS A, UID B, TICKETS C 
WHERE A.UNAME = B.USERS_UNAME AND B.TICKETS_ID = C.ID;
SET SQL_MODE=@OLD_SQL_MODE;  


-- -----------------------------------------------------
-- Static Data INIT
-- -----------------------------------------------------
INSERT INTO "AUTHL" VALUES  ("USER"), ("ADMIN");

INSERT INTO "STATES" VALUES  ("OPEN"), ("PROCESSING"), ("CLOSED");
INSERT INTO "USERS" VALUES ("admin","21232f297a57a5a743894a0e4a801fc3","ADMIN");