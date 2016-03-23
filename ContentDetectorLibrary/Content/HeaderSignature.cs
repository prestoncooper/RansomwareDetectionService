namespace RansomwareDetection.ContentDetectorLib.Content
{
	#region Using directives.
	// ----------------------------------------------------------------------

	using System.IO;

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
			_checker = new SimplePatternSignatureChecker(
				hexStringSignature,
				signatureMode );
			_signatureName = signatureName;
			_fileExtensions = fileExtensions;
			_prohibitionMode = prohibitionMode;
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
            Delimon.Win32.IO.FileInfo filePath)
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
            Delimon.Win32.IO.FileInfo filePath,
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
                    using (FileStream fs = filePath.Open(Delimon.Win32.IO.FileMode.Open,Delimon.Win32.IO.FileAccess.Read,Delimon.Win32.IO.FileShare.Read))
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

					return MatchesSignature( realBuffer );
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
				new HeaderSignature( @"FFD8FFFE00", "JPG Graphic File", new string[] { @".JPEG", @".JPE", @".JPG" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"FFD8FFE000", "JPG Graphic File", new string[] { @".JPEG", @".JPE", @".JPG" }, ProhibitionMode.Allowed ),
                new HeaderSignature( @"FFD8FFE128", "JPG Graphic File", new string[] { @".JPEG", @".JPE", @".JPG" }, ProhibitionMode.Allowed ),
                new HeaderSignature( @"FFD8FFE1", "JPG Graphic File", new string[] { @".JPEG", @".JPE", @".JPG" }, ProhibitionMode.Allowed ),
                new HeaderSignature( @"FFD8FFE0", "JPG Graphic File", new string[] { @".JPEG", @".JPE", @".JPG" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"474946383961", "GIF 89A", new string[] { @".gif" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"474946383761", "GIF 87A", new string[] { @".gif" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"424D", "Windows Bitmap", new string[] { @".bmp" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"504B0304140006000800000021", "Microsoft Office Open XML Format", new string[] { @".docx",@".pptx",@".xlsx", @".vsdx" }, ProhibitionMode.Allowed ),
                new HeaderSignature( @"504B030414000600", "Microsoft Office Open XML Format", new string[] { @".docx",@".pptx",@".xlsx", @".vsdx" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"504B030414000200", "Microsoft Office Open XML Format", new string[] { @".docx",@".pptx",@".xlsx", @".vsdx" }, ProhibitionMode.Allowed ),
				
                
                new HeaderSignature( @"3A42617365", "", new string[] { @".cnt" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"D0CF11E0A1B11AE1", "MS Compound Document v1 or Lotus Approach APR file", new string[] { @".doc", @".xls", @".xlt", @".ppt", @".apr", @".dot", @".pps",@".wps",@".vsd",@".qbm",@".pub",@".adp",@".ade" }, ProhibitionMode.Allowed ),
                
                new HeaderSignature( @"0100000058000000", "", new string[] { @".emf" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"03000000C466C456", "", new string[] { @".evt" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"3F5F0300", "Windows Help File", new string[] { @".gid", @".hlp", @".lhp" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"1F8B08", "GZ Compressed File", new string[] { @".gz" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"28546869732066696C65", "", new string[] { @".hqx" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"0000010000", "Icon File", new string[] { @".ico" }, ProhibitionMode.Allowed ),
                new HeaderSignature( @"000001000100", "Icon File", new string[] { @".ico" }, ProhibitionMode.Allowed ),

				new HeaderSignature( @"4C000000011402", "Windows Link File", new string[] { @".lnk" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"25504446", "Adobe PDF File", new string[] { @".pdf" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"5245474544495434", "", new string[] { @".reg" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"7B5C72746631", "Rich Text Format File", new string[] { @".rtf", @".doc" }, ProhibitionMode.Allowed ),
                new HeaderSignature( @"7B5C727466", "Rich Text Format File", new string[] { @".rtf",@".doc" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"lh", "Lz compression file", new string[] { @".lzh" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"MThd", "", new string[] { @".mid" }, ProhibitionMode.Allowed, SignatureMode.Text ),
				new HeaderSignature( @"0A050108", "", new string[] { @".pcx" }, ProhibitionMode.Allowed, SignatureMode.Text ),
				new HeaderSignature( @"25215053", "Adobe EPS File", new string[] { @".eps" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"2112", "AIN Archive File", new string[] { @".ain" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"1A02", "ARC/PKPAK Compressed 1", new string[] { @".arc" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"1A03", "ARC/PKPAK Compressed 2", new string[] { @".arc" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"1A04", "ARC/PKPAK Compressed 3", new string[] { @".arc" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"1A08", "ARC/PKPAK Compressed 4", new string[] { @".arc" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"1A09", "ARC/PKPAK Compressed 5", new string[] { @".arc" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"60EA", "ARJ Compressed", new string[] { @".arj" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"41564920", "Audio Video Interleave (AVI)", new string[] { @".avi" }, ProhibitionMode.Allowed ),
                new HeaderSignature( @"52494646", "Audio Video Interleave (AVI)", new string[] { @".avi" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"425A68", "Bzip Archive", new string[] { @".bz", @".bz2" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"49536328", "Cabinet File", new string[] { @".cab" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"4C01", "Compiled Object Module", new string[] { @".obj" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"303730373037", "CPIO Archive File", new string[] { @".tar", @".cpio" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"4352555348", "CRUSH Archive File", new string[] { @".cru", @".crush" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"3ADE68B1", "DCX Graphic File", new string[] { @".dcx" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"1F8B", "Gzip Archive File", new string[] { @".gz", @".tar", @".tgz" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"91334846", "HAP Archive File", new string[] { @".hap" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"3C68746D6C3E", "HyperText Markup Language 1", new string[] { @".htm", @".html" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"3C48544D4C3E", "HyperText Markup Language 2", new string[] { @".htm", @".html" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"3C21444F4354", "HyperText Markup Language 3", new string[] { @".htm", @".html" }, ProhibitionMode.Allowed ),
                new HeaderSignature( @"3C646976", "HyperText Markup Language No Header Starts with div tag", new string[] { @".htm", @".html" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"100", "ICON File", new string[] { @".ico" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"5F27A889", "JAR Archive File", new string[] { @".jar" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"2D6C68352D", "LHA Compressed", new string[] { @".lha" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"20006040600", "Lotus 123 v1 Worksheet", new string[] { @".wk1", @".wks" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"00001A0007800100", "Lotus 123 v3 FMT file", new string[] { @".fm3" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"00001A0000100400", "Lotus 123 v3 Worksheet", new string[] { @".wk3" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"20006800200", "Lotus 123 v4 FMT file", new string[] { @".fmt" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"00001A0002100400", "Lotus 123 v5", new string[] { @".wk4" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"5B7665725D", "Lotus Ami Pro", new string[] { @".ami" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"300000041505052", "Lotus Approach ADX file", new string[] { @".adx" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"1A0000030000", "Lotus Notes Database/Template", new string[] { @".nsf", @".ntf" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"4D47582069747064", "Micrografix Designer 4", new string[] { @".ds4" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"4D534346", "Microsoft CAB File Format", new string[] { @".cab" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"4D546864", "Midi Audio File", new string[] { @".mid" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"000001B3", "MPEG Movie", new string[] { @".mpg", @".mpeg" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"0902060000001000B9045C00", "MS Excel v2", new string[] { @".xls" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"0904060000001000F6055C00", "MS Excel v4", new string[] { @".xls" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"7FFE340A", "MS Word", new string[] { @".doc" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"1234567890FF", "MS Word 6.0", new string[] { @".doc" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"31BE000000AB0000", "MS Word for DOS 6.0", new string[] { @".doc" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"1A00000300001100", "Notes Database", new string[] { @".nsf" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"7E424B00", "PaintShop Pro Image File", new string[] { @".psp" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"504B0304", "PKZIP Compressed", new string[] { @".zip" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"89504E470D0A", "PNG Image File", new string[] { @".png" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"6D646174", "QuickTime Movie", new string[] { @".mov" }, ProhibitionMode.Allowed ),
                new HeaderSignature( @"6D6F6F76", "QuickTime Movie", new string[] { @".mov" }, ProhibitionMode.Allowed ),
                
                new HeaderSignature( @"71742020", "QuickTime Movie", new string[] { @".mov" }, ProhibitionMode.Allowed ),
                new HeaderSignature( @"0000001466747970", "QuickTime Movie", new string[] { @".mov" }, ProhibitionMode.Allowed ),

				new HeaderSignature( @"6D646174", "Quicktime Movie File", new string[] { @".qt" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"526172211A07", "RAR Archive File", new string[] { @".rar" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"2E7261FD", "Real Audio File", new string[] { @".ra", @".ram" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"EDABEEDB", "RPM Archive File", new string[] { @".rpm" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"2E736E64", "SoundMachine Audio File", new string[] { @".au" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"53495421", "Stuffit v1 Archive File", new string[] { @".sit" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"53747566664974", "Stuffit v5 Archive File", new string[] { @".sit" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"1F9D", "TAR Compressed Archive File", new string[] { @".z" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"49492A", "TIFF (Intel)", new string[] { @".tif", @".tiff" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"4D4D2A", "TIFF (Motorola)", new string[] { @".tif", @".tiff" }, ProhibitionMode.Allowed ),
                new HeaderSignature( @"4D4D002A", "TIFF (Adobe)", new string[] { @".tif", @".tiff" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"554641", "UFA Archive File", new string[] { @".ufa" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"57415645666D74", "Wave File", new string[] { @".wav" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"D7CDC69A", "Windows Meta File", new string[] { @".wmf" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"4C000000", "Windows Shortcut (Link File)", new string[] { @".lnk" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"504B3030504B0304", "WINZIP Compressed", new string[] { @".zip" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"FF575047", "WordPerfect Graphics", new string[] { @".wpg" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"FF575043", "WordPerfect v5 or v6", new string[] { @".wp" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"3C3F786D6C", "XML Document", new string[] { @".xml",@".config" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"FFFE3C0052004F004F0054005300540055004200", "XML Document (ROOTSTUB)", new string[] { @".xml" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"3C21454E54495459", "XML DTD", new string[] { @".dtd" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"5A4F4F20", "ZOO Archive File", new string[] { @".zoo" }, ProhibitionMode.Allowed ),
                new HeaderSignature( @"01766F72626973", "Ogg Vorbis", new string[] { @".ogg" }, ProhibitionMode.Allowed ),
                new HeaderSignature( @"4D5A", "Executable File",new string[] { @".exe", @".com", @".386", @".ax", @".acm", @".sys", @".dll", @".drv", @".flt", @".fon", @".ocx", @".scr", @".lrc", @".vxd", @".cpl", @".x32" }, ProhibitionMode.Allowed ),
                new HeaderSignature( new Mp3SignatureChecker(), "MPEG 3", new string[] { @".mp3" }, ProhibitionMode.Allowed ),
                
                // My own.
                new HeaderSignature( new Mp4SignatureChecker(), "MPEG 4", new string[] { @".mp4", @".m4v"}, ProhibitionMode.Allowed ),
                
                new HeaderSignature( new MSAccessSignatureChecker(), "Microsoft Access", new string[] { @".mdb", @".accdb", @".accde", @".accdr",@".accdt",@".adp",@".ade"}, ProhibitionMode.Allowed ),
				new HeaderSignature( @"D0CF11E0A1B11AE1", "Microsoft Installer", new string[] { @".msi" }, ProhibitionMode.Allowed ),
                new HeaderSignature( @"D0CF11E0A1B11AE1", "Outlook Message File", new string[] { @".msg" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"DBA52D00", "MS Word 2.0", new string[] { @".doc" }, ProhibitionMode.Allowed ),
                new HeaderSignature( @"504B0304140000000800", "Smart Notebook", new string[] { @".notebook" }, ProhibitionMode.Allowed ),
                new HeaderSignature( @"464C56", "Flash Video", new string[] { @".flv" }, ProhibitionMode.Allowed ),
                new HeaderSignature( @"465753", "Flash Shockwave", new string[] { @".swf" }, ProhibitionMode.Allowed ),
                new HeaderSignature( @"3026B2758E66CF", "Windows Video File", new string[] { @".wmv" }, ProhibitionMode.Allowed ),
                new HeaderSignature( @"3026B2758E66CF", "Windows Audio File", new string[] { @".wma" }, ProhibitionMode.Allowed ),
                new HeaderSignature( @"2142444E42", "Outlook Post Office File", new string[] { @".pst" }, ProhibitionMode.Allowed ),
                new HeaderSignature( @"FFF1", "MPEG-4 Advanced Audio Coding (AAC) Low Complexity", new string[] { @".aac" }, ProhibitionMode.Allowed ),                      
                new HeaderSignature( @"FFF9", "MPEG-2 Advanced Audio Coding (AAC) Low Complexity", new string[] { @".aac" }, ProhibitionMode.Allowed ),
                new HeaderSignature( @"D0CF11E0A1B11AE1", "Windows Thumbs.db", new string[] { @".db" }, ProhibitionMode.Allowed ),
                new HeaderSignature( @"377ABCAF271C", "7zip", new string[] { @".7z" }, ProhibitionMode.Allowed ),
                new HeaderSignature( @"4D344120", "Apple Lossless Audio Codec file", new string[] { @".m4a" }, ProhibitionMode.Allowed ),
                new HeaderSignature( @"0000002066747970", "Apple Lossless Audio Codec file", new string[] { @".m4a" }, ProhibitionMode.Allowed ),
                new HeaderSignature( @"000001BA", "MPEG Movie", new string[] { @".mpg", @".mpeg" }, ProhibitionMode.Allowed ),
                new HeaderSignature( @"000001B", "MPEG Movie", new string[] { @".mpg", @".mpeg" }, ProhibitionMode.Allowed ),
                new HeaderSignature( @"52494646", "Wave File", new string[] { @".wav" }, ProhibitionMode.Allowed ),
                new HeaderSignature( @"53514C6974652066", "SQL Lite Database", new string[] { @".db", @".sqlite" }, ProhibitionMode.Allowed ),
                new HeaderSignature( @"6F726D6174203300", "SQL Lite Database", new string[] { @".db", @".sqlite" }, ProhibitionMode.Allowed ),
                new HeaderSignature( @"636F6E6563746978", "Virtual PC Virtual HD image", new string[] { @".vhd" }, ProhibitionMode.Allowed ),
                new HeaderSignature( @"4B444D56", "VMWare Disk file", new string[] { @".vmdk" }, ProhibitionMode.Allowed ),
                new HeaderSignature( @"504B0304140000000000", "epub book", new string[] { @".epub" }, ProhibitionMode.Allowed ),
                new HeaderSignature( @"504B0304140008000800", "Java JAR File", new string[] { @".jar" }, ProhibitionMode.Allowed ),
                new HeaderSignature( @"4A4152435300", "JARCS compressed archive", new string[] { @".jar" }, ProhibitionMode.Allowed ),
                new HeaderSignature( @"504B0506", "PKZip Compressed", new string[] { @".zip", @".jar" }, ProhibitionMode.Allowed ),
                new HeaderSignature( @"504B0708", "PKZip Compressed", new string[] { @".zip", @".jar" }, ProhibitionMode.Allowed ),
                new HeaderSignature( @"504B0304", "Zip Compressed", new string[] { @".zip", @".jar", @".odt", @".odp", @".odt", @"kwd", @".sxc", @".sxd", @".sxi", @".sxw", @".xps" }, ProhibitionMode.Allowed ),
				new HeaderSignature( @"EFBBBF3C", "XML Document", new string[] { @".xml",@".config" }, ProhibitionMode.Allowed ),
                new HeaderSignature( @"46726F6D3A", "Web Archive", new string[] { @".mht" }, ProhibitionMode.Allowed ),
                new HeaderSignature( @"D0CF11E0A1B11AE1", "msp Installer", new string[] { @".msp" }, ProhibitionMode.Allowed ),
                new HeaderSignature( @"4D534346", "Microsoft Update Installer Package", new string[] { @".msu" }, ProhibitionMode.Allowed ),
                new HeaderSignature( @"454E49474D412042494E415259", "Finale Music File", new string[] { @".mus" }, ProhibitionMode.Allowed ),
                new HeaderSignature( @"504B0304140000", "Make Music File", new string[] { @".musx" }, ProhibitionMode.Allowed ),
                new HeaderSignature( new PogSignatureChecker(), "POG File", new string[] { @".pog"}, ProhibitionMode.Allowed ),
                new HeaderSignature( @"5075726368617365204F72646572", "pof file", new string[] { @".pof" }, ProhibitionMode.Allowed ),
                new HeaderSignature( @"5B7B3030303231344130", "URL Shortcut File", new string[] { @".url" }, ProhibitionMode.Allowed ),
                new HeaderSignature( new QBWSignatureChecker(), "Quickbooks file", new string[] { @".qbw", @".tlg"}, ProhibitionMode.Allowed ),
                new HeaderSignature( @"2F2F5468697320697320517569636B", "Quickbooks configuration file", new string[] { @".nd"}, ProhibitionMode.Allowed ),
                new HeaderSignature( @"D0CF11E0A1B11AE1", "Quickbooks backup file", new string[] { @".qbb"}, ProhibitionMode.Allowed ),
                new HeaderSignature( @"5B44454641554C545D", "URL Shortcut File", new string[] { @".url"}, ProhibitionMode.Allowed ),
                

                /*new HeaderSignature( @"", "Windows Media", new string[] { @".wmv", @".asf" }, ProhibitionMode.Prohibited ),*/
				
			};

		// ------------------------------------------------------------------
		#endregion

		#region Public methods.
		// ------------------------------------------------------------------


        public static bool ExtensionSupported(string strExtension)
        {
            bool blsupported = false;
            switch (strExtension.ToLower())
            {
                case @".exe":
                    blsupported = true;
                    break;
                case @".com":
                    blsupported = true;
                    break;
                case @".386":
                    blsupported = true;
                    break;
                case @".ax":
                    blsupported = true;
                    break;
                case @".acm":
                    blsupported = true;
                    break;
                case @".sys":
                    blsupported = true;
                    break;
                case @".dll":
                    blsupported = true;
                    break;
                case @".drv":
                    blsupported = true;
                    break;
                case @".flt":
                    blsupported = true;
                    break;
                case @".fon":
                    blsupported = true;
                    break;
                case @".ocx":
                    blsupported = true;
                    break;
                case @".scr":
                    blsupported = true;
                    break;
                case @".lrc":
                    blsupported = true;
                    break;
                case @".vxd":
                    blsupported = true;
                    break;
                case @".cpl":
                    blsupported = true;
                    break;
                case @".x32":
                    blsupported = true;
                    break;
                case @".mp3":
                    blsupported = true;
                    break;
                case @".ogg":
                    blsupported = true;
                    break;
                case @".zoo":
                    blsupported = true;
                    break;
                case @".dtd":
                    blsupported = true;
                    break;
                case @".xml":
                    blsupported = true;
                    break;
                case @".wp":
                    blsupported = true;
                    break;
                case @".wpg":
                    blsupported = true;
                    break;
                case @".zip":
                    blsupported = true;
                    break;
                case @".lnk":
                    blsupported = true;
                    break;
                case @".wmf":
                    blsupported = true;
                    break;
                case @".adx":
                    blsupported = true;
                    break;
                case @".ain":
                    blsupported = true;
                    break;
                case @".ami":
                    blsupported = true;
                    break;
                case @".apr":
                    blsupported = true;
                    break;
                case @".arc":
                    blsupported = true;
                    break;
                case @".arj":
                    blsupported = true;
                    break;
                case @".au":
                    blsupported = true;
                    break;
                case @".avi":
                    blsupported = true;
                    break;
                case @".bmp":
                    blsupported = true;
                    break;
                case @".bz":
                    blsupported = true;
                    break;
                case @".bz2":
                    blsupported = true;
                    break;
                case @".cab":
                    blsupported = true;
                    break;
                case @".cnt":
                    blsupported = true;
                    break;
                case @".cpio":
                    blsupported = true;
                    break;
                case @".cru":
                    blsupported = true;
                    break;
                case @".crush":
                    blsupported = true;
                    break;
                case @".dcx":
                    blsupported = true;
                    break;
                case @".doc":
                    blsupported = true;
                    break;
                case @".docx":
                    blsupported = true;
                    break;
                case @".ds4":
                    blsupported = true;
                    break;
                case @".emf":
                    blsupported = true;
                    break;
                case @".eps":
                    blsupported = true;
                    break;
                case @".evt":
                    blsupported = true;
                    break;
                case @".fm3":
                    blsupported = true;
                    break;
                case @".fmt":
                    blsupported = true;
                    break;
                case @".gid":
                    blsupported = true;
                    break;
                case @".gif":
                    blsupported = true;
                    break;
                case @".gz":
                    blsupported = true;
                    break;
                case @".hap":
                    blsupported = true;
                    break;
                case @".hlp":
                    blsupported = true;
                    break;
                case @".hqx":
                    blsupported = true;
                    break;
                case @".htm":
                    blsupported = true;
                    break;
                case @".html":
                    blsupported = true;
                    break;
                case @".ico":
                    blsupported = true;
                    break;
                case @".jar":
                    blsupported = true;
                    break;
                case @".jpe":
                    blsupported = true;
                    break;
                case @".jpeg":
                    blsupported = true;
                    break;
                case @".jpg":
                    blsupported = true;
                    break;
                case @".lha":
                    blsupported = true;
                    break;
                case @".lhp":
                    blsupported = true;
                    break;
                case @".lzh":
                    blsupported = true;
                    break;
                case @".mid":
                    blsupported = true;
                    break;
                case @".mov":
                    blsupported = true;
                    break;
                case @".mpeg":
                    blsupported = true;
                    break;
                case @".mpg":
                    blsupported = true;
                    break;
                case @".nsf":
                    blsupported = true;
                    break;
                case @".ntf":
                    blsupported = true;
                    break;
                case @".obj":
                    blsupported = true;
                    break;
                case @".pcx":
                    blsupported = true;
                    break;
                case @".pdf":
                    blsupported = true;
                    break;
                case @".png":
                    blsupported = true;
                    break;
                case @".ppt":
                    blsupported = true;
                    break;
                case @".pptx":
                    blsupported = true;
                    break;
                case @".psp":
                    blsupported = true;
                    break;
                case @".qt":
                    blsupported = true;
                    break;
                case @".ra":
                    blsupported = true;
                    break;
                case @".ram":
                    blsupported = true;
                    break;
                case @".rar":
                    blsupported = true;
                    break;
                case @".reg":
                    blsupported = true;
                    break;
                case @".rpm":
                    blsupported = true;
                    break;
                case @".rtf":
                    blsupported = true;
                    break;
                case @".sit":
                    blsupported = true;
                    break;
                case @".tar":
                    blsupported = true;
                    break;
                case @".tgz":
                    blsupported = true;
                    break;
                case @".tif":
                    blsupported = true;
                    break;
                case @".tiff":
                    blsupported = true;
                    break;
                case @".ufa":
                    blsupported = true;
                    break;
                case @".wav":
                    blsupported = true;
                    break;
                case @".wk1":
                    blsupported = true;
                    break;
                case @".wk3":
                    blsupported = true;
                    break;
                case @".wk4":
                    blsupported = true;
                    break;
                case @".wks":
                    blsupported = true;
                    break;
                case @".xls":
                    blsupported = true;
                    break;
                case @".xlsx":
                    blsupported = true;
                    break;
                case @".xlt":
                    blsupported = true;
                    break;
                case @".z":
                    blsupported = true;
                    break;
                case @".dot":
                    blsupported = true;
                    break;
                case @".pps":
                    blsupported = true;
                    break;
                case @".mp4":
                    blsupported = true;
                    break;
                case @".m4v":
                    blsupported = true;
                    break;
                case @".msi":
                    blsupported = true;
                    break;
                case @".msg":
                    blsupported = true;
                    break;
                case @".wps":
                    blsupported = true;
                    break;
                case @".vsd":
                    blsupported = true;
                    break;
                case @".qbm":
                    blsupported = true;
                    break;
                case @".pub":
                    blsupported = true;
                    break;
                case @".adp":
                    blsupported = true;
                    break;
                case @".notebook":
                    blsupported = true;
                    break;
                case @".flv":
                    blsupported = true;
                    break;
                case @".swf":
                    blsupported = true;
                    break;
                case @".wmv":
                    blsupported = true;
                    break;
                case @".wma":
                    blsupported = true;
                    break;
                case @".pst":
                    blsupported = true;
                    break;
                case @".config":
                    blsupported = true;
                    break;
                case @".7z":
                    blsupported = true;
                    break;
                case @".kwd":
                    blsupported = true;
                    break;
                case @".vsdx":
                    blsupported = true;
                    break;
                case @".odt":
                    blsupported = true;
                    break;
                case @".odp":
                    blsupported = true;
                    break;
                case @".xps":
                    blsupported = true;
                    break;
                case @".sxc":
                    blsupported = true;
                    break;
                case @".sxd":
                    blsupported = true;
                    break;
                case @".sxi":
                    blsupported = true;
                    break;
                case @".sxw":
                    blsupported = true;
                    break;
                case @".aac":
                    blsupported = true;
                    break;
                case @".db":
                    blsupported = true;
                    break;
                case @".m4a":
                    blsupported = true;
                    break;
                case @".sqlite":
                    blsupported = true;
                    break;
                case @".epub":
                    blsupported = true;
                    break;
                case @".vmdk":
                    blsupported = true;
                    break;
                case @".vhd":
                    blsupported = true;
                    break;
                case @".msu":
                    blsupported = true;
                    break;
                case @".msp":
                    blsupported = true;
                    break;
                case @".mus":
                    blsupported = true;
                    break;
                case @".musx":
                    blsupported = true;
                    break;
                case @".url":
                    blsupported = true;
                    break;
                case @".pog":
                    blsupported = true;
                    break;
                case @".pof":
                    blsupported = true;
                    break;
                case @".qbw":
                    blsupported = true;
                    break;
                case @".nd":
                    blsupported = true;
                    break;
                case @".tlg":
                    blsupported = true;
                    break;
                case @".qbb":
                    blsupported = true;
                    break;
                default:
                    break;
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

		// ------------------------------------------------------------------
		#endregion
	}

	/////////////////////////////////////////////////////////////////////////
}