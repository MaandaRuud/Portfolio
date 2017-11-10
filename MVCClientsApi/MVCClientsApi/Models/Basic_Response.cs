using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCClientsApi.Models
{
    public class Basic_Response
    {
        public string CustomMessage { get; set; }
        public int  StatusCode { get; set; }
        public string Message { get; set; }
    }
}