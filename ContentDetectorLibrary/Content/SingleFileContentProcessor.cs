namespace RansomwareDetection.ContentDetectorLib.Content
{
	#region Using directives.
	// ----------------------------------------------------------------------

	using System.IO;

	// ----------------------------------------------------------------------
	#endregion

	/////////////////////////////////////////////////////////////////////////

	/// <summary>
	/// Checks whether the given file contains any prohibited content.
	/// </summary>
	internal sealed class SingleFileContentProcessor
	{
		#region Public methods.
		// ------------------------------------------------------------------

		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="SingleFileContentProcessor"/> class.
		/// </summary>
		/// <param name="filePath">The file path.</param>
		public SingleFileContentProcessor(
            Delimon.Win32.IO.FileInfo filePath)
		{
			_filePath = filePath;
		}

		/// <summary>
		/// Determines whether [contains prohibited content].
		/// </summary>
		/// <returns>
		/// 	<c>true</c> if [contains prohibited content]; otherwise, <c>false</c>.
		/// </returns>
		public bool ContainsProhibitedContent(bool ignoreExtension)
		{
			foreach ( HeaderSignature signature in HeaderSignature.StockSignatures )
			{
				if ( signature.ProhibitionMode == ProhibitionMode.Prohibited &&
					signature.MatchesFile( _filePath,ignoreExtension) )
				{
					return true;
				}
			}

			return false;
		}

        /// <summary>
        /// Determines whether [contains prohibited content].
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if [contains prohibited content]; otherwise, <c>false</c>.
        /// </returns>
        public bool VerifyHeaderContent()
        {
            foreach (HeaderSignature signature in HeaderSignature.StockSignatures)
            {
                if (signature.MatchesFile(_filePath, false))
                {
                    return true;
                }
            }

            //Add Non Stock Signatures Here

            return false;
        }

		// ------------------------------------------------------------------
		#endregion

		#region Private variables.
		// ------------------------------------------------------------------

        private Delimon.Win32.IO.FileInfo _filePath;

		// ------------------------------------------------------------------
		#endregion
	}

	/////////////////////////////////////////////////////////////////////////
}