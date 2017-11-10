using MVCClientsApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Web;
using System.Configuration;
using System.Data.Entity;

namespace MVCClientsApi.Repository
{
    public class Repo_Clients
    {
        public Basic_Response Clients(out List<Client> clients)
        {
            List<Client> liClients = new List<Models.Client>();
            try
            {
                var dbContext = new clientsEntities();
                using (var con = new SqlConnection(dbContext.Database.Connection.ConnectionString))
                {
                    con.Open();
                    string query = "Select C.*,I.IdentityTypeDescription From Clients C inner Join IdentityTypes I On I.IdentityTypeID = C.IdentityTypeID";
                    using (var cmd = new SqlCommand(query, con))
                    {
                        using (var rd = cmd.ExecuteReader())
                        {
                            while (rd.Read())
                            {
                                liClients.Add(new Models.Client()
                                {
                                    DateOfBirth=DateTime.Parse(rd["DateOfBirth"].ToString()),
                                    Firstname=rd["Firstname"].ToString(),
                                    IdentityNumber=rd["IdentityNumber"].ToString(),
                                    Surname=rd["Surname"].ToString(),
                                    IdentityTypeId=Guid.Parse(rd["IdentityTypeId"].ToString()),
                                    IdentityType=new IdentityType()
                                    {
                                        IdentityTypeDescription=rd["IdentityTypeDescription"].ToString(),
                                        IdentityTypeId=Guid.Parse(rd["IdentityTypeId"].ToString())
                                    }
                                });
                            }
                        }
                    }
                    clients = liClients;
                    return new Basic_Response()
                    {
                        CustomMessage = "Clients Retrieved",
                        StatusCode = 200,
                        Message = "Clients Retrieved"
                    };
                }
            }
            catch (Exception err)
            {
                clients = new List<Client>();
                return new Basic_Response()
                {
                    StatusCode = 500,
                    CustomMessage = "Failed To Retrieve Client",
                    Message = err.Message
                };
            }
        }

        public Basic_Response IdentityTypes(out List<IdentityType> identityTypes)
        {
            List<IdentityType> liIdentityTypes = new List<Models.IdentityType>();
            try
            {
                var dbContext = new clientsEntities();
                using (var con = new SqlConnection(dbContext.Database.Connection.ConnectionString))
                {
                    con.Open();
                    string query = "Select * From IdentityTypes I";
                    using (var cmd = new SqlCommand(query, con))
                    {
                        using (var rd = cmd.ExecuteReader())
                        {
                            while (rd.Read())
                            {
                                liIdentityTypes.Add(new IdentityType()
                                {
                                    IdentityTypeDescription = rd["IdentityTypeDescription"].ToString(),
                                    IdentityTypeId = Guid.Parse(rd["IdentityTypeId"].ToString())
                                });
                            }
                        }
                    }
                    identityTypes = liIdentityTypes;
                    return new Basic_Response()
                    {
                        CustomMessage = "Identity Types Retrieved",
                        StatusCode = 200,
                        Message = "Identity Types Retrieved"
                    };
                }
            }
            catch (Exception err)
            {
                identityTypes = new List<IdentityType>();
                return new Basic_Response()
                {
                    StatusCode = 500,
                    CustomMessage = "Failed To Retrieve Identity Types",
                    Message = err.Message
                };
            }
        }

        public Basic_Response ClientSave(Client client)
        {
            try
            {
                using (var dbContext = new clientsEntities())
                {
                    client.IdentityType = null;
                    dbContext.Entry(client).State = EntityState.Modified;
                    dbContext.SaveChanges();
                    return new Basic_Response()
                    {
                        CustomMessage = "Client Updated",
                        Message = "Client Updated",
                        StatusCode = 200
                    };
                }
            }catch(Exception err)
            {
                return new Basic_Response()
                {
                    CustomMessage = "Failed To Save Client",
                    Message = err.Message,
                    StatusCode = 500
                };
            }
        }
    }
}