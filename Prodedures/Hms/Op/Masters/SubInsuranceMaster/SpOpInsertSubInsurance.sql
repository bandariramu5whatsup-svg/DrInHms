GO
CREATE OR ALTER PROCEDURE SpOpInsertSubInsurance
(
    @SubInsuranceId        VARCHAR(30) OUTPUT,
    @SubInsuranceName      VARCHAR(100),
    @SubInsuranceCode      VARCHAR(20),
    @InsuranceId           VARCHAR(20),
    @IsActive              NUMERIC(1),

    @UserId                VARCHAR(60),
    @UserName              VARCHAR(100),
    @WorkstationId         VARCHAR(100)
)
AS
BEGIN
    SET NOCOUNT ON;

    -- UPDATE CASE
    IF EXISTS (SELECT 1 FROM SubInsuranceMaster WHERE SubInsuranceId = @SubInsuranceId)
    BEGIN
        UPDATE SubInsuranceMaster
        SET 
            SubInsuranceName  = @SubInsuranceName,
            SubInsuranceCode  = @SubInsuranceCode,
            InsuranceId       = @InsuranceId,
            IsActive          = @IsActive,

            UpdatedAt         = GETDATE(),
            UpdatedById       = @UserId,
            UpdatedByName     = @UserName,
            WorkstationId     = @WorkstationId
        WHERE SubInsuranceId = @SubInsuranceId;

        SELECT @SubInsuranceId AS SubInsuranceId;
        RETURN;
    END

    -- INSERT CASE
    SET @SubInsuranceId = CONCAT('SIM', NEXT VALUE FOR SubInsuranceId);

    INSERT INTO SubInsuranceMaster
    (
        SubInsuranceId,
        SubInsuranceName,
        SubInsuranceCode,
        InsuranceId,
        IsActive,
        CreatedAt,
        CreatedById,
        CreatedByName,
        WorkstationId
    )
    VALUES
    (
        @SubInsuranceId,
        @SubInsuranceName,
        @SubInsuranceCode,
        @InsuranceId,
        @IsActive,
        GETDATE(),
        @UserId,
        @UserName,
        @WorkstationId
    );

    SELECT @SubInsuranceId AS SubInsuranceId;
END;
GO
