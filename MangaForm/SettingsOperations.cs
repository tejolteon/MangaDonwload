using MangaForm.Entities;
using MangaForm.Properties;

namespace MangaForm.Operations
{
    class SettingsOperations
    {
        public void Save(SettingsEntity settings)
        {
            Settings.Default.Chrome = settings.Chrome;
            Settings.Default.CapInit = settings.CapInit;
            Settings.Default.VolQuantity = settings.VolQuantity;
            Settings.Default.DownloadLocal = settings.DownloadLocal;
            Settings.Default.Save();
        }

        public SettingsEntity Get()
        {
            SettingsEntity settings = new SettingsEntity
            {
                Chrome = Settings.Default.Chrome,
                CapInit = Settings.Default.CapInit,
                VolQuantity = Settings.Default.VolQuantity,
                DownloadLocal = Settings.Default.DownloadLocal
            };

            return settings;
        }
    }
}
