using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace kategorijeVozila1
{
    public partial class Form1 : Form
    {
        List<Vozilo> listaVozila = new List<Vozilo>();
        int brojAutomobila = 0, brojMotocikla = 0, brojKamiona = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnUnesi_Click(object sender, EventArgs e)
        {
            try
            {
                Vozilo vozilo = new Vozilo(txtModel.Text, Convert.ToInt16(txtGodinaProizvodnje.Text),
                    Convert.ToInt16(txtBrojKotaca.Text));

                if(Convert.ToInt16(txtBrojKotaca.Text) % 2 != 0)
                {
                    MessageBox.Show("Pogrešan unos. Molimo pokušajte ponovo",
    "Pogrešan unos", MessageBoxButtons.OK,
    MessageBoxIcon.Error);

                    return;
                }

                txtModel.Clear();
                txtGodinaProizvodnje.Clear();
                txtBrojKotaca.Clear();
                txtModel.Focus();

                DialogResult upit = MessageBox.Show("Želite li unesti još podataka?", "Upit",
        MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                switch (upit)
                {
                    case DialogResult.Yes:
                        {
                            listaVozila.Add(vozilo);
                            break;
                        }
                    case DialogResult.No:
                        {
                            listaVozila.Add(vozilo);
                            txtModel.Enabled = false;
                            txtGodinaProizvodnje.Enabled = false;
                            txtBrojKotaca.Enabled = false;

                            break;
                        }
                }
            }
            catch
            {
                MessageBox.Show("Pogrešan unos. Molimo pokušajte ponovo",
    "Pogrešan unos", MessageBoxButtons.OK,
    MessageBoxIcon.Error);
                txtModel.Clear();
                txtGodinaProizvodnje.Clear();
                txtBrojKotaca.Clear();
            }
        }

        private void btnIspis_Click(object sender, EventArgs e)
        {
            foreach (Vozilo vozilo in listaVozila)
            {
                txtIspis.AppendText(vozilo.ToString());
            }

            txtIspis.AppendText("Ukupan broj vozila po kategorijama:"
    + "\tMotocikla: " + brojMotocikla
    + "\tAutomobil: " + brojAutomobila
    + "\tKamion: " + brojKamiona);

            CSV();
        }

        private void CSV()
        {
            string putanjaDoDatoteke = "C:\\TestTest";
            try
            {
                using (StreamWriter sw = new StreamWriter(putanjaDoDatoteke))
                {
                    sw.WriteLine("Model,GodinaProizvodnje,BrojKotaca");

                    foreach (Vozilo vozilo in listaVozila)
                    {
                        sw.WriteLine(vozilo.ToString());
                    }
                }

                MessageBox.Show("Podaci su uspješno spremljeni u CSV datoteku!", "Uspjeh",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Došlo je do pogreške prilikom spremanja podataka: " + ex.Message,
                    "Pogreška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnObradi_Click(object sender, EventArgs e)
        {

            foreach (Vozilo vozilo in listaVozila)
            {
                if (vozilo.BrojKotaca == 2)
                {
                    vozilo.Kategorija = "Motocikl";
                    brojMotocikla += 1;
                    
                }
                else if (vozilo.BrojKotaca == 4)
                {
                    vozilo.Kategorija = "Automobil";
                    brojAutomobila += 1;
                }
                else
                {
                    vozilo.Kategorija = "Kamion";
                    brojKamiona += 1;
                }
            }


        }
        }
    }
