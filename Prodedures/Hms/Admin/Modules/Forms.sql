Go
CREATE TABLE Forms (
    FormId VARCHAR(50) PRIMARY KEY,
    FormName VARCHAR(150),
    MenuId VARCHAR(50), 
    PageUrl VARCHAR(250),
    DisplayOrder INT,
    IsActive NUMERIC(1)
);
GO
INSERT INTO Forms (FormId, FormName, MenuId, PageUrl, DisplayOrder, IsActive)
VALUES
('FRM_OP_UNITS', 'Units', 'OP_MASTERS', '/Hms/Op/Masters/Units', 1, 1);
