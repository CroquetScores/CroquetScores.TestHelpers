@echo off
if "%1" == "version" (
    call npm version %2
    goto finally
)

call npm run %1
goto finally

:finally
exit /b %errorlevel%
