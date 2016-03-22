namespace RansomwareDetection.ContentDetectorLib.Content
{
	/////////////////////////////////////////////////////////////////////////

	/// <summary>
	/// Interface to implement when checking a buffer for a certain signature.
	/// </summary>
	internal interface ISignatureChecker
	{
		#region Interface members.
		// ------------------------------------------------------------------

		/// <summary>
		/// Check whether a given buffer matches the signature.
		/// </summary>
		/// <param name="buffer">The buffer.</param>
		/// <returns></returns>
		bool MatchesSignature(
			byte[] buffer );

		/// <summary>
		/// Gets the first number of bytes to read.
		/// </summary>
		/// <value>The first number of bytes to read.</value>
		int FirstNumberOfBytesToRead
		{
			get;
		}

		/// <summary>
		/// Gets the minimum length of the required buffer.
		/// </summary>
		/// <value>The minimum length of the required buffer.</value>
		int MinimumRequiredBufferLength
		{
			get;
		}

		// ------------------------------------------------------------------
		#endregion
	}

	/////////////////////////////////////////////////////////////////////////
}