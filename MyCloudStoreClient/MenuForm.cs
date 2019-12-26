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
    public partial class MenuForm : Form
    {
        public MenuForm()
        {
            InitializeComponent();
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            this.Hide();
            DefaultForm nf = new DefaultForm();
            nf.ShowDialog();
            this.Close();
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            this.Hide();
            DownloadForm df = new DownloadForm();
            df.ShowDialog();
            this.Close();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginInfo.UserID = null;
            loginForm lf = new loginForm();
            lf.ShowDialog();
            this.Close();
        }
    }
}
