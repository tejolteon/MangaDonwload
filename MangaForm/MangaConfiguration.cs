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
            }

            if (cbkVol.Checked)
            {
                if (!string.IsNullOrEmpty(txtVol.Text))
                    settings.VolQuantity = int.Parse(txtVol.Text);
            }

            if (cbkLocal.Checked)
                settings.DownloadLocal = txtPath.Text;

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
                cbkVol.Checked = true;
            }
            else
            {
                txtVol.Text = "";
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
                txtVol.Enabled = true;
            else
                txtVol.Enabled = false;
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
