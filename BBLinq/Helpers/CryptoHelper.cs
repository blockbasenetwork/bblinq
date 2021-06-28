using Cryptography.ECDSA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlockBase.BBLinq.Helpers
{
    /// <summary>
    /// Helper class with crypto functions
    /// </summary>
    public class CryptoHelper
    {
        public static readonly int PrivateKeyDataSize = 32;

        /// <summary>
        /// Get private key bytes without is checksum
        /// </summary>
        /// <param name="privateKey">private key</param>
        /// <returns>byte array</returns>
        public static byte[] GetPrivateKeyBytesWithoutCheckSum(string privateKey)
        {
            return privateKey.StartsWith("PVT_R1_") ?
                PrivKeyStringToBytes(privateKey).Take(PrivateKeyDataSize).ToArray() :
                PrivKeyStringToBytes(privateKey).Skip(1).Take(PrivateKeyDataSize).ToArray();
        }
        /// <summary>
        /// Convert encoded public key to byte array
        /// </summary>
        /// <param name="key">encoded public key</param>
        /// <returns>public key bytes</returns>
        public static byte[] PrivKeyStringToBytes(string key)
        {
            return key.StartsWith("PVT_R1_") ?
                StringToKey(key[7..], PrivateKeyDataSize, "R1") :
                StringToKey(key, PrivateKeyDataSize, "sha256x2");
        }

        /// <summary>
        /// Convert Pub/Priv key or signature to byte array
        /// </summary>
        /// <param name="key">generic key</param>
        /// <param name="size">Key size</param>
        /// <param name="keyType">Optional key type. (sha256x2, R1, K1)</param>
        /// <returns>key bytes</returns>
        public static byte[] StringToKey(string key, int size, string keyType = null)
        {
            var keyBytes = Base58.Decode(key);
            byte[] digest;
            var skipSize = 0;

            if (keyType == "sha256x2")
            {
                // skip version
                skipSize = 1;
                digest = Sha256Manager.GetHash(Sha256Manager.GetHash(keyBytes.Take(size + skipSize).ToArray()));
            }
            else if (!string.IsNullOrWhiteSpace(keyType))
            {
                // skip K1 recovery param
                if (keyType == "K1")
                    skipSize = 1;

                digest = Ripemd160Manager.GetHash(SerializationHelper.Combine(new List<byte[]>() {
                    keyBytes.Take(size + skipSize).ToArray(),
                    Encoding.UTF8.GetBytes(keyType)
                }));
            }
            else
            {
                digest = Ripemd160Manager.GetHash(keyBytes.Take(size).ToArray());
            }

            if (!keyBytes.Skip(size + skipSize).SequenceEqual(digest.Take(4)))
            {
                throw new Exception("checksum doesn't match.");
            }
            return keyBytes;
        }
    }
}
