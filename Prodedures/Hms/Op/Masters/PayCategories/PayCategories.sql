GO
CREATE SEQUENCE PayCategoryId
    INCREMENT BY 1
    START WITH 1
    NO MAXVALUE
    NO CYCLE
    NO CACHE;
GO

CREATE TABLE PayCategories
(
    PayCategoryId         VARCHAR(30)     NOT NULL PRIMARY KEY,
    PayCategoryName       VARCHAR(100)    NULL,
    PayCategoryCode       VARCHAR(20)     NULL,
	PayCategoryOption     VARCHAR(20)     NULL, -- O Cash ,1-Credit.2-Free , 3-Insurance
	PayCategoryOptionText VARCHAR(20)     NULL,-- 0
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
 