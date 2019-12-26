using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using Algorithms;


namespace MyCloudStoreClient
{
    public partial class loginForm : Form
    {

        TigerHash hash;

        public loginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            checkLogin();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void checkLogin()
        {

            string usn = tbUsername.Text;
            string psw = tbPassword.Text;
            byte[] bPassword = Encoding.Unicode.GetBytes(psw);

            hash = new TigerHash();
            psw = Convert.ToBase64String(hash.Crypt(bPassword));
            
          

            if (string.IsNullOrEmpty(usn) || string.IsNullOrEmpty(psw))
            {
                MessageBox.Show("Please fill both fields.");
                return;
            }


            bool f = ServiceReference.GetServis().authenthicateUser(usn,psw);

            if (f)
            {
                this.Hide();
                LoginInfo.UserID = usn;
                MenuForm df = new MenuForm();
                df.ShowDialog();
                this.Close();
                
            }
            else
                MessageBox.Show("Pogresno korisnicko ime i/ili sifra");

        
        }

        private void loginForm_Load(object sender, EventArgs e)
        {
            tbPassword.PasswordChar = '*';
        }
    }
}
