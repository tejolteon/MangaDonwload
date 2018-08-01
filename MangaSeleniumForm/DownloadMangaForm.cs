using System;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MangaSeleniumForm
{
    using Manga = MangaSelenium.DownloadManga;
    using Controler = MangaSeleniumController.Controller;

    public partial class DownloadMangaForm : Form
    {
        public DownloadMangaForm()
        {
            InitializeComponent();
        }

        public static string mangaName;
        string unionMangas = "http://unionmangas.cc";

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            
            if (string.IsNullOrEmpty(txtMangaName.Text))
                MessageBox.Show("Por favor, digite o nome do Mangá desejado");
            else
            {
                mangaName = txtMangaName.Text;
                //Opacity = 0;
                try
                {
                    //Manga.Inicio(unionMangas, mangaName);
                    //new Task(WriteLog).Start();
                    Controler.Download(unionMangas, mangaName);
                    
                    MessageBox.Show("Concluido", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch(Exception ex)
                {
                    MessageBox.Show("ERROR: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            
        }

        void WriteLog()
        {
            int id = 0;
            List<string> l = new List<string>();
            while (true)
            {
                var Logs = Controler.ReadLogs();

                foreach (var log in Logs)
                {
                    if (!l.Contains(log))
                    {
                        id++;
                        string[] str = new string[] { id.ToString(), log };
                        ListViewItem item = new ListViewItem(str);
                        lsvLog.Items.Add(item);
                        l.Add(log);
                        break;
                    }
                }
                if (l.Count == Logs.Count)
                    break;
            }
        }
    }
}
