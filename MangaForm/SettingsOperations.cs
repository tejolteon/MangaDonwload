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
            Settings.Default.VolNumber = settings.VolNumber;
            Settings.Default.Save();
        }

        public void ChangeURL(SettingsEntity settings)
        {
            Settings.Default.MangaSite = settings.MangaSite;
            Settings.Default.HentaiSite = settings.HentaiSite;
            Settings.Default.Save();
        }

        public SettingsEntity Get()
        {
            SettingsEntity settings = new SettingsEntity
            {
                Chrome = Settings.Default.Chrome,
                CapInit = Settings.Default.CapInit,
                VolQuantity = Settings.Default.VolQuantity,
                DownloadLocal = Settings.Default.DownloadLocal,
                VolNumber = Settings.Default.VolNumber,
                MangaSite = Settings.Default.MangaSite,
                HentaiSite = Settings.Default.HentaiSite
            };

            return settings;
        }

        public void Reset()
        {
            Settings.Default.Reset();
        }
    }
}
