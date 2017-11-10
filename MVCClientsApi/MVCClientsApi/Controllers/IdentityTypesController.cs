using MVCClientsApi.Models;
using MVCClientsApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MVCClientsApi.Controllers
{
    public class IdentityTypesController : ApiController
    {
        Repo_Clients rp = new Repo_Clients();
        public List<IdentityType> Get()
        {
            List<IdentityType> identityTypes = new List<IdentityType>();
            rp.IdentityTypes(out identityTypes);
            return identityTypes;
        }
    }
}
