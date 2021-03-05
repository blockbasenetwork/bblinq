using System;

namespace BlockBase.BBLinq.Exceptions
{
    public class UnavailableCacheException : Exception
    {
        public UnavailableCacheException() : base(
            "The cache is unavailable. Make sure that your context as not been disposed.")
        { }
    }
}
