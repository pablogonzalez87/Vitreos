using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Web;
using Tienda_Vidreos.Entities;
using LN_WEB.Entities;
using System.Net.Http.Json;

namespace LN_WEB.Model
{
    public class VidreoModel
    {
        public List<VidreoEnt> ConsultaVidreos()
        {
            using (var client = new HttpClient())
            {
               
                string url = ConfigurationManager.AppSettings["urlApi"].ToString() + "api/ConsultaVidreos";
                HttpResponseMessage resp = client.GetAsync(url).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<List<VidreoEnt>>().Result;
                }

                return new List<VidreoEnt>();
            }
        }
    }
}