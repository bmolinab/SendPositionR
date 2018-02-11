using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SendPositionR
{
    class Program
    {/// <summary>
    /// Se debe escribir la url donde se encuentre la pagina que recibirá los datos 
    /// </summary>
        public static SignalRCliente signalRCliente = new SignalRCliente("http://localhost:52127");

        static void Main(string[] args)
        {
            Console.ReadLine();

            for (int i = 0; i < 1000; i++)
            {
                Console.WriteLine("Latitud");
                float Lat = (float)-0.174391 + i;
                Console.WriteLine("Longitud");
                float Lon = (float)-78.48326500000002 + i;
                Enviar(Lat, Lon, 1, 2, "nestor");

                Thread.Sleep(10000);
                Console.WriteLine("Latitud");
                Lat = (float)-0.1717243 + i;
                Console.WriteLine("Longitud");
                Lon = (float)-78.48371889999999 + i;
                Enviar(Lat, Lon, 2, 2, "Brian");

                Thread.Sleep(20000);
            }




            //Console.WriteLine("Latitud");
            //Lat = (float)-0.176688;
            //Console.WriteLine("Longitud");
            //Lon = (float)-78.4841;
            //Enviar(Lat, Lon, 2);

            Console.WriteLine("Posicion enviada correctamente");
            Console.ReadLine();
        }


        private static async Task Enviar(float lat, float lon, int agente, int empresa, string nombre)
        {
            await signalRCliente.Start().ContinueWith(task =>
            {
                if (task.IsFaulted)
                    Console.WriteLine("Error", "An error occurred when trying to connect to SignalR: " + task.Exception.InnerExceptions[0].Message);
            }
                   );
            LivePositionRequest lpr = new LivePositionRequest
            {
                EmpresaId = empresa,
                AgenteId = agente,
                fecha = DateTime.Now,
                Lat = lat,
                Lon = lon,
                Nombre = nombre,
            };
            //try to reconnect every 10 seconds, just in case

            signalRCliente.SendMessage(lpr);


        }

    }
}
