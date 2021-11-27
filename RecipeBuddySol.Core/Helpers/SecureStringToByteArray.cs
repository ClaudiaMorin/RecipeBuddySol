using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;

namespace RecipeBuddy.Core.Helpers
{
    public static class ConvertingStringToByteArray
    {
        public static byte[] ConvertSecureStringToByteArray(SecureString value)
        {
            byte[] returnVal = new byte[value.Length];

            IntPtr valuePtr = IntPtr.Zero;
            try
            {
                valuePtr = System.Runtime.InteropServices.Marshal.SecureStringToGlobalAllocUnicode(value);
                for (int i = 0; i < value.Length; i++)
                {
                    short unicodeChar = System.Runtime.InteropServices.Marshal.ReadInt16(valuePtr, i * 2);
                    returnVal[i] = Convert.ToByte(unicodeChar);
                }

                return returnVal;
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }
        }

        public static byte[] ConvertStringToByteArray(String value)
        {
            byte[] returnVal = new byte[value.Length];

            IntPtr valuePtr = IntPtr.Zero;
            try
            {
                valuePtr = System.Runtime.InteropServices.Marshal.StringToHGlobalUni(value);
                for (int i = 0; i < value.Length; i++)
                {
                    short unicodeChar = System.Runtime.InteropServices.Marshal.ReadInt16(valuePtr, i * 2);
                    returnVal[i] = Convert.ToByte(unicodeChar);
                }

                return returnVal;
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }
        }
    }
}
