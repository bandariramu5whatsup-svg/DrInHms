-- Units Table (PascalCase + Audit Fields)
GO
CREATE SEQUENCE UnitId
    INCREMENT BY 1
    START WITH 1
    NO MAXVALUE
    NO CYCLE
    NO CACHE;

CREATE TABLE Units (
    UnitId          VARCHAR(50) NOT NULL PRIMARY KEY,
    UnitName        VARCHAR(200),
    Description     VARCHAR(MAX),
    IsActive        NUMERIC(1),

    CreatedAt       DATETIME DEFAULT(GETDATE()),
    UpdatedAt       DATETIME NULL,
    CreatedById     VARCHAR(60),
    CreatedByName   VARCHAR(100),
    UpdatedById     VARCHAR(60),
    UpdatedByName   VARCHAR(100),
    WorkstationId   VARCHAR(100)
);
GO