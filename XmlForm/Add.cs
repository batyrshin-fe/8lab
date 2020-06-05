using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XmlForm
{
    public partial class Add : Form
    {
        private string Name;
        private string Phone;
        private string Date;
        private string Tariff;
        private string Disount;
        private string Time;
        public string name
        {
            get { return Name; }
            set { Name = value; }
        }
        public string tariff
        {
            get { return Tariff; }
            set { Tariff = value; }
        }
        public string disount
        {
            get { return Disount; }
            set { Disount = value; }
        }
        public string time
        {
            get { return Time; }
            set { Time = value; }
        }
        public string date
        {
            get { return Date; }
            set { Date = value; }
        }
        public string phone
        {
            get { return Phone; }
            set { Phone = value; }
        }
        public Add()
        {
            InitializeComponent();
        }

        private void DoneButton_Click(object sender, EventArgs e)
        {
            name = textBoxName.Text;
            phone = textBoxPhone.Text;
            date = textBoxDate.Text;
            tariff = textBoxTariff.Text;
            disount = textBoxDisount.Text;
            time = textBoxTime.Text;
            int temp = Int32.Parse(disount); 
        }
    }
}
