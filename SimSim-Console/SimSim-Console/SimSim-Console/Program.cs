using System.IO.Ports;


namespace SimSim_Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string data = "";
            string enMelding = "";
            SerialPort sp = new SerialPort("COM4", 9600);

            bool ferdig = false;
            try
            {
                sp.Open();
            }
            catch(Exception u)
            {
                Console.WriteLine("Feil: " + u.Message);
            }

            if (!sp.IsOpen) ferdig = true;
            else
            {
                sp.Write("$S002");

                while (!ferdig)
                {
                    data = data + LesData(sp, ref ferdig);
                    Thread.Sleep(50);
                    if (EnHelMelding(data))
                    {
                        data = HentUtEnMelding(data, ref enMelding);
                        Console.WriteLine(enMelding);
                        Console.WriteLine("Temperatur: " + HentUtTemperatur(enMelding).ToString());
                    }
                }
            }

        }  // av Main


        static string LesData(SerialPort sp, ref bool ferdig)
        {
            string svar = "";
            try
            {
                svar = sp.ReadExisting();
            }
            catch (Exception u)
            {
                Console.WriteLine("Feil: " + u.Message);
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

    }
}
