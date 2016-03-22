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
            byte e = buffer[4];
            byte f = buffer[5];
            byte g = buffer[6];
            byte h = buffer[7];
            byte i = buffer[8];
            byte j = buffer[9];
            byte k = buffer[10];
            byte l = buffer[11];

            //00 00 00 14 66 74 79 70 6D 70 34
            //00 00 00 18 66 74 79 70 6D 70 34
            
            //[4 byte offset]
            //66 74 79 70 4D 53 4E 56
            
            //[4 byte offset]
            //66 74 79 70 69 73 6F 6D

            //[4 byte offset]
            //66 74 79 70 6D 70 34

            //[4 byte offset]
            //66 74 79 70 33 67 70 35
            return

                (e == 0x66 && f == 0x74 && g == 0x79 && h == 0x70 && i == 0x4D && j == 0x53 && k == 0x4E && l == 0x56)
                || (e == 0x66 && f == 0x74 && g == 0x79 && h == 0x70 && i == 0x69 && j == 0x73 && k == 0x6F && l == 0x6D)
                || (e == 0x66 && f == 0x74 && g == 0x79 && h == 0x70 && i == 0x6D && j == 0x70 && k == 0x34)
                || (e == 0x66 && f == 0x74 && g == 0x79 && h == 0x70 && i == 0x33 && j == 0x67 && k == 0x70 && l == 0x35);
                
        }

        /// <summary>
        /// Gets the minimum length of the required buffer.
        /// </summary>
        /// <value>The minimum length of the required buffer.</value>
        public int MinimumRequiredBufferLength
        {
            get
            {
                return 12;
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
                return 12;
            }
        }

        // ------------------------------------------------------------------
        #endregion
    }

    /////////////////////////////////////////////////////////////////////////
}