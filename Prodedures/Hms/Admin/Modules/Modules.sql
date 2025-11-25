go
CREATE TABLE Modules (
    ModuleId VARCHAR(50) PRIMARY KEY,
    ModuleName VARCHAR(100) NOT NULL,
    DisplayOrder INT,
    IsActive NUMERIC(1),

    CreatedAt         DATETIME        DEFAULT(GETDATE()),
    UpdatedAt         DATETIME        NULL,
    CreatedById       VARCHAR(60)     NULL,
    CreatedByName     VARCHAR(100)    NULL,
    UpdatedById       VARCHAR(60)     NULL,
    UpdatedByName     VARCHAR(100)    NULL,
    WorkstationId     VARCHAR(100)    NULL
);

go
INSERT INTO Modules (ModuleId, ModuleName, DisplayOrder, IsActive)
VALUES 
('OP', 'OP', 1, 1);
