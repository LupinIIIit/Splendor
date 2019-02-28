using System;
using System.IO;

namespace Splendor.Utility.IO {
    public static class CheckOrCreateAppFolder {
        static readonly string userPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        public static bool CheckFolder() => Directory.Exists(HomeFolder());
        public static string HomeFolder() => Path.Combine(userPath, ".splendor");
        public static bool CreateHomeFolder() {
            var di = Directory.CreateDirectory(HomeFolder());
            return di.Exists;
        }
    }
}