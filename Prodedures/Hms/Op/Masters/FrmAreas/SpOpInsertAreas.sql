--DECLARE @NewId VARCHAR(20);

--EXEC SpInsertAreas
--     @AreaId = @NewId OUTPUT,
--     @AreaName = 'Madhapur',
--     @AreaCode = 'MDP',
--     @IsActive = 1,
--     @IsDefaultArea = 1,
--     @CountryId = 'CNT00001',
--     @StateId = 'STT00001',
--     @DistrictId = 'DST00001',
--     @CreatedByName = 'Admin',
--     @CreatedById = 'USR001',
--     @WorkstationId = 'SERVER01';

--SELECT @NewId;

GO
CREATE OR ALTER PROCEDURE SpOpInsertAreas
(
    @AreaId         VARCHAR(20) OUTPUT,
    @AreaName       VARCHAR(100),
    @AreaCode       VARCHAR(20),
    @IsActive       BIT,
    @IsDefaultArea  BIT,
    @CountryId      VARCHAR(30),
    @StateId        VARCHAR(20),
    @DistrictId     VARCHAR(20),

    @CreatedByName  VARCHAR(60),
    @CreatedById    VARCHAR(60),
    @WorkstationId  VARCHAR(60)
)
AS
BEGIN
    SET NOCOUNT ON;

    -------------------------------------------------------
    -- 1. UPDATE EXISTING RECORD
    -------------------------------------------------------
    IF EXISTS (SELECT 1 FROM Areas WHERE AreaId = @AreaId)
    BEGIN
        UPDATE Areas
        SET 
            AreaName        = @AreaName,
            AreaCode        = @AreaCode,
            IsActive        = @IsActive,
            IsDefaultArea   = @IsDefaultArea,
            CountryId       = @CountryId,
            StateId         = @StateId,
            DistrictId      = @DistrictId,

            UpdatedById     = @CreatedById,
            UpdatedByName   = @CreatedByName,
            UpdatedAt       = GETDATE(),
            WorkstationId   = @WorkstationId
        WHERE AreaId = @AreaId;

        SELECT @AreaId AS AreaId;
        RETURN;
    END


    -------------------------------------------------------
    -- 2. INSERT NEW RECORD – Generate New AreaId
    -------------------------------------------------------

    DECLARE @Seq BIGINT = NEXT VALUE FOR AreaIdSeq;

    -- Example: AR00001
    SET @AreaId = 'AR' + RIGHT('00000' + CAST(@Seq AS VARCHAR(10)), 5);

    INSERT INTO Areas
    (
        AreaId,
        AreaName,
        AreaCode,
        IsActive,
        IsDefaultArea,
        CountryId,
        StateId,
        DistrictId,
        CreatedAt,
        CreatedByName,
        CreatedById,
        WorkstationId
    )
    VALUES
    (
        @AreaId,
        @AreaName,
        @AreaCode,
        @IsActive,
        @IsDefaultArea,
        @CountryId,
        @StateId,
        @DistrictId,
        GETDATE(),
        @CreatedByName,
        @CreatedById,
        @WorkstationId
    );

    SELECT @AreaId AS AreaId;
END;
GO

 