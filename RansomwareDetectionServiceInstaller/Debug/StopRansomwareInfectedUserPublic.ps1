

import-module ActiveDirectory

#Prerequisites for this Powershell script
#RSAT Tools
    #RSAT Tools for Windows 10                 https://www.microsoft.com/en-us/download/details.aspx?id=45520&751be11f-ede8-5a0c-058c-2ee190a24fa6=True&e6b34bbe-475b-1abd-2c51-b5034bcdd6d2=True
    #RSAT Tools for Windows 8.1                https://www.microsoft.com/en-us/download/details.aspx?id=39296&e6b34bbe-475b-1abd-2c51-b5034bcdd6d2=True
    #RSAT Tools for Windows 7 SP1              https://www.microsoft.com/en-us/download/details.aspx?id=7887

#RansomwareDetectionService 2.0.4.1 or higher  https://ransomwaredetectionservice.codeplex.com/

#*********************************************************************************************************************
# Object Name: StopRansomwareInfectedUserPublic.ps1
# Object Type: Powershell 2.0 script
# Related Objects: RSAT Tools, RansomwareDetectionService
# Object Description: This will accept the username as a parameter from the Ransomware Detection Service and query 
#					

# Created Date: 4-22-2016
# Created by: pcooper
# Comments:
#	Make sure and only run this script when pointed to a home directory in Compare(Detect Ransomware) tab
#	A file share where multiple users can make changes will not work properly with this script
# Example Usage:

# CommandWorkingDirectory:  Path to where this script is saved
# CommandProgram: powershell.exe
# CommandArguments: -ExecutionPolicy UNRESTRICTED -File "c:\temp\StopRansomwareInfectedUserPublic.ps1" -username [Username]

# ---------------------------------------------------------------------------------------------------------------------
# Object Modified:

# Modification Number:			(e.g. 2-12-2008-1, 2-12-2008-2
# Modified Related Objects:
# Modified Description:

# Modified By:  Preston
# Modified By Company:  
# Modified Date:
# Modified Comments:

#*********************************************************************************************************************

$strUsername=''
$strFileName=''
$strFolderName=''

#Possible Arguments passed to this powershell script
for ( $i = 0; $i -lt $args.count; $i++ ) {
    if ($args[ $i ] -eq "/username"){ $strUsername=$args[ $i+1 ]}
    if ($args[ $i ] -eq "-username"){ $strUsername=$args[ $i+1 ]}
    if ($args[ $i ] -eq "/FileName"){ $strFileName=$args[ $i+1 ]}
    if ($args[ $i ] -eq "-FileName"){ $strFileName=$args[ $i+1 ]}
    if ($args[ $i ] -eq "/FolderName"){ $strFolderName=$args[ $i+1 ]}
    if ($args[ $i ] -eq "-FolderName"){ $strFolderName=$args[ $i+1 ]}
}




$strUsername = $strUsername.ToString().ToLower()

#Users to Exclude from this script and any IT or administrators that you do not want to get locked out of their machines in lower case
if ($strUsername -ne 'administrator' -and $strUsername -ne 'administrators' -and $strUsername -ne 'domain admins' -and $strUsername -ne 'exchange admin' -and $strUsername -ne 'domain users' -and $strUsername -ne 'authenticated users')
{	
    Disable-ADAccount -Identity $strUsername
}   
	
    
