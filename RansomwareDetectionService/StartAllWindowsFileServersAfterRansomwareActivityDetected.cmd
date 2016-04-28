REM Stop "Windows File Server" File Sharing After Ransomware Activity Detected


REM *********************************************************************************************************************
REM Object Name: StopAllWindowsFileServersAfterRansomwareActivityDetected.cmd
REM Object Type: Windows Batch File 
REM Related Objects: RansomwareDetectionService
REM Object Description: 

REM Created Date: 4-22-2016
REM Created by: pcooper
REM Comments:
REM		This is overkill, but someone might want to be overprotective of their file shares.
REM Example Usage:

REM CommandWorkingDirectory:  Path to where this script is saved
REM CommandProgram: StopAllWindowsFileServersAfterRansomwareActivityDetected.cmd
REM CommandArguments: 

REM ---------------------------------------------------------------------------------------------------------------------



REM Repeat these uncommented commands below for each Windows File Server and if LanmanServer has any other services that need to be shutdown prior add them
REM change the options below as you see fit for your windows file servers
sc \\[WINDOWS_FILE_SERVER_NAME] start LanmanServer
timeout 10
sc \\[WINDOWS_FILE_SERVER_NAME] start Dfs
timeout 10
sc \\[WINDOWS_FILE_SERVER_NAME] start DFSR
REM timeout 5
REM sc \\[WINDOWS_FILE_SERVER_NAME] start Browser