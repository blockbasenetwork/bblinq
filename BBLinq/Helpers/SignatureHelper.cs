using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Cryptography.ECDSA;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace BlockBase.BBLinq.Helpers
{
    public static class SignatureHelper
    {
        private static readonly byte[] KeyTypeBytes = Encoding.UTF8.GetBytes("K1");
        private const int ChecksumLength = 4;
        private const string SigPrefix = "SIG_K1_";

        public static string SignHash(string privateKeyString, byte[] hash)
        {
            try
            {

                ECPrivateKeyParameters privateKeyParameters =
                    EosKeyHelper.GetEcPrivateKeyParametersFromString(privateKeyString);
                ISigner signer = SignerUtilities.GetSigner("NONEwithECDSA");
                signer.Init(true, privateKeyParameters);
                signer.BlockUpdate(hash, 0, hash.Length);
                var sigBytes = signer.GenerateSignature();

                var check = new List<byte[]>() {sigBytes, KeyTypeBytes};
                var checksum = Ripemd160Manager.GetHash(SerializationHelper.Combine(check)).Take(ChecksumLength)
                    .ToArray();
                var signAndChecksum = new List<byte[]>() {sigBytes, checksum};
                var finalSig = SigPrefix + Base58.Encode(SerializationHelper.Combine(signAndChecksum));

                return finalSig;
            }
            catch (Exception exc)
            {
                Console.WriteLine($"Signing Failed: {exc}");
                return null;
            }
        }
    }
}