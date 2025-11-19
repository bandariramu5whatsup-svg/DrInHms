GO
CREATE OR ALTER PROCEDURE SpOpInsertState
(
    @StateId         VARCHAR(20),     -- ❗ INPUT ONLY (NOT OUTPUT)
    @StateName       VARCHAR(100),
    @StateCode       VARCHAR(20),
    @IsActive        NUMERIC(1),
    @IsDefaultState  NUMERIC(1),
    @CountryId       VARCHAR(30),
    @CountryName     VARCHAR(150),

    @UserId          VARCHAR(60),
    @UserName        VARCHAR(100),
    @WorkstationId   VARCHAR(60)
)
AS
BEGIN
    SET NOCOUNT ON;

    -------------------------------------------------------
    -- 🔥 DUPLICATE NAME CHECK
    -------------------------------------------------------
    IF EXISTS
    (
        SELECT 1
        FROM States
        WHERE UPPER(LTRIM(RTRIM(StateName))) = UPPER(LTRIM(RTRIM(@StateName)))
          AND CountryId = @CountryId
          AND StateId <> @StateId   -- ✔ safe now
    )
    BEGIN
        RAISERROR('State name already exists in this country!', 16, 1);
        RETURN;
    END

    -------------------------------------------------------
    -- 🔥 DUPLICATE CODE CHECK
    -------------------------------------------------------
    IF EXISTS
    (
        SELECT 1
        FROM States
        WHERE UPPER(LTRIM(RTRIM(StateCode))) = UPPER(LTRIM(RTRIM(@StateCode)))
          AND CountryId = @CountryId
          AND StateId <> @StateId
    )
    BEGIN
        RAISERROR('State code already exists in this country!', 16, 1);
        RETURN;
    END

    -------------------------------------------------------
    -- 🔥 UPDATE
    -------------------------------------------------------
    IF EXISTS (SELECT 1 FROM States WHERE StateId = @StateId)
    BEGIN
        UPDATE States
        SET
            StateName       = @StateName,
            StateCode       = @StateCode,
            IsActive        = @IsActive,
            IsDefaultState  = @IsDefaultState,
            CountryId       = @CountryId,
            CountryName     = @CountryName,
            UpdatedAt       = GETDATE(),
            UpdatedById     = @UserId,
            UpdatedByName   = @UserName,
            WorkstationId   = @WorkstationId
        WHERE StateId = @StateId;

        SELECT @StateId AS StateId;
        RETURN;
    END

    -------------------------------------------------------
    -- 🔥 INSERT
    -------------------------------------------------------
    DECLARE @NewStateId VARCHAR(20) = 'ST' + CAST(NEXT VALUE FOR StateId AS VARCHAR);

    INSERT INTO States
    (
        StateId, StateName, StateCode, IsActive, IsDefaultState,
        CountryId, CountryName, CreatedAt, CreatedById,
        CreatedByName, WorkstationId
    )
    VALUES
    (
        @NewStateId, @StateName, @StateCode, @IsActive, @IsDefaultState,
        @CountryId, @CountryName, GETDATE(), @UserId,
        @UserName, @WorkstationId
    );

    SELECT @NewStateId AS StateId;
END;
GO
