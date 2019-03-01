namespace Splendor.Utility.Utils {
    public static class Strings {
        #region Validators
        public static bool IsValid ( string str ) => !string.IsNullOrWhiteSpace (str);
        public static bool IsEmpty ( string str ) => !IsValid (str);
        #endregion
        #region Utilities
        public static string CenterString ( string stringToCenter, int totalLength ) => stringToCenter.PadLeft (((totalLength - stringToCenter.Length) / 2) + stringToCenter.Length).PadRight (totalLength);
        #endregion
    }
}