using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClienteSocketApp.Comunicacion
{
    public class ClienteSocket
    {
        private string ip;
        private int puerto;
        private Socket comunicacionServidor;
        private StreamReader reader;
        private StreamWriter writer;

        public ClienteSocket(string ip, int puerto)
        {
            this.puerto = puerto;
            this.ip = ip;
        }

        public bool Conectar()
        {
            try
            {
                this.comunicacionServidor = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse(ip), puerto);
                this.comunicacionServidor.Connect(endpoint);
                Stream stream = new NetworkStream(this.comunicacionServidor);
                this.reader = new StreamReader(stream);
                this.writer = new StreamWriter(stream);

                return true;
            }
            catch (IOException ex)
            {
                return false;
            }
        }

        public bool Escribir(string mensaje)
        {
            try
            {
                this.writer.WriteLine(mensaje);
                this.writer.Flush();

                return true;
            }
            catch (IOException ex)
            {
                return false;
            }
        }

        public string Leer()
        {
            try
            {
                return this.reader.ReadLine().Trim();
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public void Desconectar()
        {
            this.comunicacionServidor.Close();
        }
    }
}
