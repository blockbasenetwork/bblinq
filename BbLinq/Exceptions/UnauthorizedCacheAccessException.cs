using System;

namespace BlockBase.BBLinq.Exceptions
{
    class MisusedCacheAccessException : Exception
    {
        public static string GenerateMessage()
        {
            return $"An access to the cache was attempted without initializing the context. Check your context scope.";
        }

        public MisusedCacheAccessException() : base(GenerateMessage())
        {
        }
    }
}
