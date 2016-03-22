namespace RansomwareDetection.ContentDetectorLib.Content
{
	/////////////////////////////////////////////////////////////////////////

	/// <summary>
	/// Simple enum to flag certain signatures with.
	/// </summary>
	public enum SignatureMode
	{
		#region Enum members.
		// ------------------------------------------------------------------

		/// <summary>
		/// A literal text.
		/// </summary>
		Text,

		/// <summary>
		/// A string containing hexadecimal values.
		/// </summary>
		HexString

		// ------------------------------------------------------------------
		#endregion
	}

	/////////////////////////////////////////////////////////////////////////
}