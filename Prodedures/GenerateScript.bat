@echo off
setlocal enabledelayedexpansion

:: Root folder
set "ROOT=D:\HanuMediSoft\HanuMediSoft\HanuMediSoft\Prodedures"

:: Output file path
set "OUTPUT=%ROOT%\All_Procedures_List.sql"

echo Generating list of SQL procedure files...
if exist "%OUTPUT%" del "%OUTPUT%"

:: Loop through all .sql files in subfolders
for /r "%ROOT%" %%f in (*.sql) do (
    echo :r %%f >> "%OUTPUT%"
)

echo.
echo ✅ Procedure list generated successfully:
echo %OUTPUT%
pause
