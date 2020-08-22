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
    public partial class Form3 : Form
    {
        int FlightID;
        long ID;
        string PNR;
        public Form3(int FlightID, long ID, string PNR)
        {
            InitializeComponent();
            this.FlightID = FlightID;
            this.ID = ID;
            this.PNR = PNR;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            Context context = new Context();
            textBox1.Text = this.ID.ToString();
            textBox2.Text = this.PNR;
            var EmptySeatControl = from q in context.Flight
                                   where q.FlightID == this.FlightID
                                   select q;
            EmptySeatControl.First().EmptySeat = EmptySeatControl.First().EmptySeat - 1;
            context.SaveChanges();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
