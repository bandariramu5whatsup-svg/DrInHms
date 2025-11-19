-- Units Table (PascalCase + Audit Fields)
GO
CREATE SEQUENCE UserId
    INCREMENT BY 1
    START WITH 1
    NO MAXVALUE
    NO CYCLE
    NO CACHE;

CREATE TABLE Users (
    UserId          VARCHAR(50) NOT NULL PRIMARY KEY,
    UserName        VARCHAR(200),
    Password        VARCHAR(MAX),
    IsActive        NUMERIC(1),
	UserType        NUMERIC(1),
	LoginType        NUMERIC(1),
    CreatedAt       DATETIME DEFAULT(GETDATE()),
    UpdatedAt       DATETIME NULL,
    CreatedById     VARCHAR(60),
    CreatedByName   VARCHAR(100),
    UpdatedById     VARCHAR(60),
    UpdatedByName   VARCHAR(100),
    WorkstationId   VARCHAR(100)
);
GO

INSERT INTO Users (
    UserId, 
    UserName, 
    Password, 
    IsActive, 
    UserType,
    LoginType,
    CreatedAt, 
    UpdatedAt, 
    CreatedById, 
    CreatedByName, 
    UpdatedById, 
    UpdatedByName, 
    WorkstationId
)
VALUES (
    'U0',
    'admin',
    'Admin@123',      -- ideally hash this
    1,                -- IsActive
    1,                -- UserType (example: 1=Admin,2 User)
    1,                -- LoginType (example: 1=Normal login,2=Doctor login,3=Patient login)
    GETDATE(),
    NULL,
    'SYS001',
    'System Admin',
    NULL,
    NULL,
    'WS-01'
);
