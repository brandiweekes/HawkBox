using System.IO;
using Xamarin.Forms;
using Crawl.UWP;
using Windows.Storage;

[assembly: Dependency(typeof(FileHelper))]
namespace Crawl.UWP
{
public class FileHelper : IFileHelper
{
    public string GetLocalFilePath(string filename)
    {
        return Path.Combine(ApplicationData.Current.LocalFolder.Path, filename);
    }
}
}
