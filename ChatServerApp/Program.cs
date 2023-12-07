using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ChatServerApp.Comunicacion;

namespace ChatServerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            int puerto = int.Parse(ConfigurationManager.AppSettings["puerto"]);
            Console.WriteLine("Iniciando servidor en el puerto {0}", puerto);
            ServerSocket servidor = new ServerSocket(puerto);
            if (servidor.Iniciar())
            {
                while (true)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Esperando clientes...");
                    if (servidor.ObtenerCliente())
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("Conexion establecida!");

                        //Protocolo de comunicación.
                        string mensaje = "";
                        while (mensaje.ToLower() != "chao")
                        {
                            //Leer mensaje del cliente.
                            mensaje = servidor.Leer();
                            Console.WriteLine("El cliente dice: {0}", mensaje);
                            if (mensaje.ToLower() != "chao")
                            {
                                //El cliente espera una respuesta.
                                Console.WriteLine("Escribir respuesta...");
                                mensaje = Console.ReadLine().Trim();
                                servidor.Escribir("El servidor dice: " + mensaje);
                            }
                        }
                        servidor.CerrarConexion();
                    }
                }

            } else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error al levantar el servidor");
                Console.ReadKey();
            }
        }
    }
}
