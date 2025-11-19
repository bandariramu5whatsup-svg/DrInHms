-- Insert + Update Procedure for Districts
GO
CREATE OR ALTER PROCEDURE SpOpInsertDistrict
(
    @DistrictId        VARCHAR(20) OUTPUT,
    @DistrictName      VARCHAR(100),
    @DistrictCode      VARCHAR(20),
    @IsActive          NUMERIC(1),
    @IsDefaultDistrict  NUMERIC(1),
    @CountryId         VARCHAR(30),
    @StateId           VARCHAR(20),
    @StateName         VARCHAR(150),

    @UserId            VARCHAR(60),
    @UserName          VARCHAR(100),
    @WorkstationId     VARCHAR(60)
)
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM Districts WHERE DistrictId = @DistrictId)
    BEGIN
        UPDATE Districts
        SET
            DistrictName      = @DistrictName,
            DistrictCode      = @DistrictCode,
            IsActive          = @IsActive,
            IsDefaultDistrict = @IsDefaultDistrict,
            CountryId         = @CountryId,
            StateId           = @StateId,
            StateName         = @StateName,

            UpdatedAt         = GETDATE(),
            UpdatedById       = @UserId,
            UpdatedByName     = @UserName,
            WorkstationId     = @WorkstationId
        WHERE DistrictId      = @DistrictId;

        SELECT @DistrictId AS DistrictId;
        RETURN;
    END

    SET @DistrictId = CONCAT('DT', NEXT VALUE FOR DistrictId);

    INSERT INTO Districts
    (
        DistrictId,
        DistrictName,
        DistrictCode,
        IsActive,
        IsDefaultDistrict,
        CountryId,
        StateId,
        StateName,
        CreatedAt,
        CreatedById,
        CreatedByName,
        WorkstationId
    )
    VALUES
    (
        @DistrictId,
        @DistrictName,
        @DistrictCode,
        @IsActive,
        @IsDefaultDistrict,
        @CountryId,
        @StateId,
        @StateName,
        GETDATE(),
        @UserId,
        @UserName,
        @WorkstationId
    );

    SELECT @DistrictId AS DistrictId;
END;
GO