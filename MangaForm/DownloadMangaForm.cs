using System;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;

namespace MangaForm
{
    using Controller = MangaController.Controller;

    public partial class DownloadMangaForm : Form
    {
        public DownloadMangaForm()
        {
            InitializeComponent();
        }

        delegate void SetTextCallback(ListViewItem item);
        delegate void SetBoolCallback(bool en);
        public static string mangaName;
        readonly string unionMangas = "http://unionmangas.site";

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            int cap = 1;

            if (string.IsNullOrEmpty(txtMangaName.Text))
                MessageBox.Show("Por favor, digite o nome do Mangá desejado");
            else
            {
                mangaName = txtMangaName.Text;

                if (cbkCap.Checked)
                    cap = int.Parse(txtCap.Text);

                try
                {
                    if (rdbNao.Checked)
                        new Task(() => { Controller.Download(unionMangas + "/manga/", mangaName, false, cap); }).Start();
                    else if (rdbSim.Checked)
                        new Task(() => { Controller.Download(unionMangas, mangaName, true, cap); }).Start();
                    else
                        throw new Exception("Selecione um opção");

                    new Task(WriteLog).Start();
                    rdbNao.Enabled = false;
                    rdbSim.Enabled = false;
                    cbkCap.Enabled = false;
                    btnLimpar.Enabled = false;
                    btnConfirm.Enabled = false;
                    txtCap.Enabled = false;
                    txtMangaName.Enabled = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnConfirm.Enabled = true;
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

            Controller.logs.Clear();
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
            rdbNao.Enabled = en;
            rdbSim.Enabled = en;
            cbkCap.Enabled = en;
            btnLimpar.Enabled = en;
            btnConfirm.Enabled = en;
            txtMangaName.Enabled = en;

            if (cbkCap.Checked)
                txtCap.Enabled = true;
            else
                txtCap.Enabled = false;
        }

        private void BtnLimpar_Click(object sender, EventArgs e)
        {
            lsvLog.Items.Clear();
        }

        private void CbkCap_CheckedChanged(object sender, EventArgs e)
        {
            if (cbkCap.Checked)
                txtCap.Enabled = true;
            else
                txtCap.Enabled = false;
        }

        private void TxtCap_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
