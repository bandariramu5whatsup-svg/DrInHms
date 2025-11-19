GO
CREATE OR ALTER PROCEDURE SpOpGetAreas
    @AreaName NVARCHAR(255) = NULL,
    @AreaId NVARCHAR(255) = NULL,
    @DistrictId NVARCHAR(255) = NULL,
    @StateId NVARCHAR(255) = NULL,
    @CountryId NVARCHAR(255) = NULL,
    @IsActive BIT = NULL,
    @IsDefaultArea BIT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        AreaId,
        AreaName,
        AreaCode,
        CountryId,
        StateId,
        DistrictId,
        IsActive,
        IsDefaultArea,
        
        -- New Audit Columns
        CreatedAt,
        UpdatedAt,
        CreatedByName,
        UpdatedByName,
        CreatedById,
        UpdatedById,
        WorkstationId

    FROM Areas
    WHERE
        (@AreaName IS NULL OR AreaName LIKE '%' + @AreaName + '%')
        AND (@AreaId IS NULL OR @AreaId = '' OR AreaId = @AreaId)
        AND (@DistrictId IS NULL OR @DistrictId = '' OR DistrictId = @DistrictId)
        AND (@StateId IS NULL OR @StateId = '' OR StateId = @StateId)
        AND (@CountryId IS NULL OR @CountryId = '' OR CountryId = @CountryId)
        AND (@IsActive IS NULL OR IsActive = @IsActive)
        AND (@IsDefaultArea IS NULL OR IsDefaultArea = @IsDefaultArea)
    ORDER BY AreaName ASC;
END;
GO

 