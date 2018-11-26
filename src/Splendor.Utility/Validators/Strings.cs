namespace Splendor.Utility.Validators {
    public sealed class Strings {
        public static bool IsValid(string str) => !string.IsNullOrWhiteSpace(str);
        public static bool IsEmpty(string str) => !IsValid(str);
    }
}