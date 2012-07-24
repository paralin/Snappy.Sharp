﻿namespace Snappy.Sharp
{
    internal class Utilities
    {
        /// <summary>
        /// Copies 64 bits (8 bytes) from source array starting at sourceIndex into dest array starting at destIndex.
        /// </summary>
        /// <param name="source">The source array.</param>
        /// <param name="sourceIndex">Index to start copying.</param>
        /// <param name="dest">The destination array.</param>
        /// <param name="destIndex">Index to start writing.</param>
        /// <remarks>The name comes from the original Snappy C++ source. I don't think there is a good way to look at 
        /// things in an aligned manner in the .NET Framework.</remarks>
        public unsafe static void UnalignedCopy64(byte[] source, int sourceIndex, byte[] dest, int destIndex)
        {
            fixed (byte* src = &source[sourceIndex], dst = &dest[destIndex])
            {
                *((long*)dst) = *((long*)src);
            }
        }

        public unsafe static uint GetUInt(byte[] source, int index)
        {
            fixed (byte* src = &source[index])
            {
                return *((uint*)src);
            }
        }

        public unsafe static ulong GetULong(byte[] source, int index)
        {
            fixed (byte* src = &source[index])
            {
                return *((ulong*)src);
            }
        }

        // Function from http://aggregate.org/MAGIC/
        public static uint Log2Floor(uint x)
        {
            x |= (x >> 1);
            x |= (x >> 2);
            x |= (x >> 4);
            x |= (x >> 8);
            x |= (x >> 16);
            // here Log2Floor(0) = 0
            return(NumberOfOnes(x >> 1));
        }

        // Function from http://aggregate.org/MAGIC/
        public static uint NumberOfLeadingZeros(uint x)
        {
            x |= (x >> 1);
            x |= (x >> 2);
            x |= (x >> 4);
            x |= (x >> 8);
            x |= (x >> 16);
            return (sizeof(int) * 8 - NumberOfOnes(x));
        }

        // Function from http://aggregate.org/MAGIC/
        public static uint NumberOfTrailingZeros(uint x)
        {
            return NumberOfOnes((uint) ((x & -x) - 1));
        }

        // Function from http://aggregate.org/MAGIC/
        public static uint NumberOfOnes(uint x)
        {
            x -= ((x >> 1) & 0x55555555);
            x = (((x >> 2) & 0x33333333) + (x & 0x33333333));
            x = (((x >> 4) + x) & 0x0f0f0f0f);
            x += (x >> 8);
            x += (x >> 16);
            return (x & 0x0000003f);
        }
    }
}
