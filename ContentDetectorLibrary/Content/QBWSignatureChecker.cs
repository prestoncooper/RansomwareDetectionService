namespace RansomwareDetection.ContentDetectorLib.Content
{
    /////////////////////////////////////////////////////////////////////////

    /// <summary>
    /// Special checker for qbw files.
    /// </summary>
    internal class QBWSignatureChecker :
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
            byte u = buffer[20];
            byte v = buffer[21];
            byte w = buffer[22];
            byte x = buffer[23];
            byte y = buffer[24];

            //XX XX XX XX XX XX XX XX XX XX XX XX XX XX XX XX XX XX XX XX 5E BA 7A DA C9
            return
                (u==0x5E && v== 0xBA && w== 0x7A && x==0xDA && y == 0xC9);
                
        }

        /// <summary>
        /// Gets the minimum length of the required buffer.
        /// </summary>
        /// <value>The minimum length of the required buffer.</value>
        public int MinimumRequiredBufferLength
        {
            get
            {
                return 25;
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
                return 25;
            }
        }

        // ------------------------------------------------------------------
        #endregion
    }

    /////////////////////////////////////////////////////////////////////////
}