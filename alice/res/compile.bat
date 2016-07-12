@ECHO OFF

CLS

IF DEFINED COMPILE_PLATFORM (
    SET COMPILE_PROFILE="%COMPILE_PROFILE%|%COMPILE_PLATFORM%"
  )

ECHO Building: %COMPILE_DESCRIPTION% (%COMPILE_PROFILE%)
ECHO.

REM Set up the environment.

c:
CD\
CD %VS_VC_BIN_PATH%
CALL vcvars32.bat

REM Run the build.

ECHO.
ECHO Building...
ECHO.
ECHO Warning: Closing this window will not stop the build.
ECHO.
ECHO To stop the build find the correct 'devenv.exe' process in 'Task Manager' and 'end' it.

IF EXIST %COMPILE_OUTPUT% (
  DEL %COMPILE_OUTPUT%
)

CALL %COMPILER_FULL_FILENAME% %COMPILE_SOLUTION% /build %COMPILE_PROFILE% /out %COMPILE_OUTPUT%

ECHO.

REM Build errors?
IF %ERRORLEVEL% EQU 0 (
    REM Build was ok, just show a message in the console.
    ECHO --- Build Successful ---
    CALL %ALICE_RESOURCE_PATH%FlashWindow.exe
    ECHO.
    
    PAUSE
  ) ELSE (
    REM Build failed, show the output file.
    ECHO !!! Build FAILED !!!
    CALL %ALICE_RESOURCE_PATH%FlashWindow.exe
    ECHO.
    PAUSE
    %WINDIR%\notepad.exe %COMPILE_OUTPUT%
  )
