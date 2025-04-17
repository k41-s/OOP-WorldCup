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
        public LoadingForm()
        {
            InitializeComponent();
        }

        private void LoadingForm_Load(object sender, EventArgs e)
        {
            try
            {
                using Stream stream = new MemoryStream(Properties.Resources.Loading_Icon);
                // Detatch image form the stream
                Image tempImg = Image.FromStream(stream);
                pbLoading.Image = new Bitmap(tempImg);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
