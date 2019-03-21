namespace Crawl
{
    /// <summary>
    /// Interface to platform specific file systems to create datastore.
    /// </summary>
    public interface IFileHelper
    {
        /// <summary>
        /// Implementaions will be on each platform specific project.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        string GetLocalFilePath(string filename);
    }
}

