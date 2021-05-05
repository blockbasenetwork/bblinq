namespace BlockBase.BBLinq.Sets.Interfaces
{
    public interface IBlockBaseSet<T> : IJoin<T>, IFetchableSet<T>, IInsertableSet<T>, IQueryableSet<T>, IBlockBaseBaseSet<IBlockBaseSet<T>>, ISet
    {
    }
}
