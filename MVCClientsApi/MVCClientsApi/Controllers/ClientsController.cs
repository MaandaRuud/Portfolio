using MVCClientsApi.Models;
using MVCClientsApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Web.Hosting;
using System.Web.Http;

namespace MVCClientsApi.Controllers
{
    public class ClientsController : ApiController
    {
        Repo_Clients rp = new Repo_Clients();

        [HttpGet]
        public List<Client> Get()
        {
            List<Client> clients = new List<Client>();
            rp.Clients(out clients);
            return clients;
        }

        [HttpPut]
        public Basic_Response Save(Client client)
        {
            return rp.ClientSave(client);
        }


    }
}
