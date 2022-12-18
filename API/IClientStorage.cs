namespace BrowserStorage.API
{
    /// <summary>
    /// Defines the <see cref="IClientStorage" />.
    /// </summary>
    public interface IClientStorage
    {
        /// <summary>
        /// Reads the asynchronous.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        ValueTask<string> ReadAsync(string key);

        /// <summary>
        /// Reads the asynchronous.
        /// </summary>
        /// <param name="readKeys">The read keys.</param>
        /// <returns></returns>
        ValueTask<Dictionary<string, object>> ReadAsync(IDictionary<string, object> readKeys);

        /// <summary>
        /// Writes the asynchronous.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="isRemoveAndAdd">if set to <c>true</c> [is remove and add].</param>
        /// <returns></returns>
        ValueTask WriteAsync(string key, object value, bool isRemoveAndAdd = default);

        /// <summary>
        /// Writes the asynchronous.
        /// </summary>
        /// <param name="setKeys">The set keys.</param>
        /// <param name="removeKeys">The remove keys.</param>
        /// <returns></returns>
        ValueTask WriteAsync(IDictionary<string, object> setKeys, List<string> removeKeys = default);

        /// <summary>
        /// Removes the asynchronous.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        ValueTask RemoveAsync(string key);

        /// <summary>
        /// Removes the asynchronous.
        /// </summary>
        /// <param name="keys">The keys.</param>
        /// <returns></returns>
        ValueTask RemoveAsync(IEnumerable<string> keys);

        /// <summary>
        /// Removes the asynchronous.
        /// </summary>
        /// <param name="keys">The keys.</param>
        /// <returns></returns>
        ValueTask RemoveAsync(string[] keys);

        /// <summary>
        /// Removes all.
        /// </summary>
        /// <returns></returns>
        ValueTask RemoveAll();
    }
}