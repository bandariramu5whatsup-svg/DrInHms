GO
CREATE OR ALTER PROCEDURE SpOpGetSubInsurance
(
    @SubInsuranceId      VARCHAR(30) = NULL,
    @SubInsuranceName    VARCHAR(100) = NULL,
    @InsuranceId         VARCHAR(20) = NULL,
    @IsActive            NUMERIC(1) = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        SubInsuranceId,
        SubInsuranceName,
        SubInsuranceCode,
        InsuranceId,
        IsActive,
        CreatedAt,
        UpdatedAt,
        CreatedById,
        CreatedByName,
        UpdatedById,
        UpdatedByName,
        WorkstationId
    FROM SubInsuranceMaster
    WHERE
        (@SubInsuranceId IS NULL OR @SubInsuranceId = '' OR SubInsuranceId = @SubInsuranceId)
        AND (@SubInsuranceName IS NULL OR SubInsuranceName LIKE '%' + @SubInsuranceName + '%')
        AND (@InsuranceId IS NULL OR InsuranceId = @InsuranceId)
        --AND (@IsActive IS NULL OR IsActive = @IsActive)
    ORDER BY SubInsuranceName;
END;
GO
