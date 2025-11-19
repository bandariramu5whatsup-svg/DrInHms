GO
CREATE OR ALTER PROCEDURE SpOpInsertCountry
(
    @CountryId        VARCHAR(30) OUTPUT,
    @CountryName      VARCHAR(100),
    @CountryCode      VARCHAR(20),
    @IsActive         BIT,
    @IsDefaultCountry BIT,

    @UserId           VARCHAR(60),
    @UserName         VARCHAR(100),
    @WorkstationId    VARCHAR(100)
)
AS
BEGIN
    SET NOCOUNT ON;

    -------------------------------------------------------
    -- 1. UPDATE EXISTING
    -------------------------------------------------------
    IF EXISTS (SELECT 1 FROM Countries WHERE CountryId = @CountryId)
    BEGIN
        UPDATE Countries
        SET 
            CountryName       = @CountryName,
            CountryCode       = @CountryCode,
            IsActive          = @IsActive,
            IsDefaultCountry  = @IsDefaultCountry,

            UpdatedAt         = GETDATE(),
            UpdatedById       = @UserId,
            UpdatedByName     = @UserName,
            WorkstationId     = @WorkstationId
        WHERE CountryId = @CountryId;

        SELECT @CountryId AS CountryId;
        RETURN;
    END


    -------------------------------------------------------
    -- 2. INSERT NEW RECORD
    -------------------------------------------------------
    SET @CountryId = CONCAT('CT', NEXT VALUE FOR CountryId);

    INSERT INTO Countries
    (
        CountryId,
        CountryName,
        CountryCode,
        IsActive,
        IsDefaultCountry,
        CreatedAt,
        CreatedById,
        CreatedByName,
        WorkstationId
    )
    VALUES
    (
        @CountryId,
        @CountryName,
        @CountryCode,
        @IsActive,
        @IsDefaultCountry,
        GETDATE(),
        @UserId,
        @UserName,
        @WorkstationId
    );

    SELECT @CountryId AS CountryId;
END;
GO


--GO

--GO
--CREATE OR ALTER PROCEDURE SP_GET_COUNTRY
--    @COUNTRY_NAME NVARCHAR(255) = NULL,
--    @COUNTRY_ID NVARCHAR(255) = NULL,
--    @ACTIVE BIT = NULL,
--    @DEFAULT_COUNTRY BIT = NULL
--AS
--BEGIN
--    SET NOCOUNT ON;

--    SELECT
--        COUNTRY_ID,
--        COUNTRY_NAME,
--        COUNTRY_CODE,
--        ACTIVE,
--        DEFAULT_COUNTRY,
--        ENTRY_USER_NAME,
--        ENTRY_USER_ID,
--        ENTRY_DATE,
--        EDIT_USER_ID,
--        EDIT_USER_NAME,
--        EDIT_DATE,
--        TERMINAL_ID,
--        PROFILE_ID 
--    FROM COUNTRY
--    WHERE
--        -- Optional filters
--        (@COUNTRY_NAME IS NULL OR COUNTRY_NAME LIKE '%' + @COUNTRY_NAME + '%')
--        AND (@COUNTRY_ID IS NULL OR @COUNTRY_ID = '' OR COUNTRY_ID = @COUNTRY_ID)
--        AND (@ACTIVE IS NULL OR ACTIVE = @ACTIVE)
--        AND (@DEFAULT_COUNTRY IS NULL OR DEFAULT_COUNTRY = @DEFAULT_COUNTRY)
--    ORDER BY COUNTRY_NAME ASC;
--END;


-- GO