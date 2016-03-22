namespace RansomwareDetection.ContentDetectorLib.Content
{
	/////////////////////////////////////////////////////////////////////////

	/// <summary>
	/// Special checker for MP3 files.
	/// </summary>
	internal class Mp3SignatureChecker :
		ISignatureChecker
	{
		#region ISignatureChecker members.
		// ------------------------------------------------------------------

		/// <summary>
		/// Check whether a given buffer matches the signature.
		/// </summary>
		/// <param name="buffer">The buffer.</param>
		/// <returns></returns>
		public bool MatchesSignature(
			byte[] buffer )
		{
			// http://www.mars.org/mailman/public/mad-dev/2002-November/000778.html
			// http://www.mars.org/mailman/public/mad-dev/2002-November/000779.html
			byte a = buffer[0];
			byte b = buffer[1];
			byte c = buffer[2];
			byte d = buffer[3];

			return
				(((a == 0xFF) &&
					((b & 0xE0) == 0xE0) &&
						((b & 0x18) != 0x08) &&
							((b & 0x06) != 0) &&
								((c & 0xF0) != 0xF0) &&
									((c & 0x0C) != 0x0C) &&
										((d & 0x03) != 0x02))
                                            ||(a==0x49 && b==0x44 && c== 0x33));
		}

		/// <summary>
		/// Gets the minimum length of the required buffer.
		/// </summary>
		/// <value>The minimum length of the required buffer.</value>
		public int MinimumRequiredBufferLength
		{
			get
			{
				return 4;
			}
		}

		/// <summary>
		/// Gets the first number of bytes to read.
		/// </summary>
		/// <value>The first number of bytes to read.</value>
		public int FirstNumberOfBytesToRead
		{
			get
			{
				return 10;
			}
		}

		// ------------------------------------------------------------------
		#endregion
	}

	/////////////////////////////////////////////////////////////////////////
}