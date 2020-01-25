using System;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;

namespace MangaForm
{
    using MangaDownloadController = MangaController.Controller;
    using Controller;
    using Entities;

    public partial class DownloadHForm : Form
    {
        public DownloadHForm()
        {
            InitializeComponent();
        }

        delegate void SetTextCallback(ListViewItem item);
        delegate void SetBoolCallback(bool en);

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
                if (string.IsNullOrEmpty(txtHName.Text))
                    MessageBox.Show("Por favor, digite o nome do Mangá desejado");
                else
                {
                    SettingsController settingsController = new SettingsController();

                    SettingsEntity settings = settingsController.Get();
                    string HName = txtHName.Text;

                    try
                    {
                    settings.Chrome = false;
                        if (!settings.Chrome)
                            new Task(() => { MangaDownloadController.DownloadH(settings.HentaiSite, HName, false, settings.DownloadLocal); }).Start();
                        else
                            new Task(() => { MangaDownloadController.DownloadH(settings.HentaiSite, HName, true, settings.DownloadLocal); }).Start();

                        new Task(WriteLog).Start();
                        btnLimpar.Enabled = false;
                        btnConfirm.Enabled = false;
                        txtHName.Enabled = false;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("ERROR: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        btnLimpar.Enabled = true;
                        btnConfirm.Enabled = true;
                        txtHName.Enabled = true;
                    }
                }
        }

        void WriteLog()
        {
            int id = 0;
            List<string> l = new List<string>();

            while (true)
            {
                var Logs = new List<string>(MangaDownloadController.logs);

                if (Logs.Count > 0)
                {
                    foreach (var log in Logs)
                    {
                        if (!l.Contains(log))
                        {
                            id++;
                            string[] str = new string[] { id.ToString(), log, DateTime.Now.ToString() };
                            ListViewItem item = new ListViewItem(str);
                            ListAdd(item);
                            l.Add(log);
                        }
                    }

                    if (l.Contains("Fim da Execução"))
                        break;

                    Thread.Sleep(2000);
                }
            }

            MangaDownloadController.logs.Clear();
            ButtonEnable(true);
        }

        private void ListAdd(ListViewItem item)
        {
            if (lsvLog.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(ItemToAdd);
                Invoke(d, new object[] { item });
            }
            else
            {
                lsvLog.Items.Insert(0, item);
            }
        }

        private void ItemToAdd(ListViewItem item)
        {
            lsvLog.Items.Insert(0, item);
        }

        private void ButtonEnable(bool en)
        {
            if (lsvLog.InvokeRequired)
            {
                SetBoolCallback e = new SetBoolCallback(ButtonStatus);
                Invoke(e, new object[] { en });
            }
            else
            {
                btnConfirm.Enabled = en;
            }
        }

        private void ButtonStatus(bool en)
        {
            MessageBox.Show("H baixado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnLimpar.Enabled = en;
            btnConfirm.Enabled = en;
            txtHName.Enabled = en;
        }

        private void BtnLimpar_Click(object sender, EventArgs e)
        {
            lsvLog.Items.Clear();
        }
    }
}
