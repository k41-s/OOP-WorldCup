using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorldCupManager
{
    /* 
     * This form is to be displayed while data is being fetched, 
     * or any other processes that may take some time to complete
     */

    public partial class LoadingForm : Form
    {
        private Stream? _stream;
        public LoadingForm()
        {
            InitializeComponent();
        }

        private void LoadingForm_Load(object sender, EventArgs e)
        {
            try
            {
                _stream = new MemoryStream(Properties.Resources.Loading_Icon);
                pbLoading.Image = Image.FromStream(_stream);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadingForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            pbLoading.Image = null;
            _stream?.Dispose();
            _stream = null;
        }
    }
}
