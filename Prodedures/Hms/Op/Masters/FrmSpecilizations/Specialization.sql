-- Specialization Table (PascalCase + Audit Fields)
CREATE SEQUENCE SpecializationId
    INCREMENT BY 1
    START WITH 1
    NO MAXVALUE
    NO CYCLE
    NO CACHE;

CREATE TABLE Specialization (
    SpecializationId   VARCHAR(30)     NOT NULL PRIMARY KEY,
    DepartmentsName    VARCHAR(100)    NULL,
    DepartmentsCode    VARCHAR(20)     NULL,
    IsActive            NUMERIC(1),
    DepartmentId       VARCHAR(30)     NULL,

    CreatedAt          DATETIME        DEFAULT(GETDATE()),
    UpdatedAt          DATETIME        NULL,
    CreatedById        VARCHAR(60)     NULL,
    CreatedByName      VARCHAR(100)    NULL,
    UpdatedById        VARCHAR(60)     NULL,
    UpdatedByName      VARCHAR(100)    NULL,
    WorkstationId      VARCHAR(100)    NULL
);
