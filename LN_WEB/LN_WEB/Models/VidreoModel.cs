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
using OpenAI_API.Models;
using System.Web.Mvc;

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
        public VidreoEnt ConsultaVidreo(long q)
        {

            using (var client = new HttpClient())
            {
                string url = ConfigurationManager.AppSettings["urlApi"].ToString() + "api/ConsultaVidreo?q=" + q;
                ////string token = HttpContext.Current.Session["TokenUsuario"].ToString();

                ////client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                HttpResponseMessage resp = client.GetAsync(url).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<VidreoEnt>().Result;
                }

                return null;
            }
        }
        public long RegistrarVidreo(VidreoEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = ConfigurationManager.AppSettings["urlApi"].ToString() + "api/RegistrarVidreo";
                JsonContent body = JsonContent.Create(entidad); //Serializar

                HttpResponseMessage resp = client.PostAsync(url, body).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<long>().Result;
                }

                return 0;
            }
        }
            public void ActualizarRuta(VidreoEnt entidad)
            {
                using (var client = new HttpClient())
                {
                    string url = ConfigurationManager.AppSettings["urlApi"].ToString() + "api/ActualizarRuta";
                    JsonContent body = JsonContent.Create(entidad); //Serializar

                    HttpResponseMessage resp = client.PutAsync(url, body).Result;
                }
            }


        }
    }
    
