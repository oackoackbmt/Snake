using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class SettingsMenu : Form
    {
        public SettingsMenu(bool hastighetsByte,bool walls)
        {
            InitializeComponent();
            checkBox1.Checked = hastighetsByte;
            checkBox2.Checked = walls;
            checkBox1.Text = "Tryck i denna boxen ifall du vill kunna \n byta hastighet mitt i spelet med hjälp \n utav + och - tecknet på ditt tangentbord.";
            for (int i = 1; i < 11; i++)
            {
                listBox1.Items.Add(i);
            }
            listBox1.SelectedItem = listBox1.Items[0];
        }
    }
}
