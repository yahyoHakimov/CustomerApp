
using System.Data;
using System.Reflection.Metadata;

public class GenericRepository<T> : IRepository<T>, IDisposableResource where T : BaseClient
{
    private readonly List<T> _data;
    private bool _disposed = false;
    public bool IsDisposed => _disposed;

    public async Task AddAsync(T entity)
    {
        CheckDispose();
        entity.Id = _data.Count + 1;
        entity.CreatedAt = DateTime.Now;
        _data.Add(entity);
        await Task.CompletedTask;

    }

    public async Task DeleteAsync(int id)
    {
        CheckDispose();
        var existing = _data.FirstOrDefault(x => x.Id == id);
        if (existing != null)
        {
            _data.Remove(existing);
        }
        await Task.CompletedTask;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        CheckDispose();
        return await Task.FromResult(_data.AsEnumerable());
    }

    public async Task<T> GetByIdAsync(int id)
    {
        CheckDispose();
        return await Task.FromResult(_data.FirstOrDefault(x => x.Id == id));
    }

    public async Task UpdateAsync(T entity)
    {
        CheckDispose();
        var existing = _data.FirstOrDefault(x => x.Id == entity.Id);
        if (existing != null)
        {
            var index = _data.IndexOf(existing);
            _data[index] = entity;
        }

        await Task.CompletedTask;
    }

    public void CheckDispose()
    {
        if (IsDisposed)
            throw new ObjectDisposedException(nameof(GenericRepository<T>));
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            _data.Clear();
            _disposed = true;
        }
    }
}