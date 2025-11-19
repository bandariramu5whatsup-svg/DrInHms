-- Get Procedure for Specialization
GO
CREATE OR ALTER PROCEDURE SpOpGetSpecialization
(
    @SpecializationId  VARCHAR(30) = NULL,
    @DepartmentsName   VARCHAR(100) = NULL,
    @DepartmentId      VARCHAR(30) = NULL,
    @IsActive          BIT = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        SpecializationId,
        DepartmentsName,
        DepartmentsCode,
        IsActive,
        DepartmentId,
        CreatedAt,
        UpdatedAt,
        CreatedById,
        CreatedByName,
        UpdatedById,
        UpdatedByName,
        WorkstationId
    FROM Specialization
    WHERE
        (@SpecializationId IS NULL OR @SpecializationId = '' OR SpecializationId = @SpecializationId)
        AND (@DepartmentsName IS NULL OR DepartmentsName LIKE '%' + @DepartmentsName + '%')
        AND (@DepartmentId IS NULL OR DepartmentId = @DepartmentId)
        AND (@IsActive IS NULL OR IsActive = @IsActive)
    ORDER BY DepartmentsName;
END;
GO