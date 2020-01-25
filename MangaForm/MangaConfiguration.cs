using System;
using System.IO;
using System.Windows.Forms;

namespace MangaForm
{
    using Entities;
    using Controller;

    public partial class MangaConfiguration : Form
    {
        public MangaConfiguration()
        {
            InitializeComponent();
            LoadConfiguration();
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(pcbCap, "Inicia o download a partir do capítulo digitado");
            toolTip.SetToolTip(pcbChrome, "Inicia uma sessão do chrome em segundo plano e realiza o download (Necessário chrome instalado)");
            toolTip.SetToolTip(pcbLocal, "Local onde serão salvos os mangás baixados");
            toolTip.SetToolTip(pcbVol, "Compila vários capítulos em uma única pasta, digite o número de capítulos que deseja por volume e o número do volume inicial");
        }

        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            SettingsController controller = new SettingsController();
            SettingsEntity settings = new SettingsEntity();

            if (cbkChrome.Checked)
                settings.Chrome = true;

            if (cbkCap.Checked)
            {
                if (!string.IsNullOrEmpty(txtCap.Text))
                    settings.CapInit = int.Parse(txtCap.Text);
                else
                    MessageBox.Show("Por favor digite uma informação", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            if (cbkVol.Checked)
            {
                if (!string.IsNullOrEmpty(txtVol.Text))
                    settings.VolQuantity = int.Parse(txtVol.Text);
                else
                    MessageBox.Show("Por favor digite uma informação", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            if (cbkLocal.Checked)
                settings.DownloadLocal = txtPath.Text;

            settings.MangaSite = "http://unionmangas.site";
            settings.HentaiSite = "https://nhentai.net/g/";

            controller.Save(settings);

            MessageBox.Show("Configuração salva com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();

        }

        void LoadConfiguration()
        {
            SettingsController controller = new SettingsController();

            SettingsEntity settings = controller.Get();

            if ((!string.IsNullOrEmpty(settings.DownloadLocal)) && (settings.DownloadLocal != Path.Combine(Environment.CurrentDirectory, @"Data\")))
            {
                txtPath.Text = settings.DownloadLocal;
                cbkLocal.Checked = true;
            }
            else
            {
                txtPath.Text = Path.Combine(Environment.CurrentDirectory, @"Data\");
                cbkLocal.Checked = false;
            }

            if (settings.CapInit > 0)
            {
                txtCap.Text = settings.CapInit.ToString();
                cbkCap.Checked = true;
            }
            else
            {
                txtCap.Text = "";
                cbkCap.Checked = false;
            }

            if (settings.VolQuantity > 0)
            {
                txtVol.Text = settings.VolQuantity.ToString();
                txtNumeroVol.Text = settings.VolNumber.ToString();
                cbkVol.Checked = true;
            }
            else
            {
                txtVol.Text = "";
                txtNumeroVol.Text = "";
                cbkVol.Checked = false;
            }

            cbkChrome.Checked = settings.Chrome;
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Tem certeza que deseja voltar a configuração padrão?", "Aviso!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                SettingsController controller = new SettingsController();

                controller.Save(new SettingsEntity());
                LoadConfiguration();
            }
        }

        private void BtnVoltar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CbkCap_CheckedChanged(object sender, EventArgs e)
        {
            if (cbkCap.Checked)
                txtCap.Enabled = true;
            else
                txtCap.Enabled = false;
        }

        private void CbkVol_CheckedChanged(object sender, EventArgs e)
        {
            if (cbkVol.Checked)
            {
                txtVol.Enabled = true;
                txtNumeroVol.Enabled = true;
            }
            else
            {
                txtVol.Enabled = false;
                txtNumeroVol.Enabled = false;
                txtNumeroVol.Text = "1";
            }
        }

        private void CbkLocal_CheckedChanged(object sender, EventArgs e)
        {
            if (cbkLocal.Checked)
            {
                btnSearchFolder.Enabled = true;
            }
            else
            {
                btnSearchFolder.Enabled = false;
                txtPath.Text = Path.Combine(Environment.CurrentDirectory, @"Data\");
            }
        }

        private void TxtCap_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtVol_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void BtnSearchFolder_Click(object sender, EventArgs e)
        {
            if (fbdPath.ShowDialog() == DialogResult.OK)
            {
                txtPath.Text = fbdPath.SelectedPath;
            }
        }
    }
}
