@echo off
pushd %~dp0

goto check_Permissions

:check_Permissions
    net session >nul 2>&1
    if %errorLevel% == 0 (
        goto checkIfExistsDirectory
    ) else (
        echo Run this as administrator.
        pause
        exit
    )

:checkIfExistsDirectory
    IF EXIST MelonLoader (
        rmdir MelonLoader
        goto createLink
    ) ELSE (
        goto createLink
    )

:createLink
    set /p melonloader=Enter Path to MelonLoader folder:
    pushd 
    mklink /J MelonLoader "%melonloader%"
    pause