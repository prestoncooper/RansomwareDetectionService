namespace RansomwareDetection.ContentDetectorLib.Content
{
    /////////////////////////////////////////////////////////////////////////

    /// <summary>
    /// Special checker for MP4 files.
    /// </summary>
    internal class MSAccessSignatureChecker :
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
            byte i = buffer[8];
            byte j = buffer[9];
            byte k = buffer[10];
            byte l = buffer[11];
            byte m = buffer[12];

            //ACCDB
            //00 01 00 00 53 74 61 6E
            //64 61 72 64 20 41 43 45
            //20 44 42
            //50 4B 03 04 14 00 06 00 08 00 00 00 21
            //53 74 61 6E 64 61 72 64 20 41 43 45


            //MDB
            //00 01 00 00 53 74 61 6E
            //64 61 72 64 20 4A 65 74
            //20 44 42
            //53 74 61 6E 64 61 72 64 20 4A 65 74

            

            return
                   (a == 0x00 && b == 0x01 && c == 0x00 && d == 0x00 && e == 0x53 && f == 0x74 && g == 0x61 && h == 0x6E)
                || (a == 0x64 && b == 0x61 && c == 0x72 && d == 0x64 && e == 0x20 && f == 0x41 && g == 0x43 && h == 0x45)
                || (a == 0x20 && b == 0x44 && c == 0x42)
                || (a == 0x00 && b == 0x01 && c == 0x00 && d == 0x00 && e == 0x53 && f == 0x74 && g == 0x61 && h == 0x6E)
                || (a == 0x64 && b == 0x61 && c == 0x72 && d == 0x64 && e == 0x20 && f == 0x4A && g == 0x65 && h == 0x74)
                || (a == 0x53 && b == 0x74 && c == 0x61 && d == 0x6E && e == 0x64 && f == 0x61 && g == 0x72 && h == 0x64 && i == 0x20 && j == 0x4A && k == 0x65 && l == 0x74)
                || (a == 0x50 && b == 0x4B && c == 0x03 && d == 0x04 && e == 0x14 && f == 0x00 && g == 0x06 && h == 0x00 && i == 0x08 && j == 0x00 && k == 0x00 && l == 0x00 && m == 0x21);

        }

        /// <summary>
        /// Gets the minimum length of the required buffer.
        /// </summary>
        /// <value>The minimum length of the required buffer.</value>
        public int MinimumRequiredBufferLength
        {
            get
            {
                return 30;
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
                return 30;
            }
        }

        // ------------------------------------------------------------------
        #endregion
    }

    /////////////////////////////////////////////////////////////////////////
}