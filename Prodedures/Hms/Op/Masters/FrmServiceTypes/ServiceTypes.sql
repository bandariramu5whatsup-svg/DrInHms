-- ServiceTypes Table (PascalCase + Audit Fields)
CREATE SEQUENCE ServiceTypeId
    INCREMENT BY 1
    START WITH 1
    NO MAXVALUE
    NO CYCLE
    NO CACHE;

CREATE TABLE ServiceTypes (
    ServiceTypeId      VARCHAR(50) NOT NULL PRIMARY KEY,
    ServiceTypeName    VARCHAR(200),
    Description        VARCHAR(200),
    IsActive            NUMERIC(1),

    CreatedAt          DATETIME DEFAULT(GETDATE()),
    UpdatedAt          DATETIME NULL,
    CreatedById        VARCHAR(60) NULL,
    CreatedByName      VARCHAR(100) NULL,
    UpdatedById        VARCHAR(60) NULL,
    UpdatedByName      VARCHAR(100) NULL,
    WorkstationId      VARCHAR(100) NULL
); 