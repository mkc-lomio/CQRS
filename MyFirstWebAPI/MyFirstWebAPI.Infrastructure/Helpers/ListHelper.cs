using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstWebAPI.Infrastructure.Helpers
{
    public static class ListHelper
    {
        public static List<List<T>> ChunkBy<T>(this List<T> source, int chunkSize)
        {
            return source
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / chunkSize)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();
        }
    }

    #region Implementation
     /* 
     
       var policyReportByChunks = ListHelper.ChunkBy<PolicyQueryData>(policyData, 1000);
                foreach (var data in policyReportByChunks)
                {
                  ///
                }
     */
    #endregion
}
