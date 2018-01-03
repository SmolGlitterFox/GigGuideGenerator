using Gig_Guide_Generator.SupportingClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gig_Guide_Generator
{
    public partial class frmMainForm : Form
    {
        public frmMainForm()
        {
            InitializeComponent();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            var gigWeek = new GigWeek();
            gigWeek.GenerateGigs(
                dtStartDate.Value,
                dtEndDate.Value,
                lbVenues.Items.Cast<string>().ToList(),
                rtbOutput);
        }
    }
}
