using AirplaneSaleTicket.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirplaneSaleTicket
{
    public partial class Form1 : Form
    {
        int i=0;
        string FromFlight;
        string ToFlight;
        DateTime date;
        DateTime Hour;
        public Form1()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(i != 0){
                Form2 form2 = new Form2(FromFlight,ToFlight,date,Hour);
                form2.ShowDialog();
            }
            if(i == 0)
            {
                MessageBox.Show("Invalid Request");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Context context = new Context();
            context.Database.CreateIfNotExists();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            FromFlight = textBox1.Text;
            ToFlight = textBox2.Text;
            date = DateTime.Parse(dateTimePicker1.Text);

            Context context = new Context();
            var query = from q in context.Flight
                        where q.FromFlight == FromFlight && q.ToFlight == ToFlight && q.Date == date
                        select q.Hour;
            i = 0;
            foreach (DateTime q in query)
            {
                Hour = q;
                i++;
                var que = q.ToString().Split(' ');
                comboBox1.Items.Add(que[1]);
            }
            if (i==0)
            {
                comboBox1.Items.Add("The Flight is not Found!");
            }
        }
    }
}
