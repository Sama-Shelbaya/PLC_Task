using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using com.calitha.commons;
using com.calitha.goldparser;
namespace PLC_PROJ
{
    public partial class Form1 : Form
    {
        MyParser p;

        public Form1()
        {
            InitializeComponent();
            p = new MyParser("EL_GRAMMAR.cgt",listBox1);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            p.Parse(textBox1.Text);
        }
    }
}
