

import-module ActiveDirectory


#*********************************************************************************************************************
# Object Name: StopRansomwareInfectedComputerPublic.ps1
# Object Type: Powershell 2.0 script
# Related Objects: MySQL Connector 6.9 and Dell Kace K1000
# Object Description: This will accept the username as a parameter from the Ransomware Detection Service and query 
#					the dell kace k1000 mysql host for the computers associated with the username with ransomware activity

# Created Date: 4-22-2016
# Created by: pcooper
# Comments:
#	Make sure and only run this script when pointed to a home directory in Compare(Detect Ransomware) tab
#	A file share where multiple users can make changes will not work properly with this script
# Example Usage:

# CommandWorkingDirectory:  Path to where this script is saved
# CommandProgram: powershell.exe
# CommandArguments: -ExecutionPolicy UNRESTRICTED -File "c:\temp\StopRansomwareInfectedComputerPublic.ps1" -username [Username]

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

#Kace MySQL Connection Variables
$server= "Kace_K1000_Host"
$username= "MySQL_Readonly_Username"
$password= "MySQL_Password"
$database= "ORG1"


#Copy MySQL dll files for this script to the pc (This is needed to read the machines names associated with a user account)
#https://dev.mysql.com/downloads/connector/net/6.9.html  c# MySQL Connector can be downloaded here install on the computer you are using to run this script

$path = "C:\Program Files (x86)\MySQL\Connector.NET 6.9\Assemblies\v2.0\MySql.Data.dll"




## The path will need to match the mysql connector you downloaded
[void][system.reflection.Assembly]::LoadFrom($path)


function global:Set-SqlConnection ( $server = $(Read-Host "SQL Server Name"), $username = $(Read-Host "Username"), $password = $(Read-Host "Password"), $database = $(Read-Host "Default Database") ) {
	$SqlConnection.ConnectionString = "server=$server;user id=$username;password=$password;database=$database;pooling=false;Allow Zero Datetime=True;"
}


function global:Get-SqlDataTable( $Query = $(if (-not ($Query -gt $null)) {Read-Host "Query to run"}) ) {
	if (-not ($SqlConnection.State -like "Open")) { $SqlConnection.Open() }
	$SqlCmd = New-Object MySql.Data.MySqlClient.MySqlCommand $Query, $SqlConnection
	$SqlAdapter = New-Object MySql.Data.MySqlClient.MySqlDataAdapter
	$SqlAdapter.SelectCommand = $SqlCmd
	$DataSet = New-Object System.Data.DataSet
	$SqlAdapter.Fill($DataSet) | Out-Null
	$SqlConnection.Close()
	return $DataSet.Tables[0]
}

Set-Variable SqlConnection (New-Object MySql.Data.MySqlClient.MySqlConnection) -Scope Global -Option AllScope -Description "Personal variable for Sql Query functions"
Set-SqlConnection $server $username $password $database


#Populate Variables from Current Computer
$ccomputername = get-content env:computername
$cosversion = [Environment]::OSVersion
$cosname = (Get-WmiObject -class Win32_OperatingSystem).Caption
$strUsername = $strUsername.ToString().ToLower()


#Get Computers Names from Kace with Username as a filter limited to 7 results
$global:Query = "SELECT MACHINE.NAME AS SYSTEM_NAME FROM MACHINE WHERE MACHINE.USER_NAME='{0}' LIMIT 0,7" -f $strUsername
$mysqlresults = Get-SqlDataTable $Query

#We don't know which computer is infected we only which user is possibly infected so shutting down and disabling all computers associated with the user
ForEach ($result in $mysqlresults)
{	
    
    $systemname1 = $result.SYSTEM_NAME
    $systemname1 = $systemname1.ToString().ToUpper()

    #Users to Exclude from this script and any IT or administrators that you do not want to get locked out of their machines in lower case
	if ($strUsername -ne 'administrator' -and $strUsername -ne 'administrators' -and $strUsername -ne 'domain admins' -and $strUsername -ne 'exchange admin' -and $strUsername -ne 'domain users' -and $strUsername -ne 'authenticated users')
    {	
        #You could disable the AD account for the user (We chose to only disable the computer accounts)
        ##Disable-ADAccount -Identity $strUsername

        if ($systemname1.length -gt 0)
	    {
            
            Write-Host "Possible Ransomware Activity Detected with Username: $strUsername"
            Write-Host "Shutting Computer Down and Disabling computer account: $systemname1"
            
            #Computers to Exclude from being disabled in AD in UPPER CASE
            if ($systemname1 -ne "[ANY SERVERS OR MACHINES YOU DO NOT WANT TO HAVE SHUTDOWN AND AD ACCOUNT DISABLED]" -and $systemname1 -ne "ANOTHER SERVER NAME")
            {
                #Get-ADComputer -Identity $systemname1 | Disable-ADAccount
                $ADComputer = Get-ADComputer $systemname1
                if ($ADComputer.Enabled -eq $True)
                {
                    Disable-ADAccount $ADComputer
                }

                #Shutdown the computer
                Invoke-Expression -Command:"shutdown /m \\$systemname1 /s /f /c `"IT department detected an encryption virus on at least one computer you use. All of your computers connected are now disabled from the network.  Please call extension XXXX to make a help ticket.`" /t 35"
                
                

            }
	    }
    }   
	
    
}