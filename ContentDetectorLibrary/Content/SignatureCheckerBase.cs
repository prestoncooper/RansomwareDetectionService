namespace RansomwareDetection.ContentDetectorLib.Content
{
	#region Using directives.
	// ----------------------------------------------------------------------

	using System.Collections.Generic;

	// ----------------------------------------------------------------------
	#endregion

	/////////////////////////////////////////////////////////////////////////
	
	/// <summary>
	/// Base class for custom signature checkers. Provides common methods.
	/// </summary>
	internal abstract class SignatureCheckerBase :
		ISignatureChecker
	{
		#region ISignatureChecker members.
		// ------------------------------------------------------------------

		/// <summary>
		/// Check whether a given buffer matches the signature.
		/// </summary>
		/// <param name="buffer">The buffer.</param>
		/// <returns></returns>
		public abstract bool MatchesSignature(
			byte[] buffer );

		/// <summary>
		/// Gets the minimum length of the required buffer.
		/// </summary>
		/// <value>The minimum length of the required buffer.</value>
		public abstract int MinimumRequiredBufferLength
		{
			get;
		}

		/// <summary>
		/// Gets the first number of bytes to read.
		/// </summary>
		/// <value>The first number of bytes to read.</value>
		public abstract int FirstNumberOfBytesToRead
		{
			get;
		}

        /// <summary>
        /// Gets the first number of bytes to read.
        /// </summary>
        /// <value>The first number of bytes to read.</value>
        public abstract int ByteOffset
        {
            get;
        }

		// ------------------------------------------------------------------
		#endregion

		#region Private methods.
		// ------------------------------------------------------------------

		/// <summary>
		/// Determines whether [is pattern contained in buffer]
		/// [the specified buffer].
		/// </summary>
		/// <param name="buffer">The buffer.</param>
		/// <param name="pattern">The pattern.</param>
		/// <returns>
		/// 	<c>true</c> if [is pattern contained in buffer] 
		/// [the specified buffer]; otherwise, <c>false</c>.
		/// </returns>
		protected static bool IsPatternContainedInBuffer(
			byte[] buffer,
			byte[] pattern )
		{
			if ( pattern == null || pattern.Length <= 0 ||
				buffer == null || buffer.Length <= 0 ||
				pattern.Length > buffer.Length )
			{
				return false;
			}
			else
			{
				return IndexOfPattern( buffer, pattern ) >= 0;
			}
		}

        /// <summary>
        /// Determines whether [is pattern contained in buffer]
        /// [the specified buffer].
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        /// <param name="pattern">The pattern.</param>
        /// <returns>
        /// 	<c>true</c> if [is pattern contained in buffer] 
        /// [the specified buffer]; otherwise, <c>false</c>.
        /// </returns>
        protected static bool IsPatternContainedInBuffer(
            byte[] buffer,
            int byteoffset,
            byte[] pattern)
        {
            if (pattern == null || pattern.Length <= 0 ||
                buffer == null || buffer.Length <= 0 ||
                pattern.Length > buffer.Length)
            {
                return false;
            }
            else
            {
                return IndexOfPattern(byteoffset,buffer, pattern) >= 0;
            }
        }

		/// <summary>
		/// Checks whether one pattern is contained within another.
		/// </summary>
		/// <param name="array">The array.</param>
		/// <param name="pattern">The pattern.</param>
		/// <returns>
		/// Returns the index or -1 if not contained.
		/// </returns>
		/// <typeparam name="T"></typeparam>
		/// <remarks>
		/// http://snippets.dzone.com/posts/show/3889.
		/// </remarks>
		private  static int IndexOfPattern<T>(
			IEnumerable<T> array,
			IEnumerable<T> pattern )
		{
			bool found;

			IEnumerator<T> i = array.GetEnumerator();
			IEnumerator<T> j = pattern.GetEnumerator();

			int i_index = -1;
			int pattern_length = 0;
			for ( ; ; )
			{
				if ( !i.MoveNext() )
				{
					if ( !j.MoveNext() )
					{
						i_index++;
						found = true;
						break;
					}
					found = false;
					break;
				}
				i_index++;
				if ( !j.MoveNext() )
				{
					found = true;
					break;
				}
				pattern_length++;
				if ( !i.Current.Equals( j.Current ) )
				{
					j = pattern.GetEnumerator();
					pattern_length = 0;
					continue;
				}
			}
			if ( !found )
			{
				return -1;
			}
			else
			{
				return i_index - pattern_length;
			}
		}


        /// <summary>
        /// Checks whether one pattern is contained within another.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="pattern">The pattern.</param>
        /// <returns>
        /// Returns the index or -1 if not contained.
        /// </returns>
        /// <typeparam name="T"></typeparam>
        /// <remarks>
        /// http://snippets.dzone.com/posts/show/3889.
        /// </remarks>
        private static int IndexOfPattern<T>(
            int byteoffset,
            IEnumerable<T> array,
            IEnumerable<T> pattern)
        {
            bool found;

            IEnumerator<T> i = array.GetEnumerator();
            IEnumerator<T> j = pattern.GetEnumerator();
            int i_index = -1;
            int pattern_length = 0;

            for (int b = 1; b <= byteoffset; b++)
            {
                if (!i.MoveNext())
                {
                    found = false;
                    return -1;
                }

            }

           
            for (; ; )
            {
                if (!i.MoveNext())
                {
                    if (!j.MoveNext())
                    {
                        i_index++;
                        found = true;
                        break;
                    }
                    found = false;
                    break;
                }
                i_index++;
                if (!j.MoveNext())
                {
                    found = true;
                    break;
                }
                pattern_length++;
                if (!i.Current.Equals(j.Current))
                {
                    j = pattern.GetEnumerator();
                    pattern_length = 0;
                    continue;
                }
            }
            if (!found)
            {
                return -1;
            }
            else
            {
                return i_index - pattern_length;
            }
        }

		// ------------------------------------------------------------------
		#endregion
	}

	/////////////////////////////////////////////////////////////////////////
}