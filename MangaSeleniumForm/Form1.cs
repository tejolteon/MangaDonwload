using System;
using System.Windows.Forms;

namespace MangaSeleniumForm
{
    using manga = MangaSelenium;
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static string mangaName;
        string unionMangas = "http://unionmangas.cc";

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            
            if (mangaName == string.Empty)
                MessageBox.Show("Por favor, digite o nome do Mangá desejado");
            else
            {
                mangaName = txtMangaName.Text;
                this.Opacity = 0;
                try
                {
                    manga.Program.Inicio(unionMangas, mangaName);
                    MessageBox.Show("Concluido", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch(Exception ex)
                {
                    MessageBox.Show("ERROR: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    this.Opacity = 100;
                }  
            }
        }
    }
}
