using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blockchain
{
    public partial class Form1 : Form
    {
        Chain chain;
        public Form1()
        {            
            InitializeComponent();
            chain = new Chain();
            dataGridView1.DataSource = chain.Blocks;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text != null && textBox1.Text.Length > 0)
            {
                chain.Add(textBox1.Text, "User" + new Random().Next(10));
                UpdateGrid();
                textBox1.Text = string.Empty;
            }
        }

        private void UpdateGrid()
        {
            dataGridView1.DataSource = typeof(List<Block>);
            dataGridView1.DataSource = chain.Blocks;
        }
    }
}
