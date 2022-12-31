﻿using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Web.Http;
using TISBackend.Db;
using TISBackend.Models;

namespace TISBackend.Controllers
{
    public class AddressController : TISController
    {
        private const string tableName = "ADRESY";
        private const string idName = "id_adresa";
        
        // GET: api/Address
        public IEnumerable<Address> Get()
        {
            List<Address> list = new List<Address>();

            if (IsAuthorized())
            {
                DataTable query = DatabaseController.Query($"SELECT * FROM {tableName}");
                foreach (DataRow dr in query.Rows)
                {
                    list.Add(new Address() {
                        Id = int.Parse(dr[idName].ToString()),
                        Street = dr["ulice"].ToString(),
                        HouseNumber = dr["cislo_popisne"].ToString(),
                        City = dr["obec"].ToString(),
                        PostalCode = dr["psc"].ToString(),
                        Country = dr["zeme"].ToString()
                    });
                }
            }

            return list;
        }

        // GET: api/Address/5
        public Address Get(int id)
        {
            if (!IsAuthorized())
            {
                return null;
            }
            DataRow query = DatabaseController.Query($"SELECT * FROM {tableName} WHERE {idName} = id", new OracleParameter(":id", id)).Rows[0];
            return new Address() {
                Id = int.Parse(query[idName].ToString()),
                Street = query["ulice"].ToString(),
                HouseNumber = query["cislo_popisne"].ToString(),
                City = query["obec"].ToString(),
                PostalCode = query["psc"].ToString(),
                Country = query["zeme"].ToString()
            };
        }

        // POST: api/Address
        public IHttpActionResult Post([FromBody] string value)
        {
            if (!IsAdmin())
            {
                return StatusCode(HttpStatusCode.Unauthorized);
            }

            // TODO

            return StatusCode(HttpStatusCode.OK);
        }

        // DELETE: api/Address/5
        public IHttpActionResult Delete(int id)
        {
            return DeleteById(tableName, idName, id);
        }
    }
}
