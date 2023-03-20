using System.IO;
using SalesTrack.Droid;
using SalesTrack;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]
namespace SalesTrack.Droid
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }
    }
}