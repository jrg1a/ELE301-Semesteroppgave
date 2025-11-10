using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Adgangskontroll
{
    internal class Server
    {
        static void Main(string[] args)
        {
            bool avslutt = false;

            Socket lytteSokkel = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint serverEP = new IPEndPoint(IPAddress.Any, 8080); // placeholder atm

            lytteSokkel.Bind(serverEP);
            lytteSokkel.Listen(10);

            while (!avslutt)
            {
                Console.WriteLine("Venter på en klient ...");
                Socket nyKlient = lytteSokkel.Accept(); // blokkerende metode

                VisKlientInfo(nyKlient);

                Thread t = new Thread(KlientKommunikasjon);
                t.Start(nyKlient);

                // ThreadPool.QueueUserWorkItem(KlientKommunikasjon, nyKlient);
            }

            lytteSokkel.Close();

        }

        static void VisKlientInfo(Socket s)
        {
            IPEndPoint klientEP = s.RemoteEndPoint as IPEndPoint;
            IPEndPoint serverEP = s.LocalEndPoint as IPEndPoint;

            Console.WriteLine($"Snakker med klient {klientEP.Address}:{klientEP.Port}, bruker selv {serverEP.Address}:{serverEP.Port}");
        }

        static void KlientKommunikasjon(object o)
        {
            Socket minSokkel = o as Socket;

            string dataFraKlient;
            string dataTilKlient;
            bool harForbindelse = true;

            // Console.WriteLine("Har forbindelse med {0} på port {1}", klientEP.Address, serverEP.Port);
            string hilsen = "Velkommen til en enkel testserver";

            SendData(minSokkel, hilsen, out harForbindelse);

            while (harForbindelse)
            {
                dataFraKlient = MottaData(minSokkel, out harForbindelse);
                if (harForbindelse)
                {
                    Console.WriteLine(dataFraKlient);
                    dataTilKlient = ReverserTekst(dataFraKlient);
                    SendData(minSokkel, dataTilKlient, out harForbindelse);
                }
            }

            IPEndPoint klientEP = minSokkel.RemoteEndPoint as IPEndPoint;
            Console.WriteLine("Forbindelsen med {0}:{1} er brutt", klientEP.Address, klientEP.Port);
            minSokkel.Close();
        }

        static string MottaData(Socket s, out bool gjennomført)
        {
            string svar = "";
            byte[] dataSomBytes = new byte[1024];
            gjennomført = true;

            try
            {
                int antalBytesMotatt = s.Receive(dataSomBytes);
                svar = Encoding.ASCII.GetString(dataSomBytes, 0, antalBytesMotatt);
            }
            catch (Exception e)
            {
                gjennomført = false;
            }
            return svar;
        }

        static void SendData(Socket s, string data, out bool gjennomført)
        {
            byte[] dataSomBytes = new byte[1024];
            gjennomført = true;

            try
            {
                dataSomBytes = Encoding.ASCII.GetBytes(data);
                s.Send(dataSomBytes);
            }
            catch (Exception e)
            {
                gjennomført = false;
            }
        }



    }
}
