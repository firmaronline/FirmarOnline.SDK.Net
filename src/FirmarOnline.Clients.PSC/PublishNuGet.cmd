@ECHO OFF

SETLOCAL

SET repopath=D:\Repos\Edatalia\firmarOnline.api\Clients\FirmarOnline.Clients.PSC
SET modelpath=D:\Repos\Edatalia\firmarOnline.api

SET usernugetrepository=%USERPROFILE%\.nuget\packages

SET version=%1

SET nugetrepopath=D:\LocalNuGet

SET buildconfig=%2
IF NOT DEFINED buildconfig (
	SET buildconfig=Debug
)

ECHO ON

rmdir "%usernugetrepository%\FirmarOnline.Clients.PSC\%version%" /S /Q
rmdir "%usernugetrepository%\FirmarOnline.Model\%version%" /S /Q
rmdir "%usernugetrepository%\FirmarOnline.Model.PSC\%version%" /S /Q
rmdir "%usernugetrepository%\FirmarOnline.Api.Enums\%version%" /S /Q
rmdir %nugetrepopath%\FirmarOnline.Clients.PSC\%version% /S /Q
rmdir %nugetrepopath%\FirmarOnline.Model\%version% /S /Q
rmdir %nugetrepopath%\FirmarOnline.Model.PSC\%version% /S /Q
rmdir %nugetrepopath%\FirmarOnline.Api.Enums\%version% /S /Q

nuget add %repopath%\bin\%buildconfig%\FirmarOnline.Clients.PSC.%version%.nupkg -Source %nugetrepopath%
nuget add %modelpath%\FirmarOnline.Model\bin\%buildconfig%\FirmarOnline.Model.%version%.nupkg -Source %nugetrepopath%
nuget add %modelpath%\FirmarOnline.Model.PSC\bin\%buildconfig%\FirmarOnline.Model.PSC.%version%.nupkg -Source %nugetrepopath%
nuget add %modelpath%\FirmarOnline.Api.Enums\bin\%buildconfig%\FirmarOnline.Api.Enums.%version%.nupkg -Source %nugetrepopath%

@ECHO OFF

ENDLOCAL
