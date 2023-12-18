namespace RansomwareDetection.ContentDetectorLib.Content
{
	#region Using directives.
	// ----------------------------------------------------------------------

	using System.IO;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System;

	// ----------------------------------------------------------------------
	#endregion

    //Added more StockSignatures 3-22-2016

	/////////////////////////////////////////////////////////////////////////

	/// <summary>
    /// http://www.codeproject.com/Articles/18611/A-small-Content-Detection-Library
    /// https://en.wikipedia.org/wiki/List_of_file_signatures
	/// http://en.wikipedia.org/wiki/Magic_number_%28programming%29.
    /// http://www.garykessler.net/library/file_sigs.html
    /// Reference Articles for further review and later features
    /// http://en.wikipedia.org/wiki/Magic_number_(programming)
    /// http://stackoverflow.com/questions/1654846/in-c-how-can-i-know-the-file-type-from-a-byte
    /// http://stackoverflow.com/questions/58510/using-net-how-can-you-find-the-mime-type-of-a-file-based-on-the-file-signature
    /// http://stackoverflow.com/questions/15300567/alternative-to-findmimefromdata-method-in-urlmon-dll-one-which-has-more-mime-typ
    /// http://stackoverflow.com/questions/58510/using-net-how-can-you-find-the-mime-type-of-a-file-based-on-the-file-signature
    /// https://en.wikipedia.org/wiki/Magic_number_(programming)
    /// http://www.garykessler.net/library/file_sigs.html
    /// https://msdn.microsoft.com/en-us/library/ms775107(v=vs.85).aspx
    /// https://en.wikipedia.org/wiki/List_of_file_signatures
    /// http://asecuritysite.com/forensics/magic
    /// https://www.nationalarchives.gov.uk/PRONOM/Default.aspx
    /// https://www.nationalarchives.gov.uk/aboutapps/pronom/droid-signature-files.htm
    /// </summary>
	internal sealed class HeaderSignature
	{
		#region Public constructors.
		// ------------------------------------------------------------------

		/// <summary>
		/// Initializes a new instance of the <see cref="HeaderSignature"/> class.
		/// </summary>
		/// <param name="checker">The checker.</param>
		/// <param name="signatureName">Name of the signature.</param>
		/// <param name="fileExtensions">The file extensions.</param>
		/// <param name="prohibitionMode">The prohibition mode.</param>
		public HeaderSignature(
			ISignatureChecker checker,
			string signatureName,
			string[] fileExtensions,
			ProhibitionMode prohibitionMode )
		{
			_checker = checker;
			_signatureName = signatureName;
			_fileExtensions = fileExtensions;
			_prohibitionMode = prohibitionMode;
            _byteoffset = 0;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="HeaderSignature"/> class.
		/// </summary>
		/// <param name="hexStringSignature">The hex string signature.</param>
		/// <param name="signatureName">Name of the signature.</param>
		/// <param name="fileExtensions">The file extensions.</param>
		/// <param name="prohibitionMode">The prohibition mode.</param>
		/// <param name="signatureMode">The signature mode.</param>
		public HeaderSignature(
			string hexStringSignature,
			string signatureName,
			string[] fileExtensions,
			ProhibitionMode prohibitionMode,
			SignatureMode signatureMode )
		{
			_checker = new SimplePatternSignatureChecker(0,0,
				hexStringSignature,
				signatureMode );
			_signatureName = signatureName;
			_fileExtensions = fileExtensions;
			_prohibitionMode = prohibitionMode;
            _byteoffset = 0;
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderSignature"/> class.
        /// </summary>
        /// <param name="hexStringSignature">The hex string signature.</param>
        /// <param name="signatureName">Name of the signature.</param>
        /// <param name="fileExtensions">The file extensions.</param>
        /// <param name="prohibitionMode">The prohibition mode.</param>
        /// <param name="signatureMode">The signature mode.</param>
        public HeaderSignature(
            int byteoffset,
            int firstnumberofbytestoread,
            string hexStringSignature,
            string signatureName,
            string[] fileExtensions,
            ProhibitionMode prohibitionMode,
            SignatureMode signatureMode)
        {
            _checker = new SimplePatternSignatureChecker(
                byteoffset,
                firstnumberofbytestoread,
                hexStringSignature,
                signatureMode);
            _signatureName = signatureName;
            _fileExtensions = fileExtensions;
            _prohibitionMode = prohibitionMode;
            _byteoffset = byteoffset;

        }

		/// <summary>
		/// Initializes a new instance of the <see cref="HeaderSignature"/> class.
		/// </summary>
		/// <param name="hexStringSignature">The hex string signature.</param>
		/// <param name="signatureName">Name of the signature.</param>
		/// <param name="fileExtensions">The file extensions.</param>
		/// <param name="prohibitionMode">The prohibition mode.</param>
		public HeaderSignature(
			string hexStringSignature,
			string signatureName,
			string[] fileExtensions,
			ProhibitionMode prohibitionMode )
			:
			this(
				hexStringSignature,
				signatureName,
				fileExtensions,
				prohibitionMode,
				SignatureMode.HexString )
		{
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderSignature"/> class.
        /// </summary>
        /// <param name="hexStringSignature">The hex string signature.</param>
        /// <param name="signatureName">Name of the signature.</param>
        /// <param name="fileExtensions">The file extensions.</param>
        /// <param name="prohibitionMode">The prohibition mode.</param>
        public HeaderSignature(
            int byteoffset,
            int firstnumberofbytestoread,
            string hexStringSignature,
            string signatureName,
            string[] fileExtensions,
            ProhibitionMode prohibitionMode)
            :
            this(
                byteoffset,
                firstnumberofbytestoread,
                hexStringSignature,
                signatureName,
                fileExtensions,
                prohibitionMode,
                SignatureMode.HexString)
        {
        }

		// ------------------------------------------------------------------
		#endregion

		#region Public matching methods.
		// ------------------------------------------------------------------

		/// <summary>
		/// Matches the file extension.
		/// </summary>
		/// <param name="extension">The extension.</param>
		/// <returns></returns>
		public bool MatchesFileExtension(
			string extension )
		{
			if ( string.IsNullOrEmpty( extension ) )
			{
				return false;
			}
			else
			{
				extension = extension.Trim( '.' );

				foreach ( string fileExtension in _fileExtensions )
				{
					if ( string.Compare(
						extension,
						fileExtension.Trim( '.' ), true ) == 0 )
					{
						return true;
					}
				}

				// No matching extension.
				return false;
			}
		}

		/// <summary>
		/// Matches the file.
		/// </summary>
		/// <param name="filePath">The file path.</param>
		/// <returns></returns>
		public bool MatchesFile(
            Alphaleonis.Win32.Filesystem.FileInfo filePath)
		{
			// By default, don't care about the extension.
			return MatchesFile( filePath, true );
		}

		/// <summary>
		/// Matches the file.
		/// </summary>
		/// <param name="filePath">The file path.</param>
		/// <param name="ignoreExtension">if set to <c>true</c> 
		/// [ignore extension].</param>
		/// <returns></returns>
		public bool MatchesFile(
            Alphaleonis.Win32.Filesystem.FileInfo filePath,
			bool ignoreExtension )
		{
			if ( filePath == null || !filePath.Exists || filePath.Length <= 0 )
			{
				return false;
			}
			else
			{
				if ( ignoreExtension ||
					MatchesFileExtension( filePath.Extension ) )
				{
                    using (FileStream fs = filePath.Open(System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.ReadWrite))
					{
						return MatchesStream( fs );
					}
				}
				else
				{
					return false;
				}
			}
		}

		/// <summary>
		/// Matches the stream.
		/// </summary>
		/// <param name="stream">The stream.</param>
		/// <returns></returns>
		public bool MatchesStream(
			Stream stream )
		{
			if ( stream == null )
			{
				return false;
			}
			else
			{
				int read = _checker.FirstNumberOfBytesToRead;

				byte[] buffer = new byte[read];
				int readCount = stream.Read( buffer, 0, read );

				using ( MemoryStream ms = new MemoryStream( buffer ) )
				{
					ms.Seek( 0, SeekOrigin.Begin );

					byte[] realBuffer = new byte[readCount];
					ms.Read( realBuffer, 0, readCount );

					return MatchesSignature( realBuffer);
				}
			}
		}

		/// <summary>
		/// Matches the signature.
		/// </summary>
		/// <param name="buffer">The buffer.</param>
		/// <returns></returns>
		public bool MatchesSignature(
			byte[] buffer )
		{
			if ( buffer == null ||
				buffer.Length < _checker.MinimumRequiredBufferLength )
			{
				return false;
			}
			else
			{
				return _checker.MatchesSignature( buffer );
			}
		}

		// ------------------------------------------------------------------
		#endregion

		#region Predefined signatures.
		// ------------------------------------------------------------------

		/// <summary>
		/// A list of all recognized signatures.
		/// </summary>
		public static readonly HeaderSignature[] StockSignatures =
			new HeaderSignature[]
			{
				new HeaderSignature(0,10,@"FFD8FFFE00", "JPG Graphic File", new string[] { @".JPEG", @".JPE", @".JPG" }, ProhibitionMode.Allowed ),
				new HeaderSignature(0,10,@"FFD8FFE000", "JPG Graphic File", new string[] { @".JPEG", @".JPE", @".JPG" }, ProhibitionMode.Allowed ),
                new HeaderSignature(0,10,@"FFD8FFE128", "JPG Graphic File", new string[] { @".JPEG", @".JPE", @".JPG" }, ProhibitionMode.Allowed ),
                new HeaderSignature(0,10,@"FFD8FFE1", "JPG Graphic File", new string[] { @".JPEG", @".JPE", @".JPG" }, ProhibitionMode.Allowed ),
                new HeaderSignature(0,10,@"FFD8FFE0", "JPG Graphic File", new string[] { @".JPEG", @".JPE", @".JPG" }, ProhibitionMode.Allowed ),
				new HeaderSignature(0,20,@"474946383961", "GIF 89A", new string[] { @".gif" }, ProhibitionMode.Allowed ),
				new HeaderSignature(0,20,@"474946383761", "GIF 87A", new string[] { @".gif" }, ProhibitionMode.Allowed ),
				new HeaderSignature(0,4,@"424D", "Windows Bitmap", new string[] { @".bmp" }, ProhibitionMode.Allowed ),
				new HeaderSignature(0,20,@"504B0304140006000800000021", "Microsoft Office Open XML Format", new string[] { @".doc",@".docx",@".pptx",@".xlsx", @".vsdx", @".dotx",@"dotm",@"docm",@".ppsx", @".potx"}, ProhibitionMode.Allowed ),
                new HeaderSignature(0,20,@"504B030414000600", "Microsoft Office Open XML Format", new string[] { @".doc",@".docx",@".pptx",@".xlsx", @".vsdx", @".dotx",@"dotm",@"docm",@".ppsx", @".potx"}, ProhibitionMode.Allowed ),
				new HeaderSignature(0,20,@"504B030414000200", "Microsoft Office Open XML Format", new string[] { @".doc",@".docx",@".pptx",@".xlsx", @".vsdx", @".dotx",@"dotm",@"docm",@".ppsx", @".potx"}, ProhibitionMode.Allowed ),
				new HeaderSignature(0,20,@"504B03040A00", "Microsoft Office Open XML Format", new string[] { @".doc",@".docx",@".pptx",@".xlsx", @".vsdx", @".dotx",@"dotm",@"docm",@".ppsx", @".potx"}, ProhibitionMode.Allowed ),
				
                
                
                new HeaderSignature(0,20,@"3A42617365", "", new string[] { @".cnt" }, ProhibitionMode.Allowed ),
				new HeaderSignature(0,20,@"D0CF11E0A1B11AE1", "MS Compound Document v1 or Lotus Approach APR file", new string[] { @".doc", @".xls", @".xlt", @".ppt", @".apr", @".dot", @".pps",@".wps",@".vsd",@".qbm",@".pub",@".adp",@".ade",@"docm" }, ProhibitionMode.Allowed ),
                
                new HeaderSignature(0,20,@"0100000058000000", "", new string[] { @".emf" }, ProhibitionMode.Allowed ),
				new HeaderSignature(0,20,@"03000000C466C456", "", new string[] { @".evt" }, ProhibitionMode.Allowed ),
				new HeaderSignature(0,10,@"3F5F0300", "Windows Help File", new string[] { @".gid", @".hlp", @".lhp" }, ProhibitionMode.Allowed ),
				new HeaderSignature(0,6,@"1F8B08", "GZ Compressed File", new string[] { @".gz" }, ProhibitionMode.Allowed ),
				new HeaderSignature(0,20,@"28546869732066696C65", "", new string[] { @".hqx" }, ProhibitionMode.Allowed ),
				new HeaderSignature(0,20,@"4C000000011402", "Windows Link File", new string[] { @".lnk" }, ProhibitionMode.Allowed ),
				new HeaderSignature(0,20,@"25504446", "Adobe PDF File", new string[] { @".pdf" }, ProhibitionMode.Allowed ),
				new HeaderSignature(0,20,@"5245474544495434", "", new string[] { @".reg" }, ProhibitionMode.Allowed ),
				new HeaderSignature(0,20,@"7B5C72746631", "Rich Text Format File", new string[] { @".rtf", @".doc" }, ProhibitionMode.Allowed ),
                new HeaderSignature(0,20,@"7B5C727466", "Rich Text Format File", new string[] { @".rtf",@".doc" }, ProhibitionMode.Allowed ),
				new HeaderSignature(0,8,@"MThd", "", new string[] { @".mid" }, ProhibitionMode.Allowed, SignatureMode.Text ),
				new HeaderSignature(0,10,@"0A050108", "", new string[] { @".pcx" }, ProhibitionMode.Allowed, SignatureMode.Text ),
				new HeaderSignature(0,10,@"25215053", "Adobe EPS File", new string[] { @".eps" }, ProhibitionMode.Allowed ),
				new HeaderSignature(0,4,@"2112", "AIN Archive File", new string[] { @".ain" }, ProhibitionMode.Allowed ),
				new HeaderSignature(0,4,@"1A02", "ARC/PKPAK Compressed 1", new string[] { @".arc" }, ProhibitionMode.Allowed ),
				new HeaderSignature(0,4,@"1A03", "ARC/PKPAK Compressed 2", new string[] { @".arc" }, ProhibitionMode.Allowed ),
				new HeaderSignature(0,4,@"1A04", "ARC/PKPAK Compressed 3", new string[] { @".arc" }, ProhibitionMode.Allowed ),
				new HeaderSignature(0,4,@"1A08", "ARC/PKPAK Compressed 4", new string[] { @".arc" }, ProhibitionMode.Allowed ),
				new HeaderSignature(0,4,@"1A09", "ARC/PKPAK Compressed 5", new string[] { @".arc" }, ProhibitionMode.Allowed ),
				new HeaderSignature(0,4,@"60EA", "ARJ Compressed", new string[] { @".arj" }, ProhibitionMode.Allowed ),
				new HeaderSignature(0,20,@"41564920", "Audio Video Interleave (AVI)", new string[] { @".avi" }, ProhibitionMode.Allowed ),
                new HeaderSignature(0,20,@"52494646", "Audio Video Interleave (AVI)", new string[] { @".avi" }, ProhibitionMode.Allowed ),
				new HeaderSignature(0,10,@"425A68", "Bzip Archive", new string[] { @".bz", @".bz2" }, ProhibitionMode.Allowed ),
				new HeaderSignature(0,20,@"49536328", "Cabinet File", new string[] { @".cab" }, ProhibitionMode.Allowed ),
				new HeaderSignature(0,4,@"4C01", "Compiled Object Module", new string[] { @".obj" }, ProhibitionMode.Allowed ),
				new HeaderSignature(0,20,@"303730373037", "CPIO Archive File", new string[] { @".tar", @".cpio" }, ProhibitionMode.Allowed ),
				new HeaderSignature(0,20,@"4352555348", "CRUSH Archive File", new string[] { @".cru", @".crush" }, ProhibitionMode.Allowed ),
				new HeaderSignature(0,20,@"3ADE68B1", "DCX Graphic File", new string[] { @".dcx" }, ProhibitionMode.Allowed ),
				new HeaderSignature(0,4,@"1F8B", "Gzip Archive File", new string[] { @".gz", @".tar", @".tgz" }, ProhibitionMode.Allowed ),
				new HeaderSignature(0,10,@"91334846", "HAP Archive File", new string[] { @".hap" }, ProhibitionMode.Allowed ),
				new HeaderSignature(0,20,@"3C68746D6C3E", "HyperText Markup Language 1", new string[] { @".htm", @".html" }, ProhibitionMode.Allowed ),
				new HeaderSignature(0,20,@"3C48544D4C3E", "HyperText Markup Language 2", new string[] { @".htm", @".html" }, ProhibitionMode.Allowed ),
				
                

                new HeaderSignature(0,20,@"3C646976", "HyperText Markup Language No Header Starts with div tag", new string[] { @".htm", @".html" }, ProhibitionMode.Allowed ),
				new HeaderSignature(0,3,@"100", "ICON File", new string[] { @".ico" }, ProhibitionMode.Allowed ),
				new HeaderSignature(0,20,@"5F27A889", "JAR Archive File", new string[] { @".jar" }, ProhibitionMode.Allowed ),
				new HeaderSignature(0,20,@"2D6C68352D", "LHA Compressed", new string[] { @".lha" }, ProhibitionMode.Allowed ),
				new HeaderSignature(0,20,@"20006040600", "Lotus 123 v1 Worksheet", new string[] { @".wk1", @".wks" }, ProhibitionMode.Allowed ),
				new HeaderSignature(0,30,@"00001A0007800100", "Lotus 123 v3 FMT file", new string[] { @".fm3" }, ProhibitionMode.Allowed ),
				new HeaderSignature(0,30,@"00001A0000100400", "Lotus 123 v3 Worksheet", new string[] { @".wk3" }, ProhibitionMode.Allowed ),
				new HeaderSignature(0,20,@"20006800200", "Lotus 123 v4 FMT file", new string[] { @".fmt" }, ProhibitionMode.Allowed ),
				new HeaderSignature(0,30,@"00001A0002100400", "Lotus 123 v5", new string[] { @".wk4" }, ProhibitionMode.Allowed ),
				new HeaderSignature(0,20,@"5B7665725D", "Lotus Ami Pro", new string[] { @".ami" }, ProhibitionMode.Allowed ),
				new HeaderSignature(0,30,@"300000041505052", "Lotus Approach ADX file", new string[] { @".adx" }, ProhibitionMode.Allowed ),
				new HeaderSignature(0,20,@"1A0000030000", "Lotus Notes Database/Template", new string[] { @".nsf", @".ntf" }, ProhibitionMode.Allowed ),
				new HeaderSignature(0,30,@"4D47582069747064", "Micrografix Designer 4", new string[] { @".ds4" }, ProhibitionMode.Allowed ),
				new HeaderSignature(0,10,@"4D534346", "Microsoft CAB File Format", new string[] { @".cab" }, ProhibitionMode.Allowed ),
				new HeaderSignature(0,10,@"4D546864", "Midi Audio File", new string[] { @".mid" }, ProhibitionMode.Allowed ),
				new HeaderSignature(0,10,@"000001B3", "MPEG Movie", new string[] { @".mpg", @".mpeg" }, ProhibitionMode.Allowed ),
				new HeaderSignature(0,30,@"0902060000001000B9045C00", "MS Excel v2", new string[] { @".xls" }, ProhibitionMode.Allowed ),
				new HeaderSignature(0,30,@"0904060000001000F6055C00", "MS Excel v4", new string[] { @".xls" }, ProhibitionMode.Allowed ),
				new HeaderSignature(0,10,@"7FFE340A", "MS Word", new string[] { @".doc", @".xls", @".xlt", @".ppt", @".apr", @".dot", @".pps",@".wps",@".vsd",@".qbm",@".pub",@".adp",@".ade",@"docm" }, ProhibitionMode.Allowed ),
				new HeaderSignature(0,20,@"1234567890FF", "MS Word 6.0", new string[] { @".doc", @".xls", @".xlt", @".ppt", @".apr", @".dot", @".pps",@".wps",@".vsd",@".qbm",@".pub",@".adp",@".ade",@"docm" }, ProhibitionMode.Allowed ),
				new HeaderSignature(0,30,@"31BE000000AB0000", "MS Word for DOS 6.0", new string[] { @".doc", @".xls", @".xlt", @".ppt", @".apr", @".dot", @".pps",@".wps",@".vsd",@".qbm",@".pub",@".adp",@".ade",@"docm" }, ProhibitionMode.Allowed ),
				new HeaderSignature(0,20,@"1A00000300001100", "Notes Database", new string[] { @".nsf" }, ProhibitionMode.Allowed ),
				new HeaderSignature(0,10,@"7E424B00", "PaintShop Pro Image File", new string[] { @".psp" }, ProhibitionMode.Allowed ),
				new HeaderSignature(0,10,@"504B0304", "PKZIP Compressed", new string[] { @".zip" }, ProhibitionMode.Allowed ),
				new HeaderSignature(0,20,@"89504E470D0A", "PNG Image File", new string[] { @".png" }, ProhibitionMode.Allowed ),
				new HeaderSignature(0,10,@"6D646174", "QuickTime Movie", new string[] { @".mov" }, ProhibitionMode.Allowed ),
                new HeaderSignature(0,10,@"6D6F6F76", "QuickTime Movie", new string[] { @".mov" }, ProhibitionMode.Allowed ),
                
                new HeaderSignature(0,20,@"71742020", "QuickTime Movie", new string[] { @".mov" }, ProhibitionMode.Allowed ),
                new HeaderSignature(0,20,@"0000001466747970", "QuickTime Movie", new string[] { @".mov" }, ProhibitionMode.Allowed ),

				new HeaderSignature(0,10,@"6D646174", "Quicktime Movie File", new string[] { @".qt" }, ProhibitionMode.Allowed ),
				new HeaderSignature(0,20,@"526172211A07", "RAR Archive File", new string[] { @".rar" }, ProhibitionMode.Allowed ),
				new HeaderSignature(0,10,@"2E7261FD", "Real Audio File", new string[] { @".ra", @".ram" }, ProhibitionMode.Allowed ),
				new HeaderSignature(0,10,@"EDABEEDB", "RPM Archive File", new string[] { @".rpm" }, ProhibitionMode.Allowed ),
				new HeaderSignature(0,10,@"2E736E64", "SoundMachine Audio File", new string[] { @".au" }, ProhibitionMode.Allowed ),
				new HeaderSignature(0,10,@"53495421", "Stuffit v1 Archive File", new string[] { @".sit" }, ProhibitionMode.Allowed ),
				new HeaderSignature(0,20,@"53747566664974", "Stuffit v5 Archive File", new string[] { @".sit" }, ProhibitionMode.Allowed ),
				new HeaderSignature(0,10,@"1F9D", "TAR Compressed Archive File", new string[] { @".z" }, ProhibitionMode.Allowed ),
				new HeaderSignature(0,10,@"49492A", "TIFF (Intel)", new string[] { @".tif", @".tiff" }, ProhibitionMode.Allowed ),
				new HeaderSignature(0,10,@"4D4D2A", "TIFF (Motorola)", new string[] { @".tif", @".tiff" }, ProhibitionMode.Allowed ),
                new HeaderSignature(0,10,@"4D4D002A", "TIFF (Adobe)", new string[] { @".tif", @".tiff" }, ProhibitionMode.Allowed ),
				new HeaderSignature(0,10,@"554641", "UFA Archive File", new string[] { @".ufa" }, ProhibitionMode.Allowed ),
				new HeaderSignature(0,20,@"57415645666D74", "Wave File", new string[] { @".wav",@".cda" }, ProhibitionMode.Allowed ),
				new HeaderSignature(0,10,@"D7CDC69A", "Windows Meta File", new string[] { @".wmf" }, ProhibitionMode.Allowed ),
				new HeaderSignature(0,10,@"4C000000", "Windows Shortcut (Link File)", new string[] { @".lnk" }, ProhibitionMode.Allowed ),
				new HeaderSignature(0,30,@"504B3030504B0304", "WINZIP Compressed", new string[] { @".zip" }, ProhibitionMode.Allowed ),
				new HeaderSignature(0,10,@"FF575047", "WordPerfect Graphics", new string[] { @".wpg" }, ProhibitionMode.Allowed ),
				new HeaderSignature(0,10,@"FF575043", "WordPerfect v5 or v6", new string[] { @".wp" }, ProhibitionMode.Allowed ),
				new HeaderSignature(0,20,@"3C3F786D6C", "XML Document", new string[] { @".xml",@".config" }, ProhibitionMode.Allowed ),
				new HeaderSignature(0,50,@"FFFE3C0052004F004F0054005300540055004200", "XML Document (ROOTSTUB)", new string[] { @".xml" }, ProhibitionMode.Allowed ),
				new HeaderSignature(0,30,@"3C21454E54495459", "XML DTD", new string[] { @".dtd" }, ProhibitionMode.Allowed ),
				new HeaderSignature(0,10,@"5A4F4F20", "ZOO Archive File", new string[] { @".zoo" }, ProhibitionMode.Allowed ),
                new HeaderSignature(0,30,@"01766F72626973", "Ogg Vorbis", new string[] { @".ogg" }, ProhibitionMode.Allowed ),
                new HeaderSignature(0,6,@"4D5A", "Executable File",new string[] { @".exe", @".com", @".386", @".ax", @".acm", @".sys", @".dll", @".drv", @".flt", @".fon", @".ocx", @".scr", @".lrc", @".vxd", @".cpl", @".x32" }, ProhibitionMode.Prohibited ),
                new HeaderSignature( new Mp3SignatureChecker(), "MPEG 3", new string[] { @".mp3" }, ProhibitionMode.Allowed ),
                
                // My own.
                new HeaderSignature( new Mp4SignatureChecker(), "MPEG 4", new string[] { @".mp4", @".m4v"}, ProhibitionMode.Allowed ),
                
                new HeaderSignature( new MSAccessSignatureChecker(), "Microsoft Access", new string[] { @".mdb", @".accdb", @".accde", @".accdr",@".accdt",@".adp",@".ade"}, ProhibitionMode.Allowed ),
				new HeaderSignature(0,20,@"D0CF11E0A1B11AE1", "Microsoft Installer", new string[] { @".msi" }, ProhibitionMode.Allowed ),
                new HeaderSignature(0,20,@"D0CF11E0A1B11AE1", "Outlook Message File", new string[] { @".msg" }, ProhibitionMode.Allowed ),
				new HeaderSignature(0,10,@"DBA52D00", "MS Word 2.0", new string[] { @".doc", @".xls", @".xlt", @".ppt", @".apr", @".dot", @".pps",@".wps",@".vsd",@".qbm",@".pub",@".adp",@".ade",@"docm" }, ProhibitionMode.Allowed ),
                
                new HeaderSignature(0,10,@"464C56", "Flash Video", new string[] { @".flv" }, ProhibitionMode.Allowed ),
                new HeaderSignature(0,10,@"465753", "Flash Shockwave", new string[] { @".swf" }, ProhibitionMode.Allowed ),
                new HeaderSignature(0,20,@"3026B2758E66CF", "Windows Video File", new string[] { @".wmv" }, ProhibitionMode.Allowed ),
                new HeaderSignature(0,20,@"3026B2758E66CF", "Windows Audio File", new string[] { @".wma" }, ProhibitionMode.Allowed ),
                new HeaderSignature(0,20,@"2142444E42", "Outlook Post Office File", new string[] { @".pst" }, ProhibitionMode.Allowed ),
                new HeaderSignature(0,4,@"FFF1", "MPEG-4 Advanced Audio Coding (AAC) Low Complexity", new string[] { @".aac" }, ProhibitionMode.Allowed ),                      
                new HeaderSignature(0,4,@"FFF9", "MPEG-2 Advanced Audio Coding (AAC) Low Complexity", new string[] { @".aac" }, ProhibitionMode.Allowed ),
                new HeaderSignature(0,30,@"D0CF11E0A1B11AE1", "Windows Thumbs.db", new string[] { @".db" }, ProhibitionMode.Allowed ),
                new HeaderSignature(0,20,@"377ABCAF271C", "7zip", new string[] { @".7z" }, ProhibitionMode.Allowed ),
                new HeaderSignature(0,10,@"4D344120", "Apple Lossless Audio Codec file", new string[] { @".m4a" }, ProhibitionMode.Allowed ),
                new HeaderSignature(0,20,@"0000002066747970", "Apple Lossless Audio Codec file", new string[] { @".m4a" }, ProhibitionMode.Allowed ),
                new HeaderSignature(0,10,@"000001BA", "MPEG Movie", new string[] { @".mpg", @".mpeg" }, ProhibitionMode.Allowed ),
                new HeaderSignature(0,10,@"000001B", "MPEG Movie", new string[] { @".mpg", @".mpeg" }, ProhibitionMode.Allowed ),
                new HeaderSignature(0,10,@"52494646", "Wave File", new string[] { @".wav",@".cda" }, ProhibitionMode.Allowed ),
                new HeaderSignature(0,30,@"53514C6974652066", "SQL Lite Database", new string[] { @".db", @".sqlite" }, ProhibitionMode.Allowed ),
                new HeaderSignature(0,30,@"6F726D6174203300", "SQL Lite Database", new string[] { @".db", @".sqlite" }, ProhibitionMode.Allowed ),
                new HeaderSignature(0,30,@"636F6E6563746978", "Virtual PC Virtual HD image", new string[] { @".vhd" }, ProhibitionMode.Allowed ),
                new HeaderSignature(0,10,@"4B444D56", "VMWare Disk file", new string[] { @".vmdk" }, ProhibitionMode.Allowed ),
                new HeaderSignature(0,20,@"504B0304140000000000", "epub book", new string[] { @".epub" }, ProhibitionMode.Allowed ),
                new HeaderSignature(0,20,@"504B0304140008000800", "Java JAR File", new string[] { @".jar",@".xps",@".oxps" }, ProhibitionMode.Allowed ),
                new HeaderSignature(0,20,@"4A4152435300", "JARCS compressed archive", new string[] { @".jar" }, ProhibitionMode.Allowed ),
                new HeaderSignature(0,10,@"504B0506", "PKZip Compressed", new string[] { @".zip", @".jar" }, ProhibitionMode.Allowed ),
                new HeaderSignature(0,10,@"504B0708", "PKZip Compressed", new string[] { @".zip", @".jar" }, ProhibitionMode.Allowed ),
                new HeaderSignature(0,10,@"504B0304", "Zip Compressed", new string[] { @".zip", @".jar", @".odt", @".odp", @".ods", @"kwd", @".sxc", @".sxd", @".sxi", @".sxw", @".xps",@".oxps" }, ProhibitionMode.Allowed ),
				new HeaderSignature(0,10,@"EFBBBF3C", "XML Document", new string[] { @".xml",@".config" }, ProhibitionMode.Allowed ),
                new HeaderSignature(0,20,@"46726F6D3A", "Web Archive", new string[] { @".mht" }, ProhibitionMode.Allowed ),
                new HeaderSignature(0,20,@"D0CF11E0A1B11AE1", "msp Installer", new string[] { @".msp" }, ProhibitionMode.Allowed ),
                new HeaderSignature(0,10,@"4D534346", "Microsoft Update Installer Package", new string[] { @".msu" }, ProhibitionMode.Allowed ),
                new HeaderSignature(0,30,@"454E49474D412042494E415259", "Finale Music File", new string[] { @".mus" }, ProhibitionMode.Allowed ),
                new HeaderSignature(0,30,@"504B0304140000", "Make Music File", new string[] { @".musx" }, ProhibitionMode.Allowed ),
                
               
                new HeaderSignature(0,30,@"5B7B3030303231344130", "URL Shortcut File", new string[] { @".url",@".lnk" }, ProhibitionMode.Allowed ),
                new HeaderSignature( new QBWSignatureChecker(), "Quickbooks file", new string[] { @".qbw", @".tlg"}, ProhibitionMode.Allowed ),
                new HeaderSignature(0,40,@"2F2F5468697320697320517569636B", "Quickbooks configuration file", new string[] { @".nd"}, ProhibitionMode.Allowed ),
                new HeaderSignature(0,40,@"D0CF11E0A1B11AE1", "Quickbooks backup file", new string[] { @".qbb"}, ProhibitionMode.Allowed ),
                new HeaderSignature(0,20,@"5B44454641554C545D", "URL Shortcut File", new string[] { @".url",@".lnk"}, ProhibitionMode.Allowed ),
                

                new HeaderSignature(0,20,@"EEDE12000100F40100000000", "APR file", new string[] { @".apr" }, ProhibitionMode.Allowed ),
                new HeaderSignature(0,10,@"B168DE3A0410", "dcx file", new string[] { @".dcx" }, ProhibitionMode.Allowed ),
                new HeaderSignature(0,6,@"FF575043", "MS Word", new string[] { @".doc", @".xls", @".xlt", @".ppt", @".apr", @".dot", @".pps",@".wps",@".vsd",@".qbm",@".pub",@".adp",@".ade",@"docm" }, ProhibitionMode.Allowed ),
                new HeaderSignature(0,20,@"0A020101000000009F", "pcx file", new string[] { @".pcx" }, ProhibitionMode.Allowed ),
                new HeaderSignature(0,20,@"3C003F0078006D006C", "xml file", new string[] { @".xml" }, ProhibitionMode.Allowed ),
                new HeaderSignature(0,20,@"0000000041000000EC", "db file", new string[] { @".db" }, ProhibitionMode.Allowed ),
                new HeaderSignature(0,30,@"5B496E7465726E657453686F7274", "URL Shortcut file", new string[] { @".url",@".lnk" }, ProhibitionMode.Allowed ),
                

                new HeaderSignature(0,20,@"504B0304140000000800", "Smart Notebook", new string[] { @".notebook" }, ProhibitionMode.Allowed ),
                new HeaderSignature(0,20,@"5075726368617365204F72646572", "pof file", new string[] { @".pof" }, ProhibitionMode.Allowed ),
                new HeaderSignature( new PogSignatureChecker(), "POG File", new string[] { @".pog"}, ProhibitionMode.Allowed ),
                new HeaderSignature(0,8,"49545346", "chm help file", new string[] { @".chm"}, ProhibitionMode.Allowed ),
                
                new HeaderSignature(0,30,"5374616E6461726420", "MS Access", new string[] { @".mdb", @".accdb", @".accde", @".accdr",@".accdt",@".adp",@".ade"}, ProhibitionMode.Allowed ),
                new HeaderSignature(0,10,@"504B0304", "Open Document format", new string[] { @".odt",@".ods",@".odp"}, ProhibitionMode.Allowed ),
				
                
                new HeaderSignature(0,3,@"100", "Icon File", new string[] { @".ico" }, ProhibitionMode.Allowed ),
                new HeaderSignature(0,10,@"00000100", "Icon File", new string[] { @".ico" }, ProhibitionMode.Allowed ),
                new HeaderSignature(0,16,@"667479704D3441", "Apple Lossless Audio Codec file", new string[] { @".m4a" }, ProhibitionMode.Allowed ),
                new HeaderSignature(0,50,@"3C21444F4354595045", "HyperText Markup Language 3", new string[] { @".htm", @".html" }, ProhibitionMode.Allowed ),
                new HeaderSignature(0,0,@"3C68746D6C", "HyperText Markup Language 3", new string[] { @".htm", @".html" }, ProhibitionMode.Allowed ),
                new HeaderSignature(0,16,@"C5D0D3C6", "EPS Image", new string[] { @".eps" }, ProhibitionMode.Allowed ),
                new HeaderSignature(0,2,@"3C", "xml file", new string[] { @".xml" }, ProhibitionMode.Allowed ),
                new HeaderSignature(0,6,@"FE37001C00", "MS Word", new string[] { @".doc", @".xls", @".xlt", @".ppt", @".apr", @".dot", @".pps",@".wps",@".vsd",@".qbm",@".pub",@".adp",@".ade",@"docm" }, ProhibitionMode.Allowed ),
                new HeaderSignature(0,20,@"B168DE3A0410", "DCX Survey file", new string[] { @".dcx" }, ProhibitionMode.Allowed ),
                new HeaderSignature(0,20,"690000000000000000", "bm2 board maker", new string[] { @".bm2"}, ProhibitionMode.Allowed ),
                new HeaderSignature(0,20,"770000000000000000", "bm2 board maker", new string[] { @".bm2"}, ProhibitionMode.Allowed ),
                new HeaderSignature(0,30,"0101D430", "bm2 board maker", new string[] { @".bm2"}, ProhibitionMode.Allowed ),
                new HeaderSignature(0,30,"0A020101000000009F0697", "pcx survey", new string[] { @".pcx"}, ProhibitionMode.Allowed ),
                new HeaderSignature(0,20,"E4525C7B8CD8A74DAEB1", "Microsoft One Note", new string[] { @".one"}, ProhibitionMode.Allowed ),
                new HeaderSignature(0,20,"A12FFF43D9EF764C9EE210", "Microsoft One Note", new string[] { @".onetoc2"}, ProhibitionMode.Allowed ),
                
                //new HeaderSignature(3,"000019000E", "POG File", new string[] { @".pog"}, ProhibitionMode.Allowed ),
                /*new HeaderSignature( @"", "Windows Media", new string[] { @".wmv", @".asf" }, ProhibitionMode.Prohibited ),*/
				
			};

		// ------------------------------------------------------------------
		#endregion

		#region Public methods.
		// ------------------------------------------------------------------

        public static HeaderSignature[] CustomSignatures(DataTable dtSignatures)
        {
            List<HeaderSignature> hds = new List<HeaderSignature>();
            string strHexPattern;
            string strSignatureName;
            int intByteOffset = 0;
            int intFirstNumberOfBytesToRead = 0;
            string strFileExtensions;
            bool blEnabled = false;
            bool blProhibited = false;
            ProhibitionMode pmode = ProhibitionMode.Allowed;
            char[] delimiters = new char[] { ';' };

            foreach(DataRow row in dtSignatures.Rows)
            {
                blEnabled = FixNullbool(row["Enabled"]);
                int.TryParse(FixNullstring(row["ByteOffset"]),out intByteOffset);
                int.TryParse(FixNullstring(row["FirstNumberOfBytesToRead"]), out intFirstNumberOfBytesToRead);
                strHexPattern = FixNullstring(row["HexPattern"]);
                strSignatureName=FixNullstring(row["SignatureName"]);
                strFileExtensions = FixNullstring(row["FileExtensions"]);
                
                string[] strArr_FileExtensions = strFileExtensions.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                blProhibited = FixNullbool(row["Prohibited"]);
                if (blProhibited)
                {
                    pmode = ProhibitionMode.Prohibited;
                }
                else
                {
                    pmode = ProhibitionMode.Allowed;
                }
                if (blEnabled)
                {
                    hds.Add(new HeaderSignature(intByteOffset,intFirstNumberOfBytesToRead,strHexPattern, strSignatureName, strArr_FileExtensions, pmode));
                }
            }
            return hds.ToArray();
        }


        

        /// <summary>
        /// Fixes null strings and returns the string or ""
        /// </summary>
        /// <param name="objData"></param>
        /// <returns></returns>
        public static string FixNullstring(object objData)
        {
            string strValue = "";
            if (System.DBNull.Value == objData || objData == null)
            {
                return strValue;
            }
            else
            {
                return objData.ToString();
            }
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

        

        public static bool ZipRelatedExtension(string strExtension)
        {
            bool blZipExtension = false;
            //@".docx",@".pptx",@".xlsx", @".vsdx", @".dotx",@"dotm",@"docm",@".ppsx", @".potx"
            //@".zip", @".jar", @".odt", @".odp", @".odt", @"kwd", @".sxc", @".sxd", @".sxi", @".sxw", @".xps"
            //504B
            
            switch(strExtension.ToLower())
            {
                case @".zip":
                    blZipExtension = true;
                    break;
                case @".docx":
                    blZipExtension = true;
                    break;
                case @".xlsx":
                    blZipExtension = true;
                    break;
                case @".pptx":
                    blZipExtension = true;
                    break;
                case @".ppsx":
                    blZipExtension = true;
                    break;
                case @".vsdx":
                    blZipExtension = true;
                    break;
                case @".dotx":
                    blZipExtension = true;
                    break;
                case @".dotm":
                    blZipExtension = true;
                    break;
                case @".docm":
                    blZipExtension = true;
                    break;
                case @".potx":
                    blZipExtension = true;
                    break;
                case @".odt":
                    blZipExtension = true;
                    break;
                case @".ods":
                    blZipExtension = true;
                    break;
                case @".odp":
                    blZipExtension = true;
                    break;
                case @".notebook":
                    blZipExtension = true;
                    break;
                case @".musx":
                    blZipExtension = true;
                    break;
                case @".epub":
                    blZipExtension = true;
                    break;
                case @".jar":
                    blZipExtension = true;
                    break;
                case @".xps":
                    blZipExtension = true;
                    break;
                case @".oxps":
                    blZipExtension = true;
                    break;
                default:
                    break;
            }
            return blZipExtension;

        }

        public static bool IsZipRelatedFile(Alphaleonis.Win32.Filesystem.FileInfo file1)
        {
            bool blZipExtension = false;

            byte[] Zipbytes = {0x50,0x4B};
            //504B


            using (FileStream fs = file1.Open(System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.ReadWrite))
            {
                if (fs == null)
                {
                    return false;
                }
                else
                {
                    int read = 2;

                    byte[] buffer = new byte[read];
                    int readCount = fs.Read(buffer, 0, read);

                    using (MemoryStream ms = new MemoryStream(buffer))
                    {
                        ms.Seek(0, SeekOrigin.Begin);

                        byte[] realBuffer = new byte[readCount];
                        ms.Read(realBuffer, 0, readCount);

                        blZipExtension = realBuffer == Zipbytes;
                    }
                }
                try
                {
                    fs.Close();
                }
                catch (Exception)
                {
                    
                    
                }
                
            }
            
            
            return blZipExtension;

        }

        public static bool ExtensionSupported(string strExtension, HeaderSignature[] sigs)
        {
            bool blsupported = false;

            

            foreach (HeaderSignature hs in sigs)
            {
                try
                {
                    
                    string[] strArr_extensionlist = hs.FileExtensions;

                    if (!(strArr_extensionlist == null || strArr_extensionlist.Length == 0))
                    {
                        //loop through excluded folders
                        foreach (string strExtension1 in strArr_extensionlist)
                        {
                            if (strExtension1.ToLower() == strExtension.ToLower())
                            {
                                blsupported = true;
                                return blsupported;
                            }
                        }
                    }
                }
                catch (Exception)
                {


                }


            }

            return blsupported;
        }


		/// <summary>
		/// Gets the checker.
		/// </summary>
		/// <value>The checker.</value>
		public ISignatureChecker Checker
		{
			get
			{
				return _checker;
			}
		}

		/// <summary>
		/// Gets the name of the signature.
		/// </summary>
		/// <value>The name of the signature.</value>
		public string SignatureName
		{
			get
			{
				return _signatureName;
			}
		}

        public int ByteOffset
        {
            get
            {
                return _byteoffset;
            }
        }

        public int FirstNumberOfBytesToRead
        {
            get
            {
               return _checker.FirstNumberOfBytesToRead;
            }
        }

		/// <summary>
		/// Gets the file extensions.
		/// </summary>
		/// <value>The file extensions.</value>
		public string[] FileExtensions
		{
			get
			{
				return _fileExtensions;
			}
		}

		/// <summary>
		/// Gets the prohibition mode.
		/// </summary>
		/// <value>The prohibition mode.</value>
		public ProhibitionMode ProhibitionMode
		{
			get
			{
				return _prohibitionMode;
			}
		}

		// ------------------------------------------------------------------
		#endregion

		#region Private variables.
		// ------------------------------------------------------------------

		private readonly ISignatureChecker _checker;
		private readonly string _signatureName;
		private readonly string[] _fileExtensions;
		private readonly ProhibitionMode _prohibitionMode;
        private readonly int _byteoffset;

		// ------------------------------------------------------------------
		#endregion
	}

	/////////////////////////////////////////////////////////////////////////
}