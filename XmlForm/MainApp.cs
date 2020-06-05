using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.IO;

namespace XmlForm
{
    public partial class MainApp : Form
    {
        public MainApp()
        {
            InitializeComponent();
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label1.Text = Procedures.GetInfo();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string name, date, tariff, disount, time, phone;
                int temp;
                Add f2 = new Add();
                if (f2.ShowDialog() == DialogResult.OK)
                {
                    name = f2.name;
                    date = f2.date;
                    phone = f2.phone;
                    disount = f2.disount;
                    time = f2.time;
                    tariff = f2.tariff;
                    temp = Int32.Parse(disount);
                    Procedures.Add(name, phone, time, date, disount, tariff);
                }
                label1.Text = Procedures.GetInfo();
            }
            catch (FormatException)
            {
                MessageBox.Show("Wrong input! Disount is digit!");
            }
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string disount, date, tariff;
            Remove f3 = new Remove();
            if (f3.ShowDialog() == DialogResult.OK)
            {
                date = f3.date;
                disount = f3.disount;
                tariff = f3.tariff;
                if (date == "" || disount == "" || tariff == "")
                {
                    MessageBox.Show("You have to fill all fields!");
                }
                else
                    Procedures.Remove(date, tariff, disount);
            }
            label1.Text = Procedures.GetInfo();
        }

        private void Search_Click(object sender, EventArgs e)
        {
            string val;
            ToolStripMenuItem b = (ToolStripMenuItem)sender;
            subj s = new subj();
            if (s.ShowDialog() == DialogResult.OK)
            {
                val = s.val;
                if (val == "")
                {
                    MessageBox.Show("You didn't fill the field!");
                }
                else
                {
                    switch (b.Text)
                    {
                        case "tariff":
                            label1.Text = Procedures.SearchTariff(val);
                            break;
                        case "disount":
                            label1.Text = Procedures.SearchDisount(val);
                            break;
                        case "name":
                            label1.Text = Procedures.SearchName(val);
                            break;
                        case "date":
                            label1.Text = Procedures.SearchDate(val);
                            break;
                        case "phone":
                            label1.Text = Procedures.SearchPhone(val);
                            break;
                        case "time":
                            label1.Text = Procedures.SearchTime(val);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private void MainApp_Load(object sender, EventArgs e)
        {

        }
    }
    public class Procedures
    {
        public static void Remove(string date, string tariff, string disount)
        {
            XDocument xdoc = XDocument.Load("payers.xml");
            XElement root = xdoc.Element("payers");
            foreach (XElement temp in root.Elements("payer").ToList())
                if (temp.Element("date").Value == date && temp.Element("tariff").Value == tariff && temp.Element("disount").Value == disount)
                {
                    temp.Remove();
                }
            xdoc.Save("payers.xml");
        }
        public static void Add(string name, string phone, string time, string date, string disount, string tariff)
        {
            XDocument xdoc = XDocument.Load("payers.xml");
            XElement root = xdoc.Element("payers");
            root.Add(new XElement("payer",
                             new XAttribute("name", name),
                             new XElement("tariff", tariff),
                             new XElement("disount", disount),
                             new XElement("date", date),
                             new XElement("Time", time),
                             new XElement("phone", phone)));
            xdoc.Save("payers.xml");
        }
        public static string GetInfo()
        {
            string text = "";
            XDocument xdoc = XDocument.Load("payers.xml");
            var items = from xe in xdoc.Element("payers").Elements("payer")
                        select new Payer
                        {
                            Name = xe.Attribute("name").Value,
                            Disount = xe.Element("disount").Value,
                            Phone = xe.Element("phone").Value,
                            Date = xe.Element("date").Value,
                            Tariff = xe.Element("tariff").Value,
                            Time = xe.Element("Time").Value
                        };

            foreach (var item in items)
            {
                text = text + $"Payer: {item.Name} with tariff: {item.Time} on date: {item.Phone} with time {item.Disount} and discount {item.Tariff} on phone: {item.Date}\n";
            }
            return text;
        }

        public static string SearchName(string p_name)
        {
            string text = "";
            XDocument xdoc = XDocument.Load("payers.xml");
            var items = from xe in xdoc.Element("payers").Elements("payer")
                        where xe.Attribute("name").Value == p_name
                        select new Payer
                        {
                            Name = xe.Attribute("name").Value,
                            Disount = xe.Element("disount").Value,
                            Phone = xe.Element("phone").Value,
                            Date = xe.Element("date").Value,
                            Tariff = xe.Element("tariff").Value,
                            Time = xe.Element("Time").Value
                        };

            foreach (var item in items)
            {
                text = text + $"Payer: {item.Name} with tariff: {item.Time} on date: {item.Phone} with time {item.Disount} and discount {item.Tariff} on phone: {item.Date}\n";
            }
            return text;
        }

        public static string SearchDate(string p_zach)
        {
            string text = "";
            XDocument xdoc = XDocument.Load("payers.xml");
            var items = from xe in xdoc.Element("payers").Elements("payer")
                        where xe.Element("date").Value == p_zach
                        select new Payer
                        {
                            Name = xe.Attribute("name").Value,
                            Disount = xe.Element("disount").Value,
                            Phone = xe.Element("phone").Value,
                            Date = xe.Element("date").Value,
                            Tariff = xe.Element("tariff").Value,
                            Time = xe.Element("Time").Value
                        };

            foreach (var item in items)
            {
                text = text + $"Payer: {item.Name} with tariff: {item.Time} on date: {item.Phone} with time {item.Disount} and discount {item.Tariff} on phone: {item.Date}\n";
            }
            return text;
        }
        public static string SearchPhone(string p_phone)
        {
            string text = "";
            XDocument xdoc = XDocument.Load("payers.xml");
            var items = from xe in xdoc.Element("payers").Elements("payer")
                        where xe.Element("phone").Value == p_phone
                        select new Payer
                        {
                            Name = xe.Attribute("name").Value,
                            Disount = xe.Element("disount").Value,
                            Phone = xe.Element("phone").Value,
                            Date = xe.Element("date").Value,
                            Tariff = xe.Element("tariff").Value,
                            Time = xe.Element("Time").Value
                        };

            foreach (var item in items)
            {
                text = text + $"Payer: {item.Name} with tariff: {item.Time} on date: {item.Phone} with time {item.Disount} and discount {item.Tariff} on phone: {item.Date}\n";
            }
            return text;
        }
        public static string SearchDisount(string p_disount)
        {
            string text = "";
            XDocument xdoc = XDocument.Load("payers.xml");
            var items = from xe in xdoc.Element("payers").Elements("payer")
                        where xe.Element("disount").Value == p_disount
                        select new Payer
                        {
                            Name = xe.Attribute("name").Value,
                            Disount = xe.Element("disount").Value,
                            Phone = xe.Element("phone").Value,
                            Date = xe.Element("date").Value,
                            Tariff = xe.Element("tariff").Value,
                            Time = xe.Element("Time").Value
                        };

            foreach (var item in items)
            {
                text = text + $"Payer: {item.Name} with tariff: {item.Time} on date: {item.Phone} with time {item.Disount} and discount {item.Tariff} on phone: {item.Date}\n";
            }
            return text;
        }
        public static string SearchTime(string p_time)
        {
            string text = "";
            XDocument xdoc = XDocument.Load("payers.xml");
            var items = from xe in xdoc.Element("payers").Elements("payer")
                        where xe.Element("Time").Value == p_time
                        select new Payer
                        {
                            Name = xe.Attribute("name").Value,
                            Disount = xe.Element("disount").Value,
                            Phone = xe.Element("phone").Value,
                            Date = xe.Element("date").Value,
                            Tariff = xe.Element("tariff").Value,
                            Time = xe.Element("Time").Value
                        };

            foreach (var item in items)
            {
                text = text + $"Payer: {item.Name} with tariff: {item.Time} on date: {item.Phone} with time {item.Disount} and discount {item.Tariff} on phone: {item.Date}\n";
            }
            return text;
        }
        public static string SearchTariff(string p_tariff)
        {
            string text = "";
            XDocument xdoc = XDocument.Load("payers.xml");
            var items = from xe in xdoc.Element("payers").Elements("payer")
                        where xe.Element("tariff").Value == p_tariff
                        select new Payer
                        {
                            Name = xe.Attribute("name").Value,
                            Disount = xe.Element("disount").Value,
                            Phone = xe.Element("phone").Value,
                            Date = xe.Element("date").Value,
                            Tariff = xe.Element("tariff").Value,
                            Time = xe.Element("Time").Value
                        };

            foreach (var item in items)
            {
                text = text + $"Payer: {item.Name} with tariff: {item.Time} on date: {item.Phone} with time {item.Disount} and discount {item.Tariff} on phone: {item.Date}\n";
            }
            return text;
        }
        class Payer
        {
            public string Name { get; set; }
            public string Disount { get; set; }
            public string Phone { get; set; }
            public string Date { get; set; }
            public string Tariff { get; set; }
            public string Time { get; set; }
        }
    }
}