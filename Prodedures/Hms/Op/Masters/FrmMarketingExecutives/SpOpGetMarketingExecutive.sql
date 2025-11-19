-- Get Procedure for MarketingExecutive
GO
CREATE OR ALTER PROCEDURE SpOpGetMarketingExecutive
(
    @MarketingExecutiveId  VARCHAR(30) = NULL,
    @MarketingPersonName   VARCHAR(300) = NULL,
    @Mobile                VARCHAR(20) = NULL,
    @IsActive              BIT = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        MarketingExecutiveId,
        MarketingPersonName,
        MailId,
        Mobile,
        Dob,
        Address,
        CountryId,
        StateId,
        DistrictId,
        AreaId,
        PlaceId,
        IsActive,
        CreatedAt,
        UpdatedAt,
        CreatedById,
        CreatedByName,
        UpdatedById,
        UpdatedByName,
        WorkstationId
    FROM MarketingExecutive
    WHERE
        (@MarketingExecutiveId IS NULL OR @MarketingExecutiveId = '' OR MarketingExecutiveId = @MarketingExecutiveId)
        AND (@MarketingPersonName IS NULL OR MarketingPersonName LIKE '%' + @MarketingPersonName + '%')
        AND (@Mobile IS NULL OR Mobile LIKE '%' + @Mobile + '%')
        AND (@IsActive IS NULL OR IsActive = @IsActive)
    ORDER BY MarketingPersonName;
END;
GO