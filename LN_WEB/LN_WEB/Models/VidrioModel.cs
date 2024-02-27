using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net.Http.Json;
using System.Web;
using Tienda_Vidreos.Entities;
using LN_WEB.Entities;

namespace LN_WEB.Models
{
    public class VidrioModel
    {

        public int CrearVidrio(VidrioEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = ConfigurationManager.AppSettings["urlApi"].ToString() + "api/CrearVidrio";
                JsonContent body = JsonContent.Create(entidad); //Serializar
                HttpResponseMessage resp = client.PostAsync(url, body).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<int>().Result;
                }

                return 0;
            }
        }
        public List<VidrioEnt> ConsultaVidrio(List<VidrioEnt> entidad)
        {
            using (var client = new HttpClient())
            {
                string token = HttpContext.Current.Session["Token"].ToString();
                string url = ConfigurationManager.AppSettings["urlApi"].ToString() + "api/ConsultaVidrio";

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage resp = client.GetAsync(url).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<List<VidrioEnt>>().Result;
                }

                return new List<VidrioEnt>();
            }
        }

        public int EditarVidrio(VidrioEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string token = HttpContext.Current.Session["Token"].ToString();
                string url = ConfigurationManager.AppSettings["urlApi"].ToString() + "api/EditarVidrio";
                JsonContent body = JsonContent.Create(entidad);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage resp = client.PutAsync(url, body).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<int>().Result;
                }

                return 0;
            }
        }

        public int EliminarVidrio(VidrioEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string token = HttpContext.Current.Session["Token"].ToString();
                string url = ConfigurationManager.AppSettings["urlApi"].ToString() + "api/EliminarVidrio";
                JsonContent body = JsonContent.Create(entidad);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage resp = client.DeleteAsync(url).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<int>().Result;
                }

                return 0;
            }
        }

        internal object ConsultaVidrio()
        {
            throw new NotImplementedException();
        }
    }
}