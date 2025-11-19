-- Get Procedure for Place
GO
CREATE OR ALTER PROCEDURE SpOpGetPlace
(
    @PlaceId        VARCHAR(30) = NULL,
    @PlaceName      VARCHAR(100) = NULL,
    @IsActive       BIT = NULL,
    @IsDefaultPlace BIT = NULL,
    @AreaId         VARCHAR(30) = NULL,
    @DistrictId     VARCHAR(30) = NULL,
    @StateId        VARCHAR(30) = NULL,
    @CountryId      VARCHAR(30) = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
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
        UpdatedAt,
        CreatedById,
        CreatedByName,
        UpdatedById,
        UpdatedByName,
        WorkstationId
    FROM Place
    WHERE
        (@PlaceId IS NULL OR @PlaceId = '' OR PlaceId = @PlaceId)
        AND (@PlaceName IS NULL OR PlaceName LIKE '%' + @PlaceName + '%')
        AND (@IsActive IS NULL OR IsActive = @IsActive)
        AND (@IsDefaultPlace IS NULL OR IsDefaultPlace = @IsDefaultPlace)
        AND (@AreaId IS NULL OR AreaId = @AreaId)
        AND (@DistrictId IS NULL OR DistrictId = @DistrictId)
        AND (@StateId IS NULL OR StateId = @StateId)
        AND (@CountryId IS NULL OR CountryId = @CountryId)
    ORDER BY PlaceName;
END;
GO