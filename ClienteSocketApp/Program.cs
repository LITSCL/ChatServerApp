using ClienteSocketApp.Comunicacion;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteSocketApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string ip = ConfigurationManager.AppSettings["ip"];
            int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["puerto"]);
            ClienteSocket clienteSocket = new ClienteSocket(ip, puerto);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Conectando...");
            if (clienteSocket.Conectar())
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Conectado!");
                string mensaje = "";
                while (mensaje.ToLower() != "chao")
                {
                    Console.WriteLine("Escribir mensaje...");
                    mensaje = Console.ReadLine().Trim();
                    clienteSocket.Escribir(mensaje);

                    if (mensaje.ToLower() != "chao")
                    {
                        mensaje = clienteSocket.Leer();
                        Console.WriteLine(mensaje);
                    }
                }
                clienteSocket.Desconectar();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error de conexión");
            }
        }
    }
}
