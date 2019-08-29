using Splendor.Utility.Utils;
using System.IO;
namespace Splendor.Utility.IO {
    public static class FileSearch {
        public static bool IsFileExists ( FileInfo file ) => Objects.IsValid (file) && (file.Exists && !file.IsReadOnly);
        public static bool IsFileExists ( string fileName ) => Strings.IsValid (fileName) && IsFileExists (new FileInfo (fileName));
        public static bool IsDirectoryExists ( DirectoryInfo di ) => Objects.IsValid (di) && (di.Exists && !di.Attributes.HasFlag (FileAttributes.ReadOnly));
        public static bool IsDirectoryExists ( string dir ) => Strings.IsValid (dir) && IsDirectoryExists (new DirectoryInfo (dir));
    }
}