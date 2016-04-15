@echo off
call npm run %1
exit /b %errorlevel%
