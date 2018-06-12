using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;

namespace Dados
{
    public class leJson
    {
        public string retornaId(string url)
        {
            WebClient client = new WebClient();
            String htmlCode = client.DownloadString(url);

            dynamic device = JsonConvert.DeserializeObject(htmlCode);
            string deviceID = device.Device;

            return deviceID;
        }

        public string retornaLocal(string url)
        {
            WebClient client = new WebClient();
            String htmlCode = client.DownloadString(url);

            dynamic device = JsonConvert.DeserializeObject(htmlCode);
            string local = device.Local;

            return local;
        }

        public string retornaTemperatura(string url)
        {
            WebClient client = new WebClient();
            String htmlCode = client.DownloadString(url);

            dynamic device = JsonConvert.DeserializeObject(htmlCode);
            string temperatura = device.Temperatura;

            return temperatura;
        }

        public string retornaUmidade(string url)
        {
            WebClient client = new WebClient();
            String htmlCode = client.DownloadString(url);

            dynamic device = JsonConvert.DeserializeObject(htmlCode);
            string umidade = device.Umidade;

            return umidade;
        }

        public string retornaCorrente(string url)
        {
            WebClient client = new WebClient();
            String htmlCode = client.DownloadString(url);

            dynamic device = JsonConvert.DeserializeObject(htmlCode);
            string corrente = device.Corrente;

            return corrente;
        }
    }
}
