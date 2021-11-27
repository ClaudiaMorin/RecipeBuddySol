using System;
using System.Security.Cryptography;


namespace RecipeBuddy.Core.Helpers
{
    public static class PasswordHashing
    {
        public static byte[] CalculateHash(byte[] inputBytes)
        {
            SHA256Managed algorithm = new SHA256Managed();
            algorithm.ComputeHash(inputBytes);
            return algorithm.Hash;
        }

        public static bool SequenceEquals(byte[] originalByteArray, byte[] newByteArray)
        {
            if (originalByteArray == null || newByteArray == null)
                throw new ArgumentNullException(originalByteArray == null ? "originalByteArray" : "newByteArray", "byte arrays are null.");

            if (originalByteArray.Length != newByteArray.Length)
                return false;

            for (int i = 0; i < originalByteArray.Length; i++)
            {
                if (originalByteArray[i] != newByteArray[i])
                    return false;
            }
            
            return true;
        }
    }
}
