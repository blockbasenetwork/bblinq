namespace BlockBase.BBLinq.Interfaces
{
    public interface ISkips<out TResult>
    {
        public TResult Skip(int amount);
    }
}
