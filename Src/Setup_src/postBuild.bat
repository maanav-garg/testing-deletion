@ECHO OFF
SET RELEASE_DIR=..\AutosarBCM\AutosarBCM\bin\x64\Release
SET EXE_NAME=AutosarBCM.exe
SET ILMERGE_BIN_DIR=.\ILMerge_bin
SET CONFUSER_BIN=.\confuser_ex\Confuser.CLI.exe
SET DLL_DIR=..\Dlls
SET MSBUILD_DIR="C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe"
SET PROJECT_PATH=..\AutosarBCM\AutosarBCM\AutosarBCM.csproj
SET MSBUILD_PATH="C:\Program Files (x86)\Microsoft Visual Studio\2022\BuildTools\MSBuild\Current\Bin\MSBuild.exe"


IF "%1"=="JenkinsRelease" (
%MSBUILD_PATH% %PROJECT_PATH% /p:Configuration=Release /p:Platform=x64
ECHO Successfully Build Using Jenkins
)

REM If a parameter is given to the script, it will skip the build part. Added due to Jenkins release.
IF Not "%1"=="JenkinsRelease" (
%MSBUILD_DIR% %PROJECT_PATH% /t:Clean
%MSBUILD_DIR% %PROJECT_PATH% /p:Configuration=Release /p:Platform=x64
ECHO Built Succesfull
)

if Not exist "%RELEASE_DIR%\Merged\" (
	ECHO Create Merged dir
	mkdir "%RELEASE_DIR%\Merged"
)

if exist "%RELEASE_DIR%\Merged\%EXE_NAME%" (
	ECHO Deleting previously merged file...
	del "%RELEASE_DIR%\Merged\%EXE_NAME%"
)

copy /y "%DLL_DIR%\Kvaser.CanLib.dll" "%RELEASE_DIR%"
copy /y "%DLL_DIR%\vxlapi_NET.dll" "%RELEASE_DIR%"

ECHO Start merging 
%ILMERGE_BIN_DIR%\ILMerge.exe /out:"%RELEASE_DIR%\Merged\%EXE_NAME%" "%EXE_NAME%" "Connection.dll" /target:WinExe /targetplatform:v4,C:\Windows\Microsoft.NET\Framework64\v4.0.30319 /internalize:merge_exclude.txt /lib:"%RELEASE_DIR%"
ECHO .

copy /y "%RELEASE_DIR%\*.dll" "%RELEASE_DIR%\Merged"

ECHO Start Confuser
%CONFUSER_BIN% confuser.crproj

ECHO InnoSetup - Compiling ...
"C:\Program Files (x86)\Inno Setup 6\Compil32.exe" /cc "Inno_Setup_Config.iss" 

REM if Not exist "%RELEASE_DIR%\Confused\" (
	REM ECHO Create Obfuscated dir
	REM mkdir "%RELEASE_DIR%\Confused"
REM )

REM if exist "%RELEASE_DIR%\Confused\%EXE_NAME%" (
	REM ECHO Deleting previously obfuscated file...
	REM del "%RELEASE_DIR%\Confused\%EXE_NAME%"
REM )


REM ECHO Start obfuscation...
REM %CONFUSER_BIN% ".\confuser.crproj"

pause