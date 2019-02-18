using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game_of_Life
{
    public partial class Seed_Dialog : Form
    {
        public Seed_Dialog()
        {
            InitializeComponent();
        }

        public int SeedValue
        {
            get
            {
                return (int)seedUpDown.Value;
            }
            set
            {
                seedUpDown.Value = value;
            }
        }

        private void randomSeedButton_Click(object sender, EventArgs e)
        {
            Random randomNumber = new Random();
        }
    }
}
