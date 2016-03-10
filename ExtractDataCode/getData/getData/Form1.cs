using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace getData
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
         
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog f = new OpenFileDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {
                listBox1.Items.Clear();

                List<string> lines = new List<string>();
                using (StreamReader r = new StreamReader(f.OpenFile()))
                {
                    string line;
                    string deger = "";
                    while ((line = r.ReadLine()) != null)
                    {
                        deger = line.Replace("http://dbpedia.org/resource/", "");
                        deger = deger.Replace("http://dbpedia.org/ontology/", "");
                        int kacoldu=0;
                        string yenideger = "";
                        for (int k = 0; k < deger.Length; k++)
                        {
                            if (deger.Substring(k, 1) == ">" || deger.Substring(k, 1) == "<") kacoldu++;
                            if (kacoldu != 3 && kacoldu != 4)
                            {
                                yenideger = yenideger + deger.Substring(k, 1);
                            }
                        }
                        yenideger = yenideger.Substring(1, yenideger.Length - 1);
                        yenideger = yenideger.Replace("> <", " - ");
                        yenideger = yenideger.Replace(">", "");
                        listBox1.Items.Add(yenideger);
                        Application.DoEvents();
                    }
                }
                button2.Enabled = true;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            TextWriter tw = new StreamWriter("C:\\SavedListNew.txt");

            foreach (String s in listBox1.Items)
                tw.WriteLine(s);

            tw.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int c = 0; c < listBox1.Items.Count; c++)
            {
                for (int k = c; k < listBox1.Items.Count - c; k++)
                {
                    if (listBox1.Items[c].ToString() == listBox1.Items[k].ToString())
                        listBox1.Items.Remove(listBox1.Items[c].ToString());
                }
            }
        }
    }
}
