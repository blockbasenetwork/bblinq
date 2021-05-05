using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;

namespace BlockBase.BBLinq.Helpers
{
    public static class EosKeyHelper
    {
        private static readonly X9ECParameters EcParams = ECNamedCurveTable.GetByName("secp256k1");
        private static readonly ECDomainParameters DomainParams = new ECDomainParameters(EcParams.Curve, EcParams.G, EcParams.N, EcParams.H, EcParams.GetSeed());

        internal static ECPrivateKeyParameters GetEcPrivateKeyParametersFromString(string privateKeyString)
        {
            var privateKeyBytes = CryptoHelper.GetPrivateKeyBytesWithoutCheckSum(privateKeyString);
            var d = new BigInteger(1, privateKeyBytes);
            return new ECPrivateKeyParameters(d, DomainParams);
        }

    }

}