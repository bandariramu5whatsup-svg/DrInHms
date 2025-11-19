
GO
CREATE PROCEDURE spValidateUserLogin
(
    @UserName   VARCHAR(200),
    @Password   VARCHAR(MAX)
)
AS
BEGIN
    SET NOCOUNT ON;

    -- Check if username exists
    IF NOT EXISTS (SELECT 1 FROM Users WHERE UserName = @UserName)
    BEGIN
        SELECT 
            0 AS Status,
            'Username not found' AS Message;
        RETURN;
    END

    -- Check if username + password match
    IF NOT EXISTS (SELECT 1 FROM Users WHERE UserName = @UserName AND Password = @Password)
    BEGIN
        SELECT 
            0 AS Status,
            'Invalid password' AS Message;
        RETURN;
    END

    -- Valid user → return full details
    SELECT  
        1 AS Status,
        'Login Successful' AS Message,
        UserId,
        UserName,
        UserType,
        LoginType,
        IsActive
    FROM Users
    WHERE UserName = @UserName AND Password = @Password;
END;
GO
