using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BBLinq.Interfaces
{
    public interface IBBSet<TA>
    {
        public Task<IEnumerable<TA>> SelectAsync();
        public Task<IEnumerable<TB>> SelectAsync<TB>(Expression<Func<TA,TB>> mapper);
        public IBBSet<TA> Where(Expression<Func<TA, bool>> filter);
        public Task InsertAsync(TA item);
        public Task UpdateAsync(TA item);
        public Task DeleteAsync(TA item);
        public IBBJoinSet<TA, TB> Join<TB>(Expression<Func<TA, TB, bool>> on);

    }
}
