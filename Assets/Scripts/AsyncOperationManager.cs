using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class AsyncOperationManager<T> where T : class, ICloneable<T>, IUpdatable<T>
    {
        private readonly ConcurrentBag<(Func<T, UniTask<T>> Operation, T OriginalItem, UniTask CompletionTask)> _pendingOperations = new();
        private readonly ConcurrentDictionary<T, T> _stagedResults = new();

        public async UniTask AddOperationAsync(Func<T, UniTask<T>> operation, T item)
        {
            var clonedItem = item.Clone();
            var task = UniTask.RunOnThreadPool(async () =>
            {
                try
                {
                    var result = await operation(clonedItem);
                    _stagedResults[item] = result;
                }
                catch (Exception ex)
                {
                    Debug.Log($"Error in operation: {ex.Message}");
                }
            });
            _pendingOperations.Add((operation, item, task));
            await UniTask.Yield();
        }

        public async UniTask CommitChangesAsync()
        {
            var tasks = _pendingOperations.Select(op => op.CompletionTask).ToArray();
            await UniTask.WhenAll(tasks);
            foreach (var (original, staged) in _stagedResults)
            {
                original.UpdateFrom(staged);
            }
            _stagedResults.Clear();
            _pendingOperations.Clear();
        }
    }
}