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
    public partial class Options_Dialog : Form
    {
        public Options_Dialog()
        {
            InitializeComponent();
        }

        //public int GetInterval()
        //{
        //    return (int)intervalUpDown.Value;
        //}

        //public void SetInterval(int x)
        //{
        //    intervalUpDown.Value = x;
        //}

        public int ValueX
        {
            get
            {
                return (int)intervalUpDown.Value;
            }
            set
            {
                intervalUpDown.Value = value; //value what user is trying to set value to
            }
        }

        public int ValueWidth
        {
            get
            {
                return (int)widthUpDown.Value;
            }
            set
            {
                widthUpDown.Value = value;
            }
        }

        public int ValueHeigth
        {
            get
            {
                return (int)heightUpDown.Value;
            }
            set
            {
                heightUpDown.Value = value;
            }
        }
    }
}
