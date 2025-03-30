using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts
{
    public class AsyncOperationManager<T> where T : class, ICloneable<T>, IUpdatable<T>
    {
        private readonly List<(Func<T, Task<T>> Operation, T OriginalItem, Task CompletionTask)> _pendingOperations = new();
        private readonly Dictionary<T, T> _stagedResults = new();
        private readonly SemaphoreSlim _semaphore = new(1, 1);
        public event EventHandler<double> ProgressChanged;

        public async Task AddOperationAsync(Func<T, Task<T>> operation, T item)
        {
            var clonedItem = item.Clone();
            var task = Task.Run(async () =>
            {
                try
                {
                    var result = await operation(clonedItem);
                    await _semaphore.WaitAsync();
                    try
                    {
                        _stagedResults[item] = result;
                    }
                    finally
                    {
                        _semaphore.Release();
                        OnProgressChanged();
                    }
                }
                catch (Exception ex)
                {
                    Debug.Log($"Błąd w operacji: {ex.Message}");
                }
            });
            _pendingOperations.Add((operation, item, task));
            OnProgressChanged();
        }
        public async Task CommitChangesAsync()
        {
            var tasks = _pendingOperations.Select(op => op.CompletionTask).ToArray();
            await Task.WhenAll(tasks);

            await _semaphore.WaitAsync();
            try
            {
                foreach (var (original, staged) in _stagedResults)
                {
                    original.UpdateFrom(staged);
                }
                _stagedResults.Clear();
                _pendingOperations.Clear();
            }
            finally
            {
                _semaphore.Release();
                OnProgressChanged();
            }
        }

        public double GetProgress()
        {
            if (_pendingOperations.Count == 0) return 100.0;
            int totalOperations = _pendingOperations.Count;
            int completedOperations = _pendingOperations.Count(op => op.CompletionTask.IsCompleted);
            return (double)completedOperations / totalOperations * 100.0;
        }

        private void OnProgressChanged()
        {
            ProgressChanged?.Invoke(this, GetProgress());
        }
        public Task[] GetPendingTasks() => _pendingOperations.Select(op => op.CompletionTask).ToArray();

        public int PendingCount => _pendingOperations.Count;
    }
}
