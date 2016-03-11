using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("RansomwareDetectionSystemTray")]
[assembly: AssemblyDescription("HESD's Ransomware Detection Service from http://www.questiondriven.com, on a schedule will compare files (Detect Ransomware) on local directories or windows file shares.  If the example files go missing or are changed then an event log entry will be written and an email can be sent. Added 'Find Ransomware Files' tab to find files created by ransomware for cleanup and weekly detection in all files shares to know what shares have been compromised.")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("RansomwareDetectionSystemTray")]
[assembly: AssemblyCopyright("Copyright ©  2016")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("23987f89-4718-426d-9f2e-ea63eb7d086f")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("2.0.2.2")]
[assembly: AssemblyFileVersion("2.0.2.2")]
