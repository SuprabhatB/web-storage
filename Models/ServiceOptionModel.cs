using WebStorage.Enums;

namespace WebStorage.Models
{
    /// <summary>
    /// Defines the <see cref="ServiceOptionModel" />.
    /// </summary>
    public class ServiceOptionModel
    {
        /// <summary>
        /// Gets or sets the type of the storage.
        /// </summary>
        /// <value>
        /// The type of the storage.
        /// </value>
        public WebStorageType StorageType { get; set; }
    }
}