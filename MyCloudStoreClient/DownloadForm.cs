using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Algorithms;
using System.Security.Cryptography;

namespace MyCloudStoreClient
{
    public partial class DownloadForm : Form
    {
        public int blockSize;
        public DownloadForm()
        {
            InitializeComponent();
            populateListbox();
            blockSize = Properties.Settings.Default.blockSize;
            lblFolder.Text = Properties.Settings.Default.downloadFolder.ToString();
        }

        private void DownloadForm_Load(object sender, EventArgs e)
        {

        }

        void populateListbox()
        {
            lbFiles.DataSource = null;
            lbFiles.Items.Clear();

            string hash = LoginInfo.computeHash();

            IList<string> l = ServiceReference.GetServis().filesNames(hash);

            lbFiles.DataSource = l;
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            string fileName = lbFiles.SelectedItem.ToString();
            byte[] fKey = null;
            long fSize = 0;
            DBController.readFileInfo(fileName, ref fKey, ref fSize);

            int n = (int)fSize / blockSize;
            if (fSize % blockSize != 0)
                n++;
            byte[] b = new byte[blockSize];
            byte[] tmp = new byte[blockSize];
            byte[] dec = null;

            ServiceReference.GetServis().downloadFile(fileName, LoginInfo.computeHash());
            if (fSize > blockSize)
            {
                for (int i = 0; i < n; i++)
                {
                    if (i == 0)
                    {
                        Buffer.BlockCopy(ServiceReference.GetServis().downloadFileChunk(LoginInfo.computeHash()), 0, tmp, 0, blockSize);
            
                    }
                    else
                    {
                        if (i == n - 1)
                        {
                            byte[] lastBlock = ServiceReference.GetServis().downloadFileChunk(LoginInfo.computeHash());
                            byte[] buff = new byte[lastBlock.Length];
                            Buffer.BlockCopy(lastBlock, 0, buff, 0,lastBlock.Length);
                            tmp = DBController.Combine(tmp, buff);
                        }
                        else
                        {
                            Buffer.BlockCopy(ServiceReference.GetServis().downloadFileChunk(LoginInfo.computeHash()), 0, b, 0, blockSize);
                            tmp = DBController.Combine(tmp, b);
                        }

                    }
                }

                dec = RC4.Decrypt(fKey, tmp);
                

            }
            else
            {
                byte[] nb = new byte[fSize];
                Buffer.BlockCopy(ServiceReference.GetServis().downloadFileChunk(LoginInfo.computeHash()), 0, nb, 0, (int)fSize);
                dec = RC4.Decrypt(fKey, nb);
                
            }


            if (FileManipulator._WriteFile(Properties.Settings.Default.downloadFolder + "\\" + fileName, dec))
                MessageBox.Show("File successfully downloaded!");
            else
            {
                MessageBox.Show("File download failed!");
                return;
            }

            if(cbRemove.Checked)
            {
                ServiceReference.GetServis().deleteFile(fileName, LoginInfo.computeHash());
                ServiceReference.GetServis().decUsedSpace(LoginInfo.UserID, (double)dec.Length / (double)1048576);
                DBController.deleteFileLocal(fileName);
                populateListbox();
            }
        

        }

        private void btnLocation_Click(object sender, EventArgs e)
        {
            using (var fldrDlg = new FolderBrowserDialog())
            {

                if (fldrDlg.ShowDialog() == DialogResult.OK)
                {
                    
                    Properties.Settings.Default.downloadFolder = fldrDlg.SelectedPath;
                    lblFolder.Text = Properties.Settings.Default.downloadFolder.ToString();
                    populateListbox();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            MenuForm mf = new MenuForm();
            mf.ShowDialog();
            this.Close();
        }
    }
}
