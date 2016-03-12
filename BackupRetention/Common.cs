using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Text;
using System.Configuration;
using System.IO;

using System.Security.Cryptography;
using System.Management;
using System.Runtime.InteropServices;
using System.Collections;
using System.Text.RegularExpressions;

/*
BSD License:
Copyright (c) 2016, Preston Cooper – HESD Ransomware Detection Service
http://www.questiondriven.com
All rights reserved.
Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:
1. Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.
3. Neither the name of the copyright holder nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission.
THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/

/*
Delimon.Win32.IO Class License
Copyright © 2012, Johan Delimon
http://bit.ly/delimon 
All rights reserved.

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. 
*/
namespace RansomwareDetection
{

    public enum Month 
    {
        January = 0x01,
        February  = 0x02,
        March = 0x04,
        April = 0x08,
        May =0x16,
        June = 0x32,
        July =0x64,
        August=0x128,
        September=0x256,
        October=0x512,
        November = 0x1024,
        December = 0x2048
    }


    


    

    public enum FileOperations
    {
        Created   
        ,Modified
        ,Deleted
        ,Renamed
        ,None
    }

    public enum IntervalTypes
    {
        Hourly
        ,Daily
        ,Monthly
    }
    public interface IFolderConfig
    {
        int ID
        {
            get;
            set;
        }

        string Title
        {
            get;
            set;
        }

        string Time
        {
            get;
            set;
        }

        string EndTime
        {
            get;
            set;
        }

        IntervalTypes IntervalType
        {
            get;
            set;
        }

        long Interval
        {
            get;
            set;
        }

        bool Monday
        {
            get;
            set;
        }

        bool Tuesday
        {
            get;
            set;
        }

        bool Wednesday
        {
            get;
            set;
        }

        bool Thursday
        {
            get;
            set;
        }

        bool Friday
        {
            get;
            set;
        }

        bool Saturday
        {
            get;
            set;
        }

        bool Sunday
        {
            get;
            set;
        }

        Month Months
        {
            get;
            set;
        }

        bool Enabled
        {
            get;
            set;
        }

        string Comment
        {
            get;
            set;
        }

        DateTime StartDate
        {
            get;
            set;
        }

        DateTime EndDate
        {
            get;
            set;
        }



        bool SendEmailOnFailure
        {
            get;
            set;
        }


        
        bool SendEmailOnSuccess
        {
            get;
            set;
        }

        
        string EmailTo
        {
            get;
            set;
        }

        
        string EmailFrom
        {
            get;
            set;
        }

        
        string SMTPServer
        {
            get;
            set;
        }

        
        int SMTPPort
        {
            get;
            set;
        }

        
        bool SMTPUseSSL
        {
            get;
            set;
        }

        
        bool SMTPUseDefaultCredentials
        {
            get;
            set;
        }



       
        string SMTPPassword
        {
            get;
            set;
        }

        void Execute(ref bool blShuttingDown);

    }

    /// <summary>
    /// Common class Reusable code for other classes
    /// </summary>
    public static class Common
    {
        

        public static EventLog _evt;
        /// <summary>
        /// Gets Event Log Class instantiated.
        /// </summary>
        public static EventLog GetEventLog
        {

            get
            {
                if (_evt == null)
                {
                    //_evt = new Event_Log("RansomwareDetectionService", EventLogEntryType.Information,"RanDet");
                    string strApplication_Name = "RansomwareDetection";
                    string strLog_Name = "RanDet";
                    
                    try
                    {
                        EventSourceCreationData d = new EventSourceCreationData(strApplication_Name, strLog_Name);

                        if (!EventLog.SourceExists(strApplication_Name))
                        {
                            EventLog.CreateEventSource(d);
                        }
                        
                    }
                    catch (Exception)
                    {
                        
                    }
                    _evt = new System.Diagnostics.EventLog(strLog_Name, ".", strApplication_Name);
                    

                }
                return _evt;
            }


        }

        public static string GetPathToHTMLAnchor(string strpath)
        {
            string strNewPath = "";

            System.Uri uri = new System.Uri(strpath);
            if (uri.IsUnc)
            {
                strNewPath = "<a href=\"" + uri.AbsoluteUri + "\">" + strpath + "</a>";
                strNewPath = strNewPath.Replace("file://", "file://///");
            }
            else
            {
                strNewPath = "<a href=\"" + uri.AbsoluteUri + "\">" + strpath + "</a>";

            }
            return strNewPath;
        }
        
        public static string DateTimeSQLite(DateTime datetime)
        {
            string dateTimeFormat = "{0}-{1}-{2} {3}:{4}:{5}.{6}";
            return string.Format(dateTimeFormat, datetime.Year, datetime.Month, datetime.Day, datetime.Hour, datetime.Minute, datetime.Second, datetime.Millisecond);
        }



        /// <summary>
        /// Calculates Folder Size by adding up all file.Length
        /// this works for UNC and Non UNC paths.  This can also handle long file names.
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        public static float CalculateFolderSize(string folder)
        {
            float folderSize = 0.0f;
            try
            {
                //Checks if the path is valid or not
                if (!DirectoryExists(folder))
                    return folderSize;
                else
                {
                    try
                    {
                        foreach (string file in Delimon.Win32.IO.Directory.GetFiles(folder))
                        {
                            if (Delimon.Win32.IO.File.Exists(file))
                            {
                                Delimon.Win32.IO.FileInfo finfo = new Delimon.Win32.IO.FileInfo(file);
                                folderSize += (float) finfo.Length;
                            }
                        }

                        foreach (string dir in Delimon.Win32.IO.Directory.GetDirectories(folder))
                        {
                            folderSize += CalculateFolderSize(dir);
                        }
                    }
                    catch (NotSupportedException)
                    {
                        //Console.WriteLine("Unable to calculate folder size: {0}", e.Message);
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
                //Console.WriteLine("Unable to calculate folder size: {0}", e.Message);
            }
            return folderSize;
        }


        /// <summary>
        /// Gets Number of files in entire folder and sub folder.  Long file paths are supported.
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        public static long GetFolderFileCount(string folder)
        {
            long lFileCount = 0;
            try
            {
                //Checks if the path is valid or not
                if (!DirectoryExists(folder))
                    return lFileCount;
                else
                {
                    try
                    {
                        foreach (string file in Delimon.Win32.IO.Directory.GetFiles(folder))
                        {
                            if (Delimon.Win32.IO.File.Exists(file))
                            {
                                lFileCount++;
                            }
                        }

                        foreach (string dir in Delimon.Win32.IO.Directory.GetDirectories(folder))
                        {
                            lFileCount += GetFolderFileCount(dir);
                        }
                    }
                    catch (NotSupportedException)
                    {
                        //Console.WriteLine("Unable to calculate folder size: {0}", e.Message);
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
                //Console.WriteLine("Unable to calculate folder size: {0}", e.Message);
            }
            return lFileCount;
        }

       

        /// <summary>Checks if the given path is on a network drive.</summary>
        /// <param name="pPath"></param>
        /// <returns></returns>
        public static bool isNetworkDrive(string pPath)
        {
            ManagementObject mo = new ManagementObject();

            if (pPath.StartsWith(@"\\")) { return true; }

            // Get just the drive letter for WMI call
            string driveletter = GetDriveLetter(pPath);

            mo.Path = new ManagementPath(string.Format("Win32_LogicalDisk='{0}'", driveletter));

            // Get the data we need
            uint DriveType = Convert.ToUInt32(mo["DriveType"]);
            mo = null;

            return DriveType == 4;
        }

        /// <summary>Given a path will extract just the drive letter with volume separator.</summary>
        /// <param name="pPath"></param>
        /// <returns>string</returns>
        public static string GetDriveLetter(string pPath)
        {
            if (pPath.StartsWith(@"\\")) { throw new ArgumentException("A UNC path was passed to GetDriveLetter"); }
            return System.IO.Directory.GetDirectoryRoot(pPath).Replace(System.IO.Path.DirectorySeparatorChar.ToString(), "");
        }


        /// <summary>
        ///  NthDayOfMonth
        /// </summary>
        /// <param name="date"></param>
        /// <param name="dow"></param>
        /// <param name="n"></param>
        /// <returns>bool</returns>
        private static bool NthDayOfMonth(DateTime date, DayOfWeek dow, int n)
        {
            int d = date.Day;
            return date.DayOfWeek == dow && (d - 1) / 7 == (n - 1);
        }


        /// <summary>
        /// Removes ".7z" from a filename if it is on the end.
        /// </summary>
        /// <param name="FullName"></param>
        /// <returns></returns>
        public static string Strip7zExtension(string FullName)
        {
            if (FullName.Length > 3)
            {
                if (FullName.Substring(FullName.Length - 3, 3) == ".7z")
                {
                    FullName = FullName.Substring(0, FullName.Length - 3);
                }
            }
            return FullName;
        }

       

        /// <summary>
        /// Checks whether file is locked by the operating system or another process.  Also if you have permission to the file.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static bool IsFileLocked(Delimon.Win32.IO.FileInfo file)
        {
            FileStream stream = null;
            try
            {
                file.IsReadOnly = false;
                stream = new FileStream(file.FullName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
            }
            catch (Exception)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }
            finally
            {
                try
                {
                    if (stream != null)
                    {
                        stream.Close();
                        stream.Dispose();
                    }
                }
                catch (Exception)
                {

                }

            }

            //file is not locked
            return false;
        }

       

        /// <summary>
        /// Refresh FilInfo file attributes.  Windows 7 doesn't update attributes actively.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static Delimon.Win32.IO.FileInfo RefreshFileInfo(Delimon.Win32.IO.FileInfo file)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(file.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                fs.ReadByte();
                fs.Close();
                fs.Dispose();
                fs = null;
                file.Refresh();
            }
            catch (Exception)
            {
            }
            finally
            {
                try
                {
                    if (fs != null)
                    {
                        fs.Close();
                        fs.Dispose();
                    }
                }
                catch (Exception)
                {
                }

            }
            return file;

        }

        /// <summary>
        /// GetMD5HashFromFile returns MD5 hash of a file as a string
        /// </summary>
        /// <param name="strPath"></param>
        /// <returns></returns>
        public static string GetMD5HashFromFile(string strPath)
        {
            StringBuilder sb = new StringBuilder();
            string strMD5 = "";
            FileStream fs=null;
            MD5 md5 = new MD5CryptoServiceProvider();
            try
            {
                fs = new FileStream(strPath, FileMode.Open);
                
                byte[] retVal = md5.ComputeHash(fs);
                


                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                strMD5 = sb.ToString();
            }
            catch (Exception)
            {
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
                fs = null;
                md5.Clear();
                md5 = null;
                sb.Clear();
                sb = null;
            }
            
            return strMD5;
        }


        public static string GetMD5HashFromString(string input)
        {
            // step 1, calculate MD5 hash from input
            StringBuilder sb = new StringBuilder();
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }
            return sb.ToString();
        }
        

        /// <summary>
        /// Fixes null strings and returns the string or ""
        /// </summary>
        /// <param name="objData"></param>
        /// <returns></returns>
        public static string FixNullstring(object objData)
        {
            string strValue = "";
            if (DBNull.Value == objData || objData == null)
            {
                return strValue;
            }
            else
            {
                return objData.ToString();
            }
        }

        /// <summary>
        /// Cleans the path before checking if the directory exists and long file paths are supported
        /// </summary>
        /// <param name="strPath"></param>
        /// <returns></returns>
        public static bool DirectoryExists(string strPath)
        {
            bool blSuccess = false;
            try
            {
                strPath = WindowsPathClean(strPath);
                blSuccess = Delimon.Win32.IO.Directory.Exists(strPath);
            }
            catch (Exception)
            {
                
            }
            return blSuccess;
        }

        public static bool FileExists(string strPath)
        {
            bool blSuccess = false;
            try
            {
                strPath = WindowsPathClean(strPath);
                blSuccess = Delimon.Win32.IO.File.Exists(strPath);
            }
            catch (Exception)
            {

            }
            return blSuccess;

        }

        /// <summary>
        /// Combines two Windows or Windows Share Paths
        /// </summary>
        /// <param name="strBasePath"></param>
        /// <param name="strPath"></param>
        /// <returns></returns>
        public static string WindowsPathCombine(string strBasePath, string strPath)
        {
            strBasePath = FixNullstring(strBasePath);
            strPath = FixNullstring(strPath);
            strBasePath = strBasePath.Replace("\\\\", "\\");
            if (strBasePath.Trim() != "\\" && strBasePath !="")
            {
                strBasePath += "\\" + strPath;
            }
            else
            {
                strBasePath = strPath;
            }
            strBasePath = WindowsPathClean(strBasePath);
                        
            return strBasePath;
        }


        /// <summary>
        /// Combines two Windows or Windows Share Paths and Removes a path
        /// </summary>
        /// <param name="strBasePath"></param>
        /// <param name="strPath"></param>
        /// <param name="strFolderToRemove"></param>
        /// <returns></returns>
        public static string WindowsPathCombine(string strBasePath, string strPath, string strFolderToRemove)
        {
            strBasePath = FixNullstring(strBasePath);
            strBasePath = strBasePath.Replace("\\\\", "\\");
            strPath = FixNullstring(strPath);
            strFolderToRemove = FixNullstring(strFolderToRemove);
            strFolderToRemove = strFolderToRemove.Replace("\\\\", "\\");
            //strPath = strPath.Trim();
            strPath = strPath.Replace("\\\\", "\\");

            strPath = strPath.Replace(strFolderToRemove, "").Trim();
            strPath = strPath.Replace("\\\\", "\\");
            if (strBasePath.Trim() != "" && strBasePath.Trim() != "\\")
            {
                strPath = strBasePath + "\\" + strPath;
            }

            strPath = WindowsPathClean(strPath);

            return strPath;
        }


        

        /// <summary>
        /// Cleans Windows Share Paths
        /// </summary>
        /// <param name="strPath"></param>
        /// <returns></returns>
        public static string WindowsPathClean(string strPath)
        {
            strPath = FixNullstring(strPath);
            strPath = strPath.Replace("/", "\\");
            strPath = strPath.Replace("\\\\", "\\");
            strPath = strPath.Replace("\\\\", "\\");
            
            //Fix Empty Paths
            if (strPath == "\\")
            {
                strPath = "";
            }

            if (strPath.Length > 2)
            {
                //Remove Ending BackSlashes from folders
                if (strPath.Substring(strPath.Length - 2, 2) == "\\")
                {
                    strPath = strPath.Remove(strPath.Length - 2);
                }
                //Fix Share Paths to have 2 beginning backslashes
                if (strPath.Substring(0, 1) == "\\" && strPath.Substring(0, 2) != "\\\\")
                {
                    strPath = "\\" + strPath;
                }
                
            }

            return strPath;
        }




        /// <summary>
        /// Cleans Windows or Windows Share Paths
        /// </summary>
        /// <param name="strPath"></param>
        /// <returns></returns>
        public static string WindowsArgumentClean(string strPath)
        {
            strPath = FixNullstring(strPath);
            
            strPath = strPath.Replace("\\\\", "\\");
            strPath = strPath.Replace("\\\\", "\\");

            //Fix Empty Paths
            if (strPath == "\\")
            {
                strPath = "";
            }

            return strPath;
        }

        /// <summary>
        /// Combines two Remote (FTP,FTPS,SFTP) Paths
        /// </summary>
        /// <param name="strBasePath"></param>
        /// <param name="strPath1"></param>
        /// <returns></returns>
        public static string RemotePathCombine(string strBasePath, string strPath1)
        {
            string strPath = "";
            strPath = FixNullstring(strBasePath);
            strPath = strPath.Replace("\\\\", "\\");
            strPath1 = FixNullstring(strPath1);
            strPath1 = strPath1.Trim();
            strPath = strPath.Replace("//", "/");
            if (strPath.Trim() != "" && strPath.Trim() != "/")
            {
                strPath += "/" + strPath1;
            }
            else
            {
                strPath = strPath1;
            }
            strPath = RemotePathClean(strPath);
            return strPath;
        }

        /// <summary>
        /// Combines two Remote (FTP,FTPS,SFTP) Paths and removes the third path passed
        /// </summary>
        /// <param name="strBaseDirectory"></param>
        /// <param name="strPath"></param>
        /// <param name="strFolderToRemove"></param>
        /// <returns></returns>
        public static string RemotePathCombine(string strBaseDirectory,string strPath, string strFolderToRemove)
        {
            
            strBaseDirectory = FixNullstring(strBaseDirectory);
            strBaseDirectory = strBaseDirectory.Replace("\\\\", "\\");
            strPath = FixNullstring(strPath);
            strFolderToRemove = FixNullstring(strFolderToRemove);
            strFolderToRemove = strFolderToRemove.Replace("\\\\", "\\");
            //strPath = strPath.Trim();
            strPath = strPath.Replace("\\\\", "\\");
            
            strPath = strPath.Replace(strFolderToRemove, "").Trim();
            strPath = strPath.Replace("\\", "/");
            strPath = strPath.Replace("//", "/");
            if (strBaseDirectory.Trim() !="" && strBaseDirectory.Trim() !="/")
            {
                strPath = strBaseDirectory + "/" + strPath;
            }

            strPath = RemotePathClean(strPath);
            
            return strPath;
        }

        /// <summary>
        /// Cleans the Remote Path (FTP,SFTP,FTPS)
        /// </summary>
        /// <param name="strPath"></param>
        /// <returns></returns>
        public static string RemotePathClean(string strPath)
        {
            strPath = FixNullstring(strPath);
            strPath = strPath.Replace("\\", "/");
            strPath = strPath.Replace("//", "/");
            strPath = strPath.Replace("//", "/");
            if (strPath.Trim() == "/")
            {
                strPath = "";
            }
            if (strPath.Length > 1 )
            {
                if (strPath.Substring(strPath.Length - 1, 1) == "/")
                {
                    strPath = strPath.Remove(strPath.Length - 1);
                }
                
            }
            return strPath;
        }

        /// <summary>
        /// Fixes null int32 and returns 0 if null
        /// </summary>
        /// <param name="objData"></param>
        /// <returns></returns>
        public static Int32 FixNullInt32(object objData)
        {
            Int32 intValue = 0;
            if (DBNull.Value == objData || objData == null)
            {
                return intValue;
            }
            else
            {
                Int32.TryParse(objData.ToString(), out intValue);
                return intValue;
            }
        }

        /// <summary>
        /// Fixes null int32 and returns 0 if null
        /// </summary>
        /// <param name="objData"></param>
        /// <returns></returns>
        public static Int32 FixNullInt32(object objData, int intOverrideNullValue)
        {
            Int32 intValue = intOverrideNullValue;
            if (DBNull.Value == objData || objData == null)
            {
                return intValue;
            }
            else
            {
                Int32.TryParse(objData.ToString(), out intValue);
                return intValue;
            }
        }

        /// <summary>
        /// Fixes null int32 and returns 0 if null
        /// </summary>
        /// <param name="objData"></param>
        /// <returns></returns>
        public static long FixNulllong(object objData)
        {
            long lValue = 0;
            if (DBNull.Value == objData || objData == null)
            {
                return lValue;
            }
            else
            {
                long.TryParse(objData.ToString(), out lValue);
                return lValue;
            }
        }

        /// <summary>
        /// Fixes null int32 and returns 0 if null
        /// </summary>
        /// <param name="objData"></param>
        /// <returns></returns>
        public static long FixNulllong(object objData, long longOverrideNullValue)
        {
            long lValue = longOverrideNullValue;
            if (DBNull.Value == objData || objData == null)
            {
                return lValue;
            }
            else
            {
                long.TryParse(objData.ToString(), out lValue);
                return lValue;
            }
        }

        /// <summary>
        /// Fixes null bool and returns false if null
        /// </summary>
        /// <param name="objData"></param>
        /// <returns></returns>
        public static bool FixNullboolParse(object objData)
        {
            bool blValue = false;
            if (DBNull.Value == objData || objData == null)
            {
                return blValue;
            }
            else
            {
                
                string strValue = objData.ToString();

                // 2
                // Remove whitespace from string
                strValue = strValue.Trim();

                // 3
                // Lowercase the string
                strValue = strValue.ToLower();

                // 4
                // Check for word true
                if (strValue == "true")
                {
                    return true;
                }

                // 5
                // Check for letter true
                if (strValue == "t")
                {
                    return true;
                }

                // 6
                // Check for one
                if (strValue == "1")
                {
                    return true;
                }

                // 7
                // Check for word yes
                if (strValue == "yes")
                {
                    return true;
                }

                // 8
                // Check for letter yes
                if (strValue == "y")
                {
                    return true;
                }
                       
                
            }
            return blValue;
        }

        /// <summary>
        /// Fixes null bool and returns false if null
        /// </summary>
        /// <param name="objData"></param>
        /// <returns></returns>
        public static bool FixNullbool(object objData)
        {
            bool blValue = false;
            if (DBNull.Value == objData || objData == null)
            {
                return blValue;
            }
            else
            {
                bool.TryParse(objData.ToString(), out blValue);
            }
            return blValue;
        }

        public static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public static string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }

        

        
       
       

        /// <summary>
        /// Walks Entire Path and returns Generic List of the files in the entire directory and sub directories and can handle long path names
        /// </summary>
        /// <param name="strPath"></param>
        /// <param name="blShuttingDown"></param>
        /// <returns></returns>
        public static System.Collections.Generic.List<Delimon.Win32.IO.FileInfo> WalkDirectory(string strPath, ref bool blShuttingDown)
        {
            if (DirectoryExists(strPath))
            {
                System.Collections.Generic.List<Delimon.Win32.IO.FileInfo> Files;
                Files = new System.Collections.Generic.List<Delimon.Win32.IO.FileInfo>();
                Delimon.Win32.IO.DirectoryInfo rootDir = new Delimon.Win32.IO.DirectoryInfo(strPath);
                WalkDirectoryTree(rootDir, ref Files, ref blShuttingDown);
                return Files;
            }
            else
            {
                return null;
            }
        }

        /*
        /// <summary>
        /// Walks Entire Path and returns Generic List of the files in the entire directory and sub directories
        /// </summary>
        /// <param name="strPath"></param>
        /// <param name="blShuttingDown"></param>
        /// <returns></returns>
        public static System.Collections.Generic.List<System.IO.FileInfo> WalkDirectory(string strPath, ref bool blShuttingDown)
        {
            if (DirectoryExists(strPath))
            {
                System.Collections.Generic.List<System.IO.FileInfo> Files;
                Files = new System.Collections.Generic.List<System.IO.FileInfo>();
                System.IO.DirectoryInfo rootDir = new System.IO.DirectoryInfo(strPath);
                WalkDirectoryTree(rootDir, ref Files, ref blShuttingDown);
                return Files;
            }
            else
            {
                return null;
            }
        }*/

        /*/// <summary>
        /// Walks Path and returns all Directories and sub directories
        /// </summary>
        /// <param name="strPath"></param>
        /// <returns>System.Collections.Generic.List of DirectoryInfo</returns>
        public static System.Collections.Generic.List<DirectoryInfo> GetAllDirectories(string strPath)
        {
            System.Collections.Generic.List<DirectoryInfo> Directories = new List<DirectoryInfo>();
            System.IO.DirectoryInfo rootDir = new System.IO.DirectoryInfo(strPath);
            DirectoryList(rootDir, ref Directories);
            return Directories;
        }*/

        /// <summary>
        /// Walks Path and returns all Directories and sub directories
        /// </summary>
        /// <param name="strPath"></param>
        /// <returns>System.Collections.Generic.List of DirectoryInfo</returns>
        public static System.Collections.Generic.List<Delimon.Win32.IO.DirectoryInfo> GetAllDirectories(string strPath)
        {
            System.Collections.Generic.List<Delimon.Win32.IO.DirectoryInfo> Directories = new List<Delimon.Win32.IO.DirectoryInfo>();
            Delimon.Win32.IO.DirectoryInfo rootDir = new Delimon.Win32.IO.DirectoryInfo(strPath);
            DirectoryList(rootDir, ref Directories);
            return Directories;
        }

        /*/// <summary>
        /// Walks the Path and returns all Directories and sub directories
        /// </summary>
        /// <param name="strPath"></param>
        /// <returns>System.Collections.Generic.List of RemoteFile</returns>
        public static System.Collections.Generic.List<RemoteFile> GetAllDirectoriesR(string strPath)
        {
            System.Collections.Generic.List<RemoteFile> Directories = new List<RemoteFile>();
            System.IO.DirectoryInfo rootDir = new System.IO.DirectoryInfo(strPath);
            DirectoryList(rootDir, ref Directories,strPath);
            return Directories;
        }*/


        /// <summary>
        /// Walks the Path and returns all Directories and sub directories and can handle long path names
        /// </summary>
        /// <param name="strPath"></param>
        /// <returns>System.Collections.Generic.List of RemoteFile</returns>
        public static System.Collections.Generic.List<RemoteFile> GetAllDirectoriesR(string strPath)
        {
            System.Collections.Generic.List<RemoteFile> Directories = new List<RemoteFile>();
            Delimon.Win32.IO.DirectoryInfo rootDir = new Delimon.Win32.IO.DirectoryInfo(strPath);
            DirectoryList(rootDir, ref Directories, strPath);
            return Directories;
        }

        /// <summary>
        /// Walks the path and add each directory and sub directory to the referenced list
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="folders"></param>
        private static void DirectoryList(Delimon.Win32.IO.DirectoryInfo dir, ref List<Delimon.Win32.IO.DirectoryInfo> folders)
        {

            try
            {
                foreach (Delimon.Win32.IO.DirectoryInfo d in dir.GetDirectories())
                {
                        folders.Add(d);
                        DirectoryList(d, ref folders);
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Walks the path and add each directory and sub directory to the referenced list
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="folders"></param>
        /// <param name="strParentPathRemove"></param>
        private static void DirectoryList(Delimon.Win32.IO.DirectoryInfo dir, ref List<RemoteFile> folders, string strParentPathRemove)
        {

            try
            {
                foreach (Delimon.Win32.IO.DirectoryInfo d in dir.GetDirectories())
                {
                    RemoteFile RD1 = new RemoteFile(d);
                    RD1.ParentDirectory = WindowsPathClean(d.Parent.FullName).Replace(WindowsPathClean(strParentPathRemove), "");
                    folders.Add(RD1);
                    DirectoryList(d, ref folders,strParentPathRemove);
                }
            }
            catch (Exception)
            {
            }
        }

        
        /// <summary>
        /// Walks the path and returns the files only in the first directory
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public static System.Collections.Generic.List<Delimon.Win32.IO.FileInfo> GetFilesInDirectory(Delimon.Win32.IO.DirectoryInfo dir)
        {
            System.Collections.Generic.List<Delimon.Win32.IO.FileInfo> files = new System.Collections.Generic.List<Delimon.Win32.IO.FileInfo>();
            try
            {
                foreach (Delimon.Win32.IO.FileInfo f in dir.GetFiles())
                {
                    files.Add(f);
                }
            }
            catch (Exception)
            {
            }
            return files;
        }

        /// <summary>
        /// Walks the path and returns the directories only in the path (first level only not recursive)
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public static System.Collections.Generic.List<Delimon.Win32.IO.DirectoryInfo> GetDirsInDirectory(Delimon.Win32.IO.DirectoryInfo dir)
        {
            System.Collections.Generic.List<Delimon.Win32.IO.DirectoryInfo> dirs = new System.Collections.Generic.List<Delimon.Win32.IO.DirectoryInfo>();
            try
            {
                foreach (Delimon.Win32.IO.DirectoryInfo dir1 in dir.GetDirectories())
                {
                    dirs.Add(dir1);
                }
            }
            catch (Exception)
            {
            }
            return dirs;
        }

        
        
       



        /// <summary>
        /// Creates copy of folders from sourcePath to destinationPath
        /// </summary>
        /// <param name="strSourcePath"></param>
        /// <param name="strDestinationPath"></param>
        public static void CreateDestinationFolders(string strSourcePath, string strDestinationPath)
        {
            System.Collections.Generic.List<RemoteFile> Directories;
            Directories = Common.GetAllDirectoriesR(strSourcePath);
            Common.CreateLocalFolderStructure(Directories, strDestinationPath);

        }

        /// <summary>
        /// Duplicates folder structure needed for each RemoteFile
        /// </summary>
        /// <param name="Files"></param>
        /// <param name="dlpath"></param>
        public static void CreateLocalFolderStructure(List<RemoteFile> Files, string dlpath)
        {
            _evt = GetEventLog;
            for (int i = 0; i < Files.Count; i++)
            {
                if (Files[i].IsDirectory)
                {
                    //Create a new folder under the current active one
                    string PathToNewFolder = "";
                    PathToNewFolder = WindowsPathCombine(dlpath, Files[i].ParentDirectory);
                    PathToNewFolder = WindowsPathCombine(PathToNewFolder, Files[i].Name);
                    ////PathToNewFolder = Path.Combine(dlpath + Files[i].ParentDirectory, Files[i].Name).Replace("/", "\\").Replace("\\\\", "\\");
                    if (!DirectoryExists(PathToNewFolder))
                    {
                        Delimon.Win32.IO.Directory.CreateDirectory(PathToNewFolder);
                        _evt.WriteEntry("Local Directory Created: " + PathToNewFolder,EventLogEntryType.Information, 1, 1);
                    }
                }
            }
        }


        

        /// <summary>
        /// Recursive algorithm to get all files in a directory and sub directories that can handle long path names (no filtering)
        /// </summary>
        /// <param name="root"></param>
        /// <param name="AllFiles"></param>
        /// <param name="blShuttingDown"></param>
        private static void WalkDirectoryTree(Delimon.Win32.IO.DirectoryInfo root, ref System.Collections.Generic.List<Delimon.Win32.IO.FileInfo> AllFiles, ref bool blShuttingDown)
        {
            Delimon.Win32.IO.FileInfo[] files = null;
            Delimon.Win32.IO.DirectoryInfo[] subDirs = null;
            EventLog _evt = GetEventLog;
            
            // First, process all the files directly under this folder 
            try
            {
                files = root.GetFiles();
            }
            catch (Exception e)
            {
                
                _evt.WriteEntry(e.Message);
            }
            

            if (files != null)
            {
                foreach (Delimon.Win32.IO.FileInfo fi in files)
                {
                    if (blShuttingDown)
                    {
                        _evt.WriteEntry("Shutting Down, about to walk: " + fi.FullName);
                        return;
                    }
                    // In this example, we only access the existing FileInfo object. If we 
                    // want to open, delete or modify the file, then 
                    // a try-catch block is required here to handle the case 
                    // where the file has been deleted since the call to TraverseTree().

                    //Console.WriteLine(fi.FullName);
                    RefreshFileInfo(fi);
                    AllFiles.Add(fi);
                }

                // Now find all the subdirectories under this directory.
                subDirs = root.GetDirectories();

                foreach (Delimon.Win32.IO.DirectoryInfo dirInfo in subDirs)
                {
                    if (blShuttingDown)
                    {
                        _evt.WriteEntry("Shutting Down, about to walk directory: " + dirInfo.FullName);
                        return;
                    }
                    // Resursive call for each subdirectory.
                    WalkDirectoryTree(dirInfo, ref AllFiles, ref blShuttingDown);
                }
            }
        }

        
    }



    /// <summary>
    /// Remote File Class to help reuse code for ftp or sftp files
    /// </summary>
    public class RemoteFile
    {
        #region "Properties"

        public long ID { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public long Length { get; set; }
        public string ParentDirectory { get; set; }
        public bool IsDirectory { get; set; }

        public DateTime LastWriteTime { get; set; }
        public DateTime LastWriteTimeUtc { get; set; }


        public FileOperations FileOperation { get; set; }

        public string NewFullName { get; set; }

        public string MD5 { get; set; }

        public long FolderActionID { get; set; }

        public string Comment { get; set; }

        private string _shortFullName = "";
        public string ShortFullName 
        { 
            get { return _shortFullName;} 
            set 
            { 
                _shortFullName = value;
                _shortFullNameMD5 = Common.GetMD5HashFromString(_shortFullName);
            } 
        }

        private string _shortFullNameMD5 = "";
        public string ShortFullNameMD5 { get { return _shortFullNameMD5;} }
        
        public string SourceFolderMD5 { get; set; }


        #endregion

        #region "Methods"


        /// <summary>
        ///  A list of all the files/folder in a specific location.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="fullName"></param>
        /// <param name="length"></param>
        /// <param name="parentDirectory"></param>
        /// <param name="isDirectory"></param>
        /// <param name="lastWriteTime"></param>
        /// <param name="lastWriteTimeUtc"></param>
        public RemoteFile(string name, string fullName, long length, string parentDirectory, bool isDirectory, DateTime lastWriteTime, DateTime lastWriteTimeUtc)
        {
            this.Name = name;
            this.FullName = fullName;
            this.Length = length;
            this.ParentDirectory = parentDirectory;
            this.IsDirectory = isDirectory;
            this.LastWriteTime = lastWriteTime;
            this.LastWriteTimeUtc = lastWriteTimeUtc;
        }

        public RemoteFile(System.IO.FileInfo file1)
        {
            this.Name = file1.Name;
            this.FullName = file1.FullName;
            this.Length = file1.Length;
            this.ParentDirectory = file1.Directory.FullName;
            this.IsDirectory = false;
            this.LastWriteTime = file1.LastWriteTime;
            this.LastWriteTimeUtc = file1.LastWriteTimeUtc;
        }

        public RemoteFile(Delimon.Win32.IO.FileInfo file1)
        {
            this.Name = file1.Name;
            this.FullName = file1.FullName;
            this.Length = file1.Length;
            this.ParentDirectory = file1.Directory.FullName;
            this.IsDirectory = false;
            this.LastWriteTime = file1.LastWriteTime;
            this.LastWriteTimeUtc = file1.LastWriteTimeUtc;
        }

        public RemoteFile(System.IO.FileInfo file1, string strSource)
        {
            this.Name = file1.Name;
            this.FullName = file1.FullName;
            this.Length = file1.Length;
            this.ParentDirectory = file1.Directory.FullName;
            this.IsDirectory = false;
            this.LastWriteTime = file1.LastWriteTime;
            this.LastWriteTimeUtc = file1.LastWriteTimeUtc;
            this.ShortFullName = file1.FullName.Replace(strSource, "");
            this.SourceFolderMD5 = Common.GetMD5HashFromString(strSource);
        }

        public RemoteFile(Delimon.Win32.IO.FileInfo file1, string strSource)
        {
            this.Name = file1.Name;
            this.FullName = file1.FullName;
            this.Length = file1.Length;
            this.ParentDirectory = file1.Directory.FullName;
            this.IsDirectory = false;
            this.LastWriteTime = file1.LastWriteTime;
            this.LastWriteTimeUtc = file1.LastWriteTimeUtc;
            this.ShortFullName = file1.FullName.Replace(strSource, "");
            this.SourceFolderMD5 = Common.GetMD5HashFromString(strSource);
        }

        public RemoteFile(System.IO.DirectoryInfo dir, string strSource)
        {
            this.Name = dir.Name;
            this.FullName = dir.FullName;
            this.Length = 0;
            this.ParentDirectory = dir.Parent.FullName;
            this.IsDirectory = true;
            this.LastWriteTime = dir.LastWriteTime;
            this.LastWriteTimeUtc = dir.LastWriteTimeUtc;
            this.ShortFullName = dir.FullName.Replace(strSource, "");
            this.SourceFolderMD5 = Common.GetMD5HashFromString(strSource);
        }
        
        public RemoteFile(Delimon.Win32.IO.DirectoryInfo dir, string strSource)
        {
            this.Name = dir.Name;
            this.FullName = dir.FullName;
            this.Length = 0;
            this.ParentDirectory = dir.Parent.FullName;
            this.IsDirectory = true;
            this.LastWriteTime = dir.LastWriteTime;
            this.LastWriteTimeUtc = dir.LastWriteTimeUtc;
            this.ShortFullName = dir.FullName.Replace(strSource, "");
            this.SourceFolderMD5 = Common.GetMD5HashFromString(strSource);
        }


        public RemoteFile(DataRow row)
        {
            DateTime dtLastWriteTime;
            DateTime dtLastWriteTimeUTC;
            this.ID = Common.FixNulllong(row["ID"]);
            this.Name = Common.FixNullstring(row["Name"]);
            this.FullName = Common.FixNullstring(row["FullName"]);
            this.Length = Common.FixNulllong(row["FileLength"]);
            this.ParentDirectory = Common.FixNullstring(row["FileParentDirectory"]);
            this.IsDirectory = Common.FixNullboolParse(row["IsDirectory"]);
            DateTime.TryParse(Common.FixNullstring(row["LastWriteTime"]),out dtLastWriteTime);
            DateTime.TryParse(Common.FixNullstring(row["LastWriteTimeUTC"]), out dtLastWriteTimeUTC);
            this.LastWriteTime = dtLastWriteTime;
            this.LastWriteTimeUtc = dtLastWriteTimeUTC;
            this.NewFullName = Common.FixNullstring(row["NewFileName"]);
            this.FolderActionID = Common.FixNulllong(row["FolderActionID"]);
            this._shortFullName = Common.FixNullstring(row["ShortFullName"]);
            this._shortFullNameMD5 = Common.FixNullstring(row["ShortFullNameMD5"]);
            this.SourceFolderMD5 = Common.FixNullstring(row["SourceFolderMD5"]);
        }

        public RemoteFile(System.IO.FileInfo file1, FileOperations fileOp)
        {
            this.Name = file1.Name;
            this.FullName = file1.FullName;
            this.Length = file1.Length;
            this.ParentDirectory = file1.Directory.FullName;
            this.IsDirectory = false;
            this.LastWriteTime = file1.LastWriteTime;
            this.LastWriteTimeUtc = file1.LastWriteTimeUtc;
            this.FileOperation = fileOp;
        }

        public RemoteFile(Delimon.Win32.IO.FileInfo file1, FileOperations fileOp)
        {
            this.Name = file1.Name;
            this.FullName = file1.FullName;
            this.Length = file1.Length;
            this.ParentDirectory = file1.Directory.FullName;
            this.IsDirectory = false;
            this.LastWriteTime = file1.LastWriteTime;
            this.LastWriteTimeUtc = file1.LastWriteTimeUtc;
            this.FileOperation = fileOp;
        }

        public RemoteFile(System.IO.FileInfo file1, FileOperations fileOp,string newFullName)
        {
            this.Name = file1.Name;
            this.FullName = file1.FullName;
            this.Length = file1.Length;
            this.ParentDirectory = file1.Directory.FullName;
            this.IsDirectory = false;
            this.LastWriteTime = file1.LastWriteTime;
            this.LastWriteTimeUtc = file1.LastWriteTimeUtc;
            this.FileOperation = fileOp;
            this.NewFullName = newFullName;
        }

        public RemoteFile(Delimon.Win32.IO.FileInfo file1, FileOperations fileOp, string newFullName)
        {
            this.Name = file1.Name;
            this.FullName = file1.FullName;
            this.Length = file1.Length;
            this.ParentDirectory = file1.Directory.FullName;
            this.IsDirectory = false;
            this.LastWriteTime = file1.LastWriteTime;
            this.LastWriteTimeUtc = file1.LastWriteTimeUtc;
            this.FileOperation = fileOp;
            this.NewFullName = newFullName;
        }

        public RemoteFile(System.IO.DirectoryInfo dir1)
        {
            this.Name = dir1.Name;
            this.FullName = dir1.FullName;
            this.Length = 0;
            this.ParentDirectory = "";//dir1.Parent.FullName;
            this.IsDirectory = true;
            this.LastWriteTime = dir1.LastWriteTime;
            this.LastWriteTimeUtc = dir1.LastWriteTimeUtc;
            //this.FileOperation = FileOperations.Modified;
        }

        public RemoteFile(Delimon.Win32.IO.DirectoryInfo dir1)
        {
            this.Name = dir1.Name;
            this.FullName = dir1.FullName;
            this.Length = 0;
            this.ParentDirectory = "";//dir1.Parent.FullName;
            this.IsDirectory = true;
            this.LastWriteTime = dir1.LastWriteTime;
            this.LastWriteTimeUtc = dir1.LastWriteTimeUtc;
            //this.FileOperation = FileOperations.Modified;
        }

        public RemoteFile(System.IO.DirectoryInfo dir1, FileOperations fileOp)
        {
            this.Name = dir1.Name;
            this.FullName = dir1.FullName;
            this.Length = 0;
            this.ParentDirectory = "";//dir1.Parent.FullName;
            this.IsDirectory = true;
            this.LastWriteTime = dir1.LastWriteTime;
            this.LastWriteTimeUtc = dir1.LastWriteTimeUtc;
            this.FileOperation = fileOp;
        }

        public RemoteFile(Delimon.Win32.IO.DirectoryInfo dir1, FileOperations fileOp)
        {
            this.Name = dir1.Name;
            this.FullName = dir1.FullName;
            this.Length = 0;
            this.ParentDirectory = "";//dir1.Parent.FullName;
            this.IsDirectory = true;
            this.LastWriteTime = dir1.LastWriteTime;
            this.LastWriteTimeUtc = dir1.LastWriteTimeUtc;
            this.FileOperation = fileOp;
        }

        public RemoteFile(System.IO.DirectoryInfo dir1, FileOperations fileOp, string newFullName)
        {
            this.Name = dir1.Name;
            this.FullName = dir1.FullName;
            this.Length = 0;
            this.ParentDirectory = "";//dir1.Parent.FullName;
            this.IsDirectory = true;
            this.LastWriteTime = dir1.LastWriteTime;
            this.LastWriteTimeUtc = dir1.LastWriteTimeUtc;
            this.FileOperation = fileOp;
            this.NewFullName = newFullName;
        }

        public RemoteFile(Delimon.Win32.IO.DirectoryInfo dir1, FileOperations fileOp, string newFullName)
        {
            this.Name = dir1.Name;
            this.FullName = dir1.FullName;
            this.Length = 0;
            this.ParentDirectory = "";//dir1.Parent.FullName;
            this.IsDirectory = true;
            this.LastWriteTime = dir1.LastWriteTime;
            this.LastWriteTimeUtc = dir1.LastWriteTimeUtc;
            this.FileOperation = fileOp;
            this.NewFullName = newFullName;
        }
        #endregion
    }
    

}