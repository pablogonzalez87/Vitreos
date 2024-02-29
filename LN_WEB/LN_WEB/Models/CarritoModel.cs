﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http.Json;
using System.Net.Http;
using System.Web;
using Tienda_Vidreos.Entities;
using LN_WEB.Entities;
using System.Net.Http.Headers;

namespace LN_WEB.Models
{
    public class CarritoModel
    {
        public List<CarritoEnt> ConsultarVidreoCarrito(long q)
        {
            using (var client = new HttpClient())
            {
                string url = ConfigurationManager.AppSettings["urlApi"].ToString() + "api/ConsultarVidreoCarrito?=" + q ;
                HttpResponseMessage resp = client.GetAsync(url).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<List<CarritoEnt>>().Result;
                }

                return new List<CarritoEnt>();
            }
        }


        public int AgregarVidreoCarrito(CarritoEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = ConfigurationManager.AppSettings["urlApi"].ToString() + "api/AgregarVidreoCarrito";
                JsonContent body = JsonContent.Create(entidad); //Serializar
                HttpResponseMessage resp = client.PostAsync(url, body).Result;

                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadFromJsonAsync<int>().Result;
                }

                return 0;
            }
        }
    }
}