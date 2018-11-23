using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WincarExporter.Utilities.Validators;
namespace WincarExporter.Utilities.IO {
    public sealed class FileSearch {
        public static bool IsFileExists(FileInfo file) => Objects.IsValid(file) && (file.Exists && !file.IsReadOnly); 
        public static bool IsFileExists(string fileName) => Strings.IsValid(fileName) && IsFileExists(new FileInfo(fileName));
        public static bool IsDicectoryExists(DirectoryInfo di) => Objects.IsValid(di) && (di.Exists && !di.Attributes.HasFlag(FileAttributes.ReadOnly));
    }
}