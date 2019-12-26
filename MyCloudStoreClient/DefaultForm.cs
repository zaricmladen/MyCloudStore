using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyCloudStoreClient
{
    public partial class DefaultForm : Form
    {
        public int blockSize = 512000;
        public DefaultForm()
        {
            InitializeComponent();
        }

        private void DefaultForm_Load(object sender, EventArgs e)
        {
            populateCombo();
        }

        private void populateCombo()
        {
            comboBox1.Items.Add("RC4");
            comboBox1.Items.Add("A52");
            comboBox1.Items.Add("RSA");
            comboBox1.Items.Add("Tiger Hash");
            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void btnProceed_Click(object sender, EventArgs e)
        {
           
            this.Hide();
            if(comboBox1.SelectedIndex == 0)
            {
                RC4Form nf = new RC4Form();
                nf.ShowDialog();
            }
            this.Close();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            MenuForm mf = new MenuForm();
            mf.ShowDialog();
            this.Close();
        }
    }
}
