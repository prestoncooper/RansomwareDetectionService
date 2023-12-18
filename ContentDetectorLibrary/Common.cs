using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Text;
using System.Configuration;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;

namespace RansomwareDetection.ContentDetectorLib
{

    /// <summary>
    /// File Result - File Search Result Class
    /// </summary>
    public class FileResult
    {
        
        #region "Properties"
        public string Name { get; set; }
        public string FullPath { get; set; }
        public string Extension { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime LastWriteTime { get; set; }
        public string Owner { get; set; }
        public long Length { get; set; }
        public string ParentDirectoryPath { get; set; }
        public Common.FileFilterObjectType ObjectType { get; set; }
        public string Comment { get; set; }
        public string FileFilterSearched { get; set; }
        public bool Deleted { get; set; }

        #endregion





        #region "Methods"
        public FileResult()
        {

            Name = "";
            FullPath = "";
            Extension = "";
            CreationTime = DateTime.MinValue;
            LastWriteTime = DateTime.MinValue;
            Owner = "";
            Length = 0;
            ParentDirectoryPath = "";
            ObjectType = Common.FileFilterObjectType.File;
            Comment = "";
            FileFilterSearched = "";
            Deleted = false;
        }

        public FileResult(Alphaleonis.Win32.Filesystem.DirectoryInfo ddir)
        {
            FileResultContructor(ddir, "", "");
        }

        public FileResult(Alphaleonis.Win32.Filesystem.DirectoryInfo ddir, string strComment)
        {
            FileResultContructor(ddir, strComment, "");
        }

        private void FileResultContructor(Alphaleonis.Win32.Filesystem.DirectoryInfo ddir, string strComment, string strFileFilterSearched)
        {
            Name = ddir.Name;
            FullPath = ddir.FullName;
            Extension = "";
            CreationTime = ddir.CreationTime;
            LastWriteTime = ddir.LastWriteTime;
            Owner = Common.GetFileOwner(FullPath);
            Length = 0;
            ParentDirectoryPath = ddir.Parent.FullName;
            ObjectType = Common.FileFilterObjectType.Folder;
            Comment = strComment;
            FileFilterSearched = strFileFilterSearched;
            Deleted = false;
        }

        public FileResult(Alphaleonis.Win32.Filesystem.DirectoryInfo ddir, string strComment, string strFileFilterSearched)
        {
            FileResultContructor(ddir, strComment, strFileFilterSearched);
        }

        public FileResult(Alphaleonis.Win32.Filesystem.FileInfo dfile)
        {
            FileResultContructor(dfile, "", "");
        }

        public FileResult(Alphaleonis.Win32.Filesystem.FileInfo dfile, string strComment)
        {
            FileResultContructor(dfile, strComment, "");
        }

        private void FileResultContructor(Alphaleonis.Win32.Filesystem.FileInfo dfile, string strComment, string strFileFilterSearched)
        {
            Name = dfile.Name;
            FullPath = dfile.FullName;
            Extension = dfile.Extension;
            CreationTime = dfile.CreationTime;
            LastWriteTime = dfile.LastWriteTime;
            Owner = Common.GetFileOwner(FullPath);
            Length = dfile.Length;
            ParentDirectoryPath = dfile.Directory.FullName;
            ObjectType = Common.FileFilterObjectType.File;
            Comment = strComment;
            FileFilterSearched = strFileFilterSearched;
            Deleted = false;
        }

        public FileResult(Alphaleonis.Win32.Filesystem.FileInfo dfile, string strComment, string strFileFilterSearched)
        {
            FileResultContructor(dfile, strComment, strFileFilterSearched);
            
        }

        private void FileResultContructor(string strfilefullpath, string strComment, bool blDeleted)
        {
            Name = strfilefullpath.Substring(strfilefullpath.LastIndexOf('\\') + 1,strfilefullpath.Length - (strfilefullpath.LastIndexOf('\\') + 1));
            
            FullPath = strfilefullpath;
            Extension = strfilefullpath.Substring(strfilefullpath.LastIndexOf('.'), strfilefullpath.Length - strfilefullpath.LastIndexOf('.'));
            CreationTime = DateTime.MinValue;
            LastWriteTime = DateTime.MinValue;
            Owner = "";
            Length = 0;
            ParentDirectoryPath = Common.WindowsPathClean(strfilefullpath.Replace(Name, ""));
            ObjectType = Common.FileFilterObjectType.File;
            Comment = strComment;
            FileFilterSearched = "";
            Deleted = blDeleted;
        }

        public FileResult(string strfilefullpath, string strComment, bool blDeleted)
        {
            FileResultContructor(strfilefullpath, strComment, blDeleted);
        }

        public FileResult(string strfilefullpath, string strComment)
        {
            FileResultContructor(strfilefullpath, strComment, false);
        }

        public static string FileResultCollectionToCSV(List<FileResult> results)
        {
            StringBuilder sbCSV = new StringBuilder();
            sbCSV.AppendLine("\"Name\",\"FullPath\",\"Extension\",\"CreationTime\",\"LastWriteTime\",\"Owner\",\"Length\",\"ParentDirectoryPath\",\"FileFilterSearched\",\"Deleted\",\"Comment\"");
            if (results != null)
            {
                foreach (FileResult frFile1 in results)
                {
                    sbCSV.AppendLine("\"" + frFile1.Name + "\",\"" + frFile1.FullPath + "\",\"" + frFile1.Extension + "\",\"" + frFile1.CreationTime.ToString("G") + "\",\"" + frFile1.LastWriteTime.ToString("G") + "\",\"" + frFile1.Owner + "\",\"" + frFile1.Length.ToString() + "\",\"" + frFile1.ParentDirectoryPath + "\",\"" + frFile1.FileFilterSearched + "\",\"" + frFile1.Deleted.ToString() + "\",\"" + frFile1.Comment + "\"");
                }
            }
            string strCSV = sbCSV.ToString();
            sbCSV.Clear();
            return strCSV;
            
        }
        #endregion
    }
    /// <summary>
    /// Common class Reusable code for other classes
    /// </summary>
    public static class Common
    {

        public enum FileFilterObjectType
        {
            None = 0,
            File = 1,
            Folder = 2,
            Both = 3

        }

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
                strPath = @"\\?\UNC\" + uri.LocalPath.Substring(2, uri.LocalPath.Length - 2);
            }
            else
            {
                //Local path prepend with @"\\?\" so that long file paths are supported by the search.
                strPath = @"\\?\" + uri.LocalPath;
            }
            return strPath;
        }

        /// <summary>
        /// Get File Owner from FullFilePath works with long file paths
        /// </summary>
        /// <param name="strFileName"></param>
        /// <returns></returns>
        public static string GetFileOwner(string strFileName)
        {
            string strOwner = "";
            string strLongFilePath = LongPathPrepend(strFileName);
            Microsoft.Win32.Security.SecurityDescriptor secDesc = null;
            try
            {
                if (Common.FileExists(strFileName))
                {
                    secDesc = Microsoft.Win32.Security.SecurityDescriptor.GetFileSecurity(strLongFilePath, Microsoft.Win32.Security.SECURITY_INFORMATION.OWNER_SECURITY_INFORMATION);
                    strOwner = Common.FixNullstring(secDesc.Owner.DomainName) + "\\" + Common.FixNullstring(secDesc.Owner.AccountName);
                }

            }
            catch (Exception)
            {
                strOwner = "";
            }
            finally
            {
                if (secDesc != null)
                {
                    secDesc.Dispose();
                    secDesc = null;
                }
            }

            return strOwner;
        }

        /// <summary>
        /// Converts Path to URI and makes the link clickable in an email
        /// </summary>
        /// <param name="strpath"></param>
        /// <returns></returns>
        public static string GetPathToHTMLAnchor(string strpath)
        {
            string strNewPath = "";
            try
            {
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
            }
            catch (Exception)
            {
                strNewPath = strpath;
            }

            return strNewPath;
        }

        
        



        /// <summary>
        /// Checks whether file is locked by the operating system or another process.  Also if you have permission to the file.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static bool IsFileLocked(Alphaleonis.Win32.Filesystem.FileInfo file)
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
        public static Alphaleonis.Win32.Filesystem.FileInfo RefreshFileInfo(Alphaleonis.Win32.Filesystem.FileInfo file)
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
                blSuccess = Alphaleonis.Win32.Filesystem.Directory.Exists(strPath);
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
                blSuccess = Alphaleonis.Win32.Filesystem.File.Exists(strPath);
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
            if (strBasePath.Trim() != "\\" && strBasePath != "")
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

        public static bool IsHexString(string text)
        {
            for (var i = 0; i < text.Length; i++)
            {
                var current = text[i];
                if (!(Char.IsDigit(current) || (current >= 'a' && current <= 'f') || (current >= 'A' && current <= 'F')))
                {
                    return false;
                }
            }
            return true;
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
      
    }
}
