namespace RansomwareDetection.ContentDetectorLib
{
	#region Using directives.
	// ----------------------------------------------------------------------

	using System;
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.IO;
	//using Archive;
	using Content;

	// ----------------------------------------------------------------------
	#endregion

	/////////////////////////////////////////////////////////////////////////

	/// <summary>
	/// Detection of prohibited content, depending on the file extension and
	/// the content.
	/// </summary>
	public sealed class ContentDetectorEngine
	{
		#region Public methods.
		// ------------------------------------------------------------------

		/// <summary>
		/// Determines whether the specified file contains prohibited content.
		/// </summary>
		/// <param name="filePath">The file path.</param>
		public bool ContainsFileProhibitedContent(
			Delimon.Win32.IO.FileInfo filePath )
		{
			return DoCheckContainsFileProhibitedContent( filePath, 0 );
		}

		/// <summary>
		/// Determines whether the specified folder contains files with
		/// prohibited content.
		/// </summary>
		/// <param name="folderPath">The folder path.</param>
		/// <returns>
		/// 	<c>true</c> if [contains folder prohibited content] 
		/// [the specified folder path]; otherwise, <c>false</c>.
		/// </returns>
		public Delimon.Win32.IO.FileInfo[] ContainsFolderProhibitedContent(
			Delimon.Win32.IO.DirectoryInfo folderPath )
		{
			return ContainsFolderProhibitedContent( folderPath, false );
		}

		/// <summary>
		/// Determines whether the specified folder contains files with
		/// prohibited content.
		/// </summary>
		/// <param name="folderPath">The folder path.</param>
		/// <param name="recursive">if set to <c>true</c> [recursive].</param>
		/// <returns>
		/// 	<c>true</c> if [contains folder prohibited content] 
		/// [the specified folder path]; otherwise, <c>false</c>.
		/// </returns>
		public Delimon.Win32.IO.FileInfo[] ContainsFolderProhibitedContent(
            Delimon.Win32.IO.DirectoryInfo folderPath,
			bool recursive )
		{
			Trace.WriteLine(
				string.Format(
				@"Checking folder '{0}'.",
				folderPath ) );

            List<Delimon.Win32.IO.FileInfo> prohibitedFiles = new List<Delimon.Win32.IO.FileInfo>();

			if ( true )
			{
                Delimon.Win32.IO.FileInfo[] filePaths = folderPath.GetFiles();

				int index = 0;
                foreach (Delimon.Win32.IO.FileInfo filePath in filePaths)
				{
					Trace.WriteLine(
						string.Format(
						@"[{0}/{1}] Checking file '{2}' ({3:0,0} bytes).",
						index + 1, filePaths.Length,
						filePath,
						filePath.Length ) );

					if ( ContainsFileProhibitedContent( filePath ) )
					{
						prohibitedFiles.Add( filePath );
					}

					index++;
				}
			}

			// --

			if ( recursive )
			{
                Delimon.Win32.IO.DirectoryInfo[] folderPaths = folderPath.GetDirectories();

                foreach (Delimon.Win32.IO.DirectoryInfo childFolderPath in folderPaths)
				{
					prohibitedFiles.AddRange(
						ContainsFolderProhibitedContent(
						childFolderPath,
						recursive ) );
				}
			}

			return prohibitedFiles.ToArray();
		}


        /// <summary>
        /// Determines whether the specified folder contains files with
        /// file extensions that do not match the content.
        /// </summary>
        /// <param name="folderPath">The folder path.</param>
        /// <param name="recursive">if set to <c>true</c> [recursive].</param>
        /// <returns>
        /// 	<c>true</c> if [contains folder prohibited content] 
        /// [the specified folder path]; otherwise, <c>false</c>.
        /// </returns>
        public void ContainsFolderVerifyContent(
            Delimon.Win32.IO.DirectoryInfo folderPath,
            bool recursive,
            ref List<Delimon.Win32.IO.FileInfo> verifiedFiles,
            ref List<Delimon.Win32.IO.FileInfo> unVerifiedFiles,
            ref List<Delimon.Win32.IO.FileInfo> unknownFiles
            )
        {
            Trace.WriteLine(
                string.Format(
                @"Checking folder '{0}'.",
                folderPath));


            if (true)
            {
                Delimon.Win32.IO.FileInfo[] filePaths = folderPath.GetFiles();

                int index = 0;
                foreach (Delimon.Win32.IO.FileInfo filePath in filePaths)
                {
                    Trace.WriteLine(
                        string.Format(
                        @"[{0}/{1}] Checking file '{2}' ({3:0,0} bytes).",
                        index + 1, filePaths.Length,
                        filePath,
                        filePath.Length));
                    if (!HeaderSignature.ExtensionSupported(filePath.Extension))
                    {
                        unknownFiles.Add(filePath);
                    }
                    else if (!IsVerifiedContent(filePath))
                    {
                        unVerifiedFiles.Add(filePath);
                    }
                    else
                    {
                        verifiedFiles.Add(filePath);
                    }

                    index++;
                }
            }

            // --

            if (recursive)
            {
                Delimon.Win32.IO.DirectoryInfo[] folderPaths = folderPath.GetDirectories();

                foreach (Delimon.Win32.IO.DirectoryInfo childFolderPath in folderPaths)
                {
                        ContainsFolderVerifyContent(
                        childFolderPath,
                        recursive,ref verifiedFiles, ref unVerifiedFiles, ref unknownFiles);
                }
            }

            
        }


        

		// ------------------------------------------------------------------
		#endregion

		#region Private methods.
		// ------------------------------------------------------------------

		/// <summary>
		/// Does the content of the check contains file prohibited.
		/// </summary>
		/// <param name="filePath">The file path.</param>
		/// <param name="indentLevel">The nesting depth.</param>
		/// <returns></returns>
		private bool DoCheckContainsFileProhibitedContent(
            Delimon.Win32.IO.FileInfo filePath,
			int indentLevel )
		{
			if ( filePath == null || !filePath.Exists || filePath.Length <= 0 ||
				indentLevel > maximumNestingDepth )
			{
				return false;
			}
			else
			{
				Trace.WriteLine(
					string.Format(
					@"Checking file '{0}' ({1:0,0} bytes).",
					filePath,
					filePath.Length ) );

				return IsProhibitedContent( filePath, indentLevel );
			}
		}

        /// <summary>
        /// Does the content of the check contains file prohibited.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="indentLevel">The nesting depth.</param>
        /// <returns></returns>
        private bool DoVerifyFileHeader(
            Delimon.Win32.IO.FileInfo filePath)
        {
            if (filePath == null || !filePath.Exists || filePath.Length <= 0)
            {
                return false;
            }
            else
            {
                Trace.WriteLine(
                    string.Format(
                    @"Verifying file '{0}' ({1:0,0} bytes).",
                    filePath,
                    filePath.Length));

                return IsVerifiedContent(filePath);
            }
        }

		/// <summary>
		/// Determines whether [is prohibited content] [the specified file path].
		/// </summary>
		/// <param name="filePath">The file path.</param>
		/// <param name="indentLevel">The nesting depth.</param>
		/// <returns>
		/// 	<c>true</c> if [is prohibited content] [the specified file path]; otherwise, <c>false</c>.
		/// </returns>
		private bool IsProhibitedContent(
            Delimon.Win32.IO.FileInfo filePath,
			int indentLevel )
		{
			/*if ( ArchiveExtractor.IsArchiveExtension( filePath.Extension ) )
			{
				if ( indentLevel < maximumNestingDepth )
				{
                    System.IO.FileInfo filePath1 = new System.IO.FileInfo(filePath.FullName);
                    DirectoryInfo temporaryFolder = ExtractArchive(filePath1);
					try
					{
                        Delimon.Win32.IO.DirectoryInfo dtemporaryFolder = new Delimon.Win32.IO.DirectoryInfo(temporaryFolder.FullName);
                        return ProcessDirectory(dtemporaryFolder, indentLevel);
					}
					finally
					{
						temporaryFolder.Delete( true );
					}
				}
				else
				{
					return false;
				}
			}
			else
			{*/
				return CoreProcessFileContent( filePath );
			//}
		}


        /// <summary>
        /// Determines whether File Extension Matches Header of the file
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="indentLevel">The nesting depth.</param>
        /// <returns>
        /// 	<c>true</c> if [is prohibited content] [the specified file path]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsVerifiedContent(Delimon.Win32.IO.FileInfo filePath)
        {
                return CoreVerifyFileContent(filePath);
        }

		/*/// <summary>
		/// Extracts the archive.
		/// </summary>
		/// <param name="filePath">The file path.</param>
		/// <returns></returns>
        private static DirectoryInfo ExtractArchive(
            FileInfo filePath)
		{
			Trace.WriteLine(
				string.Format(
				@"Extracting archive file '{0}' ({1:0,0} bytes).",
				filePath,
				filePath.Length ) );

            DirectoryInfo folderPath = new DirectoryInfo(
				Path.Combine(
				Path.GetTempPath(),
				@"_CDL_" +
				Guid.NewGuid().GetHashCode() ) );

			ArchiveExtractor extractor = new ArchiveExtractor(
				filePath,
				folderPath );

			extractor.Extract();
			return folderPath;
		}*/

		/// <summary>
		/// Cores the content of the process file.
		/// </summary>
		/// <param name="filePath">The file path.</param>
		/// <returns></returns>
		private static bool CoreProcessFileContent(
            Delimon.Win32.IO.FileInfo filePath)
		{
			Trace.WriteLine(
				string.Format(
				@"Processing content of file '{0}' ({1:0,0} bytes).",
				filePath,
				filePath.Length ) );

			SingleFileContentProcessor processor =
				new SingleFileContentProcessor( filePath );

			bool result = processor.ContainsProhibitedContent(true);

			if ( result )
			{
				Trace.WriteLine(
					string.Format(
						@"Detected PROHIBITED content in file '{0}' ({1:0,0} bytes).",
						filePath,
						filePath.Length ) );
			}
			else
			{
				Trace.WriteLine(
					string.Format(
						@"Detected NO prohibited content in file '{0}' ({1:0,0} bytes).",
						filePath,
						filePath.Length ) );
			}

			return result;
		}

        /// <summary>
        /// Cores the content of the process file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns></returns>
        private static bool CoreVerifyFileContent(
            Delimon.Win32.IO.FileInfo filePath)
        {
            Trace.WriteLine(
                string.Format(
                @"Processing content of file '{0}' ({1:0,0} bytes).",
                filePath,
                filePath.Length));

            SingleFileContentProcessor processor =
                new SingleFileContentProcessor(filePath);

            bool result = processor.VerifyHeaderContent();

            if (!result)
            {
                Trace.WriteLine(
                    string.Format(
                        @"Detected Unverified content in file '{0}' ({1:0,0} bytes).",
                        filePath,
                        filePath.Length));
            }
            else
            {
                Trace.WriteLine(
                    string.Format(
                        @"Detected Verified content in file '{0}' ({1:0,0} bytes).",
                        filePath,
                        filePath.Length));
            }

            return result;
        }

		/// <summary>
		/// Processes the directory.
		/// </summary>
		/// <param name="temporaryFolder">The temporary folder.</param>
		/// <param name="nestingDepth">The nesting depth.</param>
		/// <returns></returns>
		private bool ProcessDirectory(
            Delimon.Win32.IO.DirectoryInfo temporaryFolder,
			int nestingDepth )
		{
            Delimon.Win32.IO.FileInfo[] filePaths = temporaryFolder.GetFiles();

            foreach (Delimon.Win32.IO.FileInfo filePath in filePaths)
			{
				if ( DoCheckContainsFileProhibitedContent( filePath, nestingDepth + 1 ) )
				{
					return true;
				}
			}

            Delimon.Win32.IO.DirectoryInfo[] folderPaths = temporaryFolder.GetDirectories();

            foreach (Delimon.Win32.IO.DirectoryInfo folderPath in folderPaths)
			{
				if ( ProcessDirectory( folderPath, nestingDepth + 1 ) )
				{
					return true;
				}
			}

			// Nothing found.
			return false;
		}

		// ------------------------------------------------------------------
		#endregion

		#region Private variables.
		// ------------------------------------------------------------------

		/// <summary>
		/// The maximum nesting depth.
		/// </summary>
		private const int maximumNestingDepth = 10;

        

		// ------------------------------------------------------------------
		#endregion
	}

	/////////////////////////////////////////////////////////////////////////
}