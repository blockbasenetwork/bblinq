using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace agap2IT.Labs.BlockBase.BBLinq.Interfaces
{
    public interface IBbSet<TA>
    {
        public Task<IEnumerable<TA>> SelectAsync();
        public Task<IEnumerable<TB>> SelectAsync<TB>(Expression<Func<TA,TB>> mapper);
        public IBbSet<TA> Where(Expression<Func<TA, bool>> filter);
        public Task InsertAsync(TA item);
        public Task UpdateAsync(TA item);
        public Task DeleteAsync(Expression<Func<TA, bool>> condition);
        public IBbJoinSet<TA, TB> Join<TB>(Expression<Func<TA, TB, bool>> on);

    }
}
