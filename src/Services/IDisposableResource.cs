public interface IDisposableResource : IDisposable
{
    bool IsDisposed { get; }
}