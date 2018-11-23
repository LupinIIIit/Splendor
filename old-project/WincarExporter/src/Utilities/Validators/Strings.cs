using System;
using System.Collections.Generic;
using System.Text;

namespace WincarExporter.Utilities.Validators {
    public sealed class Strings {
        public static bool IsValid(string str) => !string.IsNullOrWhiteSpace(str);
        public static bool IsEmpty(string str) => !IsValid(str);
    }
}