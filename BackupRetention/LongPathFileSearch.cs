using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using System.Text.RegularExpressions;
//https://blogs.msdn.microsoft.com/bclteam/2007/03/26/long-paths-in-net-part-2-of-3-long-path-workarounds-kim-hamilton/
//http://www.pinvoke.net/default.aspx/kernel32.findfirstfileex
//https://msdn.microsoft.com/en-us/library/windows/desktop/aa364418(v=vs.85).aspx
//https://msdn.microsoft.com/en-us/library/windows/desktop/aa364419(v=vs.85).aspx
//http://www.pinvoke.net/default.aspx/kernel32.findfirstfileex
//http://www.pinvoke.net/default.aspx/kernel32/FindFirstFile.html
//http://stackoverflow.com/questions/1248816/c-sharp-call-win32-api-for-long-file-paths

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
    public class LongPathFileSearch
    {
        public const int FIND_FIRST_EX_CASE_SENSITIVE = 1;  //Case Sensitive Search Option
        public const int FIND_FIRST_EX_LARGE_FETCH = 2;     //supposedly helps with performance

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        internal static extern IntPtr FindFirstFile(string lpFileName, out WIN32_FIND_DATA lpFindFileData);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        internal static extern bool FindNextFile(IntPtr hFindFile, out WIN32_FIND_DATA lpFindFileData);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool FindClose(IntPtr hFindFile);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern IntPtr FindFirstFileEx(
            string lpFileName,
            FINDEX_INFO_LEVELS fInfoLevelId,
            out WIN32_FIND_DATA lpFindFileData,
            FINDEX_SEARCH_OPS fSearchOp,
            IntPtr lpSearchFilter,
            int dwAdditionalFlags);

        /// <summary>
        /// Using Basic helps with performance
        /// </summary>
        public enum FINDEX_INFO_LEVELS
        {
            FindExInfoStandard = 0,
            FindExInfoBasic = 1
        }

        /// <summary>
        /// Search Options
        /// </summary>
        public enum FINDEX_SEARCH_OPS
        {
            FindExSearchNameMatch = 0,
            FindExSearchLimitToDirectories = 1,
            FindExSearchLimitToDevices = 2
        }

        internal static IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);
        internal static int FILE_ATTRIBUTE_DIRECTORY = 0x00000010;
        internal const int MAX_PATH = 260;

        [StructLayout(LayoutKind.Sequential)]
        internal struct FILETIME
        {
            internal uint dwLowDateTime;
            internal uint dwHighDateTime;
        };

        /// <summary>
        /// Find Results struct
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct WIN32_FIND_DATA
        {
            internal FileAttributes dwFileAttributes;
            internal FILETIME ftCreationTime;
            internal FILETIME ftLastAccessTime;
            internal FILETIME ftLastWriteTime;
            internal int nFileSizeHigh;
            internal int nFileSizeLow;
            internal int dwReserved0;
            internal int dwReserved1;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_PATH)]
            internal string cFileName;
            // not using this
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
            internal string cAlternate;
        }

        [Flags]
        public enum EFileAccess : uint
        {
            GenericRead = 0x80000000,
            GenericWrite = 0x40000000,
            GenericExecute = 0x20000000,
            GenericAll = 0x10000000,
        }

        [Flags]
        public enum EFileShare : uint
        {
            None = 0x00000000,
            Read = 0x00000001,
            Write = 0x00000002,
            Delete = 0x00000004,
        }

        public enum ECreationDisposition : uint
        {
            New = 1,
            CreateAlways = 2,
            OpenExisting = 3,
            OpenAlways = 4,
            TruncateExisting = 5,
        }

        /// <summary>
        /// File Attributes
        /// </summary>
        [Flags]
        public enum EFileAttributes : uint
        {
            Readonly = 0x00000001,
            Hidden = 0x00000002,
            System = 0x00000004,
            Directory = 0x00000010,
            Archive = 0x00000020,
            Device = 0x00000040,
            Normal = 0x00000080,
            Temporary = 0x00000100,
            SparseFile = 0x00000200,
            ReparsePoint = 0x00000400,
            Compressed = 0x00000800,
            Offline = 0x00001000,
            NotContentIndexed = 0x00002000,
            Encrypted = 0x00004000,
            Write_Through = 0x80000000,
            Overlapped = 0x40000000,
            NoBuffering = 0x20000000,
            RandomAccess = 0x10000000,
            SequentialScan = 0x08000000,
            DeleteOnClose = 0x04000000,
            BackupSemantics = 0x02000000,
            PosixSemantics = 0x01000000,
            OpenReparsePoint = 0x00200000,
            OpenNoRecall = 0x00100000,
            FirstPipeInstance = 0x00080000
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SECURITY_ATTRIBUTES
        {
            public int nLength;
            public IntPtr lpSecurityDescriptor;
            public int bInheritHandle;
        }

        /// <summary>
        /// Verify File Filter Pattern
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static bool VerifyPattern(string pattern)
        {
            try
            {
                //If the pattern can convert to regex then it is a valid file filter
                Regex regex = FindFilesPatternToRegex.Convert(pattern);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        /// <summary>
        /// This class is used to verify File Filters
        /// </summary>
        internal static class FindFilesPatternToRegex
        {
            private static Regex HasQuestionMarkRegEx = new Regex(@"\?", RegexOptions.Compiled);
            private static Regex IlegalCharactersRegex = new Regex("[" + @"\/:<>|" + "\"]", RegexOptions.Compiled);
            private static Regex CatchExtentionRegex = new Regex(@"^\s*.+\.([^\.]+)\s*$", RegexOptions.Compiled);
            private static string NonDotCharacters = @"[^.]*";
            public static Regex Convert(string pattern)
            {
                if (pattern == null)
                {
                    throw new ArgumentNullException();
                }
                pattern = pattern.Trim();
                if (pattern.Length == 0)
                {
                    throw new ArgumentException("Pattern is empty.");
                }
                if (IlegalCharactersRegex.IsMatch(pattern))
                {
                    throw new ArgumentException("Patterns contains ilegal characters.");
                }
                bool hasExtension = CatchExtentionRegex.IsMatch(pattern);
                bool matchExact = false;
                if (HasQuestionMarkRegEx.IsMatch(pattern))
                {
                    matchExact = true;
                }
                else if (hasExtension)
                {
                    matchExact = CatchExtentionRegex.Match(pattern).Groups[1].Length != 3;
                }
                string regexString = Regex.Escape(pattern);
                regexString = "^" + Regex.Replace(regexString, @"\\\*", ".*");
                regexString = Regex.Replace(regexString, @"\\\?", ".");
                if (!matchExact && hasExtension)
                {
                    regexString += NonDotCharacters;
                }
                regexString += "$";
                Regex regex = new Regex(regexString, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                return regex;
            }
        }

        /*
        public static string WindowsPathClean(string strPath)
        {
            if (strPath != null)
            {
                if (strPath != "")
                {
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
                }
            }
            return strPath;
        }
        
        /// <summary>
        /// Converts Path to URI and makes the link clickable in an email
        /// </summary>
        /// <param name="strpath"></param>
        /// <returns></returns>
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
        */

        /// <summary>
        /// Converts Prepended Path to UNC Full Path or Local Drive Path
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string RemovePrependGetPath(string value)
        {
            value = value.Replace(@"\\?\UNC\", @"\\");
            value = value.Replace(@"\\?\", "");
            System.Uri uri = new System.Uri(value);
            
            return uri.LocalPath;
        }

        /// <summary>
        /// Prepend "\\?\" to drive path and "\\?\UNC\" to UNC path so that long file names can be handled without an error
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string LongPathPrepend(string value)
        {
            //Just in case the prepend was already added remove it.
            value = value.Replace(@"\\?\UNC\", @"\\");
            value = value.Replace(@"\\?\", "");

            //Remove the ending backslashes and fix any common problems with the path
            value = Common.WindowsPathClean(value);

            string strPath = "";
            System.Uri uri = new System.Uri(value);
            if (uri.IsUnc)
            {
                //Add the prepend and remove the 2 beginning back slashes for the UNC path so that long file paths are supported
                strPath = @"\\?\UNC\" + uri.LocalPath.Substring(2,uri.LocalPath.Length-2);
            }
            else
            {
                //Local path prepend with @"\\?\" so that long file paths are supported by the search.
                strPath = @"\\?\" + uri.LocalPath;
            }
            return strPath;
        }

        

        /// <summary>
        /// Recursively Searches for all files and folders specified by the filter; Optimized for best performance by only going through folder structure once and searching the all of the file filters in each folder
        /// </summary>
        /// <param name="dirName"></param>
        /// <param name="filter"></param>
        /// <param name="checkSubFolders"></param>
        /// <param name="excludeFolders">Separate folders with semicolon no back slashes or forward slashes</param>
        /// <returns></returns>
        public static List<string> FindAllfiles(string dirName, DataTable dtFilters, bool checkSubFolders, string excludeFolders, ref bool blShuttingDown)
        {
            List<string> results = new List<string>();
            string strDirConverted = LongPathPrepend(dirName);
            WIN32_FIND_DATA findData;
            IntPtr findHandle = INVALID_HANDLE_VALUE;
            if (!checkSubFolders)
            {
                //Get find results for the current directory being searched and no recursion
                results = FindImmediateFilesAndDirs(dirName, dtFilters, checkSubFolders);
            }
            else
            {
                try
                {
                    //Search through each folder
                    findHandle = FindFirstFileEx(strDirConverted + @"\*", FINDEX_INFO_LEVELS.FindExInfoBasic, out findData, FINDEX_SEARCH_OPS.FindExSearchLimitToDirectories, IntPtr.Zero, FIND_FIRST_EX_LARGE_FETCH);
                
                    if (findHandle != INVALID_HANDLE_VALUE)
                    {
                        bool found;
                        //Get find results for the current directory being searched
                        List<string> mainResults = FindImmediateFilesAndDirs(dirName, dtFilters, checkSubFolders);
                        results.AddRange(mainResults);
                        mainResults.Clear();
                        do
                        {
                            if (blShuttingDown)
                            {
                                results.Add("Ransomware Detection Service Search Shutting Down at:" + dirName);
                                break;
                            }
                            string currentFileName = findData.cFileName;

                            // if this is a directory, find its contents
                            if (((int)findData.dwFileAttributes & FILE_ATTRIBUTE_DIRECTORY) != 0)
                            {
                                bool blIgnoreDirectory = false;
                                char[] delimiters = new char[] { ';' };
                                string[] strArr_excludedfolders = excludeFolders.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

                                if (!(strArr_excludedfolders == null || strArr_excludedfolders.Length == 0))
                                {
                                    //loop through excluded folders
                                    foreach (string strExclude in strArr_excludedfolders)
                                    {
                                        if (currentFileName == strExclude)
                                        {
                                            blIgnoreDirectory = true;
                                        }
                                    }
                                }


                                if (!blIgnoreDirectory && !(currentFileName == "." || currentFileName == ".."))
                                {
                                    if (checkSubFolders)
                                    {
                                        //Recursively go through all folders and sub folders
                                        List<string> childResults2 = FindAllfiles(Path.Combine(dirName, currentFileName), dtFilters, checkSubFolders, excludeFolders , ref blShuttingDown);
                                        results.AddRange(childResults2);
                                        childResults2.Clear();
                                    }
                                    //string strFilePath = ConvertURIToUNCPath(Path.Combine(strDirConverted, currentFileName));
                                    //results.Add(strFilePath);
                                }
                            }
                            found = FindNextFile(findHandle, out findData);
                        }
                        while (found);
                    }
                    else
                    {
                        if (results.Count == 0)
                        {
                            results.Add("File Path Not Found:" + dirName);
                        }
                    }
                    
                }
                catch
                {
                    if (results.Count == 0)
                    {
                        results.Add("Error occurred while in Path:" + dirName);
                    }
                }
                finally
                {
                    // close the find handle
                    FindClose(findHandle);
                }
            }

            return results;

        }

        /// <summary>
        /// Find Files and Directories in the immediate folder - handles long path names, expects local or UNC path
        /// </summary>
        /// <param name="dirName">Directory to search in UNC path or local path format</param>
        /// <param name="dtFilters">Data table with "Enabled" and "FileFilter" columns required</param>
        /// <param name="checkSubFolders">Recursively check all sub folders</param>
        /// <returns></returns>
        public static List<string> FindImmediateFilesAndDirs(string dirName, DataTable dtFilters, bool checkSubFolders)
        {
            string strDirConverted = LongPathPrepend(dirName);
            List<string> results = new List<string>();
            IntPtr findHandle = INVALID_HANDLE_VALUE;
            WIN32_FIND_DATA findData;
            if (dtFilters != null)
            {
                //For Each File filter find them in the current folder
                foreach (DataRow row in dtFilters.Rows)
                {
                    try
                    {
                        string strFileFilter = Common.FixNullstring(row["FileFilter"]);
                        bool blFilterEnabled = Common.FixNullbool(row["Enabled"]);
                        if (blFilterEnabled && strFileFilter != "")
                        {
                            //Valid File Filter?
                            if (VerifyPattern(strFileFilter) && Delimon.Win32.IO.Directory.Exists(dirName))
                            {
                                try
                                {
                                    //Search for the file filter in the current directory
                                    findHandle = FindFirstFileEx(strDirConverted + @"\" + strFileFilter, FINDEX_INFO_LEVELS.FindExInfoBasic, out findData, FINDEX_SEARCH_OPS.FindExSearchNameMatch, IntPtr.Zero, FIND_FIRST_EX_LARGE_FETCH);
                                
                                    if (findHandle != INVALID_HANDLE_VALUE)
                                    {
                                        bool found;
                                        do
                                        {
                                            string currentFileName = findData.cFileName;

                                            // if this is a directory, add directory found to the results.
                                            if (((int)findData.dwFileAttributes & FILE_ATTRIBUTE_DIRECTORY) != 0)
                                            {
                                                if (currentFileName != "." && currentFileName != "..")
                                                {
                                                    string strFilePath = RemovePrependGetPath(Path.Combine(dirName, currentFileName));
                                                    results.Add(strFilePath);
                                                }
                                            }

                                            // it’s a file; add it to the results
                                            else
                                            {
                                                string strFilePath = RemovePrependGetPath(Path.Combine(dirName, currentFileName));
                                                results.Add(strFilePath);
                                            }

                                            // find next if any
                                            found = FindNextFile(findHandle, out findData);
                                        }
                                        while (found);
                                    }
                                    
                                }
                                finally
                                {
                                    // close the find handle
                                    FindClose(findHandle);
                                }
                            }
                            else
                            {
                                //invalid search filter
                                if (results.Count == 0)
                                {
                                    results.Add("Invalid Search Filter or Directory Problem: " + strFileFilter + " " + dirName );
                                }
                            }
                        }
                    }
                    catch
                    {

                    }
                }
            }
               
            return results;
        }

        /// <summary>
        /// Recursively Searches for all files and folders specified by the filter; Optimized for best performance
        /// </summary>
        /// <param name="dirName"></param>
        /// <param name="filter">Windows File Filter</param>
        /// <param name="checkSubFolders">Recursively checks all sub folders</param>
        /// <param name="excludeFolders">Separate folders with semicolon, no back slashes or forward slashes</param>
        /// <returns></returns>
        public static List<string> FindAllfiles(string dirName, string filter, bool checkSubFolders, string excludeFolders)
        {
            List<string> results = new List<string>();
            string strDirConverted = LongPathPrepend(dirName);
            WIN32_FIND_DATA findData;
            IntPtr findHandle = INVALID_HANDLE_VALUE;
            if (!checkSubFolders)
            {
                //Get find results for the current directory being searched and no recursion
                results = FindImmediateFilesAndDirs(dirName, filter, checkSubFolders);
            }
            else
            {
                //Search through each folder
                
                try
                {
                    findHandle = FindFirstFileEx(strDirConverted + "\\*", FINDEX_INFO_LEVELS.FindExInfoBasic, out findData, FINDEX_SEARCH_OPS.FindExSearchLimitToDirectories, IntPtr.Zero, FIND_FIRST_EX_LARGE_FETCH);
                
                    if (findHandle != INVALID_HANDLE_VALUE)
                    {
                        bool found;
                        //Get find results for the current directory being searched
                        List<string> mainResults = FindImmediateFilesAndDirs(dirName, filter, checkSubFolders);
                        results.AddRange(mainResults);
                        mainResults.Clear();
                        do
                        {
                            string currentFileName = findData.cFileName;

                            // if this is a directory, find its contents
                            if (((int)findData.dwFileAttributes & FILE_ATTRIBUTE_DIRECTORY) != 0)
                            {
                                bool blIgnoreDirectory = false;
                                char[] delimiters = new char[] { ';' };
                                string[] strArr_excludedfolders = excludeFolders.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

                                if (!(strArr_excludedfolders == null || strArr_excludedfolders.Length == 0))
                                {
                                    //loop through excluded folders
                                    foreach (string strExclude in strArr_excludedfolders)
                                    {
                                        if (currentFileName == strExclude)
                                        {
                                            blIgnoreDirectory = true;
                                        }
                                    }
                                }
                                

                                if (!blIgnoreDirectory && !(currentFileName == "." || currentFileName == ".."))
                                {
                                    if (checkSubFolders)
                                    {
                                        //Recursively go through all folders and sub folders
                                        List<string> childResults2 = FindAllfiles(Path.Combine(dirName, currentFileName), filter, checkSubFolders, excludeFolders);
                                        results.AddRange(childResults2);
                                        childResults2.Clear();
                                    }
                                    //string strFilePath = ConvertURIToUNCPath(Path.Combine(strDirConverted, currentFileName));
                                    //results.Add(strFilePath);
                                }
                            }
                            found = FindNextFile(findHandle, out findData);
                        }
                        while (found);
                    }
                }
                finally
                {
                    // close the find handle
                    FindClose(findHandle);
                }
            }

            return results;

        }

        /// <summary>
        /// Find Files and Directories in the immediate folder - handles long path names, expects local or UNC path
        /// </summary>
        /// <param name="dirName"></param>
        /// <param name="filter"></param>
        /// <param name="checkSubFolders"></param>
        /// <returns></returns>
        public static List<string> FindImmediateFilesAndDirs(string dirName, string filter, bool checkSubFolders) 
        {
            //string strDirConverted = ConvertPathToURI(dirName + "\\" + filter);
            //string strDirConverted = ConvertPathToURI(dirName);
            string strDirConverted = LongPathPrepend(dirName);
            List<string> results = new List<string>();
            IntPtr findHandle = INVALID_HANDLE_VALUE;
            WIN32_FIND_DATA findData;
            if (VerifyPattern(filter))
            {
                try
                {
                    //IntPtr findHandle = FindFirstFile(strDirConverted + "\\" + filter, out findData);
                    findHandle = FindFirstFileEx(strDirConverted + "\\" + filter, FINDEX_INFO_LEVELS.FindExInfoBasic, out findData, FINDEX_SEARCH_OPS.FindExSearchNameMatch, IntPtr.Zero, FIND_FIRST_EX_LARGE_FETCH);
                
                    if (findHandle != INVALID_HANDLE_VALUE)
                    {
                        bool found;
                        do
                        {
                            string currentFileName = findData.cFileName;

                            // if this is a directory, find its contents
                            if (((int)findData.dwFileAttributes & FILE_ATTRIBUTE_DIRECTORY) != 0)
                            {
                                if (currentFileName != "." && currentFileName != "..")
                                {
                                    string strFilePath = RemovePrependGetPath(Path.Combine(dirName, currentFileName));
                                    results.Add(strFilePath);
                                }
                            }

                            // it’s a file; add it to the results
                            else
                            {
                                string strFilePath = RemovePrependGetPath(Path.Combine(dirName, currentFileName));
                                results.Add(strFilePath);
                            }

                            // find next
                            found = FindNextFile(findHandle, out findData);
                        }
                        while (found);
                    }
                }
                finally
                {
                    // close the find handle
                    FindClose(findHandle);
                }
            }
            return results;
        }
    }
}
