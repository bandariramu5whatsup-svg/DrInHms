GO
CREATE OR ALTER PROCEDURE SpOpGetCountries
(
    @CountryId        VARCHAR(30) = NULL,
    @CountryName      VARCHAR(100) = NULL,
    @IsActive          NUMERIC(1),
    @IsDefaultCountry  NUMERIC(1)
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        CountryId,
        CountryName,
        CountryCode,
        IsActive,
        IsDefaultCountry,
        CreatedAt,
        UpdatedAt,
        CreatedById,
        CreatedByName,
        UpdatedById,
        UpdatedByName,
        WorkstationId
    FROM Countries
    WHERE
        (@CountryId IS NULL OR @CountryId = '' OR CountryId = @CountryId)
        AND (@CountryName IS NULL OR CountryName LIKE '%' + @CountryName + '%')
        AND (@IsActive IS NULL OR IsActive = @IsActive)
        AND (@IsDefaultCountry IS NULL OR IsDefaultCountry = @IsDefaultCountry)
    ORDER BY CountryName ASC;
END;
GO
