using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StringSearchAlgorithms
{
    public partial class SearchDialog : Form
    {
        public string Algorithm
        {
            get { return cmbAlgorithm.Text; }
        }

        public string Filepath
        {
            get { return lblFilepath.Text; }
        }

        public string TextToFind
        {
            get { return txtTextToFind.Text; }
        }

        public SearchDialog()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(Algorithm) || string.IsNullOrEmpty(Filepath) || string.IsNullOrEmpty(TextToFind))
            {
                MessageBox.Show("Please fill in all of the required fields", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                lblFilepath.Text = openFileDialog.FileName;
            }
        }
    }
}
