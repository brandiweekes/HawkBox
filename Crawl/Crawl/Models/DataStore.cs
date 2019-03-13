
namespace Crawl.Models
{
    /// What data store will be used.  
    public enum DataStoreEnum
    {
        /// <summary>
        /// Unknown is default.
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// SQL datastore.
        /// </summary>
        Sql = 1,
        /// <summary>
        /// Mock datastore
        /// </summary>
        Mock = 2
    }
}
