using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http.Json;
using System.Net.Http;
using System.Web;
using Tienda_Vidreos.Entities;
using LN_WEB.Entities;

namespace LN_WEB.Models
{
    public class FormularioModel    
    {
        public FormularioEnt FormularioUsuario(FormularioEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = ConfigurationManager.AppSettings["urlApi"].ToString() + "api/Formulario";
                JsonContent body = JsonContent.Create(entidad); // Serializar
                HttpResponseMessage resp = client.PostAsync(url, body).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<FormularioEnt>().Result;
                }
                else
                {
                    return null;
                }
            }
        }

    }
}