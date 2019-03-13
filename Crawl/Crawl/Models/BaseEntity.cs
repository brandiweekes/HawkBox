using SQLite;

namespace Crawl.Models
{
    /// <summary>
    /// This is the base class, that everything comes from that gets saved in the DB.
    /// The fields required by the DB for all records are here.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseEntity<T>
    {
        // Database record key for the entry into the database, used to fetch the data
        [PrimaryKey]
        public string Id { get; set; }

        // Used for List Identification, not used for game interaction or exposed to users
        public string Guid { get; set; }


        /// <summary>
        ///  Set the guid and ID because the DB uses it...
        /// </summary>
        public BaseEntity()
        {
            Guid = System.Guid.NewGuid().ToString();
            Id = Guid;
        }
    }
}
