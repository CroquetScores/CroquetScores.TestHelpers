@echo Off
set config=%1
if "%config%" == "" (
   set config=Release
)

set version=
if not "%PackageVersion%" == "" (
   set version=-Version %PackageVersion%
)

REM Build
"%programfiles(x86)%\MSBuild\14.0\Bin\MSBuild.exe" CroquetScores.TestHelpers.sln /p:Configuration="%config%" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=Normal /nr:false

REM Package
mkdir Build
call %nuget% pack "source\CroquetScores.TestHelpers.csproj" -symbols -o Build -p Configuration=%config% %version%
call %nuget% pack "source\CroquetScores.TestHelpers.Selenium.csproj" -symbols -o Build -p Configuration=%config% %version%