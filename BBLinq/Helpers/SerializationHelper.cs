using System;
using System.Collections.Generic;
using System.Linq;

namespace BlockBase.BBLinq.Helpers
{
    public class SerializationHelper
    {
      
        /// <summary>
        /// Combines multiple arrays into one
        /// </summary>
        /// <param name="arrays"></param>
        /// <returns></returns>
        public static byte[] Combine(IEnumerable<byte[]> arrays)
        {
            var arrayList = arrays as List<byte[]> ?? arrays.ToList();
            var ret = new byte[arrayList.Sum(x => x?.Length ?? 0)];
            var offset = 0;
            foreach (var data in arrayList.Where(data => data != null))
            {
                Buffer.BlockCopy(data, 0, ret, offset, data.Length);
                offset += data.Length;
            }
            return ret;
        }
    }
}
