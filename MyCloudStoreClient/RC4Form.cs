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
using System.IO;

namespace MyCloudStoreClient
{
    public partial class RC4Form : Form
    {
        int blockSize;
        public RC4Form()
        {
            InitializeComponent();
            blockSize = Properties.Settings.Default.blockSize;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
                lblFile.Text = ofd.FileName;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            byte[] niz = new byte[5];
            Random rnd = new Random();
            rnd.NextBytes(niz);
            tbEncryption.Text = Encoding.Default.GetString(niz);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            string ext = null;
            string fileName = null;
            byte[] encrypted = null;
            int no = 0;
            
            if(String.Compare(lblFile.Text,"not selected") == 0 || String.IsNullOrEmpty(tbEncryption.Text))
            {
                MessageBox.Show("Make sure you have selected the file and entered/generated key!");
                return;
            }

            FileInfo fi = new FileInfo(lblFile.Text);
            fileName = fi.Name;

            Encoding unicode = Encoding.Unicode;
            byte[] key = unicode.GetBytes(tbEncryption.Text);


            byte[] store = FileManipulator._ReadFile(lblFile.Text, ref ext, ref fileName);
            if(ServiceReference.GetServis().checkAvailableSpace(LoginInfo.UserID,store.Length) == null)
            {
                MessageBox.Show("You don't have enough available space on cloud for storing this file!");
                return;
            }

            encrypted = RC4.Encrypt(key, store);

            byte[][] chunks = FileManipulator.BufferSplit(encrypted, blockSize);

            for(int i=0; i<chunks.Length; i++)
            {
                ServiceReference.GetServis().getChunks(LoginInfo.computeHash(),chunks[i],no);
                no++;
            }


            if (ServiceReference.GetServis().storeFile(fileName, LoginInfo.computeHash()))
                MessageBox.Show("File successfully uploaded!");
            else
            {
                MessageBox.Show("File upload failed!");
                return;
            }

            DBController.storeFileInfo(fileName, key, store.Length);

            ServiceReference.GetServis().incUsedSpace(LoginInfo.UserID, (double)encrypted.Length / (double)1048576);

            updateSpaceInfo(0);
            clearControls();



        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            DefaultForm df = new DefaultForm();
            df.ShowDialog();
            this.Close();
        }

        private void RC4Form_Load(object sender, EventArgs e)
        {
            updateSpaceInfo(0);
        }

        private void updateSpaceInfo(double size)
        {
            double[] values = ServiceReference.GetServis().checkAvailableSpace(LoginInfo.UserID, size);
            values[0] = Math.Round(values[0], 2, MidpointRounding.AwayFromZero);
            lblSpace.Text = values[0].ToString() + " / " + values[1].ToString() + " MB";
        }

        private void clearControls()
        {
            tbEncryption.Text = "";
            lblFile.Text = "not selected";
        }
    }
}
