namespace BlockBase.BBLinq.Interfaces
{
    public interface ITakes<out TResult>
    {
        public TResult Take(int amount);
    }
}
