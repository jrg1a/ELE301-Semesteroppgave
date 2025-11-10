using System.IO.Ports;

namespace SimSim_GUI
{
    public partial class Form1 : Form
    {
        SerialPort sp;
        bool kommunikasjonErPågående;
        string data;
        string enMelding;

        public Form1()
        {
            InitializeComponent();
            kommunikasjonErPågående = false;
            sp = null;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            sp = new SerialPort(cbPorter.SelectedItem as string, 9600);

            try
            {
                sp.Open();
            }
            catch (Exception u)
            {
                MessageBox.Show("Feil: " + u.Message);
            }

            if (sp.IsOpen)
            {
                kommunikasjonErPågående = true;
                sp.Write("$S002");
                bwHentData.RunWorkerAsync();
                btnStart.Enabled = false;
                btnStopp.Enabled = true;
            }

        }


        void OppdaterSeriellePorter()
        {
            string[] porter = SerialPort.GetPortNames();

            cbPorter.Items.Clear();

            if (porter.Length > 0)
            {
                for (int i = 0; i < porter.Length; i++)
                {
                    cbPorter.Items.Add(porter[i]);
                }

                cbPorter.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("Feil: programmet finner ikke tilgjengelige (serielle) porter!");
            }

        }

        private void btnStopp_Click(object sender, EventArgs e)
        {
            kommunikasjonErPågående = false;
            btnStopp.Enabled = false;
            OppdaterSeriellePorter();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            OppdaterSeriellePorter();
        }


        static string LesData(SerialPort sp, ref bool ferdig)
        {
            string svar = "";
            try
            {
                svar = sp.ReadExisting();
            }
            catch (Exception u)
            {
                // MessageBox.Show("Feil: " + u.Message);
                ferdig = true;
            }
            return svar;
        }

        static double HentUtTemperatur(string enMelding)
        {
            double svar = 0;

            int posI = enMelding.IndexOf('I');
            if (posI != -1)
            {
                svar = Convert.ToDouble(enMelding.Substring(posI + 1, 3));
            }

            // 0 er -20, 150 er +50 grader - vi konverterer til riktig temp

            svar = -20 + (((50 - (-20))) / 150.0) * svar;
            svar = Math.Round(svar, 3);

            return svar;
        }

        static string HentUtEnMelding(string data, ref string enMelding)
        {
            string svar = "";

            int posStart = data.IndexOf('$');
            int posSlutt = data.IndexOf('#');

            enMelding = data.Substring(posStart, (posSlutt - posStart) + 1);

            svar = data.Substring(posSlutt + 1);

            return svar;
        }

        static bool EnHelMelding(string data)
        {
            bool svar = false;

            int posStart = data.IndexOf('$');
            int posSlutt = data.IndexOf('#');

            if (posStart != -1 && posSlutt != -1)
            {
                if (posSlutt > posStart) svar = true;
            }

            return svar;
        }

        private void bwHentData_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            bool ferdig = false;
            while (!ferdig)
            {
                data = data + LesData(sp, ref ferdig);
                Thread.Sleep(50);
                if (EnHelMelding(data))
                {
                    data = HentUtEnMelding(data, ref enMelding);
                    ferdig = true;
                }
            }
        }

        private void bwHentData_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            lbLogg.Items.Add(enMelding);
            string format = "+#.00;-#.00;0"; // Positive format; Negative format
            double temp = HentUtTemperatur(enMelding);
            txtTemp.Text = temp.ToString(format);
            if (kommunikasjonErPågående) bwHentData.RunWorkerAsync();
            else
            {
                sp.Close();
                btnStart.Enabled = true;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            kommunikasjonErPågående = false;

            if (sp != null)
            {
                if (sp.IsOpen) sp.Close();
            }
        }
    }
}
