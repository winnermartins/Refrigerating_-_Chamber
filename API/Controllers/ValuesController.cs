using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Actors.Interfaces;
using Dados;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Client;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/type/region/version
        [HttpGet("{id}/{location}/{temperature}/{humidity}/{current}")]
        public async Task<string> GetAsync(string id, string location, string temperature, string humidity, string current)
        {
            string guid_PK = System.Guid.NewGuid().ToString();
            string guid_RK = System.Guid.NewGuid().ToString();
            var actor = ActorProxy.Create<IThing>(new ActorId(1), new Uri("fabric:/IotExercice/ThingActorService"));
            await actor.ActivateMeAsync(guid_PK, guid_RK, id,  location, temperature, humidity, current);
            //string versionString = Convert.ToString(version);
            return "O device " + id + " Localizado em: " + location + " está medindo temperatura igual a " + temperature + "°C e umidade relativa do ar igual a " + humidity + " % e corrente de " + current + " Ampères";
        }

        // GET api/values/IP-DO-NÓ
        [HttpGet("{urlno}")]
        public async Task<string> GetAsync(string urlno)
        {
            string guid_PK = System.Guid.NewGuid().ToString();
            string guid_RK = System.Guid.NewGuid().ToString();
            var actor = ActorProxy.Create<IThing>(new ActorId(1), new Uri("fabric:/IotExercice/ThingActorService"));

            urlno = "http://" + urlno;

            leJson dev = new leJson();

            string id = dev.retornaId(urlno);
            string local = dev.retornaLocal(urlno);
            string temperatura = dev.retornaTemperatura(urlno);
            string umidade = dev.retornaUmidade(urlno);
            string corrente = dev.retornaCorrente(urlno);

            await actor.ActivateMeAsync(guid_PK, guid_RK, id, local, temperatura, umidade, corrente);
            //string versionString = Convert.ToString(version);
            return "O device " + id + " Localizado em: " + local + " está medindo temperatura igual a " + temperatura + "°C e umidade relativa do ar igual a " + umidade + " % e corrente de " + corrente + " Ampères";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
