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
echo.
echo Restoring NuGet packages
echo ------------------------
"%nuget%" restore

echo.
echo Building solution
echo -----------------
"%MsBuildExe%" CroquetScores.TestHelpers.sln /p:Configuration="%config%" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=Normal /nr:false

REM Package
mkdir Build
echo.
echo Packing CroquetScores.TestHelpers.csproj
echo ----------------------------------------
call "%nuget%" pack "source\CroquetScores.TestHelpers\CroquetScores.TestHelpers.csproj" -symbols -OutputDirectory  Build -p Configuration=%config% %version%

echo.
echo Packing CroquetScores.TestHelpers.Selenium.csproj
echo -------------------------------------------------
call "%nuget%" pack "source\CroquetScores.TestHelpers.Selenium\CroquetScores.TestHelpers.Selenium.csproj" -symbols -OutputDirectory  Build -p Configuration=%config% %version%