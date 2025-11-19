-- Get Procedure for Districts
go
CREATE OR ALTER PROCEDURE SpOpGetDistricts
(
    @DistrictId        VARCHAR(20) = NULL,
    @DistrictName      VARCHAR(100) = NULL,
    @IsActive          NUMERIC(1),
    @IsDefaultDistrict  NUMERIC(1),
    @StateId           VARCHAR(20) = NULL,
    @CountryId         VARCHAR(30) = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        DistrictId,
        DistrictName,
        DistrictCode,
        IsActive,
        IsDefaultDistrict,
        CountryId,
        StateId,
        StateName,
        CreatedAt,
        UpdatedAt,
        CreatedById,
        CreatedByName,
        UpdatedById,
        UpdatedByName,
        WorkstationId
    FROM Districts
    WHERE
        --(@DistrictId IS NULL OR @DistrictId = '' OR DistrictId = @DistrictId)
        --AND
		
		(@DistrictName IS NULL OR DistrictName LIKE '%' + @DistrictName + '%')
        --AND (@IsActive IS NULL OR IsActive = @IsActive)
        --AND (@IsDefaultDistrict IS NULL OR IsDefaultDistrict = @IsDefaultDistrict)
        --AND (@StateId IS NULL OR @StateId = '' OR StateId = @StateId)
        --AND (@CountryId IS NULL OR @CountryId = '' OR CountryId = @CountryId)
    ORDER BY DistrictName;
END;
go