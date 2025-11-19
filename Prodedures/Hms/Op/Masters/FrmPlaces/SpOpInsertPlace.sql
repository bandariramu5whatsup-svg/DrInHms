-- Insert + Update Procedure for Place
GO
CREATE OR ALTER PROCEDURE SpOpInsertPlace
(
    @PlaceId         VARCHAR(30) OUTPUT,
    @PlaceName       VARCHAR(100),
    @PlaceCode       VARCHAR(20),
    @IsActive        BIT,
    @IsDefaultPlace  BIT,
    @AreaId          VARCHAR(30),
    @DistrictId      VARCHAR(30),
    @StateId         VARCHAR(30),
    @CountryId       VARCHAR(30),

    @UserId          VARCHAR(60),
    @UserName        VARCHAR(100),
    @WorkstationId   VARCHAR(100)
)
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM Place WHERE PlaceId = @PlaceId)
    BEGIN
        UPDATE Place
        SET
            PlaceName       = @PlaceName,
            PlaceCode       = @PlaceCode,
            IsActive        = @IsActive,
            IsDefaultPlace  = @IsDefaultPlace,
            AreaId          = @AreaId,
            DistrictId      = @DistrictId,
            StateId         = @StateId,
            CountryId       = @CountryId,

            UpdatedAt       = GETDATE(),
            UpdatedById     = @UserId,
            UpdatedByName   = @UserName,
            WorkstationId   = @WorkstationId
        WHERE PlaceId = @PlaceId;

        SELECT @PlaceId AS PlaceId;
        RETURN;
    END

    SET @PlaceId = CONCAT('PL', NEXT VALUE FOR PlaceId);

    INSERT INTO Place
    (
        PlaceId,
        PlaceName,
        PlaceCode,
        IsActive,
        IsDefaultPlace,
        AreaId,
        DistrictId,
        StateId,
        CountryId,
        CreatedAt,
        CreatedById,
        CreatedByName,
        WorkstationId
    )
    VALUES
    (
        @PlaceId,
        @PlaceName,
        @PlaceCode,
        @IsActive,
        @IsDefaultPlace,
        @AreaId,
        @DistrictId,
        @StateId,
        @CountryId,
        GETDATE(),
        @UserId,
        @UserName,
        @WorkstationId
    );

    SELECT @PlaceId AS PlaceId;
END;
GO