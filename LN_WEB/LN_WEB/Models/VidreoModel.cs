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
using System.Threading.Tasks;

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
        public VidreoEnt ConsultarVidreo(long q)
        {
            using (var client = new HttpClient())
            {

                string url = ConfigurationManager.AppSettings["urlApi"].ToString() + "api/ConsultarVidreo?q=" + q;
                HttpResponseMessage resp = client.GetAsync(url).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<VidreoEnt>().Result;
                }

                return null;
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


        public int ActualizarVidreo(VidreoEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = ConfigurationManager.AppSettings["urlApi"].ToString() + "api/ActualizarVidreo";
                JsonContent body = JsonContent.Create(entidad); //Serializar

                HttpResponseMessage resp = client.PutAsync(url, body).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<int>().Result;
                }

                return 0;
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
                JsonContent body = JsonContent.Create(entidad);

                HttpResponseMessage resp = client.PutAsync(url, body).Result;
            }
        }

        public async Task<long> SubirImagenComprobante(HttpPostedFileBase archivo)
        {
            using (var client = new HttpClient())
            {
                string url = ConfigurationManager.AppSettings["urlApi"].ToString() + "api/ImagenComprobante";

                var content = new MultipartFormDataContent();
                if (archivo != null && archivo.ContentLength > 0)
                {
                    var streamContent = new StreamContent(archivo.InputStream);
                    streamContent.Headers.ContentType = new MediaTypeHeaderValue(archivo.ContentType);
                    content.Add(streamContent, "file", archivo.FileName);
                }
                else
                {
                    throw new ArgumentException("No se ha proporcionado un archivo válido.");
                }

                HttpResponseMessage resp = await client.PostAsync(url, content);

                if (resp.IsSuccessStatusCode)
                {
                    return await resp.Content.ReadFromJsonAsync<long>();
                }

                throw new Exception("Error al subir la imagen del comprobante.");
            }
        }
    }
}


    