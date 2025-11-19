-- Get Procedure for States
GO
CREATE OR ALTER PROCEDURE SpOpGetStates
(
    @StateId         VARCHAR(20) = NULL,
    @StateName       VARCHAR(100) = NULL,
    @IsActive        NUMERIC(1),
    @IsDefaultState  NUMERIC(1),
    @CountryId       VARCHAR(30) = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        StateId,
        StateName,
        StateCode,
        IsActive,
        IsDefaultState,
        CountryId,
        CountryName,
        CreatedAt,
        UpdatedAt,
        CreatedById,
        CreatedByName,
        UpdatedById,
        UpdatedByName,
        WorkstationId
    FROM States
    WHERE
        --(@StateId IS NULL OR @StateId = '' OR StateId = @StateId)
        --AND 
		
		(@StateName IS NULL OR StateName LIKE '%' + @StateName + '%')
        --AND (@IsActive IS NULL OR IsActive = @IsActive)
        --AND (@IsDefaultState IS NULL OR IsDefaultState = @IsDefaultState)
        --AND (@CountryId IS NULL OR CountryId = @CountryId)
    ORDER BY StateName;
END;
GO