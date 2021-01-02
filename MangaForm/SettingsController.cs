using MangaForm.Entities;
using MangaForm.Operations;
using System;
using System.IO;

namespace MangaForm.Controller
{
    class SettingsController
    {
        private SettingsOperations operations;

        public void Save(SettingsEntity settings)
        {
            ValidateSettings(settings);

            operations = new SettingsOperations();
            operations.Save(settings);
        }

        public SettingsEntity Get()
        {
            operations = new SettingsOperations();
            SettingsEntity settings = operations.Get();

            ValidateSettings(settings);

            return settings;
        }

        public void Reset()
        {
            operations = new SettingsOperations();
            operations.Reset();
        }

        public void ChangeURL(SettingsEntity settings)
        {
            operations = new SettingsOperations();
            operations.ChangeURL(settings);
        }

        void ValidateSettings(SettingsEntity settings)
        {
            if (string.IsNullOrEmpty(settings.DownloadLocal))
                settings.DownloadLocal = Path.Combine(Environment.CurrentDirectory, @"Data\");
        }
    }
}
