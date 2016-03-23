namespace RansomwareDetection.ContentDetectorLib.Content
{
    /////////////////////////////////////////////////////////////////////////

    /// <summary>
    /// Special checker for POG files.
    /// </summary>
    internal class PogSignatureChecker :
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
            
            byte e = buffer[4];
            byte f = buffer[5];
            byte g = buffer[6];
            byte h = buffer[7];
            byte i = buffer[8];
            byte j = buffer[9];
            byte k = buffer[10];
            byte l = buffer[11];

            //00 04 xx xx 00 00 19 00 0E
            return
                (a == 0x00 && b == 0x04 && e==0x00 && f==0x00 && g==0x19 && h==0x00 && i == 0x0E);
                
                
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