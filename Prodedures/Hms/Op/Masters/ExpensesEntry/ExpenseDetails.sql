GO
CREATE SEQUENCE ExpenseDetailsID
    INCREMENT BY 1
    START WITH 1
    NO MAXVALUE
    NO CYCLE
    NO CACHE;
GO

CREATE TABLE ExpenseDetails
(
    ExpenseDetailsID VARCHAR(50) PRIMARY KEY DEFAULT('DTL' + RIGHT('' + CAST(NEXT VALUE FOR ExpenseDetailsID AS VARCHAR(5)), 5)),
    ExpenseHeaderID  VARCHAR(50) NOT NULL,
    ExpenseTypeID    INT NOT NULL,
    Amount           DECIMAL(18,2) NOT NULL,
    Remarks          VARCHAR(300) NULL,

    CreatedAt       DATETIME DEFAULT(GETDATE()),
    UpdatedAt       DATETIME NULL,
    CreatedByName   VARCHAR(60) NULL,
    UpdatedByName   VARCHAR(60) NULL,
    CreatedById     VARCHAR(60) NULL,
    UpdatedById     VARCHAR(60) NULL,
    WorkstationId   VARCHAR(60) NULL
);
GO
