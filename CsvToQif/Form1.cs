using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CsvToQif
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                int counter = 0;
                string line;
                StringBuilder qif = new StringBuilder();

                System.IO.StreamReader file = new System.IO.StreamReader(openFileDialog1.FileName);
                while ((line = file.ReadLine()) != null)
                {
                    if (counter > 0)
                    {
                        string[] data = System.Text.RegularExpressions.Regex.Split(line, ";");
                        qif.AppendLine("D" + data[0]);
                        qif.AppendLine("T" + data[6].Replace(',', '.').Trim('"').Replace(" ", string.Empty));
                        qif.AppendLine("P" + data[2].Trim('"'));
                        qif.AppendLine("^");
                    } else
                    {
                        qif.AppendLine("!Type:Bank");
                    }

                    counter++;
                }

                file.Close();
                string cheminComplet = Path.GetDirectoryName(openFileDialog1.FileName) + "\\" + openFileDialog1.SafeFileName.Substring(0, openFileDialog1.SafeFileName.Length - 3) + "qif";
                System.IO.File.WriteAllText(cheminComplet, qif.ToString());         
            }

        }
    }
}
