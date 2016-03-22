namespace RansomwareDetection.ContentDetectorLib.Content
{
    /////////////////////////////////////////////////////////////////////////

    /// <summary>
    /// Special checker for MP4 files.
    /// </summary>
    internal class Mp4SignatureChecker :
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
            byte[] buffer)
        {
            byte a = buffer[0];
            byte b = buffer[1];
            byte c = buffer[2];
            byte d = buffer[3];
            byte e = buffer[4];
            byte f = buffer[5];
            byte g = buffer[6];
            byte h = buffer[7];


            return
                (a == 0x00 && b == 0x00 && c == 0x00 && d == 0x18 && e == 0x66 && f == 0x74 && g == 0x79 && h == 0x70)
                || (a == 0x69 && b == 0x73 && c == 0X6F && d == 0x6D)
                || (a == 0x6D && b == 0x70 && c == 0x34 && d == 0x32)
                || (a == 0x00 && b == 0x00 && c == 0x00 && d == 0x1C && e == 0x66 && f == 0x74 && g == 0x79 && h == 0x70)
                || (a == 0x4D && b == 0x53 && c == 0x4E && d == 0x56 && e == 0x01 && f == 0x29 && g == 0x00 && h == 0x46)
                || (a == 0x4D && b == 0x53 && c == 0x4E && d == 0x56 && e == 0x6D && f == 0x70 && g == 0x34 && h == 0x32);
                       
        }

        /// <summary>
        /// Gets the minimum length of the required buffer.
        /// </summary>
        /// <value>The minimum length of the required buffer.</value>
        public int MinimumRequiredBufferLength
        {
            get
            {
                return 8;
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