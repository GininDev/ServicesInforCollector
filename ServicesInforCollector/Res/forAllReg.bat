@echo off

set fname=%~n1
set fullname=%1

for /f "delims=" %%i in ("%CD%") do (
        set route=%%i
        set folder=
        call :separate
)
pause
goto :eof

:separate
if not "%route:~-1%"=="\" (
	set folder=%route:~-1%%folder%
	set route=%route:~0,-1%
	goto separate
	) else (
		set zf=%folder% 
		set zpaht=%route:~0,-1%

sc create %fname% binpath= %fullname% DisplayName= "%fname%" start= demand
sc description %fname% %folder%数据接收服务

	)
goto :eof

pause