namespace BlockBase.BBLinq.Sets.Interfaces
{
    public interface IBlockBaseBaseSet<out TResult> where TResult : IBlockBaseBaseSet<TResult>
    {
        public TResult Encrypt();
    }
}
