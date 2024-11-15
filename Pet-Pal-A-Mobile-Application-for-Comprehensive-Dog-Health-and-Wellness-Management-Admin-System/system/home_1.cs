using System;
using System.Windows.Forms;

namespace system
{
    public partial class home : Form
    {
        public home()
        {
            InitializeComponent();

            //  URL of the WebBrowser 
            webBrowser1.Navigate("https://rainbowpages.lk/office/web-designing-developing/ceylon-innovations-labs-pvt-ltd/"); // Replace with your Facebook page URL
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
           
            
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            login Login = new login();
            Login.Show();
        }
    }
}
