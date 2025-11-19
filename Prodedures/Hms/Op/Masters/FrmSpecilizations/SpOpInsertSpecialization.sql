-- Insert + Update Procedure for Specialization
GO
CREATE OR ALTER PROCEDURE SpOpInsertSpecialization
(
    @SpecializationId  VARCHAR(30) OUTPUT,
    @DepartmentsName   VARCHAR(100),
    @DepartmentsCode   VARCHAR(20),
    @IsActive          BIT,
    @DepartmentId      VARCHAR(30),

    @UserId            VARCHAR(60),
    @UserName          VARCHAR(100),
    @WorkstationId     VARCHAR(100)
)
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM Specialization WHERE SpecializationId = @SpecializationId)
    BEGIN
        UPDATE Specialization
        SET
            DepartmentsName  = @DepartmentsName,
            DepartmentsCode  = @DepartmentsCode,
            IsActive         = @IsActive,
            DepartmentId     = @DepartmentId,

            UpdatedAt        = GETDATE(),
            UpdatedById      = @UserId,
            UpdatedByName    = @UserName,
            WorkstationId    = @WorkstationId
        WHERE SpecializationId = @SpecializationId;

        SELECT @SpecializationId AS SpecializationId;
        RETURN;
    END

    SET @SpecializationId = CONCAT('SP', NEXT VALUE FOR SpecializationId);

    INSERT INTO Specialization
    (
        SpecializationId,
        DepartmentsName,
        DepartmentsCode,
        IsActive,
        DepartmentId,
        CreatedAt,
        CreatedById,
        CreatedByName,
        WorkstationId
    )
    VALUES
    (
        @SpecializationId,
        @DepartmentsName,
        @DepartmentsCode,
        @IsActive,
        @DepartmentId,
        GETDATE(),
        @UserId,
        @UserName,
        @WorkstationId
    );

    SELECT @SpecializationId AS SpecializationId;
END;
GO