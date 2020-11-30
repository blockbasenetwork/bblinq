namespace BlockBase.BBLinq.Interfaces
{
    public interface IEncrypts<out TResult>
    {
        public TResult Encrypt();
    }
}
