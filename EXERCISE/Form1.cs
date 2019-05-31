using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EXERCISE.Classes;

namespace EXERCISE
{
    public partial class Form1 : Form
    {
        IContext context;

        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (context == null)
                return;

            Record rec = context.CreateNewRecord();

            lblID.Text = "ID: " + rec.ID.ToString();
            lblType.Text = "Type: " + rec.Type.ToString();
            lblSubType.Text = "SubType: "+ rec.Subtype.ToString();
            lblData.Text = "Data: "+ rec.Data;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (context == null)
                return;

            byte Bout = 0; ushort Uout = 0;

            if (!(byte.TryParse(txtType.Text, out Bout) && ushort.TryParse(txtSubType.Text, out Uout)))
                return;

            dataGridView1.DataSource = context.Search(byte.Parse(txtType.Text), ushort.Parse(txtSubType.Text));
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        { 
            if (comboBox1.SelectedIndex == 1)
                context = new JSONContext();

            if (comboBox1.SelectedIndex == 2)
                context = new BinaryContext();

            if (comboBox1.SelectedIndex == 3)
                context = new SerializedBinaryContext();

            context.Initialize();
        }
    }
}
