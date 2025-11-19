GO
CREATE OR ALTER PROCEDURE SpOpInsertExpensesPaidTo
(
    @ExpensesPaidToId        VARCHAR(30) OUTPUT,
    @ExpensesPaidToName      VARCHAR(100),
    @ExpensesPaidToCode      VARCHAR(20),
    @IsActive                NUMERIC(1),

    @UserId                  VARCHAR(60),
    @UserName                VARCHAR(100),
    @WorkstationId           VARCHAR(100)
)
AS
BEGIN
    SET NOCOUNT ON;

    -- UPDATE CASE
    IF EXISTS (SELECT 1 FROM ExpensesPaidTo WHERE ExpensesPaidToId = @ExpensesPaidToId)
    BEGIN
        UPDATE ExpensesPaidTo
        SET 
            ExpensesPaidToName = @ExpensesPaidToName,
            ExpensesPaidToCode = @ExpensesPaidToCode,
            IsActive           = @IsActive,

            UpdatedAt          = GETDATE(),
            UpdatedById        = @UserId,
            UpdatedByName      = @UserName,
            WorkstationId      = @WorkstationId
        WHERE ExpensesPaidToId  = @ExpensesPaidToId;

        SELECT @ExpensesPaidToId AS ExpensesPaidToId;
        RETURN;
    END

    -- INSERT CASE
    SET @ExpensesPaidToId = CONCAT('EPT', NEXT VALUE FOR ExpensesPaidToId);

    INSERT INTO ExpensesPaidTo
    (
        ExpensesPaidToId,
        ExpensesPaidToName,
        ExpensesPaidToCode,
        IsActive,
        CreatedAt,
        CreatedById,
        CreatedByName,
        WorkstationId
    )
    VALUES
    (
        @ExpensesPaidToId,
        @ExpensesPaidToName,
        @ExpensesPaidToCode,
        @IsActive,
        GETDATE(),
        @UserId,
        @UserName,
        @WorkstationId
    );

    SELECT @ExpensesPaidToId AS ExpensesPaidToId;
END;
GO
