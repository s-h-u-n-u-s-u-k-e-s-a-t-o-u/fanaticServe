using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.fanaticServe.Extentions
{
    // LINQ-to-Objects 上で EF の非同期メソッド呼び出しを互換的に使えるようにする簡易拡張。
    public static class LinqAsyncExtensions
    {
        public static Task<List<TSource>> ToListAsync<TSource>(this IEnumerable<TSource> source)
        {
            return Task.FromResult(source.ToList());
        }

        public static Task<TSource?> FirstOrDefaultAsync<TSource>(this IEnumerable<TSource> source)
        {
            return Task.FromResult(source.FirstOrDefault());
        }

        public static Task<TSource[]> ToArrayAsync<TSource>(this IEnumerable<TSource> source)
        {
            return Task.FromResult(source.ToArray());
        }
    }
}