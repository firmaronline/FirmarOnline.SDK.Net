@REM @echo off
setlocal

REM Directory of the src folder
set REPOPATH=%~dp0

REM Destination folder for local NuGet repository
set NUGET_REPO_PATH=D:\LocalNuGet

REM User's global package cache
set USER_NUGET_REPOSITORY=%USERPROFILE%\.nuget\packages

REM Build configuration (Debug, Release, Development)
set BUILD_CONFIG=%1
if "%BUILD_CONFIG%"=="" set BUILD_CONFIG=Debug

REM Build and publish each project sequentially
call :BuildAndPublish FirmarOnline.Model
call :BuildAndPublish FirmarOnline.Model.eSign
call :BuildAndPublish FirmarOnline.Model.PSC
call :BuildAndPublish FirmarOnline.Model.Verify
call :BuildAndPublish FirmarOnline.Clients.Common
call :BuildAndPublish FirmarOnline.Clients.PSC
call :BuildAndPublish FirmarOnline.Clients.eSign
call :BuildAndPublish FirmarOnline.Clients.Verify

endlocal
exit /b

:BuildAndPublish
set PROJECT=%1

REM Build generates the NuGet package thanks to GeneratePackageOnBuild
"dotnet" build "%REPOPATH%\%PROJECT%\%PROJECT%.csproj" -c %BUILD_CONFIG% -p:UseProjectReference=false
"dotnet" pack "%REPOPATH%\%PROJECT%\%PROJECT%.csproj" -c %BUILD_CONFIG% -p:UseProjectReference=false -o %NUGET_REPO_PATH%
if errorlevel 1 goto :EOF

for %%f in ("%REPOPATH%\%PROJECT%\bin\%BUILD_CONFIG%\%PROJECT%.*.nupkg") do (
    echo Adding %%~nxf to local NuGet source
    rmdir "%USER_NUGET_REPOSITORY%\%PROJECT%" /S /Q 2>nul
    rmdir "%NUGET_REPO_PATH%\%PROJECT%" /S /Q 2>nul
    nuget add "%%~ff" -Source "%NUGET_REPO_PATH%"
)

goto :EOF