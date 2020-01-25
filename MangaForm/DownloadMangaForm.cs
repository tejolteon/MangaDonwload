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

    public partial class DownloadMangaForm : Form
    {
        public DownloadMangaForm()
        {
            InitializeComponent();
        }

        delegate void SetTextCallback(ListViewItem item);
        delegate void SetBoolCallback(bool en);
        private readonly Keys[] sequence = new Keys[] { Keys.Up, Keys.Up, Keys.Down, Keys.Down, Keys.Left, Keys.Right, Keys.Left, Keys.Right, Keys.B, Keys.A };
        private int sequenceIndex;

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
                if (string.IsNullOrEmpty(txtMangaName.Text))
                    MessageBox.Show("Por favor, digite o nome do Mangá desejado");
                else
                {
                    SettingsController settingsController = new SettingsController();

                    SettingsEntity settings = settingsController.Get();
                    string mangaName = txtMangaName.Text;

                    try
                    {
                        if (!settings.Chrome)
                            new Task(() => { MangaDownloadController.Download(settings.MangaSite + "/manga/", mangaName, false, settings.CapInit, settings.VolQuantity, settings.DownloadLocal, settings.VolNumber); }).Start();
                        else
                            new Task(() => { MangaDownloadController.Download(settings.MangaSite, mangaName, true, settings.CapInit, settings.VolQuantity, settings.DownloadLocal, settings.VolNumber); }).Start();

                        new Task(WriteLog).Start();
                        btnLimpar.Enabled = false;
                        btnConfirm.Enabled = false;
                        txtMangaName.Enabled = false;
                        btnConfig.Enabled = false;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("ERROR: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        btnLimpar.Enabled = true;
                        btnConfirm.Enabled = true;
                        txtMangaName.Enabled = true;
                        btnConfig.Enabled = true;
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
            MessageBox.Show("Mangá baixado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnLimpar.Enabled = en;
            btnConfirm.Enabled = en;
            txtMangaName.Enabled = en;
            btnConfig.Enabled = en;
        }

        private void BtnLimpar_Click(object sender, EventArgs e)
        {
            lsvLog.Items.Clear();
        }

        private void BtnConfig_Click(object sender, EventArgs e)
        {
            MangaConfiguration config = new MangaConfiguration();

            config.ShowDialog();
        }

        private void DownloadMangaForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == sequence[sequenceIndex])
            {
                if(++sequenceIndex == sequence.Length)
                {
                    sequenceIndex = 0;
                    // restricted mode
                    MessageBox.Show("Hehe boi");
                    DownloadHForm h = new DownloadHForm();
                    Hide();
                    h.ShowDialog();
                    Show();
                    txtMangaName.Clear();
                }
            }
            else
                sequenceIndex = 0;
        }
    }
}
