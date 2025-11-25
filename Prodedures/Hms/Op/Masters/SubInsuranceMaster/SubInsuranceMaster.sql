GO
CREATE SEQUENCE SubInsuranceId
    INCREMENT BY 1
    START WITH 1
    NO MAXVALUE
    NO CYCLE
    NO CACHE;
GO

CREATE TABLE SubInsuranceMaster
(
    SubInsuranceId         VARCHAR(30)     NOT NULL PRIMARY KEY,
    SubInsuranceName       VARCHAR(100)    NULL,
    SubInsuranceCode       VARCHAR(20)     NULL,
	InsuranceId     VARCHAR(20)     NULL,  
    IsActive              NUMERIC(1),
   

    CreatedAt         DATETIME        DEFAULT(GETDATE()),
    UpdatedAt         DATETIME        NULL,
    CreatedById       VARCHAR(60)     NULL,
    CreatedByName     VARCHAR(100)    NULL,
    UpdatedById       VARCHAR(60)     NULL,
    UpdatedByName     VARCHAR(100)    NULL,
    WorkstationId     VARCHAR(100)    NULL
);
GO
 