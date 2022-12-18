using WebStorage.Enums;
using WebStorage.Models;

using Microsoft.Extensions.Options;
using Microsoft.JSInterop;

namespace WebStorage
{
    /// <summary>
    /// Defines the <see cref="ClientStorage" />.
    /// </summary>
    /// <seealso cref="WebStorage.IClientStorage" />
    /// <seealso cref="System.IAsyncDisposable" />
    public class ClientStorage : IClientStorage, IAsyncDisposable
    {
        private readonly Lazy<Task<IJSObjectReference>> _moduleTask;
        private int _storageType;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientStorage"/> class.
        /// </summary>
        /// <param name="jsRuntime">The js runtime.</param>
        public ClientStorage(IJSRuntime jsRuntime, IOptions<ServiceOptionModel> options)
        {
            _storageType = options != null ? (int)options.Value.StorageType : (int)WebStorageType.LocalStorage;
            _moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>("import", $"./_content/web.storage.core/js/client.js").AsTask());
        }

        /// <summary>
        /// Asynchronously get(s) the storage value by key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public async ValueTask<string> ReadAsync(string key)
        {
            if (string.IsNullOrWhiteSpace(key)) return default;

            try
            {
                var module = await _moduleTask.Value;
                return await module.InvokeAsync<string>("getFromStorageByKey", key, _storageType);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                throw ex;
            }
        }

        /// <summary>
        /// Asynchronously get(s) the storage values by keys.
        /// </summary>
        /// <param name="readKeys">The read keys.</param>
        /// <returns></returns>
        public async ValueTask<Dictionary<string, object>> ReadAsync(IDictionary<string, object> readKeys)
        {
            if (readKeys == default || readKeys.Count == 0) return default;

            try
            {
                var module = await _moduleTask.Value;
                return await module.InvokeAsync<Dictionary<string, object>>("getFromStorageByKeys", readKeys, _storageType);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                throw ex;
            }
        }

        /// <summary>
        /// Asynchronously set(s) the storage value by key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="isRemoveAndAdd">if set to <c>true</c> [is remove and add].</param>
        /// <returns></returns>
        public async ValueTask WriteAsync(string key, object value, bool isRemoveAndAdd = default)
        {
            if (string.IsNullOrWhiteSpace(key)) return;

            try
            {
                var module = await _moduleTask.Value;
                await module.InvokeVoidAsync("setToStorageByKey", key, value, isRemoveAndAdd, _storageType);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                throw ex;
            }
        }

        /// <summary>
        /// Asynchronously set(s) the storage values by keys.
        /// </summary>
        /// <param name="setKeys">The set keys.</param>
        /// <param name="removeKeys">The remove keys.</param>
        /// <returns></returns>
        public async ValueTask WriteAsync(IDictionary<string, object> setKeys, List<string> removeKeys = default)
        {
            if (setKeys == default || setKeys.Count == 0) return;

            try
            {
                var module = await _moduleTask.Value;
                await module.InvokeVoidAsync("setToStorageByKeys", setKeys, removeKeys, _storageType);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                throw ex;
            }
        }

        /// <summary>
        /// Asynchronously remove(s) the storage value by key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public async ValueTask RemoveAsync(string key)
        {
            try
            {
                var module = await _moduleTask.Value;
                await module.InvokeVoidAsync("removeFromStorageByKey", key, _storageType);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                throw ex;
            }
        }

        /// <summary>
        /// Asynchronously remove(s) the storage value by keys.
        /// </summary>
        /// <param name="keys">The keys.</param>
        /// <returns></returns>
        public async ValueTask RemoveAsync(IEnumerable<string> keys)
        {
            if (keys == default) return;

            try
            {
                var module = await _moduleTask.Value;
                await module.InvokeVoidAsync("removeFromStorageByKeys", keys, _storageType);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                throw ex;
            }
        }

        /// <summary>
        /// Asynchronously remove(s) the storage value by keys.
        /// </summary>
        /// <param name="keys">The keys.</param>
        /// <returns></returns>
        public async ValueTask RemoveAsync(string[] keys) =>
            await RemoveAsync(keys.ToList());

        /// <summary>
        /// Asynchronously removes all storage keys and its corresponding values.
        /// </summary>
        /// <returns></returns>
        public async ValueTask RemoveAll()
        {
            try
            {
                var module = await _moduleTask.Value;
                await module.InvokeVoidAsync("removeAllStorageKeys");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                throw ex;
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources asynchronously.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous dispose operation.
        /// </returns>
        public async ValueTask DisposeAsync()
        {
            if (_moduleTask.IsValueCreated)
            {
                var module = await _moduleTask.Value;
                await module.DisposeAsync();
            }
        }
    }
}