-- Insert + Update Procedure for MarketingExecutive
GO
CREATE OR ALTER PROCEDURE SpOpInsertMarketingExecutive
(
    @MarketingExecutiveId  VARCHAR(30) OUTPUT,
    @MarketingPersonName   VARCHAR(300),
    @MailId                VARCHAR(150),
    @Mobile                VARCHAR(20),
    @Dob                   DATETIME,
    @Address               VARCHAR(250),
    @CountryId             VARCHAR(30),
    @StateId               VARCHAR(30),
    @DistrictId            VARCHAR(30),
    @AreaId                VARCHAR(30),
    @PlaceId               VARCHAR(30),
    @IsActive              BIT,

    @UserId                VARCHAR(60),
    @UserName              VARCHAR(100),
    @WorkstationId         VARCHAR(100)
)
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM MarketingExecutive WHERE MarketingExecutiveId = @MarketingExecutiveId)
    BEGIN
        UPDATE MarketingExecutive
        SET
            MarketingPersonName  = @MarketingPersonName,
            MailId               = @MailId,
            Mobile               = @Mobile,
            Dob                  = @Dob,
            Address              = @Address,
            CountryId            = @CountryId,
            StateId              = @StateId,
            DistrictId           = @DistrictId,
            AreaId               = @AreaId,
            PlaceId              = @PlaceId,
            IsActive             = @IsActive,

            UpdatedAt            = GETDATE(),
            UpdatedById          = @UserId,
            UpdatedByName        = @UserName,
            WorkstationId        = @WorkstationId
        WHERE MarketingExecutiveId = @MarketingExecutiveId;

        SELECT @MarketingExecutiveId AS MarketingExecutiveId;
        RETURN;
    END

    SET @MarketingExecutiveId = CONCAT('ME', NEXT VALUE FOR MarketingExecutiveId);

    INSERT INTO MarketingExecutive
    (
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
        CreatedById,
        CreatedByName,
        WorkstationId
    )
    VALUES
    (
        @MarketingExecutiveId,
        @MarketingPersonName,
        @MailId,
        @Mobile,
        @Dob,
        @Address,
        @CountryId,
        @StateId,
        @DistrictId,
        @AreaId,
        @PlaceId,
        @IsActive,
        GETDATE(),
        @UserId,
        @UserName,
        @WorkstationId
    );

    SELECT @MarketingExecutiveId AS MarketingExecutiveId;
END;
GO