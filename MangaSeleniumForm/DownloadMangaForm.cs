using System;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;

namespace MangaSeleniumForm
{
    using Controller = MangaSeleniumController.Controller;

    public partial class DownloadMangaForm : Form
    {
        public DownloadMangaForm()
        {
            InitializeComponent();
        }

        delegate void SetTextCallback(ListViewItem item);
        delegate void SetBoolCallback(bool en);
        public static string mangaName;
        string unionMangas = "http://unionmangas.site";

        private void BtnConfirm_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtMangaName.Text))
                MessageBox.Show("Por favor, digite o nome do Mangá desejado");
            else
            {
                mangaName = txtMangaName.Text;

                try
                {
                    if (rdbNao.Checked)
                        new Task(() => { Controller.Download(unionMangas + "/manga/", mangaName, false); }).Start();
                    else if (rdbSim.Checked)
                        new Task(() => { Controller.Download(unionMangas, mangaName, true); }).Start();
                    else
                        throw new Exception("Selecione um opção");

                    new Task(WriteLog).Start();
                    btnConfirm.Enabled = false;
                }
                catch (Exception ex)
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
                var Logs = Controller.logs;

                if (Logs.Count > 0)
                {
                    foreach (var log in Logs)
                    {
                        if (!l.Contains(log))
                        {
                            id++;
                            string[] str = new string[] { id.ToString(), log };
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
            btnConfirm.Enabled = en;
        }
    }
}
