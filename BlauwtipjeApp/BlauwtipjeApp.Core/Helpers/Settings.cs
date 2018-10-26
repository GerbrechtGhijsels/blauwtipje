using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace BlauwtipjeApp.Core.Helpers
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// </summary>
    public static class Settings
    {
        private static ISettings AppSettings => CrossSettings.Current;

        #region Setting Constants
        private const string DebugModeKey = "Debug_Mode";
        private static readonly bool DebugModeDefault = false;

        private const string IsDebugModeToggledKey = "Is_Debug_Mode_Toggled";
        private static readonly bool IsDebugModeToggledDefault = false;

        private static readonly string ReleaseEndpoint = "https://blauwtipje.blob.core.windows.net/app-resources";
        private static readonly string DebugEndpoint = "https://blauwtipje.blob.core.windows.net/app-resources/develop";

        private const string LocalDatabaseNameKey = "Local_Db_Name";
        private static readonly string LocalDatabaseNameDefault = "ResourceDatabase.db";

        private const string XmlFileNameKey = "Xml_File_Name";
        private static readonly string XmlFileNameDefault = "Determination.xml";

        private const string ChangelogFileNameKey = "Changelog_File_Name";
        private static readonly string ChangelogFileNameDefault = "Changelog.txt";

        private const string UpdateWasNotCompletedKey = "Update_Was_Not_Completed";
        private static readonly bool UpdateWasNotCompletedDefault = false;
        #endregion

        public static bool DebugMode
        {
            get => AppSettings.GetValueOrDefault(DebugModeKey, DebugModeDefault);
            set => AppSettings.AddOrUpdateValue(DebugModeKey, value);
        }

        public static bool IsDebugModeToggled
        {
            get => AppSettings.GetValueOrDefault(IsDebugModeToggledKey, IsDebugModeToggledDefault);
            set => AppSettings.AddOrUpdateValue(IsDebugModeToggledKey, value);
        }

        public static string EndpointUrl
        {
            get { return DebugMode ? DebugEndpoint : ReleaseEndpoint; }
        }

        public static string LocalDatabaseName
        {
            get => AppSettings.GetValueOrDefault(LocalDatabaseNameKey, LocalDatabaseNameDefault);
            set => AppSettings.AddOrUpdateValue(LocalDatabaseNameKey, value);
        }

        public static string XmlFileName
        {
            get => AppSettings.GetValueOrDefault(XmlFileNameKey, XmlFileNameDefault);
            set => AppSettings.AddOrUpdateValue(XmlFileNameKey, value);
        }

        public static string ChangelogFileName
        {
            get => AppSettings.GetValueOrDefault(ChangelogFileNameKey, ChangelogFileNameDefault);
            set => AppSettings.AddOrUpdateValue(ChangelogFileNameKey, value);
        }

        public static bool UpdateWasNotCompleted
        {
            get => AppSettings.GetValueOrDefault(UpdateWasNotCompletedKey, UpdateWasNotCompletedDefault);
            set => AppSettings.AddOrUpdateValue(UpdateWasNotCompletedKey, value);
        }

        public static void ResetSettings()
        {
            AppSettings.Clear();
        }
    }
}
