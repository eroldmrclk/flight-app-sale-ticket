using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AirplaneSaleTicket.Entity;

namespace AirplaneSaleTicket
{
    public partial class Form2 : Form
    {
        string FromFlight;
        string ToFlight;
        DateTime date;
        DateTime Hour;
        int FlightID;
        String PNR;
        long ID;
        public Form2(string FromFlight, string ToFlight, DateTime date, DateTime Hour)
        {
            InitializeComponent();
            this.FromFlight = FromFlight;
            this.ToFlight = ToFlight;
            this.date = date;
            this.Hour = Hour;

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Context context = new Context();
            var queryFlightID = from q in context.Flight
                                where q.FromFlight == FromFlight && q.ToFlight == ToFlight && q.Date == date && q.Hour == Hour
                                select new
                                {
                                    q.FlightID,
                                    q.Price
                                };
            foreach(var q in queryFlightID)
            {
                FlightID = q.FlightID;
                textBox5.Text = q.FlightID.ToString();
                textBox4.Text = q.Price.ToString();
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Context context = new Context();
            long controlID=0;
            if(textBox1.Text != "")
                controlID = long.Parse(textBox1.Text);
            var query = from q in context.Person
                        where q.ID == controlID
                        select q;
            if(query.FirstOrDefault() != null)
            {
                textBox2.Text = query.First().Name;
                textBox3.Text = query.First().Surname;
                comboBox1.Text = query.First().Gender;
            }
            else
            {
                textBox2.Text = "";
                textBox3.Text = "";
                comboBox1.Text = null;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RandomGenerator generator = new RandomGenerator();
            Context context = new Context();
            var queryEmptySeat = from q in context.Flight
                                 where q.FlightID == this.FlightID
                                 select q.EmptySeat;
            if(queryEmptySeat.First() > 0)
            {
                ID = long.Parse(textBox1.Text);
                PNR = generator.RandomPassword();
                var queryIDControl = from q in context.Person
                                     where q.ID == ID
                                     select q.PersonID;

                if (queryIDControl.FirstOrDefault() == 0)
                {
                    context.Person.Add(new Person()
                    {
                        ID = this.ID,
                        Name = textBox2.Text,
                        Surname = textBox3.Text,
                        Gender = comboBox1.Text
                    });

                    context.SaveChanges();

                    var queryPersonID = from q in context.Person
                                        where q.ID == ID
                                        select q.PersonID;

                    context.Passenger.Add(new Passenger()
                    {
                        FlightID = this.FlightID,
                        PNR = this.PNR,
                        PersonID = queryPersonID.First()
                    });
                }
                else
                {
                    context.Passenger.Add(new Passenger()
                    {
                        FlightID = this.FlightID,
                        PNR = this.PNR,
                        PersonID = queryIDControl.First()
                    });
                }
                context.SaveChanges();
                Close();
                Form3 form3 = new Form3(this.FlightID, this.ID, this.PNR);
                form3.ShowDialog();
            }
            else
            {
                Close();
                Form4 form4 = new Form4();
                form4.ShowDialog();
            }
        }

    }




    public class RandomGenerator
    {
        // Generate a random number between two numbers    
        public int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        // Generate a random string with a given size    
        public string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }

        // Generate a random password    
        public string RandomPassword()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomString(4, true));
            builder.Append(RandomNumber(1000, 9999));
            builder.Append(RandomString(2, false));
            return builder.ToString();
        }
    }

}
